using System;
using System.Drawing;
using System.Windows.Forms;

namespace turnbinds
{
    public class CustomTrackBar : TrackBar
    {
        private bool isDragging = false;
        private Rectangle previousThumbRect = Rectangle.Empty;
        private Color thumbColor = Color.White;

        public Color ThumbColor
        {
            get { return thumbColor; }
            set
            {
                thumbColor = value;
                Invalidate();
            }
        }

        public CustomTrackBar()
        {
            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            this.MouseDown += CustomTrackBar_MouseDown;
            this.MouseMove += CustomTrackBar_MouseMove;
            this.MouseUp += CustomTrackBar_MouseUp;
        }

        private void CustomTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                SetValue(e);
            }
        }

        private void CustomTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                SetValue(e);
            }
        }

        private void CustomTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void SetValue(MouseEventArgs e)
        {
            int newValue = Minimum + (int)((Maximum - Minimum) * (e.X / (float)Width));
            Value = Math.Max(Minimum, Math.Min(Maximum, newValue));
            Refresh();
        }

        protected override void OnValueChanged(EventArgs e)
        {
            base.OnValueChanged(e);
            Refresh();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            using (BufferedGraphics bg = currentContext.Allocate(e.Graphics, this.ClientRectangle))
            {
                Graphics g = bg.Graphics;
                g.Clear(this.BackColor);

                // Draw the slider bar
                int barHeight = 4;
                int barY = this.Height / 2 - barHeight / 2;
                using (var brush = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(brush, new Rectangle(0, barY, this.Width, barHeight));
                }

                // Calculate and draw the thumb
                int thumbWidth = 10;
                int thumbHeight = 20;
                float valuePercentage = (Value - Minimum) / (float)(Maximum - Minimum);
                int thumbX = (int)(valuePercentage * (Width - thumbWidth));
                int thumbY = Height / 2 - thumbHeight / 2;

                Rectangle thumbRect = new Rectangle(thumbX, thumbY, thumbWidth, thumbHeight);

                if (previousThumbRect != thumbRect)
                {
                    if (!previousThumbRect.IsEmpty)
                    {
                        Rectangle invalidateRect = Rectangle.Union(previousThumbRect, thumbRect);
                        invalidateRect.Inflate(1, 1);
                        this.Invalidate(invalidateRect);
                    }
                    previousThumbRect = thumbRect;
                }

                using (var brush = new SolidBrush(thumbColor))
                {
                    g.FillRectangle(brush, thumbRect);
                }

                bg.Render(e.Graphics);
            }
        }
    }
}