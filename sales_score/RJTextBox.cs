using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace sales_score
{
    public class RJTextBox : TextBox
    {
        private int borderRadius = 10;
        private Color borderColor = Color.Black; // به عنوان مثال
        private int borderSize = 1;

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                if (value > 0)
                {
                    borderRadius = value;
                    Invalidate();
                }
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }

        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                if (value >= 0)
                {
                    borderSize = value;
                    Invalidate();
                }
            }
        }

        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        public Color Textcolor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (GraphicsPath path = GetRoundRectPath(ClientRectangle, borderRadius))
            using (Pen pen = new Pen(BorderColor, BorderSize))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundRectPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(rect.Right - radius * 2, rect.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(rect.Right - radius * 2, rect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
