namespace Ascension.Halo_Reach.Game.Tag_Editor
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Game.Misc.Dialogs;
    using Ascension.Halo_Reach.Game.Tag_Editor.Classes;
    using Ascension.Halo_Reach.Game.Tag_Editor.Dialog;
    using Ascension.Halo_Reach.Plugins;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Windows.Forms;

    public class TagEditorContainer : UserControl
    {
        private HaloMap _map;
        private int _tagindex;
        private static int _valoffset;
        private ToolStripMenuItem bitmask16ToolStripMenuItem;
        private ToolStripMenuItem bitmask32ToolStripMenuItem;
        private ToolStripMenuItem bitmask8ToolStripMenuItem;
        private Button button1;
        private Button button10;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button8;
        private Button button9;
        private TextBox bytesTXT;
        private ToolStripMenuItem byteToolStripMenuItem;
        private TextBox byteTXT;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem commentToolStripMenuItem;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem enum16ToolStripMenuItem;
        private ToolStripMenuItem enum32ToolStripMenuItem;
        private ToolStripMenuItem enum8ToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        public int header_size = 0;
        private ToolStripMenuItem insertToolStripMenuItem;
        private ToolStripMenuItem int16ToolStripMenuItem;
        private TextBox int16TXT;
        private ToolStripMenuItem intToolStripMenuItem;
        private TextBox intTXT;
        public string ipath = "";
        private ToolStripMenuItem jtagToolStripMenuItem;
        private ToolStripMenuItem jtagXboxToolStripMenuItem;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label16;
        private Label label2;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        public List<string> labelEntry;
        private ToolStripButton menuButtonSave;
        private MenuStrip menuStrip1;
        private MenuStrip menuStrip2;
        private MenuStrip menuStrip3;
        private MenuStrip menuStrip4;
        private TextBox offsetTXT;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem optionToolStripMenuItem;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Panel panel7;
        private Panel panel8;
        private Panel panel9;
        public string pluginpath = "";
        private RichTextBox PluginTXT;
        private ToolStripButton poke_all;
        private ToolStripButton poke_changes;
        public Point pos = new Point(0, 0);
        public static bool Reading = true;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem1;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
        private ToolStripMenuItem saveAndUpdateToolStripMenuItem;
        private ToolStripMenuItem saveAndUpdateToolStripMenuItem1;
        private ToolStripMenuItem saveAndUpdateToolStripMenuItem2;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem1;
        private ToolStripMenuItem saveToolStripMenuItem2;
        private ToolStripMenuItem saveToolStripMenuItem3;
        public bool scroll = false;
        public static Point Scrolloffset = new Point(0, 0);
        private ToolStripMenuItem showHaloStringIDDatabaseToolStripMenuItem;
        private ToolStripMenuItem showInvs;
        private ToolStripMenuItem showOptionsToolStripMenuItem;
        private ToolStripMenuItem singleToolStripMenuItem;
        private TextBox singleTXT;
        private SplitContainer splitContainer1;
        private TextBox string256TXT;
        private TextBox string32TXT;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private ToolStripMenuItem tagblockToolStripMenuItem;
        private ToolStripMenuItem tagdataToolStripMenuItem;
        public Panel tagEditorPanel;
        private ToolStripMenuItem tagrefToolStripMenuItem;
        public string tclass = "";
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator14;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem uint16ToolStripMenuItem;
        private TextBox uint16TXT;
        private ToolStripMenuItem uintToolStripMenuItem;
        private TextBox uintTXT;
        private TextBox Unicode256TXT;
        private TextBox Unicode64TXT;
        private ToolStripMenuItem viewValueAsToolStripMenuItem;
        private WebBrowser webBrowser1;
        private ToolStripMenuItem xDKToolStripMenuItem;

        public TagEditorContainer(HaloMap map)
        {
            InitializeComponent();
            Map = map;
            panel1.Size = new Size(0, 547);
            ipath = string.Format("{0}\\StringID Lists\\{1}.sidlist", (object)Application.StartupPath, (object)Map.Map_Header.internalName);
            try
            {
                if (!System.IO.File.Exists(ipath))
                    System.IO.File.Create(ipath);
            }
            catch
            {
            }
            if (map.Map_Header.haloVersion == 11)
            {
                tabControl1.Controls.Remove((Control)tabPage3);
                menuButtonSave.Visible = false;
                toolStripDropDownButton1.Visible = false;
                toolStripDropDownButton2.Visible = false;
                toolStripSeparator1.Visible = false;
                toolStripSeparator14.Visible = false;
            }
            GetSID();
        }

        private void bitmask16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <bitmask16 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </bitmask16>";
        }

        private void bitmask32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <bitmask32 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </bitmask32>";
        }

        private void bitmask8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <bitmask8 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </bitmask8>";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LO();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ValOffset = int.Parse(offsetTXT.Text);
            Reading = false;
            label2.Text = "Reading...";
            label2.ForeColor = Color.Green;
            LO();
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            using (new WebClient())
            {
                string str = webBrowser1.DocumentText.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = int.Parse(offsetTXT.Text);
            int num2 = int.Parse(bytesTXT.Text);
            ValOffset = num - num2;
            Reading = false;
            label2.Text = "Reading...";
            label2.ForeColor = Color.Green;
            LO();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int num = int.Parse(offsetTXT.Text);
            int num2 = int.Parse(bytesTXT.Text);
            ValOffset = num + num2;
            Reading = false;
            label2.Text = "Reading...";
            label2.ForeColor = Color.Green;
            LO();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://noble-research.com/wiki/index.php?title=" + tclass + "&action=edit", UriKind.Absolute);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri("http://noble-research.com/wiki/index.php?title=" + tclass + "&action=edit", UriKind.Absolute);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(webBrowser1.DocumentText);
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void byteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <byte name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = "";
        }

        private void commentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <comment title=\"Title\" visible=\"False\">\r\n    Comment\r\n  </comment>";
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                UnloadMetaEditor();
            }
            catch
            {
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void enum16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <enum16 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </enum16>";
        }

        private void enum32ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <enum32 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </enum32>";
        }

        private void enum8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <enum8 name=\"Test\" offset=\"0\" visible=\"False\">\r\n  </enum8>";
        }

        public void GetSID()
        {
            StreamReader reader;
            labelEntry = new List<string>();
            try
            {
                reader = new StreamReader(new FileStream(ipath, FileMode.Open, FileAccess.Read));
            }
            catch
            {
                return;
            }
            while (true)
            {
                string item = reader.ReadLine();
                if (item == null)
                {
                    reader.Close();
                    return;
                }
                labelEntry.Add(item);
            }
        }

        private void IdentTXT_TextChanged(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TagEditorContainer));
            tagEditorPanel = new Panel();
            toolStrip1 = new ToolStrip();
            menuButtonSave = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            jtagToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSplitButton1 = new ToolStripSplitButton();
            showInvs = new ToolStripMenuItem();
            showOptionsToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            viewValueAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            jtagXboxToolStripMenuItem = new ToolStripMenuItem();
            xDKToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            poke_changes = new ToolStripButton();
            toolStripSeparator14 = new ToolStripSeparator();
            poke_all = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            panel1 = new Panel();
            panel3 = new Panel();
            panel8 = new Panel();
            Unicode256TXT = new TextBox();
            Unicode64TXT = new TextBox();
            string256TXT = new TextBox();
            string32TXT = new TextBox();
            singleTXT = new TextBox();
            intTXT = new TextBox();
            uintTXT = new TextBox();
            int16TXT = new TextBox();
            uint16TXT = new TextBox();
            byteTXT = new TextBox();
            panel5 = new Panel();
            panel7 = new Panel();
            panel4 = new Panel();
            label14 = new Label();
            label13 = new Label();
            label16 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            panel6 = new Panel();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            label1 = new Label();
            button3 = new Button();
            button2 = new Button();
            bytesTXT = new TextBox();
            offsetTXT = new TextBox();
            panel2 = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            splitContainer1 = new SplitContainer();
            PluginTXT = new RichTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            insertToolStripMenuItem = new ToolStripMenuItem();
            byteToolStripMenuItem = new ToolStripMenuItem();
            enum8ToolStripMenuItem = new ToolStripMenuItem();
            bitmask8ToolStripMenuItem = new ToolStripMenuItem();
            uint16ToolStripMenuItem = new ToolStripMenuItem();
            int16ToolStripMenuItem = new ToolStripMenuItem();
            enum16ToolStripMenuItem = new ToolStripMenuItem();
            bitmask16ToolStripMenuItem = new ToolStripMenuItem();
            uintToolStripMenuItem = new ToolStripMenuItem();
            intToolStripMenuItem = new ToolStripMenuItem();
            enum32ToolStripMenuItem = new ToolStripMenuItem();
            bitmask32ToolStripMenuItem = new ToolStripMenuItem();
            singleToolStripMenuItem = new ToolStripMenuItem();
            tagrefToolStripMenuItem = new ToolStripMenuItem();
            tagblockToolStripMenuItem = new ToolStripMenuItem();
            tagdataToolStripMenuItem = new ToolStripMenuItem();
            optionToolStripMenuItem = new ToolStripMenuItem();
            commentToolStripMenuItem = new ToolStripMenuItem();
            menuStrip4 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem3 = new ToolStripMenuItem();
            saveAndUpdateToolStripMenuItem2 = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            saveAndUpdateToolStripMenuItem = new ToolStripMenuItem();
            richTextBox2 = new RichTextBox();
            menuStrip3 = new MenuStrip();
            tESTToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            tabPage4 = new TabPage();
            webBrowser1 = new WebBrowser();
            panel9 = new Panel();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button5 = new Button();
            button4 = new Button();
            tabPage3 = new TabPage();
            richTextBox1 = new RichTextBox();
            menuStrip2 = new MenuStrip();
            refreshToolStripMenuItem1 = new ToolStripMenuItem();
            saveToolStripMenuItem1 = new ToolStripMenuItem();
            saveToolStripMenuItem2 = new ToolStripMenuItem();
            saveAndUpdateToolStripMenuItem1 = new ToolStripMenuItem();
            showHaloStringIDDatabaseToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel6.SuspendLayout();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            splitContainer1.BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            menuStrip4.SuspendLayout();
            menuStrip1.SuspendLayout();
            menuStrip3.SuspendLayout();
            tabPage4.SuspendLayout();
            panel9.SuspendLayout();
            tabPage3.SuspendLayout();
            menuStrip2.SuspendLayout();
            base.SuspendLayout();
            tagEditorPanel.AutoScroll = true;
            tagEditorPanel.BackColor = SystemColors.Control;
            tagEditorPanel.Dock = DockStyle.Fill;
            tagEditorPanel.Location = new Point(160, 0);
            tagEditorPanel.Name = "tagEditorPanel";
            tagEditorPanel.Size = new Size(0x296, 0x20a);
            tagEditorPanel.TabIndex = 8;
            tagEditorPanel.Scroll += new ScrollEventHandler(tagEditorPanel_Scroll);
            tagEditorPanel.Paint += new PaintEventHandler(tagEditorPanel_Paint);
            toolStrip1.AllowMerge = false;
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { menuButtonSave, toolStripSeparator3, toolStripDropDownButton1, toolStripSplitButton1, toolStripDropDownButton2, toolStripMenuItem2, poke_changes, toolStripSeparator14, poke_all, toolStripSeparator });
            toolStrip1.Location = new Point(160, 0x20a);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(0x296, 0x19);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            toolStrip1.ItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
            menuButtonSave.DisplayStyle = ToolStripItemDisplayStyle.Text;
            menuButtonSave.Image = (Image) manager.GetObject("menuButtonSave.Image");
            menuButtonSave.ImageTransparentColor = Color.Magenta;
            menuButtonSave.Name = "menuButtonSave";
            menuButtonSave.Size = new Size(0x54, 0x16);
            menuButtonSave.Text = "Save Changes";
            menuButtonSave.Click += new EventHandler(menuButtonSave_Click);
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 0x19);
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { jtagToolStripMenuItem, toolStripMenuItem1 });
            toolStripDropDownButton1.Image = (Image) manager.GetObject("toolStripDropDownButton1.Image");
            toolStripDropDownButton1.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            toolStripDropDownButton1.Size = new Size(0x79, 0x16);
            toolStripDropDownButton1.Text = "Poke Changes To...";
            toolStripDropDownButton1.Visible = false;
            jtagToolStripMenuItem.Name = "jtagToolStripMenuItem";
            jtagToolStripMenuItem.Size = new Size(0x7c, 0x16);
            jtagToolStripMenuItem.Text = "Jtag Xbox";
            jtagToolStripMenuItem.Click += new EventHandler(jtagToolStripMenuItem_Click);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(0x7c, 0x16);
            toolStripMenuItem1.Text = "XDK";
            toolStripMenuItem1.Click += new EventHandler(toolStripMenuItem1_Click);
            toolStripSplitButton1.Alignment = ToolStripItemAlignment.Right;
            toolStripSplitButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripSplitButton1.DropDownItems.AddRange(new ToolStripItem[] { showInvs, showOptionsToolStripMenuItem, refreshToolStripMenuItem, viewValueAsToolStripMenuItem });
            toolStripSplitButton1.Image = (Image) manager.GetObject("toolStripSplitButton1.Image");
            toolStripSplitButton1.ImageTransparentColor = Color.Magenta;
            toolStripSplitButton1.Name = "toolStripSplitButton1";
            toolStripSplitButton1.Overflow = ToolStripItemOverflow.Never;
            toolStripSplitButton1.Size = new Size(0x41, 0x16);
            toolStripSplitButton1.Text = "Options";
            toolStripSplitButton1.ButtonClick += new EventHandler(toolStripSplitButton1_ButtonClick);
            showInvs.Name = "showInvs";
            showInvs.Size = new Size(0x9c, 0x16);
            showInvs.Text = "Show Invisibles";
            showInvs.Click += new EventHandler(showInvs_Click);
            showOptionsToolStripMenuItem.Name = "showOptionsToolStripMenuItem";
            showOptionsToolStripMenuItem.Size = new Size(0x9c, 0x16);
            showOptionsToolStripMenuItem.Text = "Show Options";
            showOptionsToolStripMenuItem.Click += new EventHandler(showOptionsToolStripMenuItem_Click);
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Size = new Size(0x9c, 0x16);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += new EventHandler(refreshToolStripMenuItem_Click);
            viewValueAsToolStripMenuItem.Name = "viewValueAsToolStripMenuItem";
            viewValueAsToolStripMenuItem.Size = new Size(0x9c, 0x16);
            viewValueAsToolStripMenuItem.Text = "View Value As...";
            viewValueAsToolStripMenuItem.Click += new EventHandler(viewValueAsToolStripMenuItem_Click);
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { jtagXboxToolStripMenuItem, xDKToolStripMenuItem });
            toolStripDropDownButton2.Image = (Image) manager.GetObject("toolStripDropDownButton2.Image");
            toolStripDropDownButton2.ImageTransparentColor = Color.Magenta;
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            toolStripDropDownButton2.Size = new Size(0x59, 0x16);
            toolStripDropDownButton2.Text = "Poke All To...";
            toolStripDropDownButton2.Visible = false;
            jtagXboxToolStripMenuItem.Name = "jtagXboxToolStripMenuItem";
            jtagXboxToolStripMenuItem.Size = new Size(0x7c, 0x16);
            jtagXboxToolStripMenuItem.Text = "Jtag Xbox";
            jtagXboxToolStripMenuItem.Click += new EventHandler(jtagXboxToolStripMenuItem_Click);
            xDKToolStripMenuItem.Name = "xDKToolStripMenuItem";
            xDKToolStripMenuItem.Size = new Size(0x7c, 0x16);
            xDKToolStripMenuItem.Text = "XDK";
            xDKToolStripMenuItem.Click += new EventHandler(xDKToolStripMenuItem_Click);
            toolStripMenuItem2.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Overflow = ToolStripItemOverflow.AsNeeded;
            toolStripMenuItem2.Size = new Size(0x59, 0x19);
            toolStripMenuItem2.Text = "Scroll To Top";
            toolStripMenuItem2.Click += new EventHandler(toolStripMenuItem2_Click);
            poke_changes.DisplayStyle = ToolStripItemDisplayStyle.Text;
            poke_changes.Image = (Image) manager.GetObject("poke_changes.Image");
            poke_changes.ImageTransparentColor = Color.Magenta;
            poke_changes.Name = "poke_changes";
            poke_changes.Size = new Size(0x56, 0x16);
            poke_changes.Text = "Poke Changes";
            poke_changes.Click += new EventHandler(poke_changes_Click);
            toolStripSeparator14.Name = "toolStripSeparator14";
            toolStripSeparator14.Size = new Size(6, 0x19);
            poke_all.DisplayStyle = ToolStripItemDisplayStyle.Text;
            poke_all.Image = (Image) manager.GetObject("poke_all.Image");
            poke_all.ImageTransparentColor = Color.Magenta;
            poke_all.Name = "poke_all";
            poke_all.Size = new Size(0x36, 0x16);
            poke_all.Text = "Poke All";
            poke_all.Click += new EventHandler(poke_all_Click);
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 0x19);
            panel1.AutoScroll = true;
            panel1.Controls.Add(panel3);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(160, 0x223);
            panel1.TabIndex = 8;
            panel1.Paint += new PaintEventHandler(tagEditorPanel_Paint);
            panel3.BackColor = Color.White;
            panel3.Controls.Add(panel8);
            panel3.Controls.Add(Unicode256TXT);
            panel3.Controls.Add(Unicode64TXT);
            panel3.Controls.Add(string256TXT);
            panel3.Controls.Add(string32TXT);
            panel3.Controls.Add(singleTXT);
            panel3.Controls.Add(intTXT);
            panel3.Controls.Add(uintTXT);
            panel3.Controls.Add(int16TXT);
            panel3.Controls.Add(uint16TXT);
            panel3.Controls.Add(byteTXT);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel7);
            panel3.Controls.Add(panel4);
            panel3.Controls.Add(panel6);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(160, 0x223);
            panel3.TabIndex = 2;
            panel8.BackColor = Color.Gray;
            panel8.Dock = DockStyle.Bottom;
            panel8.Location = new Point(0x42, 0x222);
            panel8.Name = "panel8";
            panel8.Size = new Size(0x5d, 1);
            panel8.TabIndex = 14;
            Unicode256TXT.BackColor = Color.LightGray;
            Unicode256TXT.BorderStyle = BorderStyle.None;
            Unicode256TXT.Dock = DockStyle.Top;
            Unicode256TXT.Location = new Point(0x42, 0xcb);
            Unicode256TXT.Name = "Unicode256TXT";
            Unicode256TXT.ReadOnly = true;
            Unicode256TXT.Size = new Size(0x5d, 13);
            Unicode256TXT.TabIndex = 10;
            Unicode64TXT.BackColor = Color.White;
            Unicode64TXT.BorderStyle = BorderStyle.None;
            Unicode64TXT.Dock = DockStyle.Top;
            Unicode64TXT.Location = new Point(0x42, 190);
            Unicode64TXT.Name = "Unicode64TXT";
            Unicode64TXT.ReadOnly = true;
            Unicode64TXT.Size = new Size(0x5d, 13);
            Unicode64TXT.TabIndex = 9;
            string256TXT.BackColor = Color.LightGray;
            string256TXT.BorderStyle = BorderStyle.None;
            string256TXT.Dock = DockStyle.Top;
            string256TXT.Location = new Point(0x42, 0xb1);
            string256TXT.Name = "string256TXT";
            string256TXT.ReadOnly = true;
            string256TXT.Size = new Size(0x5d, 13);
            string256TXT.TabIndex = 8;
            string32TXT.BackColor = Color.White;
            string32TXT.BorderStyle = BorderStyle.None;
            string32TXT.Dock = DockStyle.Top;
            string32TXT.Location = new Point(0x42, 0xa4);
            string32TXT.Name = "string32TXT";
            string32TXT.ReadOnly = true;
            string32TXT.Size = new Size(0x5d, 13);
            string32TXT.TabIndex = 7;
            singleTXT.BackColor = Color.LightGray;
            singleTXT.BorderStyle = BorderStyle.None;
            singleTXT.Dock = DockStyle.Top;
            singleTXT.Location = new Point(0x42, 0x97);
            singleTXT.Name = "singleTXT";
            singleTXT.ReadOnly = true;
            singleTXT.Size = new Size(0x5d, 13);
            singleTXT.TabIndex = 6;
            intTXT.BackColor = Color.White;
            intTXT.BorderStyle = BorderStyle.None;
            intTXT.Dock = DockStyle.Top;
            intTXT.Location = new Point(0x42, 0x8a);
            intTXT.Name = "intTXT";
            intTXT.ReadOnly = true;
            intTXT.Size = new Size(0x5d, 13);
            intTXT.TabIndex = 5;
            uintTXT.BackColor = Color.LightGray;
            uintTXT.BorderStyle = BorderStyle.None;
            uintTXT.Dock = DockStyle.Top;
            uintTXT.Location = new Point(0x42, 0x7d);
            uintTXT.Name = "uintTXT";
            uintTXT.ReadOnly = true;
            uintTXT.Size = new Size(0x5d, 13);
            uintTXT.TabIndex = 4;
            int16TXT.BackColor = Color.White;
            int16TXT.BorderStyle = BorderStyle.None;
            int16TXT.Dock = DockStyle.Top;
            int16TXT.Location = new Point(0x42, 0x70);
            int16TXT.Name = "int16TXT";
            int16TXT.ReadOnly = true;
            int16TXT.Size = new Size(0x5d, 13);
            int16TXT.TabIndex = 3;
            uint16TXT.BackColor = Color.LightGray;
            uint16TXT.BorderStyle = BorderStyle.None;
            uint16TXT.Dock = DockStyle.Top;
            uint16TXT.Location = new Point(0x42, 0x63);
            uint16TXT.Name = "uint16TXT";
            uint16TXT.ReadOnly = true;
            uint16TXT.Size = new Size(0x5d, 13);
            uint16TXT.TabIndex = 2;
            byteTXT.BackColor = Color.White;
            byteTXT.BorderStyle = BorderStyle.None;
            byteTXT.Dock = DockStyle.Top;
            byteTXT.Location = new Point(0x42, 0x56);
            byteTXT.Name = "byteTXT";
            byteTXT.ReadOnly = true;
            byteTXT.Size = new Size(0x5d, 13);
            byteTXT.TabIndex = 1;
            panel5.BackColor = Color.Gray;
            panel5.Dock = DockStyle.Right;
            panel5.Location = new Point(0x9f, 0x56);
            panel5.Name = "panel5";
            panel5.Size = new Size(1, 0x1cd);
            panel5.TabIndex = 11;
            panel7.BackColor = Color.Gray;
            panel7.Dock = DockStyle.Top;
            panel7.Location = new Point(0x42, 0x55);
            panel7.Name = "panel7";
            panel7.Size = new Size(0x5e, 1);
            panel7.TabIndex = 13;
            panel4.BackColor = Color.White;
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(label14);
            panel4.Controls.Add(label13);
            panel4.Controls.Add(label16);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(label11);
            panel4.Controls.Add(label10);
            panel4.Controls.Add(label9);
            panel4.Controls.Add(label8);
            panel4.Controls.Add(label7);
            panel4.Controls.Add(label6);
            panel4.Dock = DockStyle.Left;
            panel4.Location = new Point(0, 0x55);
            panel4.Name = "panel4";
            panel4.Size = new Size(0x42, 0x1ce);
            panel4.TabIndex = 0;
            panel4.Paint += new PaintEventHandler(panel4_Paint);
            label14.AutoSize = true;
            label14.Location = new Point(3, 0x75);
            label14.Name = "label14";
            label14.Size = new Size(0x3f, 13);
            label14.TabIndex = 0;
            label14.Text = "unicode256";
            label13.AutoSize = true;
            label13.Location = new Point(3, 0x68);
            label13.Name = "label13";
            label13.Size = new Size(0x39, 13);
            label13.TabIndex = 0;
            label13.Text = "unicode64";
            label13.Click += new EventHandler(label13_Click);
            label16.AutoSize = true;
            label16.Location = new Point(3, 0x4e);
            label16.Name = "label16";
            label16.Size = new Size(0x2c, 13);
            label16.TabIndex = 0;
            label16.Text = "string32";
            label12.AutoSize = true;
            label12.Location = new Point(3, 0x5b);
            label12.Name = "label12";
            label12.Size = new Size(50, 13);
            label12.TabIndex = 0;
            label12.Text = "string256";
            label11.AutoSize = true;
            label11.Location = new Point(3, 0x41);
            label11.Name = "label11";
            label11.Size = new Size(0x1b, 13);
            label11.TabIndex = 0;
            label11.Text = "float";
            label10.AutoSize = true;
            label10.Location = new Point(3, 0x34);
            label10.Name = "label10";
            label10.Size = new Size(0x12, 13);
            label10.TabIndex = 0;
            label10.Text = "int";
            label9.AutoSize = true;
            label9.Location = new Point(3, 0x27);
            label9.Name = "label9";
            label9.Size = new Size(0x18, 13);
            label9.TabIndex = 0;
            label9.Text = "uint";
            label8.AutoSize = true;
            label8.Location = new Point(3, 0x1a);
            label8.Name = "label8";
            label8.Size = new Size(30, 13);
            label8.TabIndex = 0;
            label8.Text = "int16";
            label7.AutoSize = true;
            label7.Location = new Point(3, 13);
            label7.Name = "label7";
            label7.Size = new Size(0x24, 13);
            label7.TabIndex = 0;
            label7.Text = "uint16";
            label6.AutoSize = true;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(0x1b, 13);
            label6.TabIndex = 0;
            label6.Text = "byte";
            panel6.Controls.Add(label3);
            panel6.Controls.Add(label2);
            panel6.Controls.Add(button1);
            panel6.Controls.Add(label1);
            panel6.Controls.Add(button3);
            panel6.Controls.Add(button2);
            panel6.Controls.Add(bytesTXT);
            panel6.Controls.Add(offsetTXT);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Name = "panel6";
            panel6.Size = new Size(160, 0x55);
            panel6.TabIndex = 12;
            label3.AutoSize = true;
            label3.Location = new Point(4, 0x34);
            label3.Name = "label3";
            label3.Size = new Size(0x59, 13);
            label3.TabIndex = 6;
            label3.Text = "Pos From This: 0 ";
            label2.AutoSize = true;
            label2.ForeColor = Color.Green;
            label2.Location = new Point(0x68, 0x41);
            label2.Name = "label2";
            label2.Size = new Size(0x38, 13);
            label2.TabIndex = 5;
            label2.Text = "Reading...";
            button1.Location = new Point(0x71, 3);
            button1.Name = "button1";
            button1.Size = new Size(0x29, 0x17);
            button1.TabIndex = 4;
            button1.Text = "<...>";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click_1);
            label1.AutoSize = true;
            label1.Location = new Point(0x4d, 0x21);
            label1.Name = "label1";
            label1.Size = new Size(0x20, 13);
            label1.TabIndex = 3;
            label1.Text = "bytes";
            button3.Location = new Point(0x71, 0x1d);
            button3.Name = "button3";
            button3.Size = new Size(20, 20);
            button3.TabIndex = 2;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            button2.Location = new Point(0x86, 0x1d);
            button2.Name = "button2";
            button2.Size = new Size(20, 20);
            button2.TabIndex = 2;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            bytesTXT.Location = new Point(7, 0x1d);
            bytesTXT.Name = "bytesTXT";
            bytesTXT.Size = new Size(0x40, 20);
            bytesTXT.TabIndex = 0;
            bytesTXT.Text = "2";
            offsetTXT.ForeColor = SystemColors.WindowText;
            offsetTXT.Location = new Point(7, 6);
            offsetTXT.Name = "offsetTXT";
            offsetTXT.Size = new Size(0x66, 20);
            offsetTXT.TabIndex = 0;
            offsetTXT.Text = "0";
            offsetTXT.Click += new EventHandler(offsetTXT_Click);
            offsetTXT.TextChanged += new EventHandler(offsetTXT_TextChanged);
            panel2.Controls.Add(tagEditorPanel);
            panel2.Controls.Add(toolStrip1);
            panel2.Controls.Add(panel1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(0x336, 0x223);
            panel2.TabIndex = 9;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(0x344, 0x243);
            tabControl1.TabIndex = 10;
            tabPage1.Controls.Add(panel2);
            tabPage1.Location = new Point(4, 0x16);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(0x33c, 0x229);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tag Editor";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage2.Controls.Add(splitContainer1);
            tabPage2.Location = new Point(4, 0x16);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(0x33c, 0x229);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Plugin";
            tabPage2.UseVisualStyleBackColor = true;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Panel1.Controls.Add(PluginTXT);
            splitContainer1.Panel1.Controls.Add(menuStrip4);
            splitContainer1.Panel1.Controls.Add(menuStrip1);
            splitContainer1.Panel1.RightToLeft = RightToLeft.No;
            splitContainer1.Panel2.Controls.Add(richTextBox2);
            splitContainer1.Panel2.Controls.Add(menuStrip3);
            splitContainer1.Panel2.RightToLeft = RightToLeft.No;
            splitContainer1.Size = new Size(0x336, 0x223);
            splitContainer1.SplitterDistance = 0x17b;
            splitContainer1.TabIndex = 3;
            PluginTXT.BackColor = Color.White;
            PluginTXT.BorderStyle = BorderStyle.None;
            PluginTXT.ContextMenuStrip = contextMenuStrip1;
            PluginTXT.DetectUrls = false;
            PluginTXT.Dock = DockStyle.Fill;
            PluginTXT.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            PluginTXT.ForeColor = SystemColors.ControlText;
            PluginTXT.Location = new Point(0, 0);
            PluginTXT.Name = "PluginTXT";
            PluginTXT.Size = new Size(820, 0x161);
            PluginTXT.TabIndex = 2;
            PluginTXT.Text = "";
            PluginTXT.SelectionChanged += new EventHandler(PluginTXT_SelectionChanged);
            PluginTXT.TextChanged += new EventHandler(PluginTXT_TextChanged);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { insertToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x68, 0x1a);
            insertToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 
                byteToolStripMenuItem, enum8ToolStripMenuItem, bitmask8ToolStripMenuItem, uint16ToolStripMenuItem, int16ToolStripMenuItem, enum16ToolStripMenuItem, bitmask16ToolStripMenuItem, uintToolStripMenuItem, intToolStripMenuItem, enum32ToolStripMenuItem, bitmask32ToolStripMenuItem, singleToolStripMenuItem, tagrefToolStripMenuItem, tagblockToolStripMenuItem, tagdataToolStripMenuItem, optionToolStripMenuItem,
                commentToolStripMenuItem
            });
            insertToolStripMenuItem.Name = "insertToolStripMenuItem";
            insertToolStripMenuItem.Size = new Size(0x67, 0x16);
            insertToolStripMenuItem.Text = "Insert";
            insertToolStripMenuItem.Click += new EventHandler(insertToolStripMenuItem_Click);
            byteToolStripMenuItem.Name = "byteToolStripMenuItem";
            byteToolStripMenuItem.Size = new Size(0x91, 0x16);
            byteToolStripMenuItem.Text = "byte (1)";
            byteToolStripMenuItem.Click += new EventHandler(byteToolStripMenuItem_Click);
            enum8ToolStripMenuItem.Name = "enum8ToolStripMenuItem";
            enum8ToolStripMenuItem.Size = new Size(0x91, 0x16);
            enum8ToolStripMenuItem.Text = "enum8 (1)";
            enum8ToolStripMenuItem.Click += new EventHandler(enum8ToolStripMenuItem_Click);
            bitmask8ToolStripMenuItem.Name = "bitmask8ToolStripMenuItem";
            bitmask8ToolStripMenuItem.Size = new Size(0x91, 0x16);
            bitmask8ToolStripMenuItem.Text = "bitmask8 (1)";
            bitmask8ToolStripMenuItem.Click += new EventHandler(bitmask8ToolStripMenuItem_Click);
            uint16ToolStripMenuItem.Name = "uint16ToolStripMenuItem";
            uint16ToolStripMenuItem.Size = new Size(0x91, 0x16);
            uint16ToolStripMenuItem.Text = "uint16 (2)";
            uint16ToolStripMenuItem.Click += new EventHandler(uint16ToolStripMenuItem_Click);
            int16ToolStripMenuItem.Name = "int16ToolStripMenuItem";
            int16ToolStripMenuItem.Size = new Size(0x91, 0x16);
            int16ToolStripMenuItem.Text = "int16 (2)";
            int16ToolStripMenuItem.Click += new EventHandler(int16ToolStripMenuItem_Click);
            enum16ToolStripMenuItem.Name = "enum16ToolStripMenuItem";
            enum16ToolStripMenuItem.Size = new Size(0x91, 0x16);
            enum16ToolStripMenuItem.Text = "enum16 (2)";
            enum16ToolStripMenuItem.Click += new EventHandler(enum16ToolStripMenuItem_Click);
            bitmask16ToolStripMenuItem.Name = "bitmask16ToolStripMenuItem";
            bitmask16ToolStripMenuItem.Size = new Size(0x91, 0x16);
            bitmask16ToolStripMenuItem.Text = "bitmask16 (2)";
            bitmask16ToolStripMenuItem.Click += new EventHandler(bitmask16ToolStripMenuItem_Click);
            uintToolStripMenuItem.Name = "uintToolStripMenuItem";
            uintToolStripMenuItem.Size = new Size(0x91, 0x16);
            uintToolStripMenuItem.Text = "uint (4)";
            uintToolStripMenuItem.Click += new EventHandler(uintToolStripMenuItem_Click);
            intToolStripMenuItem.Name = "intToolStripMenuItem";
            intToolStripMenuItem.Size = new Size(0x91, 0x16);
            intToolStripMenuItem.Text = "int (4)";
            intToolStripMenuItem.Click += new EventHandler(intToolStripMenuItem_Click);
            enum32ToolStripMenuItem.Name = "enum32ToolStripMenuItem";
            enum32ToolStripMenuItem.Size = new Size(0x91, 0x16);
            enum32ToolStripMenuItem.Text = "enum32 (4)";
            enum32ToolStripMenuItem.Click += new EventHandler(enum32ToolStripMenuItem_Click);
            bitmask32ToolStripMenuItem.Name = "bitmask32ToolStripMenuItem";
            bitmask32ToolStripMenuItem.Size = new Size(0x91, 0x16);
            bitmask32ToolStripMenuItem.Text = "bitmask32 (4)";
            bitmask32ToolStripMenuItem.Click += new EventHandler(bitmask32ToolStripMenuItem_Click);
            singleToolStripMenuItem.Name = "singleToolStripMenuItem";
            singleToolStripMenuItem.Size = new Size(0x91, 0x16);
            singleToolStripMenuItem.Text = "float (4)";
            singleToolStripMenuItem.Click += new EventHandler(singleToolStripMenuItem_Click);
            tagrefToolStripMenuItem.Name = "tagrefToolStripMenuItem";
            tagrefToolStripMenuItem.Size = new Size(0x91, 0x16);
            tagrefToolStripMenuItem.Text = "tagref (12)";
            tagrefToolStripMenuItem.Click += new EventHandler(tagrefToolStripMenuItem_Click);
            tagblockToolStripMenuItem.Name = "tagblockToolStripMenuItem";
            tagblockToolStripMenuItem.Size = new Size(0x91, 0x16);
            tagblockToolStripMenuItem.Text = "tagblock (12)";
            tagblockToolStripMenuItem.Click += new EventHandler(tagblockToolStripMenuItem_Click);
            tagdataToolStripMenuItem.Name = "tagdataToolStripMenuItem";
            tagdataToolStripMenuItem.Size = new Size(0x91, 0x16);
            tagdataToolStripMenuItem.Text = "tagdata (16)";
            tagdataToolStripMenuItem.Click += new EventHandler(tagdataToolStripMenuItem_Click);
            optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            optionToolStripMenuItem.Size = new Size(0x91, 0x16);
            optionToolStripMenuItem.Text = "option";
            optionToolStripMenuItem.Click += new EventHandler(optionToolStripMenuItem_Click);
            commentToolStripMenuItem.Name = "commentToolStripMenuItem";
            commentToolStripMenuItem.Size = new Size(0x91, 0x16);
            commentToolStripMenuItem.Text = "comment";
            commentToolStripMenuItem.Click += new EventHandler(commentToolStripMenuItem_Click);
            menuStrip4.AllowMerge = false;
            menuStrip4.Dock = DockStyle.Bottom;
            menuStrip4.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip4.Location = new Point(0, 0x161);
            menuStrip4.Name = "menuStrip4";
            menuStrip4.Size = new Size(820, 0x18);
            menuStrip4.TabIndex = 3;
            menuStrip4.Text = "menuStrip4";
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem3, saveAndUpdateToolStripMenuItem2 });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(0x25, 20);
            fileToolStripMenuItem.Text = "File";
            saveToolStripMenuItem3.Name = "saveToolStripMenuItem3";
            saveToolStripMenuItem3.Size = new Size(0xa2, 0x16);
            saveToolStripMenuItem3.Text = "Save";
            saveToolStripMenuItem3.Click += new EventHandler(saveToolStripMenuItem3_Click);
            saveAndUpdateToolStripMenuItem2.Name = "saveAndUpdateToolStripMenuItem2";
            saveAndUpdateToolStripMenuItem2.Size = new Size(0xa2, 0x16);
            saveAndUpdateToolStripMenuItem2.Text = "Save and Update";
            saveAndUpdateToolStripMenuItem2.Click += new EventHandler(saveAndUpdateToolStripMenuItem2_Click);
            menuStrip1.AllowMerge = false;
            menuStrip1.Dock = DockStyle.Bottom;
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 360);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(820, 0x18);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.Visible = false;
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, toolStripSeparator1, saveAndUpdateToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(0x25, 20);
            optionsToolStripMenuItem.Text = "File";
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(0xa2, 0x16);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += new EventHandler(saveToolStripMenuItem_Click);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(0x9f, 6);
            saveAndUpdateToolStripMenuItem.Name = "saveAndUpdateToolStripMenuItem";
            saveAndUpdateToolStripMenuItem.Size = new Size(0xa2, 0x16);
            saveAndUpdateToolStripMenuItem.Text = "Save and Update";
            saveAndUpdateToolStripMenuItem.Click += new EventHandler(saveAndUpdateToolStripMenuItem_Click);
            richTextBox2.BorderStyle = BorderStyle.None;
            richTextBox2.DetectUrls = false;
            richTextBox2.Dock = DockStyle.Fill;
            richTextBox2.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox2.Location = new Point(0, 0);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(820, 0x8a);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            menuStrip3.AllowMerge = false;
            menuStrip3.Dock = DockStyle.Bottom;
            menuStrip3.Items.AddRange(new ToolStripItem[] { tESTToolStripMenuItem });
            menuStrip3.Location = new Point(0, 0x8a);
            menuStrip3.Name = "menuStrip3";
            menuStrip3.Size = new Size(820, 0x18);
            menuStrip3.TabIndex = 1;
            menuStrip3.Text = "menuStrip3";
            tESTToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, closeToolStripMenuItem });
            tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            tESTToolStripMenuItem.Size = new Size(0x25, 20);
            tESTToolStripMenuItem.Text = "File";
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(0x67, 0x16);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += new EventHandler(openToolStripMenuItem_Click);
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(0x67, 0x16);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += new EventHandler(closeToolStripMenuItem_Click);
            tabPage4.Controls.Add(webBrowser1);
            tabPage4.Controls.Add(panel9);
            tabPage4.Location = new Point(4, 0x16);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(0x33c, 0x229);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Wiki Plugin";
            tabPage4.UseVisualStyleBackColor = true;
            webBrowser1.Dock = DockStyle.Fill;
            webBrowser1.Location = new Point(3, 3);
            webBrowser1.MinimumSize = new Size(20, 20);
            webBrowser1.Name = "webBrowser1";
            webBrowser1.Size = new Size(0x336, 0x20d);
            webBrowser1.TabIndex = 1;
            webBrowser1.Url = new Uri("http://noble-research.com/wiki/index.php?title=Plugins", UriKind.Absolute);
            panel9.Controls.Add(button10);
            panel9.Controls.Add(button9);
            panel9.Controls.Add(button8);
            panel9.Controls.Add(button5);
            panel9.Controls.Add(button4);
            panel9.Dock = DockStyle.Bottom;
            panel9.Location = new Point(3, 0x210);
            panel9.Name = "panel9";
            panel9.Size = new Size(0x336, 0x16);
            panel9.TabIndex = 0;
            button10.Location = new Point(0x288, 1);
            button10.Name = "button10";
            button10.Size = new Size(0x55, 20);
            button10.TabIndex = 1;
            button10.Text = "Update Plugin";
            button10.UseVisualStyleBackColor = true;
            button10.Visible = false;
            button10.Click += new EventHandler(button10_Click_1);
            button9.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            button9.Location = new Point(0x2dd, 1);
            button9.Name = "button9";
            button9.Size = new Size(0x35, 20);
            button9.TabIndex = 0;
            button9.Text = "Refresh";
            button9.UseVisualStyleBackColor = true;
            button9.Click += new EventHandler(button9_Click);
            button8.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            button8.Location = new Point(0x312, 1);
            button8.Name = "button8";
            button8.Size = new Size(0x21, 20);
            button8.TabIndex = 0;
            button8.Text = "Edit";
            button8.UseVisualStyleBackColor = true;
            button8.Click += new EventHandler(button7_Click);
            button5.Location = new Point(0x17, 1);
            button5.Name = "button5";
            button5.Size = new Size(20, 20);
            button5.TabIndex = 0;
            button5.Text = ">";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            button4.Location = new Point(3, 1);
            button4.Name = "button4";
            button4.Size = new Size(20, 20);
            button4.TabIndex = 0;
            button4.Text = "<";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            tabPage3.Controls.Add(richTextBox1);
            tabPage3.Controls.Add(menuStrip2);
            tabPage3.Location = new Point(4, 0x16);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(0x33c, 0x229);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "StringID List";
            tabPage3.UseVisualStyleBackColor = true;
            richTextBox1.BackColor = Color.White;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.ContextMenuStrip = contextMenuStrip1;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.ForeColor = SystemColors.ControlText;
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(0x336, 0x20b);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            menuStrip2.AccessibleRole = AccessibleRole.MenuBar;
            menuStrip2.AllowMerge = false;
            menuStrip2.Dock = DockStyle.Bottom;
            menuStrip2.ImeMode = ImeMode.NoControl;
            menuStrip2.Items.AddRange(new ToolStripItem[] { refreshToolStripMenuItem1, saveToolStripMenuItem1, showHaloStringIDDatabaseToolStripMenuItem });
            menuStrip2.Location = new Point(3, 0x20e);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(0x336, 0x18);
            menuStrip2.TabIndex = 4;
            menuStrip2.Text = "menuStrip2";
            refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            refreshToolStripMenuItem1.Size = new Size(0x3a, 20);
            refreshToolStripMenuItem1.Text = "Refresh";
            refreshToolStripMenuItem1.Click += new EventHandler(refreshToolStripMenuItem1_Click);
            saveToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem2, saveAndUpdateToolStripMenuItem1 });
            saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            saveToolStripMenuItem1.Size = new Size(0x25, 20);
            saveToolStripMenuItem1.Text = "File";
            saveToolStripMenuItem2.Name = "saveToolStripMenuItem2";
            saveToolStripMenuItem2.Size = new Size(0xa2, 0x16);
            saveToolStripMenuItem2.Text = "Save";
            saveToolStripMenuItem2.Click += new EventHandler(saveToolStripMenuItem2_Click);
            saveAndUpdateToolStripMenuItem1.Name = "saveAndUpdateToolStripMenuItem1";
            saveAndUpdateToolStripMenuItem1.Size = new Size(0xa2, 0x16);
            saveAndUpdateToolStripMenuItem1.Text = "Save and Update";
            saveAndUpdateToolStripMenuItem1.Click += new EventHandler(saveAndUpdateToolStripMenuItem1_Click);
            showHaloStringIDDatabaseToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            showHaloStringIDDatabaseToolStripMenuItem.Name = "showHaloStringIDDatabaseToolStripMenuItem";
            showHaloStringIDDatabaseToolStripMenuItem.Size = new Size(0xac, 20);
            showHaloStringIDDatabaseToolStripMenuItem.Text = "Show Halo StringID Database";
            showHaloStringIDDatabaseToolStripMenuItem.Click += new EventHandler(showHaloStringIDDatabaseToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(tabControl1);
            base.Name = "TagEditorContainer";
            base.Size = new Size(0x344, 0x243);
            base.Leave += new EventHandler(TagEditorContainer_Leave);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            splitContainer1.EndInit();
            splitContainer1.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            menuStrip4.ResumeLayout(false);
            menuStrip4.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            menuStrip3.ResumeLayout(false);
            menuStrip3.PerformLayout();
            tabPage4.ResumeLayout(false);
            panel9.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            base.ResumeLayout(false);
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void int16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <int16 name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void intToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <int name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void jtagToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void jtagXboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
        }

        public void LO()
        {
            Reading = true;
            while (Reading)
            {
                int offset = Map.Index_Items[TagIndex].Offset;
                label3.Text = "Pos From This: " + ((ValOffset - offset)).ToString();
                int num2 = int.Parse(offsetTXT.Text);
                Map.OpenIO();
                if (offsetTXT.Text != ValOffset.ToString())
                {
                    offsetTXT.Text = ValOffset.ToString();
                    try
                    {
                        Map.IO.In.BaseStream.Position = ValOffset;
                        byte num3 = Map.IO.In.ReadByte();
                        byte num4 = Map.IO.In.ReadByte();
                        byte num5 = Map.IO.In.ReadByte();
                        byte num6 = Map.IO.In.ReadByte();
                        byteTXT.Text = string.Concat(new object[] { num3, ", ", num4, ", ", num5, ", ", num6 }).ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        int num7 = Map.IO.In.ReadUInt16();
                        int num8 = Map.IO.In.ReadUInt16();
                        uint16TXT.Text = (num7 + ", " + num8).ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        int num9 = Map.IO.In.ReadInt16();
                        int num10 = Map.IO.In.ReadInt16();
                        int16TXT.Text = (num9 + ", " + num10).ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        uintTXT.Text = Map.IO.In.ReadInt32().ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        intTXT.Text = Map.IO.In.ReadUInt32().ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        singleTXT.Text = Map.IO.In.ReadSingle().ToString();
                        Map.IO.In.BaseStream.Position = ValOffset;
                        string32TXT.Text = Map.IO.In.ReadAsciiString(0x20);
                        Map.IO.In.BaseStream.Position = ValOffset;
                        string256TXT.Text = Map.IO.In.ReadAsciiString(0x100);
                        Map.IO.In.BaseStream.Position = ValOffset;
                        Unicode64TXT.Text = Map.IO.In.ReadUnicodeString(0x40);
                        Map.IO.In.BaseStream.Position = ValOffset;
                        Unicode256TXT.Text = Map.IO.In.ReadUnicodeString(0x100);
                        label2.Text = "Reading...";
                        label2.ForeColor = Color.Green;
                    }
                    catch
                    {
                    }
                }
                Application.DoEvents();
            }
            label2.Text = "Paused...";
            label2.ForeColor = Color.Red;
        }

        public void LoadTagEditor(int tagIndex)
        {
            DateTime now = DateTime.Now;
            scroll = true;
            string str = DateTime.Now.Subtract(now).ToString() + ", ";
            now = DateTime.Now;
            tagEditorPanel.Visible = false;
            TagIndex = tagIndex;
            tagEditorPanel.AutoScroll = false;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Refresh();
            base.SuspendLayout();
            UnloadMetaEditor();
            if (Map.Map_Header.haloVersion == 12)
            {
                pluginpath = AppSettings.Settings.PluginPath + Map.Index_Items[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
            }
            if (Map.Map_Header.haloVersion == 11)
            {
                pluginpath = AppSettings.Settings.BetaPluginPath + Map.Index_Items[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
            }
            XmlParser parser = new XmlParser();
            parser.ParsePlugin(pluginpath);
            header_size = parser.HeaderSize;
            tclass = parser.TagClass;
            Map.OpenIO();
            TagEditorHandler.LoadPluginUIAndValues(Map, parser.ValueList, tagEditorPanel, Map.Index_Items[TagIndex].Offset, labelEntry);
            Map.CloseIO();
            base.ResumeLayout(true);
            tagEditorPanel.AutoScroll = true;
            try
            {
                StreamReader reader = new StreamReader(pluginpath);
                PluginTXT.Text = reader.ReadToEnd();
                reader.Close();
            }
            catch
            {
            }
            try
            {
                StreamReader reader2 = new StreamReader(ipath);
                richTextBox1.Text = reader2.ReadToEnd();
                reader2.Close();
            }
            catch
            {
            }
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            tagEditorPanel.Visible = true;
            OutputMessenger.OutputMessage("Editor Loaded: " + (str + DateTime.Now.Subtract(now).ToString()), this);
            webBrowser1.Url = new Uri("http://noble-research.com/wiki/index.php?title=" + tclass, UriKind.Absolute);
        }

        private void menuButtonSave_Click(object sender, EventArgs e)
        {
            TagEditorHandler.SaveChangedValues(Map, tagEditorPanel, Map.Index_Items[TagIndex].Offset);
            OutputMessenger.OutputMessage("Saved changes in tag editor.", this);
        }

        private void offsetTXT_Click(object sender, EventArgs e)
        {
            Reading = false;
            label2.Text = "Paused...";
            label2.ForeColor = Color.Red;
        }

        private void offsetTXT_TextChanged(object sender, EventArgs e)
        {
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Ascension Plugin|*.asc|Alteration Plugin|*.alt|Entity Plugin|*.ent"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(dialog.FileName);
                richTextBox2.Text = reader.ReadToEnd();
                reader.Close();
            }
        }

        private void optionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "    <option name=\"Test\" value=\"0\" />";
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void PluginTXT_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void PluginTXT_TextChanged(object sender, EventArgs e)
        {
            PluginTXT.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        private void poke_all_Click(object sender, EventArgs e)
        {
            PokeValues(false);
        }

        private void poke_changes_Click(object sender, EventArgs e)
        {
            PokeValues(true);
        }

        public void PokeValues(bool changedOnly)
        {
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
                    }
                }
                XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                HaloReach3d.IO.EndianIO iO = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                iO.Open();
                TagEditorHandler.PokeValues(iO, tagEditorPanel, Map.Index_Items[TagIndex].Offset, Map.Map_Header.mapMagic, changedOnly);
                iO.Close();
                stream.Close();
                communicator.Disconnect();
                OutputMessenger.OutputMessage("Successfully poked values.", this);
            }
            else
            {
                OutputMessenger.OutputMessage("The XDK Name is not set in the settings. Please click the button on the right to show the settings and indicate your IP/Name for your Xenon Development Kit.", this);
            }
        }

        public void RefreshTagEditor(int tagIndex)
        {
            pos.X = tagEditorPanel.AutoScrollPosition.X;
            pos.Y = tagEditorPanel.AutoScrollPosition.Y * -1;
            DateTime now = DateTime.Now;
            scroll = true;
            string str = DateTime.Now.Subtract(now).ToString() + ", ";
            now = DateTime.Now;
            tagEditorPanel.Visible = false;
            TagIndex = tagIndex;
            tagEditorPanel.AutoScroll = false;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
            Refresh();
            base.SuspendLayout();
            UnloadMetaEditor();
            if (Map.Map_Header.haloVersion == 12)
            {
                pluginpath = AppSettings.Settings.PluginPath + Map.Index_Items[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
            }
            if (Map.Map_Header.haloVersion == 11)
            {
                pluginpath = AppSettings.Settings.BetaPluginPath + Map.Index_Items[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
            }
            XmlParser parser = new XmlParser();
            parser.ParsePlugin(pluginpath);
            header_size = parser.HeaderSize;
            tclass = parser.TagClass;
            Map.OpenIO();
            TagEditorHandler.LoadPluginUIAndValues(Map, parser.ValueList, tagEditorPanel, Map.Index_Items[TagIndex].Offset, labelEntry);
            Map.CloseIO();
            base.ResumeLayout(true);
            tagEditorPanel.AutoScroll = true;
            Thread.CurrentThread.Priority = ThreadPriority.Normal;
            tagEditorPanel.Visible = true;
            OutputMessenger.OutputMessage("Editor Refreshed: " + (str + DateTime.Now.Subtract(now).ToString()), this);
            tagEditorPanel.AutoScrollOffset = pos;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTagEditor(TagIndex);
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(ipath);
            richTextBox1.Text = reader.ReadToEnd();
            reader.Close();
        }

        private void saveAndUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(pluginpath);
            writer.Write(PluginTXT.Text);
            writer.Close();
            RefreshTagEditor(TagIndex);
            tabControl1.SelectedTab = tabPage1;
        }

        private void saveAndUpdateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Point autoScrollPosition = tagEditorPanel.AutoScrollPosition;
            StreamWriter writer = new StreamWriter(ipath);
            writer.WriteLine(richTextBox1.Text);
            writer.Close();
            RefreshTagEditor(TagIndex);
            tabControl1.SelectedTab = tabPage1;
            tagEditorPanel.AutoScrollPosition = autoScrollPosition;
        }

        private void saveAndUpdateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(pluginpath);
            writer.Write(PluginTXT.Text);
            writer.Close();
            RefreshTagEditor(TagIndex);
            tabControl1.SelectedTab = tabPage1;
            tagEditorPanel.AutoScrollPosition = pos;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(pluginpath);
            writer.Write(PluginTXT.Text);
            writer.Close();
        }

        private void saveToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(ipath);
            writer.WriteLine(richTextBox1.Text);
            writer.Close();
        }

        private void saveToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(pluginpath);
            writer.Write(PluginTXT.Text);
            writer.Close();
        }

        private void showHaloStringIDDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Halo_String_ID_Database { TopMost = true }.Show();
        }

        private void showInvs_Click(object sender, EventArgs e)
        {
            if (!TagEditorSettings.Visibility_Settings.Invisibles)
            {
                TagEditorSettings.Visibility_Settings.Invisibles = true;
            }
            else
            {
                TagEditorSettings.Visibility_Settings.Invisibles = false;
            }
            showInvs.Checked = TagEditorSettings.Visibility_Settings.Invisibles;
            RefreshTagEditor(TagIndex);
        }

        private void showOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TagEditorSettingsDialog().ShowDialog();
            LoadTagEditor(TagIndex);
            showInvs.Checked = TagEditorSettings.Visibility_Settings.Invisibles;
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <float name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        public void SwitchToTag(int tagIndex)
        {
            if (Map.Index_Items[tagIndex].Class != Map.Index_Items[TagIndex].Class)
            {
                LoadTagEditor(tagIndex);
            }
            else
            {
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                TagIndex = tagIndex;
                TagEditorHandler.LoadPluginValues(Map, tagEditorPanel, Map.Index_Items[TagIndex].Offset);
                Map.CloseIO();
                Thread.CurrentThread.Priority = ThreadPriority.Normal;
            }
        }

        private void tagblockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <struct name=\"Test\" offset=\"0\" visible=\"False\" size=\"0\">\r\n  </struct>";
        }

        private void tagdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <tagdata name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void TagEditorContainer_Leave(object sender, EventArgs e)
        {
            Reading = false;
        }

        private void tagEditorPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void tagEditorPanel_Scroll(object sender, ScrollEventArgs e)
        {
            if (scroll)
            {
                scroll = false;
            }
            Scrolloffset = tagEditorPanel.AutoScrollOffset;
        }

        private void tagrefToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <tagref name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            PokeValues(true);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            PokeValues(false);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PokeValues(true);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tagEditorPanel.AutoScrollPosition = new Point(0, 0);
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButton1.ShowDropDown();
        }

        private void uint16ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <uint16 name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        private void uintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PluginTXT.SelectedText = "  <uint name=\"Test\" offset=\"0\" visible=\"False\" /> ";
        }

        public void UnloadMetaEditor()
        {
            GC.Collect();
            foreach (Control control in tagEditorPanel.Controls)
            {
                control.Dispose();
            }
            GC.Collect();
            tagEditorPanel.Controls.Clear();
            viewValueAsToolStripMenuItem.CheckState = CheckState.Unchecked;
            panel1.Size = new Size(0, 0x223);
            Reading = false;
        }

        private void viewValueAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (viewValueAsToolStripMenuItem.CheckState == CheckState.Checked)
            {
                TagEditorHandler.VVA(tagEditorPanel, false);
                viewValueAsToolStripMenuItem.CheckState = CheckState.Unchecked;
                panel1.Size = new Size(0, 0x223);
                Reading = false;
            }
            else
            {
                TagEditorHandler.VVA(tagEditorPanel, true);
                viewValueAsToolStripMenuItem.CheckState = CheckState.Checked;
                panel1.Size = new Size(0x9c, 0x223);
                Reading = true;
                LO();
            }
        }

        private void xDKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PokeValues(true);
        }

        public HaloMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public int TagIndex
        {
            get
            {
                return _tagindex;
            }
            set
            {
                _tagindex = value;
            }
        }

        public static int ValOffset
        {
            get
            {
                return _valoffset;
            }
            set
            {
                _valoffset = value;
            }
        }
    }
}

