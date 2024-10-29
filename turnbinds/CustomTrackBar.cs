using System;
using System.Drawing;
using System.Windows.Forms;

namespace turnbinds
{
    public class CustomTrackBar : TrackBar
    {
        public Color ThumbColor { get; set; } = Color.White;
        private const int ThumbWidth = 10; // Width of the thumb

        public CustomTrackBar()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.MouseDown += CustomTrackBar_MouseDown;
            this.MouseMove += CustomTrackBar_MouseMove;
        }

        private void CustomTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetValue(e);
            }
        }

        private void CustomTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SetValue(e);
            }
        }

        private void SetValue(MouseEventArgs e)
        {
            // Calculate the new value based on the click position, adjusting for thumb centering
            int newValue = Minimum + (Maximum - Minimum) * (e.X - (ThumbWidth / 2)) / (Width - ThumbWidth);
            if (newValue >= Minimum && newValue <= Maximum)
            {
                Value = newValue;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Custom painting code
            Graphics g = e.Graphics;

            // Draw the track bar
            Rectangle trackRect = new Rectangle(0, Height / 2 - 2, Width, 4);
            g.FillRectangle(Brushes.Black, trackRect); // Track color

            // Draw the thumb
            int thumbX = (int)((Value - Minimum) / (double)(Maximum - Minimum) * (Width - ThumbWidth));
            Rectangle thumbRect = new Rectangle(thumbX, Height / 2 - 10, ThumbWidth, 20); // Use ThumbWidth for the thumb's width
            g.FillRectangle(new SolidBrush(ThumbColor), thumbRect); // Thumb color
        }
    }
}
