namespace DroneControl
{
    public static class Formatting
    {
        public static string FormatDecimal(double value, int places)
        {
            return FormatDecimal(value, places, 1);
        }
        public static string FormatDecimal(double value, int places, int integerPlaces)
        {
            return value.ToString("0." + new string('0', places)).PadLeft(places + 3 + (integerPlaces - 1));
        }

        public static string FormatRatio(double value)
        {
            return FormatDecimal(value, 2);
        }

        public static string FormatQuantity(ulong value, string single, string mutiple)
        {
            if (value == 1)
                return value + " " + single;
            return value + " " + mutiple;
        }

        public static string FormatDataSize(ulong value)
        {
            const ulong _byte = 1;
            const ulong _kbyte = 1024 * _byte;
            const ulong _mbyte = 1024 * _kbyte;

            if (value < 2 * _kbyte)
                return FormatQuantity(value, "byte", "bytes");
            if (value < 2 * _mbyte)
                return FormatQuantity(value / _kbyte, "kbyte", "kbytes");
            return FormatQuantity(value / _mbyte, "mbyte", "mbytes");
        }
    }
}
