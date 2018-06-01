namespace Ascension
{
    using Ascension.Settings;
    using System;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            try
            {
                AppSettings.settingsDirectory = Application.StartupPath;
                AppSettings.LoadSettings();
            }
            catch
            {
            }
            try
            {
            }
            catch
            {
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

