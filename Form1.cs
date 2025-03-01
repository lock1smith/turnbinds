#nullable enable

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace turnbinds
{
    public partial class Form1 : Form
    {
        [DllImport("winmm.dll")]
        private static extern uint timeBeginPeriod(uint uPeriod);

        [DllImport("winmm.dll")]
        private static extern uint timeEndPeriod(uint uPeriod);

        [DllImport("kernel32.dll")]
        private static extern bool SetPriorityClass(IntPtr handle, uint priorityClass);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();

        [DllImport("kernel32.dll")]
        private static extern bool SetThreadPriority(IntPtr hThread, int nPriority);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const int THREAD_PRIORITY_TIME_CRITICAL = 15;
        private const uint HIGH_PRIORITY_CLASS = 0x00000080;
        private const int BASE_MOVE_AMOUNT = 9;
        private const int MOVES_PER_CALL = 5;
        private const double MOVE_DELAY_MS = 1;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private int speed;
        private int plusSpeed;
        private Keys leftKeybind = Keys.None;
        private Keys rightKeybind = Keys.None;
        private Keys speedKeybind = Keys.None;
        private bool plusSpeedActive = false;
        private bool isAssigningLeftKey = false;
        private bool isAssigningRightKey = false;
        private bool isAssigningSpeedKey = false;
        private bool isMovingLeft = false;
        private bool isMovingRight = false;
        private bool hasUnsavedChanges = false;
        private bool clearScheduled = false;
        private CancellationTokenSource? leftCancellation;
        private CancellationTokenSource? rightCancellation;
        private string lastLoadedConfig = "Default";
        private const string SETTINGS_FILE = "settings.txt";

        private bool openToLastConfig = false;
        private bool minimizeToTray = false;
        private NotifyIcon? trayIcon;

        private readonly string configFolder;
        private readonly List<string> defaultGames = new() { "Default", "CS2", "CSGO", "CSS", "Roblox" };
        private string currentGameConfig = "Default";
        private string selectedConfig = "Default";
        private ComboBox? gameSelectComboBox;

        private double globalMultiplier = 0.95;
        private LowLevelKeyboardListener? keyboardListener;
        private const double SpeedMultiplier = 0.1;

        public Form1()
        {
            InitializeComponent();
            Process currentProcess = Process.GetCurrentProcess();
            SetPriorityClass(currentProcess.Handle, HIGH_PRIORITY_CLASS);
            SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);
            timeBeginPeriod(1);

            if (speedTrackBar != null)
            {
                speedTrackBar.Minimum = 50;
                speedTrackBar.Maximum = 300;
                speedTrackBar.Value = 210;
            }

            if (plusSpeedTrackBar != null)
            {
                plusSpeedTrackBar.Minimum = 50;
                plusSpeedTrackBar.Maximum = 300;
                plusSpeedTrackBar.Value = 160;
            }

            if (globalMultiplierTrackBar != null)
            {
                globalMultiplierTrackBar.Minimum = 1;
                globalMultiplierTrackBar.Maximum = 500;
                globalMultiplierTrackBar.Value = 95;
                globalMultiplierTrackBar.SmallChange = 1;
            }

            speed = speedTrackBar?.Value ?? 210;
            plusSpeed = plusSpeedTrackBar?.Value ?? 160;
            globalMultiplier = (globalMultiplierTrackBar?.Value ?? 95) / 100.0;

            UpdateSpeedLabel();
            UpdatePlusSpeedLabel();

            if (leftKeybindLabel != null)
                leftKeybindLabel.Text = "<unassigned>";
            if (rightKeybindLabel != null)
                rightKeybindLabel.Text = "<unassigned>";
            if (speedKeybindLabel != null)
                speedKeybindLabel.Text = "<unassigned>";
            if (speedTrackBar != null)
                speedTrackBar.ValueChanged += SpeedTrackBar_ValueChanged;
            if (plusSpeedTrackBar != null)
                plusSpeedTrackBar.ValueChanged += PlusSpeedTrackBar_ValueChanged;
            if (globalMultiplierTrackBar != null)
                globalMultiplierTrackBar.ValueChanged += GlobalMultiplierTrackBar_ValueChanged;
            configFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "turnbindz"
            );
            currentGameConfig = "Default";
            InitializeConfig();
            SetupGameSelector();
            InitializeCheckboxes();

            if (openToLastConfig && !string.IsNullOrEmpty(lastLoadedConfig))
            {
                currentGameConfig = lastLoadedConfig;
                selectedConfig = currentGameConfig;
                if (gameSelectComboBox != null)
                    gameSelectComboBox.SelectedItem = currentGameConfig;
            }
            LoadConfig(currentGameConfig);
            
            keyboardListener = new LowLevelKeyboardListener();
            if (keyboardListener != null)
            {
                keyboardListener.OnKeyPressed += KeyboardListener_OnKeyPressed;
                keyboardListener.OnKeyReleased += KeyboardListener_OnKeyReleased;
                keyboardListener.HookKeyboard();
            }
        }

        private void InitializeCheckboxes()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Open", null, (s, e) => RestoreFromTray());
            contextMenu.Items.Add("Exit", null, (s, e) => {
                if (trayIcon != null)
                    trayIcon.Visible = false;
                Application.Exit();
            });

            trayIcon = new NotifyIcon
            {
                Icon = this.Icon,
                Text = "turnbindz",
                Visible = false,
                ContextMenuStrip = contextMenu
            };

            if (trayIcon != null)
            {
                trayIcon.Click += (s, e) => {
                    if (e is MouseEventArgs mouseEvent && mouseEvent.Button == MouseButtons.Left)
                    {
                        RestoreFromTray();
                    }
                };
            }
            
            LoadGlobalSettings();
            
            if (openLastConfigCheckBox != null)
                openLastConfigCheckBox.Checked = openToLastConfig;
            if (minimizeToTrayCheckBox != null)
                minimizeToTrayCheckBox.Checked = minimizeToTray;
            if (openLastConfigCheckBox != null)
                openLastConfigCheckBox.CheckedChanged += OpenLastConfigCheckBox_CheckedChanged;
            if (minimizeToTrayCheckBox != null)
                minimizeToTrayCheckBox.CheckedChanged += MinimizeToTrayCheckBox_CheckedChanged;
            this.FormClosing += OnFormClosing;
        }
        public void ActivateFromTray()
        {
            if (!this.Visible) 
            {
                this.Show();
                if (trayIcon != null)
                    trayIcon.Visible = false;
            }
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void RestoreFromTray()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate(); 
            if (trayIcon != null)
                trayIcon.Visible = false;
        }

        private void SaveGlobalSettings()
        {
            try
            {
                string[] lines = {
                    $"OpenToLastConfig={openToLastConfig}",
                    $"MinimizeToTray={minimizeToTray}",
                    $"LastLoadedConfig={currentGameConfig}" 
                };
                File.WriteAllLines(Path.Combine(configFolder, SETTINGS_FILE), lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGlobalSettings()
        {
            try
            {
                string settingsPath = Path.Combine(configFolder, SETTINGS_FILE);
                if (File.Exists(settingsPath))
                {
                    string[] lines = File.ReadAllLines(settingsPath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            switch (parts[0])
                            {
                                case "OpenToLastConfig":
                                    openToLastConfig = bool.Parse(parts[1]);
                                    break;
                                case "MinimizeToTray":
                                    minimizeToTray = bool.Parse(parts[1]);
                                    break;
                                case "LastLoadedConfig":
                                    lastLoadedConfig = parts[1];
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    SaveGlobalSettings();
                }
            }
            catch
            {
                openToLastConfig = false;
                minimizeToTray = false;
                lastLoadedConfig = "Default";
                SaveGlobalSettings();
            }
        }

        private void OpenLastConfigCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (openLastConfigCheckBox != null)
                openToLastConfig = openLastConfigCheckBox.Checked;
            SaveGlobalSettings();
        }

        private void MinimizeToTrayCheckBox_CheckedChanged(object? sender, EventArgs e)
        {
            if (minimizeToTrayCheckBox != null)
                minimizeToTray = minimizeToTrayCheckBox.Checked;
            SaveGlobalSettings();
        }

        private void TitleBarPanel_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void MinimizeButton_Click(object? sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void CloseButton_Click(object? sender, EventArgs e)
        {
            this.Close();
        }

        private void SetupGameSelector()
        {
            if (gameSelectComboBox == null) return;

            foreach (string game in defaultGames)
            {
                gameSelectComboBox.Items.Add(game);
            }

            gameSelectComboBox.SelectedItem = "Default";
            gameSelectComboBox.SelectedIndexChanged += GameSelectComboBox_SelectedIndexChanged;

            if (saveConfigButton != null)
                saveConfigButton.Click += SaveConfigButton_Click;
            if (loadConfigButton != null)
                loadConfigButton.Click += LoadConfigButton_Click;
        }
        private void InitializeConfig()
        {
            if (!Directory.Exists(configFolder))
            {
                Directory.CreateDirectory(configFolder);
                foreach (string game in defaultGames)
                {
                    SaveConfig(game);
                }
            }

            LoadGlobalSettings();
            currentGameConfig = openToLastConfig ? lastLoadedConfig : "Default";
            selectedConfig = currentGameConfig;
            hasUnsavedChanges = false;
        }

        private void SaveConfig(string gameName)
        {
            try
            {
                string[] lines = {
                    $"Speed={speedTrackBar?.Value ?? 210}",
                    $"PlusSpeed={plusSpeedTrackBar?.Value ?? 160}",
                    $"GlobalMultiplier={globalMultiplier}",
                    $"LeftKeybind={leftKeybind}",
                    $"RightKeybind={rightKeybind}",
                    $"SpeedKeybind={speedKeybind}"
                };
                File.WriteAllLines(GetConfigPath(gameName), lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving config for {gameName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private enum StatusType
        {
            Normal,
            Success,
            Warning
        }

        private void ShowStatus(string message, StatusType type = StatusType.Normal, bool persistent = false)
        {
            if (statusLabel == null) return;

            switch (type)
            {
                case StatusType.Success:
                    statusLabel.ForeColor = Color.LightGreen;
                    break;
                case StatusType.Warning:
                    statusLabel.ForeColor = Color.Red;
                    break;
                default:
                    statusLabel.ForeColor = Color.White;
                    break;
            }

            statusLabel.Text = message;

            if (!persistent && !string.IsNullOrEmpty(message))
            {
                if (!clearScheduled)
                {
                    clearScheduled = true;
                    Task.Delay(3000).ContinueWith(t =>
                    {
                        if (this.IsDisposed) return;
                        this.BeginInvoke(new Action(() =>
                        {
                            if (statusLabel != null)
                            {
                                statusLabel.Text = "";
                                clearScheduled = false;

                                if (gameSelectComboBox?.SelectedItem?.ToString() != currentGameConfig)
                                {
                                    ShowStatus($"Active config: {currentGameConfig}", StatusType.Warning, true);
                                }
                            }
                        }));
                    });
                }
            }
        }

        private void LoadConfig(string gameName)
        {
            try
            {
                string configPath = GetConfigPath(gameName);
                if (File.Exists(configPath))
                {
                    string[] lines = File.ReadAllLines(configPath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            try
                            {
                                switch (parts[0])
                                {
                                    case "Speed":
                                        if (speedTrackBar != null)
                                            speedTrackBar.Value = int.Parse(parts[1]);
                                        break;
                                    case "PlusSpeed":
                                        if (plusSpeedTrackBar != null)
                                            plusSpeedTrackBar.Value = int.Parse(parts[1]);
                                        break;
                                    case "GlobalMultiplier":
                                        if (globalMultiplierTrackBar != null)
                                            globalMultiplierTrackBar.Value = int.Parse(parts[1]);
                                        break;
                                    case "LeftKeybind":
                                        if (Enum.TryParse<Keys>(parts[1], out Keys leftKey))
                                        {
                                            leftKeybind = leftKey;
                                            if (leftKeybindLabel != null)
                                                leftKeybindLabel.Text = leftKey.ToString();
                                        }
                                        else
                                        {
                                            leftKeybind = Keys.None;
                                            if (leftKeybindLabel != null)
                                                leftKeybindLabel.Text = "<unassigned>";
                                        }
                                        break;
                                    case "RightKeybind":
                                        if (Enum.TryParse<Keys>(parts[1], out Keys rightKey))
                                        {
                                            rightKeybind = rightKey;
                                            if (rightKeybindLabel != null)
                                                rightKeybindLabel.Text = rightKey.ToString();
                                        }
                                        else
                                        {
                                            rightKeybind = Keys.None;
                                            if (rightKeybindLabel != null)
                                                rightKeybindLabel.Text = "<unassigned>";
                                        }
                                        break;
                                    case "SpeedKeybind":
                                        if (Enum.TryParse<Keys>(parts[1], out Keys speedKey))
                                        {
                                            speedKeybind = speedKey;
                                            if (speedKeybindLabel != null)
                                                speedKeybindLabel.Text = speedKey.ToString();
                                        }
                                        else
                                        {
                                            speedKeybind = Keys.None;
                                            if (speedKeybindLabel != null)
                                                speedKeybindLabel.Text = "<unassigned>";
                                        }
                                        break;
                                }
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }
                    hasUnsavedChanges = false;
                }
                else
                {
                    SetDefaultValues();
                    SaveConfig(gameName);
                    hasUnsavedChanges = false;
                }
            }
            catch (Exception)
            {
                ShowStatus($"Error reading config file for {gameName}. Some settings may not have loaded correctly.", StatusType.Warning);
                SetDefaultValues();
                hasUnsavedChanges = false;
            }
        }
        private void SaveConfigButton_Click(object? sender, EventArgs e)
        {
            SaveConfig(selectedConfig);
            currentGameConfig = selectedConfig;
            lastLoadedConfig = currentGameConfig;
            SaveGlobalSettings();
            LoadConfig(currentGameConfig);
            hasUnsavedChanges = false;
            ShowStatus($"Config saved and loaded for {selectedConfig}", StatusType.Success);
        }

        private void LoadConfigButton_Click(object? sender, EventArgs e)
        {
            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    $"Do you want to save changes to {currentGameConfig} before loading {selectedConfig}?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveConfig(currentGameConfig);
                        ShowStatus($"Config saved for {currentGameConfig}", StatusType.Success);
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        if (gameSelectComboBox != null)
                            gameSelectComboBox.SelectedItem = currentGameConfig;
                        return;
                }
            }

            currentGameConfig = selectedConfig;
            lastLoadedConfig = currentGameConfig;
            LoadConfig(currentGameConfig);
            SaveGlobalSettings();
            hasUnsavedChanges = false;
            ShowStatus($"Config loaded for {currentGameConfig}", StatusType.Success);
        }

        private void SetDefaultValues()
        {
            if (speedTrackBar != null)
                speedTrackBar.Value = 210;
            if (plusSpeedTrackBar != null)
                plusSpeedTrackBar.Value = 160;
            if (globalMultiplierTrackBar != null)
                globalMultiplierTrackBar.Value = 95;

            leftKeybind = Keys.None;
            rightKeybind = Keys.None;
            speedKeybind = Keys.None;

            if (leftKeybindLabel != null)
                leftKeybindLabel.Text = "<unassigned>";
            if (rightKeybindLabel != null)
                rightKeybindLabel.Text = "<unassigned>";
            if (speedKeybindLabel != null)
                speedKeybindLabel.Text = "<unassigned>";

            openToLastConfig = false;
            minimizeToTray = false;

            if (openLastConfigCheckBox != null)
                openLastConfigCheckBox.Checked = false;
            if (minimizeToTrayCheckBox != null)
                minimizeToTrayCheckBox.Checked = false;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (gameSelectComboBox != null)
            {
                gameSelectComboBox.DrawMode = DrawMode.OwnerDrawFixed;
                gameSelectComboBox.DrawItem += GameSelectComboBox_DrawItem;
            }
        }
        private void GameSelectComboBox_DrawItem(object? sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || sender is not ComboBox cb) return;

            e.DrawBackground();

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(45, 45, 45)), e.Bounds);
            }

            if (e.Font != null && cb.Items[e.Index] != null)
            {
                string itemText = cb.Items[e.Index]?.ToString() ?? string.Empty;
                e.Graphics.DrawString(itemText, e.Font,
                    new SolidBrush(Color.White), new Point(e.Bounds.X, e.Bounds.Y));
            }
        }
        private string GetConfigPath(string gameName)
        {
            return Path.Combine(configFolder, $"{gameName.ToLower()}config.txt");
        }

        private void GameSelectComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox?.SelectedItem != null)
            {
                selectedConfig = comboBox.SelectedItem.ToString() ?? currentGameConfig;
                if (selectedConfig != currentGameConfig)
                {
                    ShowStatus($"Active config: {currentGameConfig}", StatusType.Warning, true);
                }
                else
                {
                    ShowStatus("", StatusType.Normal, true);
                }
            }
        }
        private void MoveMouse(bool movingLeft)
        {
            if (movingLeft)
            {
                leftCancellation?.Cancel();
                leftCancellation = new CancellationTokenSource();
                var token = leftCancellation.Token;

                Task.Run(() =>
                {
                    try
                    {
                        SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);
                        while (!token.IsCancellationRequested && isMovingLeft)
                        {
                            int currentSpeed = plusSpeedActive ? plusSpeed : speed;
                            int moveAmount = (int)(-currentSpeed * globalMultiplier * SpeedMultiplier);
                            mouse_event(MOUSEEVENTF_MOVE, moveAmount, 0, 0, 0);
                            Thread.Sleep(1);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    
                    }
                    finally
                    {
                        leftCancellation?.Dispose();
                        leftCancellation = null;
                    }
                }, token);
            }
            else
            {
                rightCancellation?.Cancel();
                rightCancellation = new CancellationTokenSource();
                var token = rightCancellation.Token;

                Task.Run(() =>
                {
                    try
                    {
                        SetThreadPriority(GetCurrentThread(), THREAD_PRIORITY_TIME_CRITICAL);
                        while (!token.IsCancellationRequested && isMovingRight)
                        {
                            int currentSpeed = plusSpeedActive ? plusSpeed : speed;
                            int moveAmount = (int)(currentSpeed * globalMultiplier * SpeedMultiplier);
                            mouse_event(MOUSEEVENTF_MOVE, moveAmount, 0, 0, 0);
                            Thread.Sleep(1);
                        }
                    }
                    catch (OperationCanceledException)
                    {
                    
                    }
                    finally
                    {
                        rightCancellation?.Dispose();
                        rightCancellation = null;
                    }
                }, token);
            }
        }

        private void KeyboardListener_OnKeyPressed(Keys key)
        {
            if (isAssigningLeftKey)
            {
                leftKeybind = key;
                if (leftKeybindLabel != null)
                    leftKeybindLabel.Text = leftKeybind.ToString();
                isAssigningLeftKey = false;
                hasUnsavedChanges = true;
            }
            else if (isAssigningRightKey)
            {
                rightKeybind = key;
                if (rightKeybindLabel != null)
                    rightKeybindLabel.Text = rightKeybind.ToString();
                isAssigningRightKey = false;
                hasUnsavedChanges = true;
            }
            else if (isAssigningSpeedKey)
            {
                speedKeybind = key;
                if (speedKeybindLabel != null)
                    speedKeybindLabel.Text = speedKeybind.ToString();
                isAssigningSpeedKey = false;
                hasUnsavedChanges = true;
            }
            else if (key == leftKeybind && !isMovingLeft)
            {
                isMovingLeft = true;
                MoveMouse(true);
            }
            else if (key == rightKeybind && !isMovingRight)
            {
                isMovingRight = true;
                MoveMouse(false);
            }
            else if (key == speedKeybind)
            {
                plusSpeedActive = true;
            }
        }

        private void KeyboardListener_OnKeyReleased(Keys key)
        {
            if (key == speedKeybind)
            {
                plusSpeedActive = false;
            }

            if (key == leftKeybind)
            {
                isMovingLeft = false;
            }
            else if (key == rightKeybind)
            {
                isMovingRight = false;
            }
        }

        private void OnFormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && minimizeToTray)
            {
                e.Cancel = true;
                this.Hide();
                if (trayIcon != null)
                    trayIcon.Visible = true;
                return;
            }

            if (hasUnsavedChanges)
            {
                var result = MessageBox.Show(
                    $"Do you want to save changes to {currentGameConfig} before exiting?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        SaveConfig(currentGameConfig);
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }
            }

            leftCancellation?.Cancel();
            rightCancellation?.Cancel();
            timeEndPeriod(1);
            trayIcon?.Dispose();
        }
        private void UpdateSpeedLabel()
        {
            if (speedValueLabel != null)
                speedValueLabel.Text = speed.ToString();
        }

        private void UpdatePlusSpeedLabel()
        {
            if (plusSpeedValueLabel != null)
                plusSpeedValueLabel.Text = plusSpeed.ToString();
        }

        private void SpeedTrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (speedTrackBar != null)
                speed = speedTrackBar.Value;
            UpdateSpeedLabel();
            hasUnsavedChanges = true;
        }

        private void PlusSpeedTrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (plusSpeedTrackBar != null)
                plusSpeed = plusSpeedTrackBar.Value;
            UpdatePlusSpeedLabel();
            hasUnsavedChanges = true;
        }

        private void GlobalMultiplierTrackBar_ValueChanged(object? sender, EventArgs e)
        {
            if (globalMultiplierTrackBar != null)
            {
                globalMultiplier = globalMultiplierTrackBar.Value / 100.0;
                if (globalMultiplierLabel != null)
                    globalMultiplierLabel.Text = globalMultiplier.ToString("F2");
            }
            hasUnsavedChanges = true;
        }

        private void LeftKeybindLabel_Click(object? sender, EventArgs e)
        {
            if (leftKeybindLabel != null)
            {
                leftKeybindLabel.Text = "<Press any key>";
                isAssigningLeftKey = true;
            }
        }

        private void RightKeybindLabel_Click(object? sender, EventArgs e)
        {
            if (rightKeybindLabel != null)
            {
                rightKeybindLabel.Text = "<Press any key>";
                isAssigningRightKey = true;
            }
        }

        private void SpeedKeybindLabel_Click(object? sender, EventArgs e)
        {
            if (speedKeybindLabel != null)
            {
                speedKeybindLabel.Text = "<Press any key>";
                isAssigningSpeedKey = true;
            }
        }

        private void HighResolutionDelay(double seconds)
        {
            long frequency = 0;
            QueryPerformanceFrequency(ref frequency);

            long start;
            QueryPerformanceCounter(out start);

            long target = (long)(frequency * seconds);
            while (true)
            {
                long now;
                QueryPerformanceCounter(out now);
                if (now - start >= target)
                    break;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public InputType type;
        public MOUSEINPUT mi;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    enum InputType : uint
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }

    [Flags]
    enum MouseEventFlags : uint
    {
        Move = 0x0001,
        Absolute = 0x8000,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x0040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        MoveNoCoalesce = 0x2000,
        Synchronous = 0x1000
    }

    public class LowLevelKeyboardListener
    {
        public delegate void KeyEventHandler(Keys key);
        public event KeyEventHandler? OnKeyPressed;
        public event KeyEventHandler? OnKeyReleased;

        private IntPtr _hookID = IntPtr.Zero;

        public void HookKeyboard()
        {
            _hookID = SetHook();
        }

        private IntPtr SetHook()
        {
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback,
                    GetModuleHandle(curModule?.ModuleName), 0);
            }
        }

        private const int WH_KEYBOARD_LL = 13;

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    var vkCode = Marshal.ReadInt32(lParam);
                    OnKeyPressed?.Invoke((Keys)vkCode);
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    var vkCode = Marshal.ReadInt32(lParam);
                    OnKeyReleased?.Invoke((Keys)vkCode);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string? lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}
