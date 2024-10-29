namespace turnbinds
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Control Declarations
        private CustomTrackBar speedTrackBar;
        private CustomTrackBar plusSpeedTrackBar;
        private System.Windows.Forms.Label speedValueLabel;
        private System.Windows.Forms.Label plusSpeedValueLabel;
        private System.Windows.Forms.Label leftKeybindLabel;
        private System.Windows.Forms.Label rightKeybindLabel;
        private System.Windows.Forms.Label speedKeybindLabel;
        private System.Windows.Forms.Label yawspeedLabel;
        private System.Windows.Forms.Label leftKeybindTextLabel; // New label for +left
        private System.Windows.Forms.Label rightKeybindTextLabel; // New label for +right
        private System.Windows.Forms.Label speedKeybindTextLabel; // New label for +right
        private System.Windows.Forms.Label globalMultiplierLabel;
        private System.Windows.Forms.Label multiplierLabel;
        private CustomTrackBar globalMultiplierTrackBar;
        private System.Windows.Forms.Label label1; // For +speed

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            speedTrackBar = new CustomTrackBar();
            plusSpeedTrackBar = new CustomTrackBar();
            speedValueLabel = new Label();
            plusSpeedValueLabel = new Label();
            leftKeybindLabel = new Label();
            rightKeybindLabel = new Label();
            speedKeybindLabel = new Label();
            yawspeedLabel = new Label();
            globalMultiplierLabel = new Label();
            multiplierLabel = new Label();
            globalMultiplierTrackBar = new CustomTrackBar();
            leftKeybindTextLabel = new Label();
            rightKeybindTextLabel = new Label();
            speedKeybindTextLabel = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plusSpeedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)globalMultiplierTrackBar).BeginInit();
            SuspendLayout();
            // 
            // speedTrackBar
            // 
            speedTrackBar.BackColor = Color.Gray;
            speedTrackBar.Location = new Point(14, 86);
            speedTrackBar.Margin = new Padding(4, 3, 4, 3);
            speedTrackBar.Maximum = 300;
            speedTrackBar.Minimum = 50;
            speedTrackBar.Name = "speedTrackBar";
            speedTrackBar.Size = new Size(463, 45);
            speedTrackBar.TabIndex = 0;
            speedTrackBar.ThumbColor = Color.White;
            speedTrackBar.TickStyle = TickStyle.None;
            speedTrackBar.Value = 100;
            // 
            // plusSpeedTrackBar
            // 
            plusSpeedTrackBar.BackColor = Color.Gray;
            plusSpeedTrackBar.Location = new Point(14, 136);
            plusSpeedTrackBar.Margin = new Padding(4, 3, 4, 3);
            plusSpeedTrackBar.Maximum = 300;
            plusSpeedTrackBar.Minimum = 50;
            plusSpeedTrackBar.Name = "plusSpeedTrackBar";
            plusSpeedTrackBar.Size = new Size(463, 45);
            plusSpeedTrackBar.TabIndex = 0;
            plusSpeedTrackBar.ThumbColor = Color.White;
            plusSpeedTrackBar.TickStyle = TickStyle.None;
            plusSpeedTrackBar.Value = 100;
            // 
            // speedValueLabel
            // 
            speedValueLabel.AutoSize = true;
            speedValueLabel.Location = new Point(255, 80);
            speedValueLabel.Margin = new Padding(4, 0, 4, 0);
            speedValueLabel.Name = "speedValueLabel";
            speedValueLabel.Size = new Size(25, 15);
            speedValueLabel.TabIndex = 1;
            speedValueLabel.Text = "210";
            // 
            // plusSpeedValueLabel
            // 
            plusSpeedValueLabel.AutoSize = true;
            plusSpeedValueLabel.Location = new Point(255, 131);
            plusSpeedValueLabel.Margin = new Padding(4, 0, 4, 0);
            plusSpeedValueLabel.Name = "plusSpeedValueLabel";
            plusSpeedValueLabel.Size = new Size(25, 15);
            plusSpeedValueLabel.TabIndex = 1;
            plusSpeedValueLabel.Text = "160";
            // 
            // leftKeybindLabel
            // 
            leftKeybindLabel.AutoSize = true;
            leftKeybindLabel.Location = new Point(67, 7);
            leftKeybindLabel.Margin = new Padding(4, 0, 4, 0);
            leftKeybindLabel.Name = "leftKeybindLabel";
            leftKeybindLabel.Size = new Size(83, 15);
            leftKeybindLabel.TabIndex = 3;
            leftKeybindLabel.Text = "<unassigned>";
            leftKeybindLabel.Click += LeftKeybindLabel_Click;
            // 
            // rightKeybindLabel
            // 
            rightKeybindLabel.AutoSize = true;
            rightKeybindLabel.Location = new Point(67, 22);
            rightKeybindLabel.Margin = new Padding(4, 0, 4, 0);
            rightKeybindLabel.Name = "rightKeybindLabel";
            rightKeybindLabel.Size = new Size(83, 15);
            rightKeybindLabel.TabIndex = 5;
            rightKeybindLabel.Text = "<unassigned>";
            rightKeybindLabel.Click += RightKeybindLabel_Click;
            // 
            // speedKeybindLabel
            // 
            speedKeybindLabel.AutoSize = true;
            speedKeybindLabel.Location = new Point(67, 37);
            speedKeybindLabel.Margin = new Padding(4, 0, 4, 0);
            speedKeybindLabel.Name = "speedKeybindLabel";
            speedKeybindLabel.Size = new Size(83, 15);
            speedKeybindLabel.TabIndex = 5;
            speedKeybindLabel.Text = "<unassigned>";
            speedKeybindLabel.Click += SpeedKeybindLabel_Click;
            // 
            // yawspeedLabel
            // 
            yawspeedLabel.AutoSize = true;
            yawspeedLabel.Location = new Point(14, 80);
            yawspeedLabel.Name = "yawspeedLabel";
            yawspeedLabel.Size = new Size(73, 15);
            yawspeedLabel.TabIndex = 6;
            yawspeedLabel.Text = "cl_yawspeed";
            // 
            // globalMultiplierLabel
            // 
            globalMultiplierLabel.AutoSize = true;
            globalMultiplierLabel.Location = new Point(252, 181);
            globalMultiplierLabel.Margin = new Padding(4, 0, 4, 0);
            globalMultiplierLabel.Name = "globalMultiplierLabel";
            globalMultiplierLabel.Size = new Size(28, 15);
            globalMultiplierLabel.TabIndex = 7;
            globalMultiplierLabel.Text = "0.95";
            // 
            // multiplierLabel
            // 
            multiplierLabel.AutoSize = true;
            multiplierLabel.Location = new Point(11, 181);
            multiplierLabel.Name = "multiplierLabel";
            multiplierLabel.Size = new Size(58, 15);
            multiplierLabel.TabIndex = 8;
            multiplierLabel.Text = "multiplier";
            // 
            // globalMultiplierTrackBar
            // 
            globalMultiplierTrackBar.BackColor = Color.Gray;
            globalMultiplierTrackBar.Location = new Point(14, 187);
            globalMultiplierTrackBar.Margin = new Padding(4, 3, 4, 3);
            globalMultiplierTrackBar.Maximum = 500;
            globalMultiplierTrackBar.Minimum = 1;
            globalMultiplierTrackBar.Name = "globalMultiplierTrackBar";
            globalMultiplierTrackBar.Size = new Size(463, 45);
            globalMultiplierTrackBar.TabIndex = 9;
            globalMultiplierTrackBar.ThumbColor = Color.White;
            globalMultiplierTrackBar.TickStyle = TickStyle.None;
            globalMultiplierTrackBar.Value = 100;
            // 
            // leftKeybindTextLabel
            // 
            leftKeybindTextLabel.AutoSize = true;
            leftKeybindTextLabel.Location = new Point(11, 7);
            leftKeybindTextLabel.Name = "leftKeybindTextLabel";
            leftKeybindTextLabel.Size = new Size(35, 15);
            leftKeybindTextLabel.TabIndex = 2;
            leftKeybindTextLabel.Text = "+left:";
            // 
            // rightKeybindTextLabel
            // 
            rightKeybindTextLabel.AutoSize = true;
            rightKeybindTextLabel.Location = new Point(11, 22);
            rightKeybindTextLabel.Name = "rightKeybindTextLabel";
            rightKeybindTextLabel.Size = new Size(43, 15);
            rightKeybindTextLabel.TabIndex = 4;
            rightKeybindTextLabel.Text = "+right:";
            // 
            // speedKeybindTextLabel
            // 
            speedKeybindTextLabel.AutoSize = true;
            speedKeybindTextLabel.Location = new Point(11, 37);
            speedKeybindTextLabel.Name = "speedKeybindTextLabel";
            speedKeybindTextLabel.Size = new Size(49, 15);
            speedKeybindTextLabel.TabIndex = 10;
            speedKeybindTextLabel.Text = "+speed:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 37);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 10;
            label1.Text = "+speed:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 131);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 12;
            label2.Text = "cl_yawspeed (+speed)";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(575, 245);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(yawspeedLabel);
            Controls.Add(multiplierLabel);
            Controls.Add(globalMultiplierLabel);
            Controls.Add(globalMultiplierTrackBar);
            Controls.Add(rightKeybindLabel);
            Controls.Add(rightKeybindTextLabel);
            Controls.Add(speedKeybindLabel);
            Controls.Add(speedKeybindTextLabel);
            Controls.Add(leftKeybindLabel);
            Controls.Add(leftKeybindTextLabel);
            Controls.Add(speedValueLabel);
            Controls.Add(plusSpeedValueLabel);
            Controls.Add(speedTrackBar);
            Controls.Add(plusSpeedTrackBar);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Mouse Movement Controller";
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)plusSpeedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)globalMultiplierTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label2;
    }
}
