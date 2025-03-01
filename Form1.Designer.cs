using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace turnbinds
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private CustomTrackBar speedTrackBar;
        private CustomTrackBar plusSpeedTrackBar;
        private System.Windows.Forms.Label speedValueLabel;
        private System.Windows.Forms.Label plusSpeedValueLabel;
        private System.Windows.Forms.Label leftKeybindLabel;
        private System.Windows.Forms.Label rightKeybindLabel;
        private System.Windows.Forms.Label speedKeybindLabel;
        private System.Windows.Forms.Label yawspeedLabel;
        private System.Windows.Forms.Label leftKeybindTextLabel;
        private System.Windows.Forms.Label rightKeybindTextLabel;
        private System.Windows.Forms.Label speedKeybindTextLabel;
        private System.Windows.Forms.Label globalMultiplierLabel;
        private System.Windows.Forms.Label multiplierLabel;
        private CustomTrackBar globalMultiplierTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button saveConfigButton;
        private System.Windows.Forms.Button loadConfigButton;
        private System.Windows.Forms.Panel titleBarPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox openLastConfigCheckBox;
        private System.Windows.Forms.CheckBox minimizeToTrayCheckBox;

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
            statusLabel = new Label();
            gameSelectComboBox = new ComboBox();
            saveConfigButton = new Button();
            loadConfigButton = new Button();
            titleBarPanel = new Panel();
            closeButton = new Button();
            minimizeButton = new Button();
            titleLabel = new Label();
            openLastConfigCheckBox = new CheckBox();
            minimizeToTrayCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)plusSpeedTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)globalMultiplierTrackBar).BeginInit();
            titleBarPanel.SuspendLayout();
            SuspendLayout();
            // 
            // speedTrackBar
            // 
            speedTrackBar.BackColor = Color.Gray;
            speedTrackBar.Location = new Point(14, 126);
            speedTrackBar.Margin = new Padding(4, 3, 4, 3);
            speedTrackBar.Maximum = 300;
            speedTrackBar.Minimum = 50;
            speedTrackBar.Name = "speedTrackBar";
            speedTrackBar.Size = new Size(328, 45);
            speedTrackBar.TabIndex = 0;
            speedTrackBar.ThumbColor = Color.White;
            speedTrackBar.TickStyle = TickStyle.None;
            speedTrackBar.Value = 100;
            // 
            // plusSpeedTrackBar
            // 
            plusSpeedTrackBar.BackColor = Color.Gray;
            plusSpeedTrackBar.Location = new Point(14, 176);
            plusSpeedTrackBar.Margin = new Padding(4, 3, 4, 3);
            plusSpeedTrackBar.Maximum = 300;
            plusSpeedTrackBar.Minimum = 50;
            plusSpeedTrackBar.Name = "plusSpeedTrackBar";
            plusSpeedTrackBar.Size = new Size(328, 45);
            plusSpeedTrackBar.TabIndex = 0;
            plusSpeedTrackBar.ThumbColor = Color.White;
            plusSpeedTrackBar.TickStyle = TickStyle.None;
            plusSpeedTrackBar.Value = 100;
            // 
            // speedValueLabel
            // 
            speedValueLabel.AutoSize = true;
            speedValueLabel.Location = new Point(317, 120);
            speedValueLabel.Margin = new Padding(4, 0, 4, 0);
            speedValueLabel.Name = "speedValueLabel";
            speedValueLabel.Size = new Size(25, 15);
            speedValueLabel.TabIndex = 1;
            speedValueLabel.Text = "210";
            // 
            // plusSpeedValueLabel
            // 
            plusSpeedValueLabel.AutoSize = true;
            plusSpeedValueLabel.Location = new Point(317, 171);
            plusSpeedValueLabel.Margin = new Padding(4, 0, 4, 0);
            plusSpeedValueLabel.Name = "plusSpeedValueLabel";
            plusSpeedValueLabel.Size = new Size(25, 15);
            plusSpeedValueLabel.TabIndex = 1;
            plusSpeedValueLabel.Text = "160";
            // 
            // leftKeybindLabel
            // 
            leftKeybindLabel.AutoSize = true;
            leftKeybindLabel.Location = new Point(67, 47);
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
            rightKeybindLabel.Location = new Point(67, 62);
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
            speedKeybindLabel.Location = new Point(67, 77);
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
            yawspeedLabel.Location = new Point(14, 120);
            yawspeedLabel.Name = "yawspeedLabel";
            yawspeedLabel.Size = new Size(73, 15);
            yawspeedLabel.TabIndex = 6;
            yawspeedLabel.Text = "cl_yawspeed";
            // 
            // globalMultiplierLabel
            // 
            globalMultiplierLabel.AutoSize = true;
            globalMultiplierLabel.Location = new Point(314, 221);
            globalMultiplierLabel.Margin = new Padding(4, 0, 4, 0);
            globalMultiplierLabel.Name = "globalMultiplierLabel";
            globalMultiplierLabel.Size = new Size(28, 15);
            globalMultiplierLabel.TabIndex = 7;
            globalMultiplierLabel.Text = "0.95";
            // 
            // multiplierLabel
            // 
            multiplierLabel.AutoSize = true;
            multiplierLabel.Location = new Point(11, 221);
            multiplierLabel.Name = "multiplierLabel";
            multiplierLabel.Size = new Size(58, 15);
            multiplierLabel.TabIndex = 8;
            multiplierLabel.Text = "multiplier";
            // 
            // globalMultiplierTrackBar
            // 
            globalMultiplierTrackBar.BackColor = Color.Gray;
            globalMultiplierTrackBar.Location = new Point(14, 227);
            globalMultiplierTrackBar.Margin = new Padding(4, 3, 4, 3);
            globalMultiplierTrackBar.Maximum = 500;
            globalMultiplierTrackBar.Minimum = 1;
            globalMultiplierTrackBar.Name = "globalMultiplierTrackBar";
            globalMultiplierTrackBar.Size = new Size(328, 45);
            globalMultiplierTrackBar.TabIndex = 9;
            globalMultiplierTrackBar.ThumbColor = Color.White;
            globalMultiplierTrackBar.TickStyle = TickStyle.None;
            globalMultiplierTrackBar.Value = 100;
            // 
            // leftKeybindTextLabel
            // 
            leftKeybindTextLabel.AutoSize = true;
            leftKeybindTextLabel.Location = new Point(11, 47);
            leftKeybindTextLabel.Name = "leftKeybindTextLabel";
            leftKeybindTextLabel.Size = new Size(35, 15);
            leftKeybindTextLabel.TabIndex = 2;
            leftKeybindTextLabel.Text = "+left:";
            // 
            // rightKeybindTextLabel
            // 
            rightKeybindTextLabel.AutoSize = true;
            rightKeybindTextLabel.Location = new Point(11, 62);
            rightKeybindTextLabel.Name = "rightKeybindTextLabel";
            rightKeybindTextLabel.Size = new Size(43, 15);
            rightKeybindTextLabel.TabIndex = 4;
            rightKeybindTextLabel.Text = "+right:";
            // 
            // speedKeybindTextLabel
            // 
            speedKeybindTextLabel.Location = new Point(0, 0);
            speedKeybindTextLabel.Name = "speedKeybindTextLabel";
            speedKeybindTextLabel.Size = new Size(100, 23);
            speedKeybindTextLabel.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 77);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 10;
            label1.Text = "+speed:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 171);
            label2.Name = "label2";
            label2.Size = new Size(123, 15);
            label2.TabIndex = 12;
            label2.Text = "cl_yawspeed (+speed)";
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statusLabel.BackColor = Color.Transparent;
            statusLabel.ForeColor = Color.White;
            statusLabel.Location = new Point(354, 43);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(218, 19);
            statusLabel.TabIndex = 0;
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // gameSelectComboBox
            // 
            gameSelectComboBox.BackColor = Color.FromArgb(64, 64, 64);
            gameSelectComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            gameSelectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            gameSelectComboBox.FlatStyle = FlatStyle.Flat;
            gameSelectComboBox.ForeColor = Color.White;
            gameSelectComboBox.Location = new Point(363, 65);
            gameSelectComboBox.Name = "gameSelectComboBox";
            gameSelectComboBox.Size = new Size(200, 24);
            gameSelectComboBox.TabIndex = 13;
            // 
            // saveConfigButton
            // 
            saveConfigButton.BackColor = Color.FromArgb(64, 64, 64);
            saveConfigButton.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            saveConfigButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            saveConfigButton.FlatStyle = FlatStyle.Flat;
            saveConfigButton.ForeColor = Color.White;
            saveConfigButton.Location = new Point(374, 96);
            saveConfigButton.Name = "saveConfigButton";
            saveConfigButton.Size = new Size(85, 25);
            saveConfigButton.TabIndex = 14;
            saveConfigButton.Text = "Save Config";
            saveConfigButton.UseVisualStyleBackColor = false;
            // 
            // loadConfigButton
            // 
            loadConfigButton.BackColor = Color.FromArgb(64, 64, 64);
            loadConfigButton.FlatAppearance.BorderColor = Color.FromArgb(60, 60, 60);
            loadConfigButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            loadConfigButton.FlatStyle = FlatStyle.Flat;
            loadConfigButton.ForeColor = Color.White;
            loadConfigButton.Location = new Point(465, 96);
            loadConfigButton.Name = "loadConfigButton";
            loadConfigButton.Size = new Size(85, 25);
            loadConfigButton.TabIndex = 15;
            loadConfigButton.Text = "Load Config";
            loadConfigButton.UseVisualStyleBackColor = false;
            // 
            // titleBarPanel
            // 
            titleBarPanel.BackColor = Color.FromArgb(64, 64, 64);
            titleBarPanel.Controls.Add(closeButton);
            titleBarPanel.Controls.Add(minimizeButton);
            titleBarPanel.Controls.Add(titleLabel);
            titleBarPanel.Location = new Point(0, 0);
            titleBarPanel.Name = "titleBarPanel";
            titleBarPanel.Size = new Size(575, 30);
            titleBarPanel.TabIndex = 0;
            titleBarPanel.MouseDown += TitleBarPanel_MouseDown;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.FromArgb(64, 64, 64);
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Font = new Font("Segoe UI", 12F);
            closeButton.ForeColor = Color.White;
            closeButton.Location = new Point(530, 0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(45, 30);
            closeButton.TabIndex = 0;
            closeButton.Text = "×";
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += CloseButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.BackColor = Color.FromArgb(64, 64, 64);
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.FlatStyle = FlatStyle.Flat;
            minimizeButton.Font = new Font("Segoe UI", 12F);
            minimizeButton.ForeColor = Color.White;
            minimizeButton.Location = new Point(485, 0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.Size = new Size(45, 30);
            minimizeButton.TabIndex = 1;
            minimizeButton.Text = "−";
            minimizeButton.UseVisualStyleBackColor = false;
            minimizeButton.Click += MinimizeButton_Click;
            // 
            // titleLabel
            // 
            titleLabel.Font = new Font("Segoe UI", 12F);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(575, 30);
            titleLabel.TabIndex = 2;
            titleLabel.Text = "turnbindz";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.MouseDown += TitleBarPanel_MouseDown;
            // 
            // openLastConfigCheckBox
            // 
            openLastConfigCheckBox.AutoSize = true;
            openLastConfigCheckBox.FlatAppearance.CheckedBackColor = Color.FromArgb(45, 45, 45);
            openLastConfigCheckBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            openLastConfigCheckBox.FlatStyle = FlatStyle.Flat;
            openLastConfigCheckBox.ForeColor = Color.Black;
            openLastConfigCheckBox.Location = new Point(376, 128);
            openLastConfigCheckBox.Name = "openLastConfigCheckBox";
            openLastConfigCheckBox.Size = new Size(130, 19);
            openLastConfigCheckBox.TabIndex = 16;
            openLastConfigCheckBox.Text = "Autoload last config";
            openLastConfigCheckBox.UseVisualStyleBackColor = true;
            // 
            // minimizeToTrayCheckBox
            // 
            minimizeToTrayCheckBox.AutoSize = true;
            minimizeToTrayCheckBox.FlatAppearance.CheckedBackColor = Color.FromArgb(45, 45, 45);
            minimizeToTrayCheckBox.FlatAppearance.MouseOverBackColor = Color.FromArgb(45, 45, 45);
            minimizeToTrayCheckBox.FlatStyle = FlatStyle.Flat;
            minimizeToTrayCheckBox.ForeColor = Color.Black;
            minimizeToTrayCheckBox.Location = new Point(376, 153);
            minimizeToTrayCheckBox.Name = "minimizeToTrayCheckBox";
            minimizeToTrayCheckBox.Size = new Size(79, 19);
            minimizeToTrayCheckBox.TabIndex = 17;
            minimizeToTrayCheckBox.Text = "Exit to tray";
            minimizeToTrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(575, 285);
            Controls.Add(titleBarPanel);
            Controls.Add(statusLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(yawspeedLabel);
            Controls.Add(multiplierLabel);
            Controls.Add(globalMultiplierLabel);
            Controls.Add(globalMultiplierTrackBar);
            Controls.Add(rightKeybindLabel);
            Controls.Add(rightKeybindTextLabel);
            Controls.Add(speedKeybindLabel);
            Controls.Add(leftKeybindLabel);
            Controls.Add(leftKeybindTextLabel);
            Controls.Add(speedValueLabel);
            Controls.Add(plusSpeedValueLabel);
            Controls.Add(speedTrackBar);
            Controls.Add(plusSpeedTrackBar);
            Controls.Add(gameSelectComboBox);
            Controls.Add(saveConfigButton);
            Controls.Add(loadConfigButton);
            Controls.Add(openLastConfigCheckBox);
            Controls.Add(minimizeToTrayCheckBox);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "turnbindz";
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)plusSpeedTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)globalMultiplierTrackBar).EndInit();
            titleBarPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label label2;
    }
}
