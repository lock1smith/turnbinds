using System;
using System.Threading;
using System.Windows.Forms;
using System.Linq;

namespace turnbinds
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "turnbindz_SingleInstance");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
            {
                // Find the existing instance
                var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                var existingProcess = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName)
                    .FirstOrDefault(p => p.Id != currentProcess.Id);

                if (existingProcess != null)
                {
                    // Force normal window state if minimized
                    if (NativeMethods.IsIconic(existingProcess.MainWindowHandle))
                    {
                        NativeMethods.ShowWindow(existingProcess.MainWindowHandle, 9); // SW_RESTORE
                    }

                    // Bring window to front and activate it
                    NativeMethods.SetForegroundWindow(existingProcess.MainWindowHandle);
                    NativeMethods.ShowWindow(existingProcess.MainWindowHandle, 5); // SW_SHOW
                    NativeMethods.FlashWindow(existingProcess.MainWindowHandle, true); // Flash window once
                }

                // Exit this instance
                Environment.Exit(0);
            }
        }
    }

    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
    }
}
