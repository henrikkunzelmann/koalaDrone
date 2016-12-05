using DroneLibrary.Protocol;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace DroneLibrary
{
    /// <summary>
    /// Bietet Funktionen um Dronen im Netzwerk zu suchen und in einer Liste zu sammeln.
    /// </summary>
    public class DroneList : IDisposable
    {
        private Config config;

        private UdpClient client;
        private List<DroneEntry> foundDrones = new List<DroneEntry>();

        public bool IsDisposed { get; private set; }
        public int TimeoutSeconds { get; set; } = 10;

        public event EventHandler<DroneListChangedEventArgs> OnListChanged;

        public DroneList(Config config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));

            this.config = config;

            client = new UdpClient(config.ProtocolHelloPort);
            client.EnableBroadcast = true;
            client.BeginReceive(ReceivePacket, null);
        }

        ~DroneList()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
                client?.Close();

            IsDisposed = true;
        }

        public void SendHello()
        {
            Log.Debug("Sending hello broadcast");

            IPAddress[] addresses = NetworkHelper.GetLocalBroadcastAddresses();
            if (addresses == null)
                return;

            RemoveTimeoutDrones();

            using (MemoryStream stream = new MemoryStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write((byte)'F');
                writer.Write((byte)'L');
                writer.Write((byte)'Y');
                writer.Write((byte)HelloPacketType.Question);

                byte[] packet = stream.GetBuffer();

                for (int i = 0; i < addresses.Length; i++)
                    client.Send(stream.GetBuffer(), (int)stream.Length, new IPEndPoint(addresses[i], config.ProtocolHelloPort)); 
            }
        }

        private void ReceivePacket(IAsyncResult result)
        {
            try
            {
                IPEndPoint sender = null;
                byte[] packet = client.EndReceive(result, ref sender);

                Log.Debug("Got hello answer from {0}", sender.Address);

                HandlePacket(packet, sender);

                client.BeginReceive(ReceivePacket, null);
            }
            catch(ObjectDisposedException)
            {
            }
            catch(Exception e)
            {
                Log.Error(e);
            }
        }

        private void HandlePacket(byte[] packet, IPEndPoint sender)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream(packet))
                {
                    PacketBuffer buffer = new PacketBuffer(stream);

                    if (packet.Length < 3 || buffer.ReadByte() != 'F' || buffer.ReadByte() != 'L' || buffer.ReadByte() != 'Y')
                    {
                        Log.Debug("Hello: Invalid magic value!");
                        return;
                    }

                    if (buffer.ReadByte() != (byte)HelloPacketType.Answer)
                        return;

                    DroneEntry entry = new DroneEntry();
                    entry.Address = sender.Address;

                    entry.LastFound = DateTime.Now;
                    entry.Name = buffer.ReadString();
                    entry.Model = buffer.ReadString();
                    entry.SerialCode = buffer.ReadString();
                    entry.FirmwareVersion = buffer.ReadByte();

                    AddDrone(entry);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        private void RemoveTimeoutDrones()
        {
            bool removed = false;
            lock (foundDrones)
            {
                for (int i = 0; i < foundDrones.Count; i++)
                {
                    if ((DateTime.Now - foundDrones[i].LastFound).TotalSeconds >= TimeoutSeconds)
                    {
                        foundDrones.RemoveAt(i--);
                        removed = true;
                    }
                }
            }
            if (removed)
                InvokeEvent();
        }

        private void RemoveDrone(IPAddress address)
        {
            for (int i = 0; i < foundDrones.Count; i++)
                if (foundDrones[i].Address.Equals(address))
                    foundDrones.RemoveAt(i--);
        }

        private bool UpdateDrone(DroneEntry entry)
        {
            for (int i = 0; i < foundDrones.Count; i++)
            {
                DroneEntry e = foundDrones[i];
                if (e.Address.Equals(entry.Address))
                {
                    foundDrones[i] = DroneEntry.UpdateEntry(entry);

                    if (!e.Equals(entry))
                        InvokeEvent();
                    return true;
                }
            }
            return false;
        }

        private void AddDrone(DroneEntry entry)
        {
            lock(foundDrones)
            {
                if (UpdateDrone(entry))
                    return;

                // alte Drone mit gleicher IP-Adresse entfernen
                RemoveDrone(entry.Address);

                foundDrones.Add(entry);
            }

            InvokeEvent();
        }

        private void InvokeEvent()
        {
            OnListChanged?.Invoke(this, new DroneListChangedEventArgs(GetDrones()));
        }

        public DroneEntry[] GetDrones()
        {
            lock(foundDrones)
            {
                return foundDrones.ToArray();
            }
        }
    }
}
