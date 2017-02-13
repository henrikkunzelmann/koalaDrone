using DroneLibrary;
using DroneLibrary.Debug;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DroneControl
{
    public partial class Graph : UserControl
    {
        public string Titel { get; set; }
        public DataHistory History { get; private set; }

        public bool ShowBaseLine { get; set; } = false;
        public double BaseLine { get; set; } = 0;

        public double ValueMin
        {
            get { return History.FullMin; }
            set { History.FullMin = value; }
        }

        public double ValueMax
        {
            get { return History.FullMax; }
            set { History.FullMax = value; }
        }

        public bool ShowHalfScaling { get; set; } = true;

        public int HighlightX { get; set; } = -1;

        private const int gridSize = 30;
        private int offsetX = 15;
        private const int offsetY = 15;

        public Graph()
        {
            InitializeComponent();

            History = new DataHistory(Width);
        }

        protected override void OnResize(EventArgs e)
        {
            if (DesignMode || History == null)
                return;

            if (Width == 0 || Height == 0)
            {
                base.OnResize(e);
                return;
            }

            DataHistory newHistory = new DataHistory(Width);
            newHistory.FullMin = ValueMin;
            newHistory.FullMax = ValueMax;

            foreach (double value in History)
                newHistory.UpdateValue(value);

            History = newHistory;
            Refresh();
            base.OnResize(e);
        }

        private int ConvertValue(double historyValue)
        {
            double min = History.FullMin;
            double max = History.FullMax;

            if (min == max)
            {
                min--;
                max++;
            }

            double value = (historyValue - min) / (max - min);
            value = 1 - value;

            int y = (int)(value * (Height - 1));
            return Math.Min(Height, Math.Max(0, y));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.Clear(Color.White);

            Font font = new Font(FontFamily.GenericSansSerif, 14);
            Font valueFont = new Font(FontFamily.GenericMonospace, 8);
            Color gridColor = Color.FromArgb(0x50B3B3B3);
            Pen gridPen = new Pen(gridColor);

            for (int x = 0; x < Width; x += gridSize)
                e.Graphics.DrawLine(gridPen, offsetX + x, 0, offsetX + x, Height);
            for (int y = 0; y < Height; y+= gridSize)
                e.Graphics.DrawLine(gridPen, 0, offsetY + y, Width, offsetY + y);

            if (ShowBaseLine)
            {
                Color baseLineColor = Color.FromArgb(0x79, 0x55, 0x48);
                Pen baseLinePen = new Pen(baseLineColor);
                int baseLineValue = ConvertValue(BaseLine);
                e.Graphics.DrawLine(baseLinePen, 0, baseLineValue, Width, baseLineValue);

                DrawValue(e.Graphics, valueFont, BaseLine, 0.5f);
            }

            if (!DesignMode && History != null)
            {
                Pen pen = new Pen(Color.Black);
                Brush highlightBrush = new SolidBrush(Color.SkyBlue);


                if (History.ValueCount > 1)
                {
                    PointF[] points = new PointF[History.ValueCount];

                    for (int i = 0; i < History.ValueCount; i++)
                        points[i] = new PointF(i, ConvertValue(History[i]));

                    e.Graphics.DrawLines(pen, points);
                }

                if (HighlightX >= 0 && HighlightX < History.ValueCount)
                {
                    int y = ConvertValue(History[HighlightX]);

                    const int size = 8;
                    RectangleF highlight = new RectangleF(HighlightX - size / 2, y - size / 2, size, size);
                    e.Graphics.FillEllipse(highlightBrush, highlight);
                }

                DrawHistoryValue(e.Graphics, valueFont, 0, 1);
                if (ShowHalfScaling)
                {
                    DrawHistoryValue(e.Graphics, valueFont, 0.25, 0.5f);
                    DrawHistoryValue(e.Graphics, valueFont, 0.75, 0.5f);
                }
                DrawHistoryValue(e.Graphics, valueFont, 1, 0);
            }

            
            if (!string.IsNullOrWhiteSpace(Titel))
                e.Graphics.DrawString(Titel, font, new SolidBrush(Color.DarkGray), 8, 8);
            base.OnPaint(e);
        }

        private void DrawHistoryValue(Graphics graphics, Font font, double value, float alignFactor = 0)
        {
            double realValue = History.FullMin + (History.FullMax - History.FullMin) * value;
            DrawValue(graphics, font, realValue, alignFactor);
        }

        private void DrawValue(Graphics graphics, Font font, double value, float alignFactor = 0)
        {
            string str = Formatting.FormatDecimal(value, 1);
            float offsetY = graphics.MeasureString(str, font).Height * alignFactor;
            graphics.DrawString(str, font, new SolidBrush(Color.DarkGray), 4, ConvertValue(value) - offsetY);
        }

        public void UpdateValue(double value)
        {
            // wenn unsere History nicht mehr Daten fassen kann, dann Grid verschieben
            if (History.MaxLength == History.ValueCount)
            {
                offsetX--;
                if (offsetX < 0)
                    offsetX = gridSize - 1;
            }

            History.UpdateValue(value);
            Invalidate();
        }
    }
}
