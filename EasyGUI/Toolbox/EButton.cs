using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace EasyGUI
{
    [ProvideToolboxControl("EasyGUI", false)]
    [DefaultEvent("Click")]
    public partial class EButton : UserControl
    {

        int wh = 20;
        string cl0 = "#305ad9", cl1 = "#ed8326", solidCl = "#305ad9";
        int degrees = 45;
        string txt = "Button";
        style _style;

        public enum style
        {
            None = 1,
            Solid = 0,
            Gradient = 2,
            Image = 3
        }

        public EButton()
        {
            DoubleBuffered = true;
            ForeColor = Color.White;
        }

        public int BorderRadius
        {
            get { return wh; }
            set { wh = value; Invalidate(); }
        }

        public int Degrees
        {
            get { return degrees; }
            set { degrees = value; Invalidate(); }
        }

        public string SolidColor
        {
            get { return solidCl; }
            set { solidCl = value; Invalidate(); }
        }

        public string Color0
        {
            get { return cl0; }
            set { cl0 = value; Invalidate(); }
        }

        public string Color1
        {
            get { return cl1; }
            set { cl1 = value; Invalidate(); }
        }

        public string Value
        {
            get { return txt; }
            set { txt = value; Invalidate(); }
        }

        [Browsable(true)]
        [DefaultValue(style.Solid)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public style BStyle
        {
            get { return _style; }
            set { _style = value; Invalidate(); }
        }


        protected override void OnPaint(PaintEventArgs e)
        {

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(new Rectangle(0, 0, wh, wh), 180, 90);
            gp.AddArc(new Rectangle(Width - wh, 0, wh, wh), -90, 90);
            gp.AddArc(new Rectangle(Width - wh, Height - wh, wh, wh), 0, 90);
            gp.AddArc(new Rectangle(0, Height - wh, wh, wh), 90, 90);

            if (style.Solid.Equals(_style))
            {
                e.Graphics.FillPath(new SolidBrush(ColorTranslator.FromHtml(solidCl)), gp);
            }
            else if (style.Gradient.Equals(_style))
            {
                e.Graphics.FillPath(new LinearGradientBrush(ClientRectangle, ColorTranslator.FromHtml(cl0), ColorTranslator.FromHtml(cl1), degrees), gp);
            } else if (style.Image.Equals(_style)) {
                if (BackgroundImage != null) { 
                    Image image = Utils.ResizeImage(BackgroundImage, ClientRectangle.Width, ClientRectangle.Height);
                    Image imageToDraw = Utils.RoundCorners(image, wh, Color.White);
                    e.Graphics.DrawImageUnscaledAndClipped(imageToDraw, ClientRectangle);
                }
            }


            e.Graphics.DrawString(txt, Font, new SolidBrush(ForeColor), ClientRectangle,
                new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });

            base.OnPaint(e);
        }


    }
}
