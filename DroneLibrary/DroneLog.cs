using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneLibrary
{
    public class DroneLog
    {
        private List<string> lines = new List<string>();
        
        /// <summary>
        /// Gibt die Anzahl aller Zeilen zurück.
        /// </summary>
        public int Count
        {
            get
            {
                lock (lines)
                {
                    return lines.Count;
                }
            }
        }

        /// <summary>
        /// Gibt alle Zeilen zurück.
        /// </summary>
        public string[] Lines
        {
            get { return lines.ToArray(); }
        }

        public event EventHandler OnAddedLines;

        /// <summary>
        /// Fügt eine Zeile zum Log hinzu.
        /// </summary>
        /// <param name="lines"></param>
        internal void AddLine(string line)
        {
            lock (lines)
            {
                lines.Add(line);

                if (OnAddedLines != null)
                    OnAddedLines(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gibt alle Zeilen welche nach dem Index kommen.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Die Zeilen nach dem Index oder null wenn keine neuen Zeilen vorhanden sind.</returns>
        public string[] GetLinesAfter(int index)
        {
            lock (lines)
            {
                if (index >= Count)
                    return null;

                return lines.Skip(index).ToArray();
            }
        }
    }
}
