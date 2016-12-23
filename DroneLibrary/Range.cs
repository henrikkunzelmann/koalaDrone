using System;

namespace DroneLibrary
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Range : Attribute
    {
        public int Min { get; private set; }
        public int Max { get; private set; }

        public Range(int min, int max)
        {
            this.Min = Math.Min(min, max);
            this.Max = Math.Max(min, max);
        }
    }
}
