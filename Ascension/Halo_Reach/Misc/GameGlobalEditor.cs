namespace Ascension.Halo_Reach.Misc
{
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using System;
    using System.Windows.Forms;

    public abstract class GameGlobalEditor
    {
        private static string[] beta_mapScenarioPaths = new string[] { @"d:\maps\20_sword_slayer", @"d:\maps\30_settlement", @"d:\maps\70_boneyard", @"d:\maps\ff10_prototype" };

        protected GameGlobalEditor()
        {
        }

        public static void LoadMap(LevelOption Level_Map, GameBuild build)
        {
            string xboxName = AppSettings.Settings.IP_and_XDK_Name;
            if (xboxName != "")
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(xboxName);
                try
                {
                    communicator.Connect();
                }
                catch (Exception ex)
                {
                    throw new Exception ("Not Connected");
                }
                EndianIO nio = new EndianIO(communicator.ReturnXboxMemoryStream(), EndianType.BigEndian);
                nio.Open();
                communicator.Freeze();
                if (build == GameBuild.PreBeta)
                {
                    nio.Out.BaseStream.Position = 0x83b8dea8L;
                }
                else
                {
                    nio.Out.BaseStream.Position = 0x8357d140L;
                }
                nio.Out.WriteAsciiString(beta_mapScenarioPaths[(int) Level_Map], 0x100);
                if (build == GameBuild.PreBeta)
                {
                    nio.Out.BaseStream.Position = 0x83b7fe78L;
                }
                else
                {
                    nio.Out.BaseStream.Position = 0x8356f110L;
                }
                if (Level_Map <= LevelOption.boneyard)
                {
                    nio.Out.Write((int) AppSettings.Settings.Force_Load_MapType);
                }
                else
                {
                    byte[] buffer = new byte[4];
                    buffer[3] = 2;
                    nio.Out.Write(buffer);
                }
                if (build == GameBuild.PreBeta)
                {
                    nio.Out.BaseStream.Position = 0x83b7fe6eL;
                }
                else
                {
                    nio.Out.BaseStream.Position = 0x8356f106L;
                }
                nio.Out.Write((byte) 1);
                communicator.Unfreeze();
                nio.Close();
                communicator.Disconnect();
            }
            else
            {
                MessageBox.Show("XDK name not set. Please set it in settings before continuing.");
            }
        }

        public static void PrintCamDebugInfo(bool printingCam)
        {
            string xboxName = AppSettings.Settings.IP_and_XDK_Name;
            if (xboxName != "")
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(xboxName);
                communicator.Connect();
                EndianIO nio = new EndianIO(communicator.ReturnXboxMemoryStream(), EndianType.BigEndian);
                nio.Open();
                communicator.Freeze();
                nio.Out.BaseStream.Position = 0x82191eecL;
                if (printingCam)
                {
                    nio.Out.Write(0x60000000);
                }
                else
                {
                    nio.Out.Write(0x419a01b0);
                }
                nio.Out.BaseStream.Position = 0x82191f04L;
                if (printingCam)
                {
                    nio.Out.Write(0x60000000);
                }
                else
                {
                    nio.Out.Write(0x419a01b0);
                }
                communicator.Unfreeze();
                nio.Close();
                communicator.Disconnect();
            }
            else
            {
                MessageBox.Show("XDK name not set. Please set it in settings before continuing.");
            }
        }

        public enum GameBuild
        {
            PreBeta,
            Beta
        }

        public enum LevelOption
        {
            sword_slayer,
            settlement,
            boneyard,
            prototype
        }
    }
}

