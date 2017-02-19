using System;
using System.Collections.Generic;
using System.Linq;

namespace DroneLibrary.Diagnostics
{
    /// <summary>
    /// Saves data in a history with a maximum length.
    /// </summary>
    public class DataHistory
    {
        /// <summary>
        /// Gets the maximum length of data saved in the history.
        /// </summary>
        public int MaxLength { get; private set; }

        private List<double> values;
        private DataCache dataCache;

        private DataCache Cache
        {
            get
            {
                if (dataCache.Dirty)
                    UpdateDataCache();
                return dataCache;
            }
        }

        /// <summary>
        /// Gets the minimum value of all values saved in the history.
        /// </summary>
        public double Min { get { return Cache.Min; } }
        /// <summary>
        /// Gets the maximum value of all values saved in the history.
        /// </summary>
        public double Max { get { return Cache.Max; } }
        /// <summary>
        /// Gets the average value of all values saved in the history.
        /// </summary>
        public double Average { get { return Cache.Average; } }

        /// <summary>
        /// Gets the current (last saved) value of the history.
        /// </summary>
        public double Current
        {
            get { return values.Last(); }
        }

        /// <summary>
        /// Gets the count of values saved in this history.
        /// </summary>
        public int ValueCount
        {
            get { return values.Count; }
        }
        
        /// <summary>
        /// Gets the minimum value of all values ever saved in the history.
        /// </summary>
        public double FullMin { get; set; } = double.MaxValue;
        /// <summary>
        /// Gets the maximum value of all values ever saved in the history.
        /// </summary>
        public double FullMax { get; set; } = double.MinValue;

        /// <summary>
        /// Returns the value at the index.
        /// </summary>
        /// <param name="index">The index of the value to return.</param>
        /// <returns>The value at the specified index.</returns>
        public double this[int index]
        {
            get
            {
                return values[Math.Min(index, values.Count)];
            }
        }

        public DataHistory(int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength));

            this.MaxLength = maxLength;
            this.values = new List<double>(maxLength);
        }

        public IEnumerator<double> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        /// <summary>
        /// Clears the history and removes all values saved.
        /// </summary>
        public void Clear()
        {
            values.Clear();
            dataCache = new DataCache();
        }

        private void UpdateDataCache()
        {
            dataCache = new DataCache();
            dataCache.Dirty = false;
            if (values.Count > 0)
            {
                dataCache.Min = values[0];
                dataCache.Max = values[0];

                double average = values[0];
                for (int i = 1; i < values.Count; i++)
                {
                    double value = values[0];
                    if (value < dataCache.Min)
                        dataCache.Min = value;
                    if (value > dataCache.Max)
                        dataCache.Max = value;
                    average += value;
                }

                dataCache.Average = average / values.Count;
            }
        }

        /// <summary>
        /// Adds a new value to the history and removes the old one.
        /// </summary>
        /// <param name="value"></param>
        public void UpdateValue(double value)
        {
            if (values.Count == MaxLength)
                values.RemoveAt(0);

            values.Add(value);
            dataCache.Dirty = true;

            if (value < FullMin)
                FullMin = value;
            if (value > FullMax)
                FullMax = value;
        }

        private struct DataCache
        {
            public bool Dirty;
            public double Min;
            public double Max;
            public double Average;
        }
    }
}
