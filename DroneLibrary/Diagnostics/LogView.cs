using System;

namespace DroneLibrary.Diagnostics
{
    /// <summary>
    /// Represents a view on a log storage. Helps keeping track of new lines.
    /// </summary>
    public class LogView
    {
        private readonly static string[] emptyLines = new string[0];

        /// <summary>
        /// The LogStorage the log data is read from.
        /// </summary>
        public LogStorage Storage { get; private set; }
        /// <summary>
        /// The index of the last read line.
        /// </summary>
        public int LastLine { get; private set; }

        /// <summary>
        /// Returns true when the LogStorage has lines newer than the last line.
        /// </summary>
        public bool HasNewLines
        {
            get { return LastLine < Storage.Count; }
        }

        public LogView(LogStorage storage)
            : this(storage, 0)
        {

        }

        public LogView(LogStorage storage, int lastLine)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));
            if (lastLine < 0 || lastLine > storage.Count)
                throw new ArgumentOutOfRangeException(nameof(lastLine));

            this.Storage = storage;
            this.LastLine = lastLine;
        }

        /// <summary>
        /// Returns all new lines after the last line was read.
        /// </summary>
        /// <returns></returns>
        public string[] GetNewLines()
        {
            if (!HasNewLines)
                return emptyLines;

            string[] lines = Storage.GetLinesAfter(LastLine);
            LastLine += lines.Length;
            return lines;
        }
    }
}
