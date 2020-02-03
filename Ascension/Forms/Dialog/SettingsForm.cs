namespace Ascension.Forms.Dialog
{
    using Ascension.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using JRPC_Client;
    using Utility;

    public class SettingsForm : Form
    {
        private Button btnAutoMapPath;
        private Button btnBetaDir;
        private Button btnCancel;
        private Button btnImageDir;
        private Button btnMapDir;
        private Button btnMapinfoDir;
        private Button btnOK;
        private Button btnRetailDir;
        private Button btnScreenDir;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private CheckBox cbGamma;
        private CheckBox cbInvisibles;
        private CheckBox cbOldSwapper;
        private CheckBox cbOpenMap;
        private CheckBox cbResizeScreens;
        private CheckBox cbSaveScreenshots;
        private CheckBox cbUpdates;
        private IContainer components = null;
        private ListBox gameslist;
        private TextBox Gametxt;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private GroupBox groupBox5;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private GroupBox groupBox8;
        private GroupBox groupBox9;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblGamma;
        private Label lblMapToOpen;
        private Label lblScreenHeight;
        private Label lblScreenLocation;
        private Label lblScreenWidth;
        private Label lblTitle;
        private TextBox p_1;
        private TextBox p_1_d;
        private TextBox p_1_m;
        private TextBox p_2;
        private TextBox p_2_d;
        private TextBox p_2_m;
        private TextBox p_3;
        private TextBox p_3_d;
        private TextBox p_3_m;
        private Panel panel1;
        private PictureBox pbxLogo;
        private RadioButton rbForceCampaign;
        private RadioButton rbForceMultiplayer;
        private TabControl tabControl1;
        private TabControl tabControl2;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private TextBox txtAutoMapPath;
        private TextBox txtBetaDir;
        private TextBox txtImageDir;
        private TextBox txtMapDir;
        private TextBox txtMapinfoDir;
        private TextBox txtRetailDir;
        private TextBox txtScreenDir;
        private TextBox txtXdkName;
        private NumericUpDown udGamma;
        private NumericUpDown udScreenHeight;
        private Button button9;
        private NumericUpDown udScreenWidth;

        public SettingsForm()
        {
            InitializeComponent();
            Ascension.Settings.Settings settings = AppSettings.Settings;
            txtXdkName.Text = settings.IP_and_XDK_Name;
            cbUpdates.Checked = settings.ShowUpdatesOnStartup;
            cbOpenMap.Checked = settings.Auto_Load_Map;
            txtAutoMapPath.Text = settings.Map_Loaded;
            switch (settings.Force_Load_MapType)
            {
                case Ascension.Settings.Settings.ForceLoadMapType.LoadAsCampaign:
                    rbForceCampaign.Checked = true;
                    break;

                case Ascension.Settings.Settings.ForceLoadMapType.LoadAsMultiplayer:
                    rbForceMultiplayer.Checked = true;
                    break;
            }
            txtMapDir.Text = settings.Map_Folder;
            txtMapinfoDir.Text = settings.MapInfo_Folder;
            txtImageDir.Text = settings.Images_Folder;
            txtRetailDir.Text = settings.PluginPath;
            txtBetaDir.Text = settings.BetaPluginPath;
            cbInvisibles.Checked = settings.ShowInvisibles;
            cbOldSwapper.Checked = settings.Old_Ident_Swapper;
            cbSaveScreenshots.Checked = settings.SaveScreenshots;
            txtScreenDir.Text = settings.ScreenShotLocation;
            cbResizeScreens.Checked = settings.ResizeScreenshots;
            udScreenWidth.Value = settings.ScreenshotWidth;
            udScreenHeight.Value = settings.ScreenshotHeight;
            cbGamma.Checked = settings.AdjustGamma;
            udGamma.Value = (decimal) settings.GammaValue;
            p_1.Text = settings.Patch_1;
            p_1_m.Text = settings.Patch_1_Map;
            p_1_d.Text = settings.Patch_1_Description;
            p_2.Text = settings.Patch_2;
            p_2_m.Text = settings.Patch_2_Map;
            p_2_d.Text = settings.Patch_2_Description;
            p_3.Text = settings.Patch_3;
            p_3_m.Text = settings.Patch_3_Map;
            p_3_d.Text = settings.Patch_3_Description;
            try
            {
                string[] strArray = AppSettings.Settings.Additional_Games.Split(new char[] { '^' });
                foreach (string str in strArray)
                {
                    gameslist.Items.Add(str);
                }
            }
            catch
            {
            }
        }

        private void BrowseDirectory(string description, bool newFolderButton, TextBox pathBox)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                Description = description
            };
            if (!string.IsNullOrWhiteSpace(pathBox.Text))
            {
                dialog.SelectedPath = pathBox.Text;
            }
            else
            {
                dialog.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;
            }
            dialog.ShowNewFolderButton = newFolderButton;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = dialog.SelectedPath + @"\";
            }
        }

        private void BrowseMap(TextBox pathBox)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "Open Map File",
                Filter = "Halo: Reach Map Files|*.map",
                FileName = pathBox.Text,
                InitialDirectory = txtMapDir.Text
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = dialog.FileName;
            }
        }

        private void BrowsePatch(TextBox pathBox)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "Open ASC Patch",
                Filter = "Halo: Ascension Patch Files|*.ascpatch",
                FileName = pathBox.Text,
                InitialDirectory = txtMapDir.Text
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathBox.Text = dialog.FileName;
            }
        }

        private void btnAutoMapPath_Click(object sender, EventArgs e)
        {
            BrowseMap(txtAutoMapPath);
        }

        private void btnBetaDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory where beta plugins are stored: ", false, txtBetaDir);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnImageDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory where map images are stored: ", false, txtImageDir);
        }

        private void btnMapDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory where map files are stored:", false, txtMapDir);
        }

        private void btnMapinfoDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory where mapinfo files are stored: ", false, txtMapinfoDir);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Ascension.Settings.Settings settings = AppSettings.Settings;
            settings.IP_and_XDK_Name = txtXdkName.Text;
            settings.ShowUpdatesOnStartup = cbUpdates.Checked;
            settings.Auto_Load_Map = cbOpenMap.Checked;
            settings.Map_Loaded = txtAutoMapPath.Text;
            if (rbForceCampaign.Checked)
            {
                settings.Force_Load_MapType = Ascension.Settings.Settings.ForceLoadMapType.LoadAsCampaign;
            }
            else
            {
                settings.Force_Load_MapType = Ascension.Settings.Settings.ForceLoadMapType.LoadAsMultiplayer;
            }
            settings.Map_Folder = txtMapDir.Text;
            settings.MapInfo_Folder = txtMapinfoDir.Text;
            settings.Images_Folder = txtImageDir.Text;
            settings.PluginPath = txtRetailDir.Text;
            settings.BetaPluginPath = txtBetaDir.Text;
            settings.ShowInvisibles = cbInvisibles.Checked;
            settings.Old_Ident_Swapper = cbOldSwapper.Checked;
            settings.SaveScreenshots = cbSaveScreenshots.Checked;
            settings.ScreenShotLocation = txtScreenDir.Text;
            settings.ResizeScreenshots = cbResizeScreens.Checked;
            settings.ScreenshotWidth = (int) udScreenWidth.Value;
            settings.ScreenshotHeight = (int) udScreenHeight.Value;
            settings.AdjustGamma = cbGamma.Checked;
            settings.GammaValue = (double) udGamma.Value;
            settings.Patch_1 = p_1.Text;
            settings.Patch_1_Map = p_1_m.Text;
            settings.Patch_1_Description = p_1_d.Text;
            settings.Patch_2 = p_2.Text;
            settings.Patch_2_Map = p_2_m.Text;
            settings.Patch_2_Description = p_2_d.Text;
            settings.Patch_3 = p_3.Text;
            settings.Patch_3_Map = p_3_m.Text;
            settings.Patch_3_Description = p_3_d.Text;
            string str = "";
            for (int i = 0; i < gameslist.Items.Count; i++)
            {
                if (i == 0)
                {
                    str = gameslist.Items[i].ToString();
                }
                else
                {
                    str = str + "^" + gameslist.Items[i].ToString();
                }
            }
            settings.Additional_Games = str;
            AppSettings.SaveSettings();
            base.Close();
        }

        private void btnRetailDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory where retail plugins are stored: ", false, txtRetailDir);
        }

        private void btnScreenDir_Click(object sender, EventArgs e)
        {
            BrowseDirectory("Select the directory to store screenshots in: ", true, txtScreenDir);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameslist.Items.Add(Gametxt.Text);
            Gametxt.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameslist.Items.Remove(gameslist.SelectedItem);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BrowsePatch(p_1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BrowseMap(p_1_m);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BrowseMap(p_2_m);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BrowsePatch(p_2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BrowseMap(p_3_m);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BrowsePatch(p_3);
        }

        private void cbGamma_CheckedChanged(object sender, EventArgs e)
        {
            lblGamma.Enabled = cbGamma.Checked;
            udGamma.Enabled = cbGamma.Checked;
        }

        private void cbOpenMap_CheckedChanged(object sender, EventArgs e)
        {
            lblMapToOpen.Enabled = cbOpenMap.Checked;
            txtAutoMapPath.Enabled = cbOpenMap.Checked;
            btnAutoMapPath.Enabled = cbOpenMap.Checked;
        }

        private void cbResizeScreens_CheckedChanged(object sender, EventArgs e)
        {
            lblScreenWidth.Enabled = cbResizeScreens.Checked;
            udScreenWidth.Enabled = cbResizeScreens.Checked;
            lblScreenHeight.Enabled = cbResizeScreens.Checked;
            udScreenHeight.Enabled = cbResizeScreens.Checked;
        }

        private void cbSaveScreenshots_CheckedChanged(object sender, EventArgs e)
        {
            lblScreenLocation.Enabled = cbSaveScreenshots.Checked;
            txtScreenDir.Enabled = cbSaveScreenshots.Checked;
            btnScreenDir.Enabled = cbSaveScreenshots.Checked;
        }

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbxLogo = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbForceCampaign = new System.Windows.Forms.RadioButton();
            this.rbForceMultiplayer = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbOpenMap = new System.Windows.Forms.CheckBox();
            this.btnAutoMapPath = new System.Windows.Forms.Button();
            this.lblMapToOpen = new System.Windows.Forms.Label();
            this.txtAutoMapPath = new System.Windows.Forms.TextBox();
            this.cbUpdates = new System.Windows.Forms.CheckBox();
            this.txtXdkName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRetailDir = new System.Windows.Forms.TextBox();
            this.btnBetaDir = new System.Windows.Forms.Button();
            this.btnRetailDir = new System.Windows.Forms.Button();
            this.txtBetaDir = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnImageDir = new System.Windows.Forms.Button();
            this.btnMapinfoDir = new System.Windows.Forms.Button();
            this.btnMapDir = new System.Windows.Forms.Button();
            this.txtImageDir = new System.Windows.Forms.TextBox();
            this.txtMapinfoDir = new System.Windows.Forms.TextBox();
            this.txtMapDir = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cbOldSwapper = new System.Windows.Forms.CheckBox();
            this.cbInvisibles = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblGamma = new System.Windows.Forms.Label();
            this.udGamma = new System.Windows.Forms.NumericUpDown();
            this.cbGamma = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lblScreenHeight = new System.Windows.Forms.Label();
            this.udScreenHeight = new System.Windows.Forms.NumericUpDown();
            this.lblScreenWidth = new System.Windows.Forms.Label();
            this.udScreenWidth = new System.Windows.Forms.NumericUpDown();
            this.cbResizeScreens = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnScreenDir = new System.Windows.Forms.Button();
            this.lblScreenLocation = new System.Windows.Forms.Label();
            this.txtScreenDir = new System.Windows.Forms.TextBox();
            this.cbSaveScreenshots = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.gameslist = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Gametxt = new System.Windows.Forms.TextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.p_1_d = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.p_1_m = new System.Windows.Forms.TextBox();
            this.p_1 = new System.Windows.Forms.TextBox();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.label10 = new System.Windows.Forms.Label();
            this.p_2_d = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.p_2_m = new System.Windows.Forms.TextBox();
            this.p_2 = new System.Windows.Forms.TextBox();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.p_3_d = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.p_3_m = new System.Windows.Forms.TextBox();
            this.p_3 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGamma)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udScreenHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScreenWidth)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage8.SuspendLayout();
            this.tabPage9.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(79, 11);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(681, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ascension Settings";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.pbxLogo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(765, 63);
            this.panel1.TabIndex = 21;
            // 
            // pbxLogo
            // 
            this.pbxLogo.Location = new System.Drawing.Point(4, 4);
            this.pbxLogo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbxLogo.Name = "pbxLogo";
            this.pbxLogo.Size = new System.Drawing.Size(67, 55);
            this.pbxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxLogo.TabIndex = 4;
            this.pbxLogo.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(16, 70);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(733, 273);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.txtXdkName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(725, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rbForceCampaign);
            this.groupBox4.Controls.Add(this.rbForceMultiplayer);
            this.groupBox4.Location = new System.Drawing.Point(8, 180);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(707, 54);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Force Map Loading (does not apply to ff10_prototype)";
            // 
            // rbForceCampaign
            // 
            this.rbForceCampaign.AutoSize = true;
            this.rbForceCampaign.Location = new System.Drawing.Point(181, 23);
            this.rbForceCampaign.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbForceCampaign.Name = "rbForceCampaign";
            this.rbForceCampaign.Size = new System.Drawing.Size(147, 21);
            this.rbForceCampaign.TabIndex = 1;
            this.rbForceCampaign.TabStop = true;
            this.rbForceCampaign.Text = "Load as Campaign";
            this.rbForceCampaign.UseVisualStyleBackColor = true;
            // 
            // rbForceMultiplayer
            // 
            this.rbForceMultiplayer.AutoSize = true;
            this.rbForceMultiplayer.Location = new System.Drawing.Point(15, 23);
            this.rbForceMultiplayer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbForceMultiplayer.Name = "rbForceMultiplayer";
            this.rbForceMultiplayer.Size = new System.Drawing.Size(152, 21);
            this.rbForceMultiplayer.TabIndex = 0;
            this.rbForceMultiplayer.TabStop = true;
            this.rbForceMultiplayer.Text = "Load as Multiplayer";
            this.rbForceMultiplayer.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbOpenMap);
            this.groupBox3.Controls.Add(this.btnAutoMapPath);
            this.groupBox3.Controls.Add(this.lblMapToOpen);
            this.groupBox3.Controls.Add(this.txtAutoMapPath);
            this.groupBox3.Controls.Add(this.cbUpdates);
            this.groupBox3.Location = new System.Drawing.Point(8, 43);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(707, 129);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Startup";
            // 
            // cbOpenMap
            // 
            this.cbOpenMap.AutoSize = true;
            this.cbOpenMap.Location = new System.Drawing.Point(15, 52);
            this.cbOpenMap.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbOpenMap.Name = "cbOpenMap";
            this.cbOpenMap.Size = new System.Drawing.Size(214, 21);
            this.cbOpenMap.TabIndex = 4;
            this.cbOpenMap.Text = "Automatically open a map file";
            this.cbOpenMap.UseVisualStyleBackColor = true;
            this.cbOpenMap.CheckedChanged += new System.EventHandler(this.cbOpenMap_CheckedChanged);
            // 
            // btnAutoMapPath
            // 
            this.btnAutoMapPath.Enabled = false;
            this.btnAutoMapPath.Location = new System.Drawing.Point(599, 81);
            this.btnAutoMapPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAutoMapPath.Name = "btnAutoMapPath";
            this.btnAutoMapPath.Size = new System.Drawing.Size(100, 28);
            this.btnAutoMapPath.TabIndex = 3;
            this.btnAutoMapPath.Text = "Browse...";
            this.btnAutoMapPath.UseVisualStyleBackColor = true;
            this.btnAutoMapPath.Click += new System.EventHandler(this.btnAutoMapPath_Click);
            // 
            // lblMapToOpen
            // 
            this.lblMapToOpen.AutoSize = true;
            this.lblMapToOpen.Enabled = false;
            this.lblMapToOpen.Location = new System.Drawing.Point(11, 87);
            this.lblMapToOpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMapToOpen.Name = "lblMapToOpen";
            this.lblMapToOpen.Size = new System.Drawing.Size(91, 17);
            this.lblMapToOpen.TabIndex = 2;
            this.lblMapToOpen.Text = "Map to open:";
            // 
            // txtAutoMapPath
            // 
            this.txtAutoMapPath.Enabled = false;
            this.txtAutoMapPath.Location = new System.Drawing.Point(112, 84);
            this.txtAutoMapPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAutoMapPath.Name = "txtAutoMapPath";
            this.txtAutoMapPath.Size = new System.Drawing.Size(477, 22);
            this.txtAutoMapPath.TabIndex = 1;
            // 
            // cbUpdates
            // 
            this.cbUpdates.AutoSize = true;
            this.cbUpdates.Enabled = false;
            this.cbUpdates.Location = new System.Drawing.Point(15, 23);
            this.cbUpdates.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbUpdates.Name = "cbUpdates";
            this.cbUpdates.Size = new System.Drawing.Size(145, 21);
            this.cbUpdates.TabIndex = 0;
            this.cbUpdates.Text = "Check for updates";
            this.cbUpdates.UseVisualStyleBackColor = true;
            // 
            // txtXdkName
            // 
            this.txtXdkName.Location = new System.Drawing.Point(123, 11);
            this.txtXdkName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtXdkName.Name = "txtXdkName";
            this.txtXdkName.Size = new System.Drawing.Size(235, 22);
            this.txtXdkName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "XDK Name/IP:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(725, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Paths";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRetailDir);
            this.groupBox2.Controls.Add(this.btnBetaDir);
            this.groupBox2.Controls.Add(this.btnRetailDir);
            this.groupBox2.Controls.Add(this.txtBetaDir);
            this.groupBox2.Location = new System.Drawing.Point(8, 138);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(707, 96);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plugin Paths";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 62);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Beta Plugins:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 30);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Retail Plugins:";
            // 
            // txtRetailDir
            // 
            this.txtRetailDir.Location = new System.Drawing.Point(120, 27);
            this.txtRetailDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRetailDir.Name = "txtRetailDir";
            this.txtRetailDir.Size = new System.Drawing.Size(469, 22);
            this.txtRetailDir.TabIndex = 14;
            // 
            // btnBetaDir
            // 
            this.btnBetaDir.Location = new System.Drawing.Point(599, 55);
            this.btnBetaDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBetaDir.Name = "btnBetaDir";
            this.btnBetaDir.Size = new System.Drawing.Size(100, 28);
            this.btnBetaDir.TabIndex = 13;
            this.btnBetaDir.Text = "Browse...";
            this.btnBetaDir.UseVisualStyleBackColor = true;
            this.btnBetaDir.Click += new System.EventHandler(this.btnBetaDir_Click);
            // 
            // btnRetailDir
            // 
            this.btnRetailDir.Location = new System.Drawing.Point(599, 25);
            this.btnRetailDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRetailDir.Name = "btnRetailDir";
            this.btnRetailDir.Size = new System.Drawing.Size(100, 28);
            this.btnRetailDir.TabIndex = 12;
            this.btnRetailDir.Text = "Browse...";
            this.btnRetailDir.UseVisualStyleBackColor = true;
            this.btnRetailDir.Click += new System.EventHandler(this.btnRetailDir_Click);
            // 
            // txtBetaDir
            // 
            this.txtBetaDir.Location = new System.Drawing.Point(120, 58);
            this.txtBetaDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBetaDir.Name = "txtBetaDir";
            this.txtBetaDir.Size = new System.Drawing.Size(469, 22);
            this.txtBetaDir.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnImageDir);
            this.groupBox1.Controls.Add(this.btnMapinfoDir);
            this.groupBox1.Controls.Add(this.btnMapDir);
            this.groupBox1.Controls.Add(this.txtImageDir);
            this.groupBox1.Controls.Add(this.txtMapinfoDir);
            this.groupBox1.Controls.Add(this.txtMapDir);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(707, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General Paths";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 91);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Map Images:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "MapInfo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Maps:";
            // 
            // btnImageDir
            // 
            this.btnImageDir.Location = new System.Drawing.Point(599, 85);
            this.btnImageDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImageDir.Name = "btnImageDir";
            this.btnImageDir.Size = new System.Drawing.Size(100, 28);
            this.btnImageDir.TabIndex = 7;
            this.btnImageDir.Text = "Browse...";
            this.btnImageDir.UseVisualStyleBackColor = true;
            this.btnImageDir.Click += new System.EventHandler(this.btnImageDir_Click);
            // 
            // btnMapinfoDir
            // 
            this.btnMapinfoDir.Location = new System.Drawing.Point(599, 53);
            this.btnMapinfoDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMapinfoDir.Name = "btnMapinfoDir";
            this.btnMapinfoDir.Size = new System.Drawing.Size(100, 28);
            this.btnMapinfoDir.TabIndex = 6;
            this.btnMapinfoDir.Text = "Browse...";
            this.btnMapinfoDir.UseVisualStyleBackColor = true;
            this.btnMapinfoDir.Click += new System.EventHandler(this.btnMapinfoDir_Click);
            // 
            // btnMapDir
            // 
            this.btnMapDir.Location = new System.Drawing.Point(599, 21);
            this.btnMapDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMapDir.Name = "btnMapDir";
            this.btnMapDir.Size = new System.Drawing.Size(100, 28);
            this.btnMapDir.TabIndex = 5;
            this.btnMapDir.Text = "Browse...";
            this.btnMapDir.UseVisualStyleBackColor = true;
            this.btnMapDir.Click += new System.EventHandler(this.btnMapDir_Click);
            // 
            // txtImageDir
            // 
            this.txtImageDir.Location = new System.Drawing.Point(120, 87);
            this.txtImageDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtImageDir.Name = "txtImageDir";
            this.txtImageDir.Size = new System.Drawing.Size(469, 22);
            this.txtImageDir.TabIndex = 2;
            // 
            // txtMapinfoDir
            // 
            this.txtMapinfoDir.Location = new System.Drawing.Point(120, 55);
            this.txtMapinfoDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMapinfoDir.Name = "txtMapinfoDir";
            this.txtMapinfoDir.Size = new System.Drawing.Size(469, 22);
            this.txtMapinfoDir.TabIndex = 1;
            // 
            // txtMapDir
            // 
            this.txtMapDir.Location = new System.Drawing.Point(120, 23);
            this.txtMapDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMapDir.Name = "txtMapDir";
            this.txtMapDir.Size = new System.Drawing.Size(469, 22);
            this.txtMapDir.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Size = new System.Drawing.Size(725, 244);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Editor";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cbOldSwapper);
            this.groupBox5.Controls.Add(this.cbInvisibles);
            this.groupBox5.Location = new System.Drawing.Point(8, 7);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox5.Size = new System.Drawing.Size(707, 82);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Tag Editor";
            // 
            // cbOldSwapper
            // 
            this.cbOldSwapper.AutoSize = true;
            this.cbOldSwapper.Location = new System.Drawing.Point(15, 52);
            this.cbOldSwapper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbOldSwapper.Name = "cbOldSwapper";
            this.cbOldSwapper.Size = new System.Drawing.Size(194, 21);
            this.cbOldSwapper.TabIndex = 2;
            this.cbOldSwapper.Text = "Use the old ident swapper";
            this.cbOldSwapper.UseVisualStyleBackColor = true;
            // 
            // cbInvisibles
            // 
            this.cbInvisibles.AutoSize = true;
            this.cbInvisibles.Location = new System.Drawing.Point(15, 23);
            this.cbInvisibles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbInvisibles.Name = "cbInvisibles";
            this.cbInvisibles.Size = new System.Drawing.Size(163, 21);
            this.cbInvisibles.TabIndex = 0;
            this.cbInvisibles.Text = "Show invisible values";
            this.cbInvisibles.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox8);
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Size = new System.Drawing.Size(725, 244);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Screenshots";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblGamma);
            this.groupBox8.Controls.Add(this.udGamma);
            this.groupBox8.Controls.Add(this.cbGamma);
            this.groupBox8.Location = new System.Drawing.Point(364, 105);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox8.Size = new System.Drawing.Size(351, 123);
            this.groupBox8.TabIndex = 2;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Gamma Adjustment";
            // 
            // lblGamma
            // 
            this.lblGamma.AutoSize = true;
            this.lblGamma.Enabled = false;
            this.lblGamma.Location = new System.Drawing.Point(11, 54);
            this.lblGamma.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGamma.Name = "lblGamma";
            this.lblGamma.Size = new System.Drawing.Size(60, 17);
            this.lblGamma.TabIndex = 2;
            this.lblGamma.Text = "Amount:";
            // 
            // udGamma
            // 
            this.udGamma.DecimalPlaces = 2;
            this.udGamma.Enabled = false;
            this.udGamma.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udGamma.Location = new System.Drawing.Point(80, 52);
            this.udGamma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.udGamma.Name = "udGamma";
            this.udGamma.Size = new System.Drawing.Size(75, 22);
            this.udGamma.TabIndex = 1;
            // 
            // cbGamma
            // 
            this.cbGamma.AutoSize = true;
            this.cbGamma.Location = new System.Drawing.Point(15, 23);
            this.cbGamma.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbGamma.Name = "cbGamma";
            this.cbGamma.Size = new System.Drawing.Size(119, 21);
            this.cbGamma.TabIndex = 0;
            this.cbGamma.Text = "Adjust gamma";
            this.cbGamma.UseVisualStyleBackColor = true;
            this.cbGamma.CheckedChanged += new System.EventHandler(this.cbGamma_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lblScreenHeight);
            this.groupBox7.Controls.Add(this.udScreenHeight);
            this.groupBox7.Controls.Add(this.lblScreenWidth);
            this.groupBox7.Controls.Add(this.udScreenWidth);
            this.groupBox7.Controls.Add(this.cbResizeScreens);
            this.groupBox7.Location = new System.Drawing.Point(8, 105);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox7.Size = new System.Drawing.Size(348, 123);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Resizing";
            // 
            // lblScreenHeight
            // 
            this.lblScreenHeight.AutoSize = true;
            this.lblScreenHeight.Enabled = false;
            this.lblScreenHeight.Location = new System.Drawing.Point(11, 86);
            this.lblScreenHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScreenHeight.Name = "lblScreenHeight";
            this.lblScreenHeight.Size = new System.Drawing.Size(53, 17);
            this.lblScreenHeight.TabIndex = 4;
            this.lblScreenHeight.Text = "Height:";
            // 
            // udScreenHeight
            // 
            this.udScreenHeight.Enabled = false;
            this.udScreenHeight.Location = new System.Drawing.Point(73, 84);
            this.udScreenHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.udScreenHeight.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.udScreenHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udScreenHeight.Name = "udScreenHeight";
            this.udScreenHeight.Size = new System.Drawing.Size(77, 22);
            this.udScreenHeight.TabIndex = 3;
            this.udScreenHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // lblScreenWidth
            // 
            this.lblScreenWidth.AutoSize = true;
            this.lblScreenWidth.Enabled = false;
            this.lblScreenWidth.Location = new System.Drawing.Point(11, 54);
            this.lblScreenWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScreenWidth.Name = "lblScreenWidth";
            this.lblScreenWidth.Size = new System.Drawing.Size(48, 17);
            this.lblScreenWidth.TabIndex = 2;
            this.lblScreenWidth.Text = "Width:";
            // 
            // udScreenWidth
            // 
            this.udScreenWidth.Enabled = false;
            this.udScreenWidth.Location = new System.Drawing.Point(73, 52);
            this.udScreenWidth.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.udScreenWidth.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.udScreenWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.udScreenWidth.Name = "udScreenWidth";
            this.udScreenWidth.Size = new System.Drawing.Size(77, 22);
            this.udScreenWidth.TabIndex = 1;
            this.udScreenWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // cbResizeScreens
            // 
            this.cbResizeScreens.AutoSize = true;
            this.cbResizeScreens.Location = new System.Drawing.Point(15, 23);
            this.cbResizeScreens.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbResizeScreens.Name = "cbResizeScreens";
            this.cbResizeScreens.Size = new System.Drawing.Size(154, 21);
            this.cbResizeScreens.TabIndex = 0;
            this.cbResizeScreens.Text = "Resize screenshots";
            this.cbResizeScreens.UseVisualStyleBackColor = true;
            this.cbResizeScreens.CheckedChanged += new System.EventHandler(this.cbResizeScreens_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnScreenDir);
            this.groupBox6.Controls.Add(this.lblScreenLocation);
            this.groupBox6.Controls.Add(this.txtScreenDir);
            this.groupBox6.Controls.Add(this.cbSaveScreenshots);
            this.groupBox6.Location = new System.Drawing.Point(8, 7);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(7, 4, 4, 4);
            this.groupBox6.Size = new System.Drawing.Size(707, 90);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Saving";
            // 
            // btnScreenDir
            // 
            this.btnScreenDir.Enabled = false;
            this.btnScreenDir.Location = new System.Drawing.Point(599, 49);
            this.btnScreenDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnScreenDir.Name = "btnScreenDir";
            this.btnScreenDir.Size = new System.Drawing.Size(100, 28);
            this.btnScreenDir.TabIndex = 3;
            this.btnScreenDir.Text = "Browse...";
            this.btnScreenDir.UseVisualStyleBackColor = true;
            this.btnScreenDir.Click += new System.EventHandler(this.btnScreenDir_Click);
            // 
            // lblScreenLocation
            // 
            this.lblScreenLocation.AutoSize = true;
            this.lblScreenLocation.Enabled = false;
            this.lblScreenLocation.Location = new System.Drawing.Point(11, 55);
            this.lblScreenLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScreenLocation.Name = "lblScreenLocation";
            this.lblScreenLocation.Size = new System.Drawing.Size(66, 17);
            this.lblScreenLocation.TabIndex = 2;
            this.lblScreenLocation.Text = "Location:";
            // 
            // txtScreenDir
            // 
            this.txtScreenDir.Enabled = false;
            this.txtScreenDir.Location = new System.Drawing.Point(87, 52);
            this.txtScreenDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtScreenDir.Name = "txtScreenDir";
            this.txtScreenDir.Size = new System.Drawing.Size(503, 22);
            this.txtScreenDir.TabIndex = 1;
            // 
            // cbSaveScreenshots
            // 
            this.cbSaveScreenshots.AutoSize = true;
            this.cbSaveScreenshots.Location = new System.Drawing.Point(15, 23);
            this.cbSaveScreenshots.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbSaveScreenshots.Name = "cbSaveScreenshots";
            this.cbSaveScreenshots.Size = new System.Drawing.Size(228, 21);
            this.cbSaveScreenshots.TabIndex = 0;
            this.cbSaveScreenshots.Text = "Automatically save screenshots";
            this.cbSaveScreenshots.UseVisualStyleBackColor = true;
            this.cbSaveScreenshots.CheckedChanged += new System.EventHandler(this.cbSaveScreenshots_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.groupBox9);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage5.Size = new System.Drawing.Size(725, 244);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Advanced Poker";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.button2);
            this.groupBox9.Controls.Add(this.gameslist);
            this.groupBox9.Controls.Add(this.button1);
            this.groupBox9.Controls.Add(this.Gametxt);
            this.groupBox9.Location = new System.Drawing.Point(8, 7);
            this.groupBox9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox9.Size = new System.Drawing.Size(707, 226);
            this.groupBox9.TabIndex = 0;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Additional Games";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 194);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(691, 25);
            this.button2.TabIndex = 4;
            this.button2.Text = "Remove Selected";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // gameslist
            // 
            this.gameslist.FormattingEnabled = true;
            this.gameslist.ItemHeight = 16;
            this.gameslist.Location = new System.Drawing.Point(8, 55);
            this.gameslist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gameslist.Name = "gameslist";
            this.gameslist.Size = new System.Drawing.Size(689, 132);
            this.gameslist.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 23);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "Add Game";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Gametxt
            // 
            this.Gametxt.Location = new System.Drawing.Point(8, 23);
            this.Gametxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Gametxt.Name = "Gametxt";
            this.Gametxt.Size = new System.Drawing.Size(581, 22);
            this.Gametxt.TabIndex = 1;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.tabControl2);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage6.Size = new System.Drawing.Size(725, 244);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Patches";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage7);
            this.tabControl2.Controls.Add(this.tabPage8);
            this.tabControl2.Controls.Add(this.tabPage9);
            this.tabControl2.Location = new System.Drawing.Point(8, 7);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(707, 144);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.label8);
            this.tabPage7.Controls.Add(this.p_1_d);
            this.tabPage7.Controls.Add(this.label9);
            this.tabPage7.Controls.Add(this.label7);
            this.tabPage7.Controls.Add(this.button4);
            this.tabPage7.Controls.Add(this.button3);
            this.tabPage7.Controls.Add(this.p_1_m);
            this.tabPage7.Controls.Add(this.p_1);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage7.Size = new System.Drawing.Size(699, 115);
            this.tabPage7.TabIndex = 0;
            this.tabPage7.Text = "Patch 1";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 81);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Description:";
            // 
            // p_1_d
            // 
            this.p_1_d.Location = new System.Drawing.Point(105, 78);
            this.p_1_d.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_1_d.Name = "p_1_d";
            this.p_1_d.Size = new System.Drawing.Size(577, 22);
            this.p_1_d.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 49);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 17);
            this.label9.TabIndex = 11;
            this.label9.Text = "Map:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Patch:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(584, 42);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 10;
            this.button4.Text = "Browse...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(584, 10);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 10;
            this.button3.Text = "Browse...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // p_1_m
            // 
            this.p_1_m.Location = new System.Drawing.Point(72, 46);
            this.p_1_m.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_1_m.Name = "p_1_m";
            this.p_1_m.Size = new System.Drawing.Size(503, 22);
            this.p_1_m.TabIndex = 9;
            // 
            // p_1
            // 
            this.p_1.Location = new System.Drawing.Point(72, 14);
            this.p_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_1.Name = "p_1";
            this.p_1.Size = new System.Drawing.Size(503, 22);
            this.p_1.TabIndex = 9;
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.label10);
            this.tabPage8.Controls.Add(this.p_2_d);
            this.tabPage8.Controls.Add(this.label11);
            this.tabPage8.Controls.Add(this.label12);
            this.tabPage8.Controls.Add(this.button5);
            this.tabPage8.Controls.Add(this.button6);
            this.tabPage8.Controls.Add(this.p_2_m);
            this.tabPage8.Controls.Add(this.p_2);
            this.tabPage8.Location = new System.Drawing.Point(4, 25);
            this.tabPage8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage8.Size = new System.Drawing.Size(699, 115);
            this.tabPage8.TabIndex = 1;
            this.tabPage8.Text = "Patch 2";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 81);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 17);
            this.label10.TabIndex = 21;
            this.label10.Text = "Description:";
            // 
            // p_2_d
            // 
            this.p_2_d.Location = new System.Drawing.Point(105, 78);
            this.p_2_d.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_2_d.Name = "p_2_d";
            this.p_2_d.Size = new System.Drawing.Size(577, 22);
            this.p_2_d.TabIndex = 20;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 49);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 17);
            this.label11.TabIndex = 18;
            this.label11.Text = "Map:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 17);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 17);
            this.label12.TabIndex = 19;
            this.label12.Text = "Patch:";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(584, 42);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 28);
            this.button5.TabIndex = 16;
            this.button5.Text = "Browse...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(584, 10);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 28);
            this.button6.TabIndex = 17;
            this.button6.Text = "Browse...";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // p_2_m
            // 
            this.p_2_m.Location = new System.Drawing.Point(72, 46);
            this.p_2_m.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_2_m.Name = "p_2_m";
            this.p_2_m.Size = new System.Drawing.Size(503, 22);
            this.p_2_m.TabIndex = 14;
            // 
            // p_2
            // 
            this.p_2.Location = new System.Drawing.Point(72, 14);
            this.p_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_2.Name = "p_2";
            this.p_2.Size = new System.Drawing.Size(503, 22);
            this.p_2.TabIndex = 15;
            // 
            // tabPage9
            // 
            this.tabPage9.Controls.Add(this.label13);
            this.tabPage9.Controls.Add(this.p_3_d);
            this.tabPage9.Controls.Add(this.label14);
            this.tabPage9.Controls.Add(this.label15);
            this.tabPage9.Controls.Add(this.button7);
            this.tabPage9.Controls.Add(this.button8);
            this.tabPage9.Controls.Add(this.p_3_m);
            this.tabPage9.Controls.Add(this.p_3);
            this.tabPage9.Location = new System.Drawing.Point(4, 25);
            this.tabPage9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage9.Size = new System.Drawing.Size(699, 115);
            this.tabPage9.TabIndex = 2;
            this.tabPage9.Text = "Patch 3";
            this.tabPage9.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 81);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 17);
            this.label13.TabIndex = 21;
            this.label13.Text = "Description:";
            // 
            // p_3_d
            // 
            this.p_3_d.Location = new System.Drawing.Point(105, 78);
            this.p_3_d.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_3_d.Name = "p_3_d";
            this.p_3_d.Size = new System.Drawing.Size(577, 22);
            this.p_3_d.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(13, 49);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(39, 17);
            this.label14.TabIndex = 18;
            this.label14.Text = "Map:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 17);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 17);
            this.label15.TabIndex = 19;
            this.label15.Text = "Patch:";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(584, 42);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(100, 28);
            this.button7.TabIndex = 16;
            this.button7.Text = "Browse...";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(584, 10);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(100, 28);
            this.button8.TabIndex = 17;
            this.button8.Text = "Browse...";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // p_3_m
            // 
            this.p_3_m.Location = new System.Drawing.Point(72, 46);
            this.p_3_m.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_3_m.Name = "p_3_m";
            this.p_3_m.Size = new System.Drawing.Size(503, 22);
            this.p_3_m.TabIndex = 14;
            // 
            // p_3
            // 
            this.p_3.Location = new System.Drawing.Point(72, 14);
            this.p_3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.p_3.Name = "p_3";
            this.p_3.Size = new System.Drawing.Size(503, 22);
            this.p_3.TabIndex = 15;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(541, 351);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(649, 351);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(365, 11);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "Find IP";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(765, 394);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxLogo)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udGamma)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udScreenHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udScreenWidth)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.tabPage8.ResumeLayout(false);
            this.tabPage8.PerformLayout();
            this.tabPage9.ResumeLayout(false);
            this.tabPage9.PerformLayout();
            this.ResumeLayout(false);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Util.Connect();
            txtXdkName.Text = Util.XboxIP();
        }
    }
}

