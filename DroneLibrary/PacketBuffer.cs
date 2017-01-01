using System;
using System.IO;
using System.Net;

namespace DroneLibrary
{
    public class PacketBuffer : IDisposable
    {
        private MemoryStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;

        public bool IsDisposed { get; private set; }
        
        public long Position
        {
            get { return stream.Position; }
        }

        public long Size
        {
            get { return stream.Length; }
        }

        public PacketBuffer(MemoryStream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.stream = stream;
            this.reader = new BinaryReader(stream);
            this.writer = new BinaryWriter(stream);
        }

        ~PacketBuffer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (disposing)
            {
                stream?.Close();
                reader?.Dispose();
                writer?.Dispose();
            }
        }

        public void ResetPosition()
        {
            stream.Position = 0;
        }

        public void Seek(int offset)
        {
            stream.Seek(offset, SeekOrigin.Current);
        }

        public bool ReadBoolean()
        {
            return ReadByte() > 0;
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public short ReadShort()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt16());
            }
        }

        public int ReadInt()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt32());
            }
        }

        public long ReadLong()
        {
            unchecked
            {
                return IPAddress.NetworkToHostOrder(reader.ReadInt64());
            }
        }

        public ushort ReadUShort()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt16());
        }

        public uint ReadUInt()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt32());
        }

        public ulong ReadULong()
        {
            return BinaryHelper.ReverseBytes(reader.ReadUInt64());
        }

        public float ReadFloat()
        {
            return reader.ReadSingle();
        }

        public double ReadDouble()
        {
            return reader.ReadDouble();
        }

        public string ReadString()
        {
            char[] str = new char[ReadUShort()];

            for (int i = 0; i < str.Length; i++)
                str[i] = (char)ReadByte();

            return new string(str);
        }

        public void Read(byte[] buffer, int offset, int count)
        {
            stream.Read(buffer, offset, count);
        }

        public void Write(bool value)
        {
            if (value)
                Write((byte)1);
            else
                Write((byte)0);
        }

        public void Write(byte value)
        {
            writer.Write(value);
        }

        public void Write(short value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(int value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(long value)
        {
            unchecked
            {
                writer.Write(IPAddress.HostToNetworkOrder(value));
            }
        }

        public void Write(ushort value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(uint value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(ulong value)
        {
            writer.Write(BinaryHelper.ReverseBytes(value));
        }

        public void Write(float value)
        {
            writer.Write(value);
        }

        public void Write(double value)
        {
            writer.Write(value);
        }

        public void Write(string str)
        {
            if (str.Length > ushort.MaxValue)
                throw new ArgumentException("String can not be longer then ushort.MaxValue", nameof(str));

            Write((ushort)str.Length);

            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c == 0)
                    throw new ArgumentException("String can not contain \\0", nameof(str));
                if (c > byte.MaxValue)
                    Write((byte)'?');
                else
                    Write((byte)c);
            }
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            stream.Write(buffer, offset, count);
        }
    }
}
