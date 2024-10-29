using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace turnbinds
{
    public partial class Form1 : Form
    {
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


        private double globalMultiplier = 0.95; // Set default global multiplier to 0.23
        private LowLevelKeyboardListener keyboardListener;

        // Timing variables
        private const double SpeedMultiplier = 0.1; // Scale for faster movement

        private Task moveMouseTaskLeft = null;
        private Task moveMouseTaskRight = null;

        public Form1()
        {
            InitializeComponent();

            speedTrackBar.Minimum = 50; // Set minimum for Yawspeed
            speedTrackBar.Maximum = 300; // Set maximum for Yawspeed
            speedTrackBar.Value = 210; // Start at 50

            // Set up the multiplier slider
            globalMultiplierTrackBar.Minimum = 1; // Set minimum for Multiplier (1.00 = 0.01 speed)
            globalMultiplierTrackBar.Maximum = 500; // Set maximum for Multiplier (5.00 = 5.00 speed)
            globalMultiplierTrackBar.Value = 23; // Start at 23 (0.23 scaled)
            globalMultiplierTrackBar.SmallChange = 1; // Small changes for .01 increments

            speedTrackBar.ValueChanged += new EventHandler(SpeedTrackBar_ValueChanged);
            plusSpeedTrackBar.ValueChanged += new EventHandler(PlusSpeedTrackBar_ValueChanged);
            globalMultiplierTrackBar.ValueChanged += new EventHandler(GlobalMultiplierTrackBar_ValueChanged);

            speed = speedTrackBar.Value;
            UpdateSpeedLabel();
            plusSpeed = plusSpeedTrackBar.Value;
            UpdatePlusSpeedLabel();

            leftKeybindLabel.Text = "<unassigned>";
            rightKeybindLabel.Text = "<unassigned>";
            speedKeybindLabel.Text = "<unassigned>";

            keyboardListener = new LowLevelKeyboardListener();
            keyboardListener.OnKeyPressed += KeyboardListener_OnKeyPressed;
            keyboardListener.OnKeyReleased += KeyboardListener_OnKeyReleased;

            keyboardListener.HookKeyboard();
        }

        private void SpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            speed = speedTrackBar.Value;
            UpdateSpeedLabel();
        }

        private void PlusSpeedTrackBar_ValueChanged(object sender, EventArgs e)
        {
            plusSpeed = plusSpeedTrackBar.Value;
            UpdatePlusSpeedLabel();
        }

        private void GlobalMultiplierTrackBar_ValueChanged(object sender, EventArgs e)
        {
            globalMultiplier = globalMultiplierTrackBar.Value / 100.0; // Scale to 0.01 - 5.00
            globalMultiplierLabel.Text = globalMultiplier.ToString("F2"); // Display with 2 decimal places
        }

        private void KeyboardListener_OnKeyPressed(Keys key)
        {
            if (isAssigningLeftKey)
            {
                leftKeybind = key;
                leftKeybindLabel.Text = leftKeybind.ToString();
                isAssigningLeftKey = false;
            }
            else if (isAssigningRightKey)
            {
                rightKeybind = key;
                rightKeybindLabel.Text = rightKeybind.ToString();
                isAssigningRightKey = false;
            }
            else if (isAssigningSpeedKey)
            {
                speedKeybind = key;
                speedKeybindLabel.Text = speedKeybind.ToString();
                isAssigningSpeedKey = false;
            }
            else if (key == leftKeybind && !isMovingLeft)
            {
                isMovingLeft = true;
                MoveMouseAsync(true);
            }
            else if (key == rightKeybind && !isMovingRight)
            {
                isMovingRight = true;
                MoveMouseAsync(false);
            }
            else if (key == speedKeybind)
            {
                // Indicate that the +speed key is held
                plusSpeedActive = true;
            }
        }

        private void KeyboardListener_OnKeyReleased(Keys key)
        {
            if (key == speedKeybind)
            {
                plusSpeedActive = false; // Reset to standard speed when +speed key is released
            }

            if (key == leftKeybind)
            {
                isMovingLeft = false;
                moveMouseTaskLeft = null; // Allow restarting the task on next press
            }
            else if (key == rightKeybind)
            {
                isMovingRight = false;
                moveMouseTaskRight = null; // Allow restarting the task on next press
            }
        }

        private async void MoveMouseAsync(bool movingLeft)
        {
            if (movingLeft && moveMouseTaskLeft == null)
            {
                moveMouseTaskLeft = Task.Run(async () =>
                {
                    while (isMovingLeft)
                    {
                        int currentSpeed = plusSpeedActive ? plusSpeed : speed; // Use appropriate speed based on +speed key
                        int xMove = (int)(-currentSpeed * globalMultiplier * SpeedMultiplier);
                        SendMouseInput(xMove, 0);
                        HighResolutionDelay(0.005); // 1 ms delay for high-resolution timing
                    }
                    moveMouseTaskLeft = null; // Reset the task when done
                });
            }
            else if (!movingLeft && moveMouseTaskRight == null)
            {
                moveMouseTaskRight = Task.Run(async () =>
                {
                    while (isMovingRight)
                    {
                        int currentSpeed = plusSpeedActive ? plusSpeed : speed; // Use appropriate speed based on +speed key
                        int xMove = (int)(currentSpeed * globalMultiplier * SpeedMultiplier);
                        SendMouseInput(xMove, 0);
                        HighResolutionDelay(0.005); // 1 ms delay for high-resolution timing
                    }
                    moveMouseTaskRight = null; // Reset the task when done
                });
            }
        }

        private void SendMouseInput(int dx, int dy)
        {
            INPUT input = new INPUT
            {
                type = InputType.Mouse,
                mi = new MOUSEINPUT
                {
                    dx = dx,
                    dy = dy,
                    dwFlags = MouseEventFlags.Move,
                    mouseData = 0,
                    time = 0,
                    dwExtraInfo = IntPtr.Zero
                }
            };
            SendInput(1, new[] { input }, Marshal.SizeOf(typeof(INPUT)));
        }

        private void UpdateSpeedLabel()
        {
            speedValueLabel.Text = speed.ToString();
        }

        private void UpdatePlusSpeedLabel()
        {
            plusSpeedValueLabel.Text = plusSpeed.ToString();
        }

        private void LeftKeybindLabel_Click(object sender, EventArgs e)
        {
            leftKeybindLabel.Text = "<Press any key>";
            isAssigningLeftKey = true;
        }

        private void RightKeybindLabel_Click(object sender, EventArgs e)
        {
            rightKeybindLabel.Text = "<Press any key>";
            isAssigningRightKey = true;
        }
        private void SpeedKeybindLabel_Click(object sender, EventArgs e)
        {
            speedKeybindLabel.Text = "<Press any key>";
            isAssigningSpeedKey = true;
        }

        // High-resolution delay
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

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(ref long lpFrequency);

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

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);
    }

    public class LowLevelKeyboardListener
    {
        public delegate void KeyEventHandler(Keys key);
        public event KeyEventHandler OnKeyPressed;
        public event KeyEventHandler OnKeyReleased;

        private IntPtr _hookID = IntPtr.Zero;

        public void HookKeyboard()
        {
            _hookID = SetHook();
        }

        private IntPtr SetHook()
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, HookCallback, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private const int WH_KEYBOARD_LL = 13;

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                OnKeyPressed?.Invoke((Keys)vkCode);
            }
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
            {
                var vkCode = Marshal.ReadInt32(lParam);
                OnKeyReleased?.Invoke((Keys)vkCode);
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
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    }
}