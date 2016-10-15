namespace DroneLibrary.Protocol
{
    public static class PacketTypeHelper
    {
        /// <summary>
        /// Gibt zurück, ob die Drohne bei einem Paket-Typ antwortet.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool DoesAnswer(this PacketType type)
        {
            switch (type)
            {
                case PacketType.Ping:
                case PacketType.Info:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsNosiy(this PacketType type)
        {
            switch(type)
            {
                case PacketType.Ping:
                case PacketType.SetRawValues:
                case PacketType.Movement:
                case PacketType.Info:
                    return true;
                default:
                    return false;
            }
        }
    }
}
