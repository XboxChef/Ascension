namespace Ascension.Settings
{
    using Ascension.Security;
    using Ascension.Update;
    using System;
    using System.IO;
    using System.Windows.Forms;

    public static class AppSettings
    {
        private static Ascension.Settings.Settings settings = new Ascension.Settings.Settings();
        public static string settingsDirectory = "";
        public static string settingsName = @"\AppSettings.dat";

        public static void ClearSettings()
        {
            settings = new Ascension.Settings.Settings();
            SaveSettings();
        }

        public static void DeleteSettings()
        {
            if (File.Exists(settingsDirectory + settingsName))
            {
                File.Delete(settingsDirectory + settingsName);
            }
        }

        public static void LoadSettings()
        {
            if (!File.Exists(settingsDirectory + settingsName))
            {
                SaveSettings();
            }
            FileStream input = new FileStream(settingsDirectory + settingsName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(input);
            if (!(0.06 == br.ReadDouble()))
            {
                input.Close();
                MessageBox.Show("You are using a different version of settings. Your settings will be cleared to prevent problems.", "Different settings Ver.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                File.Delete(settingsDirectory + settingsName);
                settings = new Ascension.Settings.Settings();
                SaveSettings();
            }
            else
            {
                RC4Engine.BinaryEncryptionKey = Ascension.Security.Security.GetUserKeyBin();
                settings.Read(br);
                input.Close();
                if (settings.SettingsAuthor != SystemInformation.UserName)
                {
                    if (MessageBox.Show("The settings file indicates that you are not user that created this Settings File. Would you like to start with a fresh Settings File?", "Hmmm...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        File.Delete(settingsDirectory + settingsName);
                        settings = new Ascension.Settings.Settings();
                        SaveSettings();
                    }
                    else
                    {
                        SaveSettings();
                    }
                }
            }
        }

        public static void SaveSettings()
        {
            FileStream output = new FileStream(settingsDirectory + settingsName, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(output);
            bw.Write((double) 0.06);
            settings.SettingsAuthor = SystemInformation.UserName;
            RC4Engine.BinaryEncryptionKey = Ascension.Security.Security.GetUserKeyBin();
            settings.Write(bw);
            output.Close();
        }

        public static Ascension.Settings.Settings Settings =>
            settings;
    }
}

