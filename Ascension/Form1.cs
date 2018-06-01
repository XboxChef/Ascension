using Ascension.Details;
using Ascension.Forms;
using Ascension.Forms.Dialog;
using Ascension.Forms.Dialog.Extras;
using Ascension.Halo_Reach.Game.Misc.Dialogs;
using Ascension.Halo_Reach.Misc;
using Ascension.Halo_Reach.Plugins;
using Ascension.Patches.File_Patches;
using Ascension.Settings;
using HaloDevelopmentExtender;
using HaloReach3d.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Ascension
{
    public partial class Form1 : Form
    {
        private MdiClient mdiClient = null;
        private bool start_values = true;
        private Dictionary<string, Poke_objects> check_values = new Dictionary<string, Poke_objects>();
        private Dictionary<string, Poke_objects> textB_values = new Dictionary<string, Poke_objects>();

        public Form1()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm { MdiParent = this }.Show();
        }

        private void add_menu_item(ToolStripMenuItem Parent, ToolStripMenuItem ChildItem)
        {
            if (ChildItem.ToString() == "DebugCam")
            {
                ChildItem.ShortcutKeys = Keys.F2;
            }
            if (ChildItem.ToString() == "Disable Intro")
            {
                ChildItem.ShortcutKeys = Keys.F3;
            }
            if (ChildItem.ToString() == "Disable RSA Checks")
            {
                ChildItem.ShortcutKeys = Keys.F4;
            }
            Parent.DropDownItems.Add(ChildItem);
            ChildItem.Click += new EventHandler(MenuItem_Click);
        }

        private void add_menu_items_array(ToolStripMenuItem Parent, ToolStripMenuItem[] ChildArray)
        {
            for (int i = 0; i < ChildArray.Length; i++)
            {
                Parent.DropDownItems.Add(ChildArray[i]);
                ChildArray[i].Click += new EventHandler(MenuItem_Click);
            }
        }

        private void add_menu_items_textB(ToolStripMenuItem Parent, ToolStripMenuItem ChildItem)
        {
            Parent.DropDownItems.Add(ChildItem);
            ToolStripMenuItem item = new ToolStripMenuItem(ChildItem.Text + " Apply");
            ToolStripTextBox box = new ToolStripTextBox
            {
                BackColor = SystemColors.ScrollBar
            };
            ToolStripMenuItem item2 = new ToolStripMenuItem(ChildItem.Text + " Reset");
            if (start_values)
            {
                try
                {
                    box.Text = getValue(textB_values[ChildItem.ToString()].poke_offset, textB_values[ChildItem.ToString()].poke_type);
                }
                catch
                {
                }
            }
            ChildItem.DropDownItems.Add(item);
            ChildItem.DropDownItems.Add(box);
            ChildItem.DropDownItems.Add(item2);
            item.Click += new EventHandler(MenuItemText_Click);
            item2.Click += new EventHandler(MenuItemText_Click);
        }

        private void advancedXeXPokerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new xex_offset_manager { MdiParent = this }.Show();
        }

        private void anniversaryMemoryOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AnniversaryMagic { MdiParent = this }.Show();
        }

        private void applyPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ApplyPatchForm { MdiParent = this }.Show();
        }

        private void boneyardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.boneyard, GameGlobalEditor.GameBuild.PreBeta);
        }

        private void boneyardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.boneyard, GameGlobalEditor.GameBuild.Beta);
        }

        private void buildLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeLogForm(false) { MdiParent = this }.Show();
        }

        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.LayoutMdi(MdiLayout.Cascade);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (base.ActiveMdiChild != null)
            {
                base.ActiveMdiChild.Close();
            }
        }

        private void closeTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            communicator.Connect();
            if (communicator.Connected)
            {
                communicator.SendTextCommand("dvdclose");
            }
        }

        private void consoleIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            communicator.Connect();
            if (communicator.Connected)
            {
                consoleIDToolStripMenuItem.Text = communicator.SendTextCommand("getconsoleid");
            }
        }

        public string ConvertToHex(string asciiString)
        {
            string str = "";
            foreach (char ch in asciiString)
            {
                int num = ch;
                str = str + "00" + $"{Convert.ToUInt32(num.ToString()):x2}";
            }
            return str;
        }

        private void createPatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CreatePatchForm { MdiParent = this }.Show();
        }

        public void DropFile(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string str in data)
                {
                    if (str.EndsWith(".map"))
                    {
                        new MapForm(str) { MdiParent = this }.Show();
                    }
                    else
                    {
                        MessageBox.Show("Dropped File is not a proper .map");
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ff10prototypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.prototype, GameGlobalEditor.GameBuild.PreBeta);
        }

        private void ff10prototypeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.prototype, GameGlobalEditor.GameBuild.Beta);
        }

        public void FileEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void finderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Finder { MdiParent = this }.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control control in base.Controls)
            {
                if (control is MdiClient)
                {
                    mdiClient = control as MdiClient;
                }
            }
            base.Show();
            Application.DoEvents();
            if (AppSettings.Settings.Auto_Load_Map && !string.IsNullOrEmpty(AppSettings.Settings.Map_Loaded))
            {
                try
                {
                    MapForm form = new MapForm(AppSettings.Settings.Map_Loaded)
                    {
                        MdiParent = this
                    };
                    form.Show();
                    form.WindowState = FormWindowState.Maximized;
                }
                catch
                {
                    MessageBox.Show("Map was not found in Map Folder");
                }
            }
        }

        private void generatePluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PluginGeneratorForm { MdiParent = this }.Show();
        }

        public string getValue(uint offset, string type)
        {
            object obj2 = null;
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                if (!communicator.Connected)
                {
                    try
                    {
                        communicator.Connect();
                    }
                    catch
                    {
                        start_values = false;
                    }
                }
                XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                nio.Open();
                try
                {
                    nio.Out.BaseStream.Position = offset;
                    if ((type == "Float") | (type == "float"))
                    {
                        obj2 = nio.In.ReadSingle();
                    }
                    if ((type == "Double") | (type == "double"))
                    {
                        obj2 = nio.In.ReadDouble();
                    }
                    if ((type == "String") | (type == "string"))
                    {
                        obj2 = nio.In.ReadString();
                    }
                }
                catch
                {
                    return "N/A";
                }
                nio.Close();
                stream.Close();
                communicator.Disconnect();
                return obj2.ToString();
            }
            return "No Console Typed In";
        }

        private void hudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new slider_poker("Player Hud") { TopMost = true }.Show();
        }

        private void memoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Memory_Reader_and_Writer { MdiParent = this }.Show();
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Poke_objects> pair in check_values)
            {
                if (((ToolStripMenuItem)sender).Text == pair.Key)
                {
                    if (pair.Value.effects == Poke_objects.Affects.OnMapReload)
                    {
                        MessageBox.Show("Will take affect on map reload");
                    }
                    if ((pair.Value.effects != Poke_objects.Affects.Irreversable) || (MessageBox.Show("Cannot Be Undone", "Continue", MessageBoxButtons.YesNo) != DialogResult.No))
                    {
                        ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
                        if (((ToolStripMenuItem)sender).Checked)
                        {
                            pokeXbox(pair.Value.poke_offset, pair.Value.poke_type, pair.Value.poke_altered);
                        }
                        else
                        {
                            pokeXbox(pair.Value.poke_offset, pair.Value.poke_type, pair.Value.poke_initial);
                        }
                    }
                    break;
                }
            }
        }

        private void MenuItemText_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, Poke_objects> pair in textB_values)
            {
                if (((ToolStripMenuItem)sender).Text == (pair.Key + " Apply"))
                {
                    if (pair.Value.effects == Poke_objects.Affects.OnMapReload)
                    {
                        MessageBox.Show("Will take affect on map reload");
                    }
                    pokeXbox(pair.Value.poke_offset, pair.Value.poke_type, ((ToolStripMenuItem)sender).Owner.Items[1].Text);
                    break;
                }
                if (((ToolStripMenuItem)sender).Text == (pair.Key + " Reset"))
                {
                    ((ToolStripMenuItem)sender).Owner.Items[1].Text = pair.Value.poke_initial;
                    pokeXbox(pair.Value.poke_offset, pair.Value.poke_type, pair.Value.poke_initial);
                    break;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Halo: Reach Map Files|*.map",
                InitialDirectory = AppSettings.Settings.Map_Folder
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                new MapForm(dialog.FileName) { MdiParent = this }.Show();
            }
        }

        private void openTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            communicator.Connect();
            if (communicator.Connected)
            {
                communicator.SendTextCommand("dvdeject");
            }
        }

        private void playerInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppSettings.Settings.IP_and_XDK_Name))
            {
                MessageBox.Show("XDK Name/IP not set");
            }
            else
            {
                new PlayerInfo { TopMost = true }.Show();
            }
        }

        private void pokePatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PokePatchForm { MdiParent = this }.Show();
        }

        private void pokeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            communicator.Connect();
            if (communicator.Connected)
            {
                string text = toolStripTextBoxGamertagPoke.Text;
                uint num = 0x81e70534;
                for (int i = 0; (i < text.Length) && (i < 15); i++)
                {
                    string command = $"setmem addr={num.ToString("X8")} data={ConvertToHex(text)}";
                    communicator.SendTextCommand(command);
                }
            }
        }

        public void pokeXbox(uint offset, string poketype, string ammount)
        {
            try
            {
                if (AppSettings.Settings.IP_and_XDK_Name == "")
                {
                    MessageBox.Show("XDK Name/IP not set");
                }
                else
                {
                    XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                    if (!communicator.Connected)
                    {
                        try
                        {
                            communicator.Connect();
                        }
                        catch
                        {
                            MessageBox.Show("error");
                        }
                    }
                    XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                    HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                    nio.Open();
                    nio.Out.BaseStream.Position = offset;
                    if (poketype == "Float")
                    {
                        nio.Out.Write(float.Parse(ammount));
                    }
                    if (poketype == "Double")
                    {
                        nio.Out.Write(double.Parse(ammount));
                    }
                    if (poketype == "String")
                    {
                        nio.Out.WriteAsciiString(ammount, 5);
                    }
                    if (poketype == "Short")
                    {
                        nio.Out.Write((short)Convert.ToUInt32(ammount, 0x10));
                    }
                    if (poketype == "Byte")
                    {
                        nio.Out.Write((byte)Convert.ToUInt32(ammount, 0x10));
                    }
                    if (poketype == "Long")
                    {
                        nio.Out.Write((long)Convert.ToUInt32(ammount, 0x10));
                    }
                    if (poketype == "Quad")
                    {
                        nio.Out.Write((long)Convert.ToUInt64(ammount, 0x10));
                    }
                    if (poketype == "Int")
                    {
                        nio.Out.Write(Convert.ToUInt32(ammount, 0x10));
                    }
                    nio.Close();
                    stream.Close();
                    communicator.Disconnect();
                }
            }
            catch
            {
            }
        }

        private void printCamDebugInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printCamDebugInfoToolStripMenuItem.Checked = !printCamDebugInfoToolStripMenuItem.Checked;
            GameGlobalEditor.PrintCamDebugInfo(printCamDebugInfoToolStripMenuItem.Checked);
        }

        private void sceneryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new slider_poker("Scenery") { TopMost = true }.Show();
        }

        private void screenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ScreenshotForm { MdiParent = this }.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().ShowDialog();
        }

        private void settlementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.settlement, GameGlobalEditor.GameBuild.PreBeta);
        }

        private void settlementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.settlement, GameGlobalEditor.GameBuild.Beta);
        }

        [DllImport("user32.dll")]
        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);
        public void streampoke(uint offset, string poketype, string ammount)
        {
            if (AppSettings.Settings.IP_and_XDK_Name == "")
            {
                MessageBox.Show("XDK Name/IP not set");
            }
            else
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                communicator.Connect();
                XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                nio.Open();
                nio.Out.BaseStream.Position = offset;
                if (poketype == "Float")
                {
                    nio.Out.Write(float.Parse(ammount));
                }
                if (poketype == "Double")
                {
                    nio.Out.Write(double.Parse(ammount));
                }
                if (poketype == "String")
                {
                    nio.Out.Write(ammount);
                }
                if (poketype == "Short")
                {
                    nio.Out.Write((short)Convert.ToUInt32(ammount, 0x10));
                }
                if (poketype == "Byte")
                {
                    nio.Out.Write((byte)Convert.ToUInt32(ammount, 0x10));
                }
                if (poketype == "Long")
                {
                    nio.Out.Write((long)Convert.ToUInt32(ammount, 0x10));
                }
                if (poketype == "Quad")
                {
                    nio.Out.Write((long)Convert.ToUInt64(ammount, 0x10));
                }
                if (poketype == "Int")
                {
                    nio.Out.Write(Convert.ToUInt32(ammount, 0x10));
                }
                nio.Close();
                stream.Close();
                communicator.Disconnect();
            }
        }

        private void stringIDDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Halo_String_ID_Database { MdiParent = this }.Show();
        }

        private void swordslayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.sword_slayer, GameGlobalEditor.GameBuild.PreBeta);
        }

        private void swordslayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GameGlobalEditor.LoadMap(GameGlobalEditor.LevelOption.sword_slayer, GameGlobalEditor.GameBuild.Beta);
        }

        private void tagComparerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TagComparison { MdiParent = this }.Show();
        }

        private void tagListMergerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Tag_merger { MdiParent = this }.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettings.Settings.IP_and_XDK_Name == "")
            {
                MessageBox.Show("XDK Name/IP not set");
            }
            else
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                communicator.Connect();
                HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(communicator.ReturnXboxMemoryStream(), HaloReach3d.IO.EndianType.BigEndian);
                nio.Open();
                nio.Out.BaseStream.Position = 0x82a0ad94L;
                nio.Out.Write((uint)0x82919cb0);
                nio.Close();
            }
        }
        private ToolStripMenuItem[] value_items_add(ToolStripMenuItem[] ExistingArray, string ItemName = "-")
        {
            ToolStripMenuItem[] array = new ToolStripMenuItem[ExistingArray.Length + 1];
            ExistingArray.CopyTo(array, 0);
            array[array.Length - 1] = new ToolStripMenuItem(ItemName);
            return array;
        }

        private void tileHorizontallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void tileVerticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.LayoutMdi(MdiLayout.TileVertical);
        }



        private void vehiclesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new slider_poker("Vehicles") { TopMost = true }.Show();
        }

        protected override void WndProc(ref Message m)
        {
            if (mdiClient != null)
            {
            }
            base.WndProc(ref m);
        }
    }
}
