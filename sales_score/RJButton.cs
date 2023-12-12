using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace sales_score
{
    public class RJButton : Button
    {
        private int borderSize = 0;
        private int borderRadius = 40;
        private Color borderColor = Color.PaleVioletRed;

        [Category("RJ Code Advance")]
        public int BorderSize
        {
            get
            {
                return borderSize;
            }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }


        [category("RJ Code Advence")]
        public int BorderRadius
        {
            get
            {
                return borderRadius;
            }
            set
            {
                if(value<=this.Height)
                borderRadius = value;
                else BorderRadius = this.Height;
                this.Invalidate();
            }
        }

        [category("RJ Code Advence")]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [category("RJ Code Advence")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [category("RJ Code Advence")]
        public Color Textcolor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        public RJButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = borderSize;
            this.Size = new Size(150, 40);
            this.BackColor = Color.MediumSlateBlue;
            this.ForeColor = Color.White;
            
            this.Resize += new EventHandler(Button_Resize);

        } 
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rectSurface = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle rectBorder = new Rectangle(1, 1, this.Width - 0, this.Height - 1);

            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - 1F))
                using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;

                    this.Region = new Region(pathSurface);

                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else
            {
                this.Region = new Region(rectSurface);
                if(borderSize >= 1)
                {
                    using (Pen penborder = new Pen(borderColor, borderSize))
                    {
                        pevent.Graphics.DrawRectangle(penborder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (DesignMode)// اگر در حالت طراحی بودم و هنوز کد اجرا نشده بود
            {
                if (this.Parent != null)
                    this.Parent.BackColorChanged += new EventHandler(Container_backcolorchenge);
            }
        }
        private void Container_backcolorchenge(object sender , EventArgs e)
        {
            if(this.DesignMode)
                this.Invalidate();
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }

    }

}