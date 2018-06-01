namespace Ascension.Forms.Dialog
{
    using Ascension.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(SettingsForm));
            lblTitle = new Label();
            panel1 = new Panel();
            pbxLogo = new PictureBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox4 = new GroupBox();
            rbForceCampaign = new RadioButton();
            rbForceMultiplayer = new RadioButton();
            groupBox3 = new GroupBox();
            cbOpenMap = new CheckBox();
            btnAutoMapPath = new Button();
            lblMapToOpen = new Label();
            txtAutoMapPath = new TextBox();
            cbUpdates = new CheckBox();
            txtXdkName = new TextBox();
            label1 = new Label();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            label6 = new Label();
            label5 = new Label();
            txtRetailDir = new TextBox();
            btnBetaDir = new Button();
            btnRetailDir = new Button();
            txtBetaDir = new TextBox();
            groupBox1 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnImageDir = new Button();
            btnMapinfoDir = new Button();
            btnMapDir = new Button();
            txtImageDir = new TextBox();
            txtMapinfoDir = new TextBox();
            txtMapDir = new TextBox();
            tabPage3 = new TabPage();
            groupBox5 = new GroupBox();
            cbOldSwapper = new CheckBox();
            cbInvisibles = new CheckBox();
            tabPage4 = new TabPage();
            groupBox8 = new GroupBox();
            lblGamma = new Label();
            udGamma = new NumericUpDown();
            cbGamma = new CheckBox();
            groupBox7 = new GroupBox();
            lblScreenHeight = new Label();
            udScreenHeight = new NumericUpDown();
            lblScreenWidth = new Label();
            udScreenWidth = new NumericUpDown();
            cbResizeScreens = new CheckBox();
            groupBox6 = new GroupBox();
            btnScreenDir = new Button();
            lblScreenLocation = new Label();
            txtScreenDir = new TextBox();
            cbSaveScreenshots = new CheckBox();
            tabPage5 = new TabPage();
            groupBox9 = new GroupBox();
            button2 = new Button();
            gameslist = new ListBox();
            button1 = new Button();
            Gametxt = new TextBox();
            tabPage6 = new TabPage();
            tabControl2 = new TabControl();
            tabPage7 = new TabPage();
            label8 = new Label();
            p_1_d = new TextBox();
            label9 = new Label();
            label7 = new Label();
            button4 = new Button();
            button3 = new Button();
            p_1_m = new TextBox();
            p_1 = new TextBox();
            tabPage8 = new TabPage();
            label10 = new Label();
            p_2_d = new TextBox();
            label11 = new Label();
            label12 = new Label();
            button5 = new Button();
            button6 = new Button();
            p_2_m = new TextBox();
            p_2 = new TextBox();
            tabPage9 = new TabPage();
            label13 = new Label();
            p_3_d = new TextBox();
            label14 = new Label();
            label15 = new Label();
            button7 = new Button();
            button8 = new Button();
            p_3_m = new TextBox();
            p_3 = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            panel1.SuspendLayout();
            ((ISupportInitialize) pbxLogo).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox3.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            groupBox5.SuspendLayout();
            tabPage4.SuspendLayout();
            groupBox8.SuspendLayout();
            udGamma.BeginInit();
            groupBox7.SuspendLayout();
            udScreenHeight.BeginInit();
            udScreenWidth.BeginInit();
            groupBox6.SuspendLayout();
            tabPage5.SuspendLayout();
            groupBox9.SuspendLayout();
            tabPage6.SuspendLayout();
            tabControl2.SuspendLayout();
            tabPage7.SuspendLayout();
            tabPage8.SuspendLayout();
            tabPage9.SuspendLayout();
            base.SuspendLayout();
            lblTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            lblTitle.Font = new Font("Microsoft Sans Serif", 14f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0x3b, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0x1ff, 0x1d);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Ascension Settings";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            panel1.Controls.Add(lblTitle);
            panel1.Controls.Add(pbxLogo);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x23e, 0x33);
            panel1.TabIndex = 0x15;
            pbxLogo.Location = new Point(3, 3);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(50, 0x2d);
            pbxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pbxLogo.TabIndex = 4;
            pbxLogo.TabStop = false;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Location = new Point(12, 0x39);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(550, 0xde);
            tabControl1.TabIndex = 0x16;
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(txtXdkName);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 0x16);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(0x21e, 0xc4);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            groupBox4.Controls.Add(rbForceCampaign);
            groupBox4.Controls.Add(rbForceMultiplayer);
            groupBox4.Location = new Point(6, 0x92);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(530, 0x2c);
            groupBox4.TabIndex = 7;
            groupBox4.TabStop = false;
            groupBox4.Text = "Force Map Loading (does not apply to ff10_prototype)";
            rbForceCampaign.AutoSize = true;
            rbForceCampaign.Location = new Point(0x88, 0x13);
            rbForceCampaign.Name = "rbForceCampaign";
            rbForceCampaign.Size = new Size(0x71, 0x11);
            rbForceCampaign.TabIndex = 1;
            rbForceCampaign.TabStop = true;
            rbForceCampaign.Text = "Load as Campaign";
            rbForceCampaign.UseVisualStyleBackColor = true;
            rbForceMultiplayer.AutoSize = true;
            rbForceMultiplayer.Location = new Point(11, 0x13);
            rbForceMultiplayer.Name = "rbForceMultiplayer";
            rbForceMultiplayer.Size = new Size(0x74, 0x11);
            rbForceMultiplayer.TabIndex = 0;
            rbForceMultiplayer.TabStop = true;
            rbForceMultiplayer.Text = "Load as Multiplayer";
            rbForceMultiplayer.UseVisualStyleBackColor = true;
            groupBox3.Controls.Add(cbOpenMap);
            groupBox3.Controls.Add(btnAutoMapPath);
            groupBox3.Controls.Add(lblMapToOpen);
            groupBox3.Controls.Add(txtAutoMapPath);
            groupBox3.Controls.Add(cbUpdates);
            groupBox3.Location = new Point(6, 0x23);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(5, 3, 3, 3);
            groupBox3.Size = new Size(530, 0x69);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            groupBox3.Text = "Startup";
            cbOpenMap.AutoSize = true;
            cbOpenMap.Location = new Point(11, 0x2a);
            cbOpenMap.Name = "cbOpenMap";
            cbOpenMap.Size = new Size(0xa3, 0x11);
            cbOpenMap.TabIndex = 4;
            cbOpenMap.Text = "Automatically open a map file";
            cbOpenMap.UseVisualStyleBackColor = true;
            cbOpenMap.CheckedChanged += new EventHandler(cbOpenMap_CheckedChanged);
            btnAutoMapPath.Enabled = false;
            btnAutoMapPath.Location = new Point(0x1c1, 0x42);
            btnAutoMapPath.Name = "btnAutoMapPath";
            btnAutoMapPath.Size = new Size(0x4b, 0x17);
            btnAutoMapPath.TabIndex = 3;
            btnAutoMapPath.Text = "Browse...";
            btnAutoMapPath.UseVisualStyleBackColor = true;
            btnAutoMapPath.Click += new EventHandler(btnAutoMapPath_Click);
            lblMapToOpen.AutoSize = true;
            lblMapToOpen.Enabled = false;
            lblMapToOpen.Location = new Point(8, 0x47);
            lblMapToOpen.Name = "lblMapToOpen";
            lblMapToOpen.Size = new Size(70, 13);
            lblMapToOpen.TabIndex = 2;
            lblMapToOpen.Text = "Map to open:";
            txtAutoMapPath.Enabled = false;
            txtAutoMapPath.Location = new Point(0x54, 0x44);
            txtAutoMapPath.Name = "txtAutoMapPath";
            txtAutoMapPath.Size = new Size(0x167, 20);
            txtAutoMapPath.TabIndex = 1;
            cbUpdates.AutoSize = true;
            cbUpdates.Enabled = false;
            cbUpdates.Location = new Point(11, 0x13);
            cbUpdates.Name = "cbUpdates";
            cbUpdates.Size = new Size(0x71, 0x11);
            cbUpdates.TabIndex = 0;
            cbUpdates.Text = "Check for updates";
            cbUpdates.UseVisualStyleBackColor = true;
            txtXdkName.Location = new Point(0x5c, 9);
            txtXdkName.Name = "txtXdkName";
            txtXdkName.Size = new Size(0xb1, 20);
            txtXdkName.TabIndex = 5;
            label1.AutoSize = true;
            label1.Location = new Point(8, 12);
            label1.Name = "label1";
            label1.Size = new Size(0x4e, 13);
            label1.TabIndex = 4;
            label1.Text = "XDK Name/IP:";
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(groupBox1);
            tabPage2.Location = new Point(4, 0x16);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(0x21e, 0xc4);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Paths";
            tabPage2.UseVisualStyleBackColor = true;
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtRetailDir);
            groupBox2.Controls.Add(btnBetaDir);
            groupBox2.Controls.Add(btnRetailDir);
            groupBox2.Controls.Add(txtBetaDir);
            groupBox2.Location = new Point(6, 0x70);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(530, 0x4e);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Plugin Paths";
            label6.AutoSize = true;
            label6.Location = new Point(8, 50);
            label6.Name = "label6";
            label6.Size = new Size(0x45, 13);
            label6.TabIndex = 0x10;
            label6.Text = "Beta Plugins:";
            label5.AutoSize = true;
            label5.Location = new Point(8, 0x18);
            label5.Name = "label5";
            label5.Size = new Size(0x4a, 13);
            label5.TabIndex = 15;
            label5.Text = "Retail Plugins:";
            txtRetailDir.Location = new Point(90, 0x16);
            txtRetailDir.Name = "txtRetailDir";
            txtRetailDir.Size = new Size(0x161, 20);
            txtRetailDir.TabIndex = 14;
            btnBetaDir.Location = new Point(0x1c1, 0x2d);
            btnBetaDir.Name = "btnBetaDir";
            btnBetaDir.Size = new Size(0x4b, 0x17);
            btnBetaDir.TabIndex = 13;
            btnBetaDir.Text = "Browse...";
            btnBetaDir.UseVisualStyleBackColor = true;
            btnBetaDir.Click += new EventHandler(btnBetaDir_Click);
            btnRetailDir.Location = new Point(0x1c1, 20);
            btnRetailDir.Name = "btnRetailDir";
            btnRetailDir.Size = new Size(0x4b, 0x17);
            btnRetailDir.TabIndex = 12;
            btnRetailDir.Text = "Browse...";
            btnRetailDir.UseVisualStyleBackColor = true;
            btnRetailDir.Click += new EventHandler(btnRetailDir_Click);
            txtBetaDir.Location = new Point(90, 0x2f);
            txtBetaDir.Name = "txtBetaDir";
            txtBetaDir.Size = new Size(0x161, 20);
            txtBetaDir.TabIndex = 11;
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnImageDir);
            groupBox1.Controls.Add(btnMapinfoDir);
            groupBox1.Controls.Add(btnMapDir);
            groupBox1.Controls.Add(txtImageDir);
            groupBox1.Controls.Add(txtMapinfoDir);
            groupBox1.Controls.Add(txtMapDir);
            groupBox1.Location = new Point(6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(5, 3, 3, 3);
            groupBox1.Size = new Size(530, 100);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "General Paths";
            label4.AutoSize = true;
            label4.Location = new Point(8, 0x4a);
            label4.Name = "label4";
            label4.Size = new Size(0x44, 13);
            label4.TabIndex = 10;
            label4.Text = "Map Images:";
            label3.AutoSize = true;
            label3.Location = new Point(8, 0x30);
            label3.Name = "label3";
            label3.Size = new Size(0x31, 13);
            label3.TabIndex = 9;
            label3.Text = "MapInfo:";
            label2.AutoSize = true;
            label2.Location = new Point(8, 0x16);
            label2.Name = "label2";
            label2.Size = new Size(0x24, 13);
            label2.TabIndex = 8;
            label2.Text = "Maps:";
            btnImageDir.Location = new Point(0x1c1, 0x45);
            btnImageDir.Name = "btnImageDir";
            btnImageDir.Size = new Size(0x4b, 0x17);
            btnImageDir.TabIndex = 7;
            btnImageDir.Text = "Browse...";
            btnImageDir.UseVisualStyleBackColor = true;
            btnImageDir.Click += new EventHandler(btnImageDir_Click);
            btnMapinfoDir.Location = new Point(0x1c1, 0x2b);
            btnMapinfoDir.Name = "btnMapinfoDir";
            btnMapinfoDir.Size = new Size(0x4b, 0x17);
            btnMapinfoDir.TabIndex = 6;
            btnMapinfoDir.Text = "Browse...";
            btnMapinfoDir.UseVisualStyleBackColor = true;
            btnMapinfoDir.Click += new EventHandler(btnMapinfoDir_Click);
            btnMapDir.Location = new Point(0x1c1, 0x11);
            btnMapDir.Name = "btnMapDir";
            btnMapDir.Size = new Size(0x4b, 0x17);
            btnMapDir.TabIndex = 5;
            btnMapDir.Text = "Browse...";
            btnMapDir.UseVisualStyleBackColor = true;
            btnMapDir.Click += new EventHandler(btnMapDir_Click);
            txtImageDir.Location = new Point(90, 0x47);
            txtImageDir.Name = "txtImageDir";
            txtImageDir.Size = new Size(0x161, 20);
            txtImageDir.TabIndex = 2;
            txtMapinfoDir.Location = new Point(90, 0x2d);
            txtMapinfoDir.Name = "txtMapinfoDir";
            txtMapinfoDir.Size = new Size(0x161, 20);
            txtMapinfoDir.TabIndex = 1;
            txtMapDir.Location = new Point(90, 0x13);
            txtMapDir.Name = "txtMapDir";
            txtMapDir.Size = new Size(0x161, 20);
            txtMapDir.TabIndex = 0;
            tabPage3.Controls.Add(groupBox5);
            tabPage3.Location = new Point(4, 0x16);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(0x21e, 0xc4);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Editor";
            tabPage3.UseVisualStyleBackColor = true;
            groupBox5.Controls.Add(cbOldSwapper);
            groupBox5.Controls.Add(cbInvisibles);
            groupBox5.Location = new Point(6, 6);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(5, 3, 3, 3);
            groupBox5.Size = new Size(530, 0x43);
            groupBox5.TabIndex = 0;
            groupBox5.TabStop = false;
            groupBox5.Text = "Tag Editor";
            cbOldSwapper.AutoSize = true;
            cbOldSwapper.Location = new Point(11, 0x2a);
            cbOldSwapper.Name = "cbOldSwapper";
            cbOldSwapper.Size = new Size(0x95, 0x11);
            cbOldSwapper.TabIndex = 2;
            cbOldSwapper.Text = "Use the old ident swapper";
            cbOldSwapper.UseVisualStyleBackColor = true;
            cbInvisibles.AutoSize = true;
            cbInvisibles.Location = new Point(11, 0x13);
            cbInvisibles.Name = "cbInvisibles";
            cbInvisibles.Size = new Size(0x7f, 0x11);
            cbInvisibles.TabIndex = 0;
            cbInvisibles.Text = "Show invisible values";
            cbInvisibles.UseVisualStyleBackColor = true;
            tabPage4.Controls.Add(groupBox8);
            tabPage4.Controls.Add(groupBox7);
            tabPage4.Controls.Add(groupBox6);
            tabPage4.Location = new Point(4, 0x16);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(0x21e, 0xc4);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Screenshots";
            tabPage4.UseVisualStyleBackColor = true;
            groupBox8.Controls.Add(lblGamma);
            groupBox8.Controls.Add(udGamma);
            groupBox8.Controls.Add(cbGamma);
            groupBox8.Location = new Point(0x111, 0x55);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(5, 3, 3, 3);
            groupBox8.Size = new Size(0x107, 100);
            groupBox8.TabIndex = 2;
            groupBox8.TabStop = false;
            groupBox8.Text = "Gamma Adjustment";
            lblGamma.AutoSize = true;
            lblGamma.Enabled = false;
            lblGamma.Location = new Point(8, 0x2c);
            lblGamma.Name = "lblGamma";
            lblGamma.Size = new Size(0x2e, 13);
            lblGamma.TabIndex = 2;
            lblGamma.Text = "Amount:";
            udGamma.DecimalPlaces = 2;
            udGamma.Enabled = false;
            int[] bits = new int[4];
            bits[0] = 1;
            bits[3] = 0x10000;
            udGamma.Increment = new decimal(bits);
            udGamma.Location = new Point(60, 0x2a);
            udGamma.Name = "udGamma";
            udGamma.Size = new Size(0x38, 20);
            udGamma.TabIndex = 1;
            cbGamma.AutoSize = true;
            cbGamma.Location = new Point(11, 0x13);
            cbGamma.Name = "cbGamma";
            cbGamma.Size = new Size(0x5c, 0x11);
            cbGamma.TabIndex = 0;
            cbGamma.Text = "Adjust gamma";
            cbGamma.UseVisualStyleBackColor = true;
            cbGamma.CheckedChanged += new EventHandler(cbGamma_CheckedChanged);
            groupBox7.Controls.Add(lblScreenHeight);
            groupBox7.Controls.Add(udScreenHeight);
            groupBox7.Controls.Add(lblScreenWidth);
            groupBox7.Controls.Add(udScreenWidth);
            groupBox7.Controls.Add(cbResizeScreens);
            groupBox7.Location = new Point(6, 0x55);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(5, 3, 3, 3);
            groupBox7.Size = new Size(0x105, 100);
            groupBox7.TabIndex = 1;
            groupBox7.TabStop = false;
            groupBox7.Text = "Resizing";
            lblScreenHeight.AutoSize = true;
            lblScreenHeight.Enabled = false;
            lblScreenHeight.Location = new Point(8, 70);
            lblScreenHeight.Name = "lblScreenHeight";
            lblScreenHeight.Size = new Size(0x29, 13);
            lblScreenHeight.TabIndex = 4;
            lblScreenHeight.Text = "Height:";
            udScreenHeight.Enabled = false;
            udScreenHeight.Location = new Point(0x37, 0x44);
            bits = new int[4];
            bits[0] = 0x2710;
            udScreenHeight.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            udScreenHeight.Minimum = new decimal(bits);
            udScreenHeight.Name = "udScreenHeight";
            udScreenHeight.Size = new Size(0x3a, 20);
            udScreenHeight.TabIndex = 3;
            bits = new int[4];
            bits[0] = 1;
            udScreenHeight.Value = new decimal(bits);
            lblScreenWidth.AutoSize = true;
            lblScreenWidth.Enabled = false;
            lblScreenWidth.Location = new Point(8, 0x2c);
            lblScreenWidth.Name = "lblScreenWidth";
            lblScreenWidth.Size = new Size(0x26, 13);
            lblScreenWidth.TabIndex = 2;
            lblScreenWidth.Text = "Width:";
            udScreenWidth.Enabled = false;
            udScreenWidth.Location = new Point(0x37, 0x2a);
            bits = new int[4];
            bits[0] = 0x2710;
            udScreenWidth.Maximum = new decimal(bits);
            bits = new int[4];
            bits[0] = 1;
            udScreenWidth.Minimum = new decimal(bits);
            udScreenWidth.Name = "udScreenWidth";
            udScreenWidth.Size = new Size(0x3a, 20);
            udScreenWidth.TabIndex = 1;
            bits = new int[4];
            bits[0] = 1;
            udScreenWidth.Value = new decimal(bits);
            cbResizeScreens.AutoSize = true;
            cbResizeScreens.Location = new Point(11, 0x13);
            cbResizeScreens.Name = "cbResizeScreens";
            cbResizeScreens.Size = new Size(0x76, 0x11);
            cbResizeScreens.TabIndex = 0;
            cbResizeScreens.Text = "Resize screenshots";
            cbResizeScreens.UseVisualStyleBackColor = true;
            cbResizeScreens.CheckedChanged += new EventHandler(cbResizeScreens_CheckedChanged);
            groupBox6.Controls.Add(btnScreenDir);
            groupBox6.Controls.Add(lblScreenLocation);
            groupBox6.Controls.Add(txtScreenDir);
            groupBox6.Controls.Add(cbSaveScreenshots);
            groupBox6.Location = new Point(6, 6);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(5, 3, 3, 3);
            groupBox6.Size = new Size(530, 0x49);
            groupBox6.TabIndex = 0;
            groupBox6.TabStop = false;
            groupBox6.Text = "Saving";
            btnScreenDir.Enabled = false;
            btnScreenDir.Location = new Point(0x1c1, 40);
            btnScreenDir.Name = "btnScreenDir";
            btnScreenDir.Size = new Size(0x4b, 0x17);
            btnScreenDir.TabIndex = 3;
            btnScreenDir.Text = "Browse...";
            btnScreenDir.UseVisualStyleBackColor = true;
            btnScreenDir.Click += new EventHandler(btnScreenDir_Click);
            lblScreenLocation.AutoSize = true;
            lblScreenLocation.Enabled = false;
            lblScreenLocation.Location = new Point(8, 0x2d);
            lblScreenLocation.Name = "lblScreenLocation";
            lblScreenLocation.Size = new Size(0x33, 13);
            lblScreenLocation.TabIndex = 2;
            lblScreenLocation.Text = "Location:";
            txtScreenDir.Enabled = false;
            txtScreenDir.Location = new Point(0x41, 0x2a);
            txtScreenDir.Name = "txtScreenDir";
            txtScreenDir.Size = new Size(0x17a, 20);
            txtScreenDir.TabIndex = 1;
            cbSaveScreenshots.AutoSize = true;
            cbSaveScreenshots.Location = new Point(11, 0x13);
            cbSaveScreenshots.Name = "cbSaveScreenshots";
            cbSaveScreenshots.Size = new Size(0xae, 0x11);
            cbSaveScreenshots.TabIndex = 0;
            cbSaveScreenshots.Text = "Automatically save screenshots";
            cbSaveScreenshots.UseVisualStyleBackColor = true;
            cbSaveScreenshots.CheckedChanged += new EventHandler(cbSaveScreenshots_CheckedChanged);
            tabPage5.Controls.Add(groupBox9);
            tabPage5.Location = new Point(4, 0x16);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(0x21e, 0xc4);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Advanced Poker";
            tabPage5.UseVisualStyleBackColor = true;
            groupBox9.Controls.Add(button2);
            groupBox9.Controls.Add(gameslist);
            groupBox9.Controls.Add(button1);
            groupBox9.Controls.Add(Gametxt);
            groupBox9.Location = new Point(6, 6);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(530, 0xb8);
            groupBox9.TabIndex = 0;
            groupBox9.TabStop = false;
            groupBox9.Text = "Additional Games";
            button2.Location = new Point(6, 0x9e);
            button2.Name = "button2";
            button2.Size = new Size(0x206, 20);
            button2.TabIndex = 4;
            button2.Text = "Remove Selected";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            gameslist.FormattingEnabled = true;
            gameslist.Location = new Point(6, 0x2d);
            gameslist.Name = "gameslist";
            gameslist.Size = new Size(0x206, 0x6c);
            gameslist.TabIndex = 3;
            button1.Location = new Point(0x1c1, 0x13);
            button1.Name = "button1";
            button1.Size = new Size(0x4b, 20);
            button1.TabIndex = 2;
            button1.Text = "Add Game";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            Gametxt.Location = new Point(6, 0x13);
            Gametxt.Name = "Gametxt";
            Gametxt.Size = new Size(0x1b5, 20);
            Gametxt.TabIndex = 1;
            tabPage6.Controls.Add(tabControl2);
            tabPage6.Location = new Point(4, 0x16);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(0x21e, 0xc4);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "Patches";
            tabPage6.UseVisualStyleBackColor = true;
            tabControl2.Controls.Add(tabPage7);
            tabControl2.Controls.Add(tabPage8);
            tabControl2.Controls.Add(tabPage9);
            tabControl2.Location = new Point(6, 6);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(530, 0x75);
            tabControl2.TabIndex = 0;
            tabPage7.Controls.Add(label8);
            tabPage7.Controls.Add(p_1_d);
            tabPage7.Controls.Add(label9);
            tabPage7.Controls.Add(label7);
            tabPage7.Controls.Add(button4);
            tabPage7.Controls.Add(button3);
            tabPage7.Controls.Add(p_1_m);
            tabPage7.Controls.Add(p_1);
            tabPage7.Location = new Point(4, 0x16);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(0x20a, 0x5b);
            tabPage7.TabIndex = 0;
            tabPage7.Text = "Patch 1";
            tabPage7.UseVisualStyleBackColor = true;
            label8.AutoSize = true;
            label8.Location = new Point(10, 0x42);
            label8.Name = "label8";
            label8.Size = new Size(0x3f, 13);
            label8.TabIndex = 13;
            label8.Text = "Description:";
            p_1_d.Location = new Point(0x4f, 0x3f);
            p_1_d.Name = "p_1_d";
            p_1_d.Size = new Size(0x1b2, 20);
            p_1_d.TabIndex = 12;
            label9.AutoSize = true;
            label9.Location = new Point(10, 40);
            label9.Name = "label9";
            label9.Size = new Size(0x1f, 13);
            label9.TabIndex = 11;
            label9.Text = "Map:";
            label7.AutoSize = true;
            label7.Location = new Point(10, 14);
            label7.Name = "label7";
            label7.Size = new Size(0x26, 13);
            label7.TabIndex = 11;
            label7.Text = "Patch:";
            button4.Location = new Point(0x1b6, 0x22);
            button4.Name = "button4";
            button4.Size = new Size(0x4b, 0x17);
            button4.TabIndex = 10;
            button4.Text = "Browse...";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            button3.Location = new Point(0x1b6, 8);
            button3.Name = "button3";
            button3.Size = new Size(0x4b, 0x17);
            button3.TabIndex = 10;
            button3.Text = "Browse...";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            p_1_m.Location = new Point(0x36, 0x25);
            p_1_m.Name = "p_1_m";
            p_1_m.Size = new Size(0x17a, 20);
            p_1_m.TabIndex = 9;
            p_1.Location = new Point(0x36, 11);
            p_1.Name = "p_1";
            p_1.Size = new Size(0x17a, 20);
            p_1.TabIndex = 9;
            tabPage8.Controls.Add(label10);
            tabPage8.Controls.Add(p_2_d);
            tabPage8.Controls.Add(label11);
            tabPage8.Controls.Add(label12);
            tabPage8.Controls.Add(button5);
            tabPage8.Controls.Add(button6);
            tabPage8.Controls.Add(p_2_m);
            tabPage8.Controls.Add(p_2);
            tabPage8.Location = new Point(4, 0x16);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(0x20a, 0x5b);
            tabPage8.TabIndex = 1;
            tabPage8.Text = "Patch 2";
            tabPage8.UseVisualStyleBackColor = true;
            label10.AutoSize = true;
            label10.Location = new Point(10, 0x42);
            label10.Name = "label10";
            label10.Size = new Size(0x3f, 13);
            label10.TabIndex = 0x15;
            label10.Text = "Description:";
            p_2_d.Location = new Point(0x4f, 0x3f);
            p_2_d.Name = "p_2_d";
            p_2_d.Size = new Size(0x1b2, 20);
            p_2_d.TabIndex = 20;
            label11.AutoSize = true;
            label11.Location = new Point(10, 40);
            label11.Name = "label11";
            label11.Size = new Size(0x1f, 13);
            label11.TabIndex = 0x12;
            label11.Text = "Map:";
            label12.AutoSize = true;
            label12.Location = new Point(10, 14);
            label12.Name = "label12";
            label12.Size = new Size(0x26, 13);
            label12.TabIndex = 0x13;
            label12.Text = "Patch:";
            button5.Location = new Point(0x1b6, 0x22);
            button5.Name = "button5";
            button5.Size = new Size(0x4b, 0x17);
            button5.TabIndex = 0x10;
            button5.Text = "Browse...";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            button6.Location = new Point(0x1b6, 8);
            button6.Name = "button6";
            button6.Size = new Size(0x4b, 0x17);
            button6.TabIndex = 0x11;
            button6.Text = "Browse...";
            button6.UseVisualStyleBackColor = true;
            button6.Click += new EventHandler(button6_Click);
            p_2_m.Location = new Point(0x36, 0x25);
            p_2_m.Name = "p_2_m";
            p_2_m.Size = new Size(0x17a, 20);
            p_2_m.TabIndex = 14;
            p_2.Location = new Point(0x36, 11);
            p_2.Name = "p_2";
            p_2.Size = new Size(0x17a, 20);
            p_2.TabIndex = 15;
            tabPage9.Controls.Add(label13);
            tabPage9.Controls.Add(p_3_d);
            tabPage9.Controls.Add(label14);
            tabPage9.Controls.Add(label15);
            tabPage9.Controls.Add(button7);
            tabPage9.Controls.Add(button8);
            tabPage9.Controls.Add(p_3_m);
            tabPage9.Controls.Add(p_3);
            tabPage9.Location = new Point(4, 0x16);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(0x20a, 0x5b);
            tabPage9.TabIndex = 2;
            tabPage9.Text = "Patch 3";
            tabPage9.UseVisualStyleBackColor = true;
            label13.AutoSize = true;
            label13.Location = new Point(10, 0x42);
            label13.Name = "label13";
            label13.Size = new Size(0x3f, 13);
            label13.TabIndex = 0x15;
            label13.Text = "Description:";
            p_3_d.Location = new Point(0x4f, 0x3f);
            p_3_d.Name = "p_3_d";
            p_3_d.Size = new Size(0x1b2, 20);
            p_3_d.TabIndex = 20;
            label14.AutoSize = true;
            label14.Location = new Point(10, 40);
            label14.Name = "label14";
            label14.Size = new Size(0x1f, 13);
            label14.TabIndex = 0x12;
            label14.Text = "Map:";
            label15.AutoSize = true;
            label15.Location = new Point(10, 14);
            label15.Name = "label15";
            label15.Size = new Size(0x26, 13);
            label15.TabIndex = 0x13;
            label15.Text = "Patch:";
            button7.Location = new Point(0x1b6, 0x22);
            button7.Name = "button7";
            button7.Size = new Size(0x4b, 0x17);
            button7.TabIndex = 0x10;
            button7.Text = "Browse...";
            button7.UseVisualStyleBackColor = true;
            button7.Click += new EventHandler(button7_Click);
            button8.Location = new Point(0x1b6, 8);
            button8.Name = "button8";
            button8.Size = new Size(0x4b, 0x17);
            button8.TabIndex = 0x11;
            button8.Text = "Browse...";
            button8.UseVisualStyleBackColor = true;
            button8.Click += new EventHandler(button8_Click);
            p_3_m.Location = new Point(0x36, 0x25);
            p_3_m.Name = "p_3_m";
            p_3_m.Size = new Size(0x17a, 20);
            p_3_m.TabIndex = 14;
            p_3.Location = new Point(0x36, 11);
            p_3.Name = "p_3";
            p_3.Size = new Size(0x17a, 20);
            p_3.TabIndex = 15;
            btnOK.Location = new Point(0x196, 0x11d);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(0x4b, 0x17);
            btnOK.TabIndex = 0;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(0x1e7, 0x11d);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(0x4b, 0x17);
            btnCancel.TabIndex = 0x17;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new EventHandler(btnCancel_Click);
            base.AcceptButton = btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = btnCancel;
            base.ClientSize = new Size(0x23e, 320);
            base.Controls.Add(btnCancel);
            base.Controls.Add(btnOK);
            base.Controls.Add(tabControl1);
            base.Controls.Add(panel1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SettingsForm";
            base.ShowIcon = false;
            Text = "Settings";
            panel1.ResumeLayout(false);
            ((ISupportInitialize) pbxLogo).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage3.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tabPage4.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            udGamma.EndInit();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            udScreenHeight.EndInit();
            udScreenWidth.EndInit();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            tabPage5.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabControl2.ResumeLayout(false);
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            tabPage8.ResumeLayout(false);
            tabPage8.PerformLayout();
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

