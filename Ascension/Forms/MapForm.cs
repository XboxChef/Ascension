namespace Ascension.Forms
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Game.Misc.Dialogs;
    using Ascension.Halo_Reach.Game.Tag_Editor;
    using Ascension.Halo_Reach.Game.Tag_Grid;
    using Ascension.Halo_Reach.Map_File.Misc.Dialogs;
    using Ascension.Halo_Reach.Meta_Parser;
    using Ascension.Halo_Reach.Plugins;
    using Ascension.Halo_Reach.Raw.Bitmaps;
    using Ascension.Halo_Reach.String___Locale;
    using Ascension.Properties;
    using Ascension.Settings;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class MapForm : Form
    {
        private DateTime _initializedTime;
        private HaloMap _map;
        private ToolStripMenuItem allFilesToolStripMenuItem;
        private ToolStripMenuItem bitmapEditorToolStripMenuItem;
        private Button button1;
        private Button button2;
        private bool ChangingTab = false;
        private ToolStripMenuItem clearOutputToolStripMenuItem;
        private ToolStripMenuItem closeAllButThisToolStripMenuItem;
        private ToolStripMenuItem closeAllToolStripMenuItem;
        private ToolStripMenuItem closeCurrentToolStripMenuItem;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ContextMenuStrip contextMenuStrip3;
        private ContextMenuStrip contextMenuStrip4;
        private ToolStripMenuItem copyFilenameToolStripMenuItem;
        private ToolStripMenuItem copyValueToolStripMenuItem;
        private ToolStripMenuItem createNewTabToolStripMenuItem;
        private ToolStripMenuItem filenamesToolStripMenuItem;
        public string formlocation = "";
        private GroupBox groupMapInfo;
        private GroupBox groupTagInformation;
        private ImageList imageList1;
        private Panel ImagePanel;
        private ToolStripMenuItem labledFilesToolStripMenuItem;
        private ListView lstMapValueInfo;
        private ListView lstTagInfoList;
        private Image mapImage;
        private ToolStripSplitButton menuItemEditor;
        private ToolStripMenuItem menuItemEditorTagEditor;
        private ToolStripMenuItem menuItemEditorTagGrid;
        private bool Painting = true;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pictureBox1;
        private ToolStripMenuItem renameTagToolStripMenuItem;
        public RichTextBox rtbOutputBox;
        private TextBox searchTXT;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private ToolStripMenuItem swapTagHeaderToolStripMenuItem;
        private TabControl tabControl1;
        private TabControl tabControl2;
        private TabControl tabControlRight;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabTagsTree;
        private ToolStripMenuItem tagFilenameDatabaseToolStripMenuItem;
        private TagNameList tagNameList;
        private ToolStrip toolStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private TreeView treeView1;
        private TextBox txtlocation;

        public MapForm(string location)
        {
            InitializeComponent();
            tabControlRight.SelectedTab = tabPage2;
            txtlocation.Text = location;
            formlocation = location;
            Initialized_Time = DateTime.Now;
            Map = new HaloMap(location);
            if (Map.Map_Header.haloVersion == 12)
                Text = "Retail Map - " + Map.Map_Header.scenarioName;
            if (Map.Map_Header.haloVersion == 11)
                Text = "Beta Map - " + Map.Map_Header.scenarioName;
            tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)Map.Map_Header.internalName));
            if (AppSettings.Settings.Map_Folder != "")
                Map.Map_Directory = AppSettings.Settings.Map_Folder;
            LoadMapInformationIntoList();
            OutputMessenger.OutputMessage("Map loaded", (Control)this);
            Map.tagNameList = tagNameList;
            treeView1.Nodes.Clear();
            Map.LoadTagsIntoTreeview(treeView1, false);
            if (Map.Map_Header.haloVersion == 12)
            {
                filenamesToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                copyFilenameToolStripMenuItem.Visible = false;
            }
            if (Map.Map_Header.haloVersion == 11)
            {
                filenamesToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                copyFilenameToolStripMenuItem.Visible = true;
            }
            TreeStyle();
            try
            {
                mapImage = Image.FromFile("map images/" + Map.Map_Header.internalName + ".jpg");
                ImagePanel.BackgroundImage = mapImage;
            }
            catch
            {
            }
            OutputMessenger.OutputMessage("Tags loaded into treeview", (Control)this);
        }

        private void AddItemToList(ListView lst, string name, string value)
        {
            ListViewItem item = new ListViewItem {
                Text = name,
                SubItems = { value }
            };
            lst.Items.Add(item);
        }

        public TabPage AddTab(string text, string name, string id, bool makeActiveTab)
        {
            TabPage page = new TabPage(text) {
                Name = name,
                Tag = id
            };
            tabControl2.TabPages.Add(page);
            if (makeActiveTab)
            {
                tabControl2.SelectedTab = page;
            }
            return page;
        }

        private void allFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labledFilesToolStripMenuItem.CheckState = CheckState.Unchecked;
            allFilesToolStripMenuItem.CheckState = CheckState.Checked;
            treeView1.Nodes.Clear();
            Map.LoadTagsIntoTreeview(treeView1, false);
            TreeStyle();
        }

        private void bitmapEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectEditor(bitmapEditorToolStripMenuItem);
            LoadEditor(GetTagIndexForSelectedTag());
        }

        private void btnOtherEditors_LocalesAndStrings_Click(object sender, EventArgs e)
        {
            TabPage currentSelectedTab = GetCurrentSelectedTab(true);
            currentSelectedTab.Text = "Locale Editor";
            currentSelectedTab.Name = "locale_editor";
            currentSelectedTab.Controls.Clear();
            StringLocaleEditor editor = new StringLocaleEditor(Map) {
                Dock = DockStyle.Fill
            };
            currentSelectedTab.Controls.Add(editor);
            OutputMessenger.OutputMessage("Loaded Locale Editor.", (Control) this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = txtlocation.Text;
            openFileDialog.Filter = "Halo: Reach Map Files|*.map";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                formlocation = openFileDialog.FileName;
                txtlocation.Text = formlocation;
                Map = new HaloMap(formlocation);
                if (Map.Map_Header.haloVersion == 12)
                    Text = "Retail Map - " + Map.Map_Header.scenarioName;
                if (Map.Map_Header.haloVersion == 11)
                    Text = "Beta Map - " + Map.Map_Header.scenarioName;
                treeView1.Nodes.Clear();
                rtbOutputBox.Text = "";
                tabControl2.Controls.Clear();
                tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)Map.Map_Header.internalName));
                if (AppSettings.Settings.Map_Folder != "")
                    Map.Map_Directory = AppSettings.Settings.Map_Folder;
                LoadMapInformationIntoList();
                OutputMessenger.OutputMessage("Map loaded", (Control)this);
                Map.tagNameList = tagNameList;
                treeView1.Nodes.Clear();
                Map.LoadTagsIntoTreeview(treeView1, false);
                if (Map.Map_Header.haloVersion == 12)
                {
                    filenamesToolStripMenuItem.Visible = true;
                    toolStripMenuItem1.Visible = true;
                    copyFilenameToolStripMenuItem.Visible = false;
                }
                if (Map.Map_Header.haloVersion == 11)
                {
                    filenamesToolStripMenuItem.Visible = false;
                    toolStripMenuItem1.Visible = false;
                    copyFilenameToolStripMenuItem.Visible = true;
                }
                TreeStyle();
                OutputMessenger.OutputMessage("Tags loaded into treeview", (Control)this);
            }
            try
            {
                mapImage = Image.FromFile("map images/" + Map.Map_Header.internalName + ".jpg");
                ImagePanel.BackgroundImage = mapImage;
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TabPage currentSelectedTab = GetCurrentSelectedTab(true);
            currentSelectedTab.Text = "Locale & String Editor";
            currentSelectedTab.Name = "locale_strings_editor";
            currentSelectedTab.Controls.Clear();
            StringLocaleEditor editor = new StringLocaleEditor(Map) {
                Dock = DockStyle.Fill
            };
            currentSelectedTab.Controls.Add(editor);
            OutputMessenger.OutputMessage("Loaded Locale & Strings Editor.", (Control) this);
        }

        private void clearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbOutputBox.Text = "";
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab != null)
            {
                foreach (Control control in tabControl2.Controls)
                {
                    if (control != tabControl2.SelectedTab)
                    {
                        tabControl2.Controls.Remove(control);
                    }
                }
                OutputMessenger.OutputMessage("Closed all tabs except \"" + tabControl2.SelectedTab.Text + "\"", (Control) this);
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.Controls.Clear();
            OutputMessenger.OutputMessage("Closed all tabs.", (Control) this);
            TagEditorContainer.Reading = false;
        }

        private void closeCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab != null)
            {
                OutputMessenger.OutputMessage("Removed tab \"" + tabControl2.SelectedTab.Text + "\"", (Control) this);
                tabControl2.Controls.Remove(tabControl2.SelectedTab);
                TagEditorContainer.Reading = false;
            }
        }

        private void copyFilenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(treeView1.SelectedNode.Text);
        }

        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstMapValueInfo.Focused && (lstMapValueInfo.SelectedItems.Count > 0))
            {
                Clipboard.SetText(lstMapValueInfo.SelectedItems[0].SubItems[1].Text);
            }
            if (lstTagInfoList.Focused && (lstTagInfoList.SelectedItems.Count > 0))
            {
                Clipboard.SetText(lstTagInfoList.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void createNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl2.SelectedTab = AddTab("New Tab*", "", "", true);
            OutputMessenger.OutputMessage("Created a new tab.", (Control) this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DoFindInTreeView(TreeNodeCollection tncoll, string strNode)
        {
            foreach (TreeNode node in tncoll)
            {
                if (node.Text.ToLower().Contains(strNode))
                {
                    node.TreeView.SelectedNode = node;
                    return true;
                }
                if (FindInTreeView(node.Nodes, strNode))
                {
                    return true;
                }
            }
            return false;
        }

        private void FindByText()
        {
            TreeNodeCollection nodes = treeView1.Nodes;
            foreach (TreeNode node in nodes)
            {
                FindRecursive(node);
            }
        }

        private bool FindInTreeView(TreeNodeCollection tncoll, string strNode) => 
            DoFindInTreeView(tncoll, strNode.ToLower());

        private void FindRecursive(TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Text.Contains(searchTXT.Text))
                {
                    node.BackColor = Color.Yellow;
                }
                FindRecursive(node);
            }
        }

        private TabPage GetCurrentSelectedTab(bool createIfNotExist)
        {
            if ((tabControl2.SelectedTab == null) && createIfNotExist)
            {
                AddTab("", "", "", true);
            }
            return tabControl2.SelectedTab;
        }

        private int GetTagIndexForSelectedTag()
        {
            if ((treeView1.SelectedNode != null) && (treeView1.SelectedNode.Parent != null))
            {
                int ident = int.Parse(treeView1.SelectedNode.Tag.ToString());
                return Map.GetTagIndexByIdent(ident);
            }
            return -1;
        }

        public void GotoTag(string tagClass, string tagName)
        {
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                if (treeView1.Nodes[i].Tag.ToString() == tagClass)
                {
                    for (int j = 0; j < treeView1.Nodes[i].Nodes.Count; j++)
                    {
                        if (treeView1.Nodes[i].Nodes[j].Text == tagName)
                        {
                            treeView1.CollapseAll();
                            treeView1.SelectedNode = treeView1.Nodes[i].Nodes[j];
                            break;
                        }
                    }
                }
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MapForm));
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            tabControl2 = new TabControl();
            contextMenuStrip2 = new ContextMenuStrip(components);
            createNewTabToolStripMenuItem = new ToolStripMenuItem();
            closeCurrentToolStripMenuItem = new ToolStripMenuItem();
            closeAllButThisToolStripMenuItem = new ToolStripMenuItem();
            closeAllToolStripMenuItem = new ToolStripMenuItem();
            imageList1 = new ImageList(components);
            toolStrip1 = new ToolStrip();
            menuItemEditor = new ToolStripSplitButton();
            menuItemEditorTagGrid = new ToolStripMenuItem();
            menuItemEditorTagEditor = new ToolStripMenuItem();
            bitmapEditorToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            rtbOutputBox = new RichTextBox();
            contextMenuStrip3 = new ContextMenuStrip(components);
            clearOutputToolStripMenuItem = new ToolStripMenuItem();
            tabControlRight = new TabControl();
            tabPage2 = new TabPage();
            ImagePanel = new Panel();
            button2 = new Button();
            groupMapInfo = new GroupBox();
            lstMapValueInfo = new ListView();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            copyValueToolStripMenuItem = new ToolStripMenuItem();
            tabTagsTree = new TabPage();
            treeView1 = new TreeView();
            contextMenuStrip4 = new ContextMenuStrip(components);
            swapTagHeaderToolStripMenuItem = new ToolStripMenuItem();
            filenamesToolStripMenuItem = new ToolStripMenuItem();
            renameTagToolStripMenuItem = new ToolStripMenuItem();
            tagFilenameDatabaseToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripMenuItem();
            allFilesToolStripMenuItem = new ToolStripMenuItem();
            labledFilesToolStripMenuItem = new ToolStripMenuItem();
            copyFilenameToolStripMenuItem = new ToolStripMenuItem();
            panel2 = new Panel();
            groupTagInformation = new GroupBox();
            lstTagInfoList = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            searchTXT = new TextBox();
            panel1 = new Panel();
            button1 = new Button();
            txtlocation = new TextBox();
            splitContainer1.BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            splitContainer2.BeginInit();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            splitContainer3.BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            toolStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            contextMenuStrip3.SuspendLayout();
            tabControlRight.SuspendLayout();
            tabPage2.SuspendLayout();
            groupMapInfo.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            tabTagsTree.SuspendLayout();
            contextMenuStrip4.SuspendLayout();
            panel2.SuspendLayout();
            groupTagInformation.SuspendLayout();
            panel3.SuspendLayout();
            ((ISupportInitialize) pictureBox1).BeginInit();
            panel1.SuspendLayout();
            base.SuspendLayout();
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0x23);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.RightToLeft = RightToLeft.Yes;
            splitContainer1.Panel2.Controls.Add(tabControlRight);
            splitContainer1.Panel2.RightToLeft = RightToLeft.Yes;
            splitContainer1.RightToLeft = RightToLeft.Yes;
            splitContainer1.Size = new Size(0x399, 0x23d);
            splitContainer1.SplitterDistance = 0x28c;
            splitContainer1.TabIndex = 0;
            splitContainer1.SplitterMoved += new SplitterEventHandler(splitContainer1_SplitterMoved);
            splitContainer2.BackColor = SystemColors.Control;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Panel1.RightToLeft = RightToLeft.Yes;
            splitContainer2.Panel1.Paint += new PaintEventHandler(splitContainer2_Panel1_Paint);
            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2.Controls.Add(splitContainer3);
            splitContainer2.Panel2.RightToLeft = RightToLeft.Yes;
            splitContainer2.Size = new Size(0x28c, 0x23d);
            splitContainer2.SplitterDistance = 0x92;
            splitContainer2.TabIndex = 0;
            splitContainer3.BackColor = SystemColors.Control;
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.FixedPanel = FixedPanel.Panel2;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            splitContainer3.Panel1.Controls.Add(tabControl2);
            splitContainer3.Panel1.Controls.Add(toolStrip1);
            splitContainer3.Panel1.RightToLeft = RightToLeft.Yes;
            splitContainer3.Panel1.Paint += new PaintEventHandler(splitContainer3_Panel1_Paint);
            splitContainer3.Panel2.Controls.Add(tabControl1);
            splitContainer3.Panel2.RightToLeft = RightToLeft.Yes;
            splitContainer3.Size = new Size(0x28c, 0x23d);
            splitContainer3.SplitterDistance = 0x197;
            splitContainer3.TabIndex = 0;
            tabControl2.ContextMenuStrip = contextMenuStrip2;
            tabControl2.Dock = DockStyle.Fill;
            tabControl2.ImageList = imageList1;
            tabControl2.ItemSize = new Size(0, 0x13);
            tabControl2.Location = new Point(0, 0x19);
            tabControl2.Name = "tabControl2";
            tabControl2.RightToLeft = RightToLeft.No;
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(0x28c, 0x17e);
            tabControl2.TabIndex = 1;
            tabControl2.SelectedIndexChanged += new EventHandler(tabControl2_SelectedIndexChanged);
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { createNewTabToolStripMenuItem, closeCurrentToolStripMenuItem, closeAllButThisToolStripMenuItem, closeAllToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(0xa7, 0x5c);
            createNewTabToolStripMenuItem.Name = "createNewTabToolStripMenuItem";
            createNewTabToolStripMenuItem.Size = new Size(0xa6, 0x16);
            createNewTabToolStripMenuItem.Text = "Create New Tab";
            createNewTabToolStripMenuItem.Click += new EventHandler(createNewTabToolStripMenuItem_Click);
            closeCurrentToolStripMenuItem.Name = "closeCurrentToolStripMenuItem";
            closeCurrentToolStripMenuItem.Size = new Size(0xa6, 0x16);
            closeCurrentToolStripMenuItem.Text = "Close Current";
            closeCurrentToolStripMenuItem.Click += new EventHandler(closeCurrentToolStripMenuItem_Click);
            closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
            closeAllButThisToolStripMenuItem.Size = new Size(0xa6, 0x16);
            closeAllButThisToolStripMenuItem.Text = "Close All But This";
            closeAllButThisToolStripMenuItem.Click += new EventHandler(closeAllButThisToolStripMenuItem_Click);
            closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            closeAllToolStripMenuItem.Size = new Size(0xa6, 0x16);
            closeAllToolStripMenuItem.Text = "Close All";
            closeAllToolStripMenuItem.Click += new EventHandler(closeAllToolStripMenuItem_Click);
            imageList1.ImageStream = (ImageListStreamer) manager.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "");
            imageList1.Images.SetKeyName(1, "");
            imageList1.Images.SetKeyName(2, "");
            imageList1.Images.SetKeyName(3, "");
            imageList1.Images.SetKeyName(4, "");
            imageList1.Images.SetKeyName(5, "");
            imageList1.Images.SetKeyName(6, "");
            imageList1.Images.SetKeyName(7, "");
            imageList1.Images.SetKeyName(8, "");
            imageList1.Images.SetKeyName(9, "");
            imageList1.Images.SetKeyName(10, "");
            imageList1.Images.SetKeyName(11, "");
            imageList1.Images.SetKeyName(12, "");
            imageList1.Images.SetKeyName(13, "Icon_1.ico");
            imageList1.Images.SetKeyName(14, "filesmall.ico");
            imageList1.Images.SetKeyName(15, "file.png");
            imageList1.Images.SetKeyName(0x10, "mode.png");
            imageList1.Images.SetKeyName(0x11, "bitm.png");
            imageList1.Images.SetKeyName(0x12, "snd.png");
            imageList1.Images.SetKeyName(0x13, "phmo.png");
            imageList1.Images.SetKeyName(20, "coll.png");
            imageList1.Images.SetKeyName(0x15, "chud.png");
            toolStrip1.AllowMerge = false;
            toolStrip1.BackColor = SystemColors.Control;
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.Items.AddRange(new ToolStripItem[] { menuItemEditor });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.RightToLeft = RightToLeft.No;
            toolStrip1.Size = new Size(0x28c, 0x19);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            menuItemEditor.DropDownItems.AddRange(new ToolStripItem[] { menuItemEditorTagGrid, menuItemEditorTagEditor, bitmapEditorToolStripMenuItem });
            menuItemEditor.Image = (Image) manager.GetObject("menuItemEditor.Image");
            menuItemEditor.ImageScaling = ToolStripItemImageScaling.None;
            menuItemEditor.ImageTransparentColor = Color.Magenta;
            menuItemEditor.Name = "menuItemEditor";
            menuItemEditor.Size = new Size(70, 0x16);
            menuItemEditor.Text = "Editor";
            menuItemEditor.ButtonClick += new EventHandler(menuItemEditor_ButtonClick);
            menuItemEditorTagGrid.CheckOnClick = true;
            menuItemEditorTagGrid.Name = "menuItemEditorTagGrid";
            menuItemEditorTagGrid.Size = new Size(0x92, 0x16);
            menuItemEditorTagGrid.Text = "Tag Grid";
            menuItemEditorTagGrid.Click += new EventHandler(menuItemEditorTagGrid_Click);
            menuItemEditorTagEditor.Checked = true;
            menuItemEditorTagEditor.CheckOnClick = true;
            menuItemEditorTagEditor.CheckState = CheckState.Checked;
            menuItemEditorTagEditor.Name = "menuItemEditorTagEditor";
            menuItemEditorTagEditor.Size = new Size(0x92, 0x16);
            menuItemEditorTagEditor.Text = "Tag Editor";
            menuItemEditorTagEditor.Click += new EventHandler(menuItemEditorTagEditor_Click);
            bitmapEditorToolStripMenuItem.Name = "bitmapEditorToolStripMenuItem";
            bitmapEditorToolStripMenuItem.Size = new Size(0x92, 0x16);
            bitmapEditorToolStripMenuItem.Text = "Bitmap Editor";
            bitmapEditorToolStripMenuItem.Click += new EventHandler(bitmapEditorToolStripMenuItem_Click);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.RightToLeft = RightToLeft.No;
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(0x28c, 0xa2);
            tabControl1.TabIndex = 0;
            tabPage1.Controls.Add(rtbOutputBox);
            tabPage1.Location = new Point(4, 0x16);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(0x284, 0x88);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Output";
            tabPage1.UseVisualStyleBackColor = true;
            rtbOutputBox.BackColor = Color.White;
            rtbOutputBox.ContextMenuStrip = contextMenuStrip3;
            rtbOutputBox.Dock = DockStyle.Fill;
            rtbOutputBox.Location = new Point(3, 3);
            rtbOutputBox.Name = "rtbOutputBox";
            rtbOutputBox.ReadOnly = true;
            rtbOutputBox.RightToLeft = RightToLeft.No;
            rtbOutputBox.Size = new Size(0x27e, 130);
            rtbOutputBox.TabIndex = 0;
            rtbOutputBox.Text = "";
            contextMenuStrip3.Items.AddRange(new ToolStripItem[] { clearOutputToolStripMenuItem });
            contextMenuStrip3.Name = "contextMenuStrip3";
            contextMenuStrip3.Size = new Size(0x8f, 0x1a);
            clearOutputToolStripMenuItem.Name = "clearOutputToolStripMenuItem";
            clearOutputToolStripMenuItem.Size = new Size(0x8e, 0x16);
            clearOutputToolStripMenuItem.Text = "Clear Output";
            clearOutputToolStripMenuItem.Click += new EventHandler(clearOutputToolStripMenuItem_Click);
            tabControlRight.Controls.Add(tabPage2);
            tabControlRight.Controls.Add(tabTagsTree);
            tabControlRight.Dock = DockStyle.Fill;
            tabControlRight.ImageList = imageList1;
            tabControlRight.ItemSize = new Size(0x2a, 0x13);
            tabControlRight.Location = new Point(0, 0);
            tabControlRight.Name = "tabControlRight";
            tabControlRight.SelectedIndex = 0;
            tabControlRight.Size = new Size(0x109, 0x23d);
            tabControlRight.TabIndex = 2;
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(ImagePanel);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(groupMapInfo);
            tabPage2.Location = new Point(4, 0x17);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.RightToLeft = RightToLeft.No;
            tabPage2.Size = new Size(0x101, 0x222);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Info";
            ImagePanel.Dock = DockStyle.Top;
            ImagePanel.Location = new Point(3, 3);
            ImagePanel.Name = "ImagePanel";
            ImagePanel.Size = new Size(0xfb, 0x7d);
            ImagePanel.TabIndex = 11;
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            button2.Location = new Point(6, 0x89);
            button2.Name = "button2";
            button2.Size = new Size(0xf2, 0x1a);
            button2.TabIndex = 10;
            button2.Text = "Locale Editor";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            groupMapInfo.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            groupMapInfo.Controls.Add(lstMapValueInfo);
            groupMapInfo.Location = new Point(6, 0xa9);
            groupMapInfo.Name = "groupMapInfo";
            groupMapInfo.RightToLeft = RightToLeft.No;
            groupMapInfo.Size = new Size(0xf5, 0x171);
            groupMapInfo.TabIndex = 9;
            groupMapInfo.TabStop = false;
            groupMapInfo.Text = "Map Information";
            lstMapValueInfo.BackColor = Color.White;
            lstMapValueInfo.Columns.AddRange(new ColumnHeader[] { columnHeader3, columnHeader4 });
            lstMapValueInfo.ContextMenuStrip = contextMenuStrip1;
            lstMapValueInfo.Dock = DockStyle.Fill;
            lstMapValueInfo.ForeColor = Color.Black;
            lstMapValueInfo.FullRowSelect = true;
            lstMapValueInfo.Location = new Point(3, 0x10);
            lstMapValueInfo.Name = "lstMapValueInfo";
            lstMapValueInfo.RightToLeft = RightToLeft.No;
            lstMapValueInfo.Scrollable = false;
            lstMapValueInfo.Size = new Size(0xef, 350);
            lstMapValueInfo.TabIndex = 5;
            lstMapValueInfo.UseCompatibleStateImageBehavior = false;
            lstMapValueInfo.View = View.Details;
            columnHeader3.Text = "Name";
            columnHeader4.Text = "Value";
            columnHeader4.Width = 0x4c;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyValueToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x87, 0x1a);
            copyValueToolStripMenuItem.Name = "copyValueToolStripMenuItem";
            copyValueToolStripMenuItem.Size = new Size(0x86, 0x16);
            copyValueToolStripMenuItem.Text = "Copy Value";
            copyValueToolStripMenuItem.Click += new EventHandler(copyValueToolStripMenuItem_Click);
            tabTagsTree.Controls.Add(treeView1);
            tabTagsTree.Controls.Add(panel2);
            tabTagsTree.Controls.Add(panel3);
            tabTagsTree.ImageKey = "file.png";
            tabTagsTree.Location = new Point(4, 0x17);
            tabTagsTree.Name = "tabTagsTree";
            tabTagsTree.Padding = new Padding(3);
            tabTagsTree.Size = new Size(0x101, 0x222);
            tabTagsTree.TabIndex = 0;
            tabTagsTree.Text = "Tags";
            tabTagsTree.UseVisualStyleBackColor = true;
            treeView1.BackColor = Color.White;
            treeView1.BorderStyle = BorderStyle.None;
            treeView1.ContextMenuStrip = contextMenuStrip4;
            treeView1.Dock = DockStyle.Fill;
            treeView1.ImageIndex = 11;
            treeView1.ImageList = imageList1;
            treeView1.Location = new Point(3, 0x21);
            treeView1.Name = "treeView1";
            treeView1.RightToLeft = RightToLeft.No;
            treeView1.SelectedImageIndex = 0;
            treeView1.Size = new Size(0xfb, 0x125);
            treeView1.TabIndex = 3;
            treeView1.AfterLabelEdit += new NodeLabelEditEventHandler(treeView1_AfterLabelEdit);
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            treeView1.Click += new EventHandler(treeView1_Click);
            contextMenuStrip4.Items.AddRange(new ToolStripItem[] { swapTagHeaderToolStripMenuItem, filenamesToolStripMenuItem, toolStripMenuItem1, copyFilenameToolStripMenuItem });
            contextMenuStrip4.Name = "contextMenuStrip4";
            contextMenuStrip4.Size = new Size(0xa7, 0x5c);
            swapTagHeaderToolStripMenuItem.Name = "swapTagHeaderToolStripMenuItem";
            swapTagHeaderToolStripMenuItem.Size = new Size(0xa6, 0x16);
            swapTagHeaderToolStripMenuItem.Text = "Swap Tag Header";
            swapTagHeaderToolStripMenuItem.Click += new EventHandler(swapTagHeaderToolStripMenuItem_Click);
            filenamesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { renameTagToolStripMenuItem, tagFilenameDatabaseToolStripMenuItem });
            filenamesToolStripMenuItem.Name = "filenamesToolStripMenuItem";
            filenamesToolStripMenuItem.RightToLeft = RightToLeft.No;
            filenamesToolStripMenuItem.Size = new Size(0xa6, 0x16);
            filenamesToolStripMenuItem.Text = "Filenames";
            renameTagToolStripMenuItem.Name = "renameTagToolStripMenuItem";
            renameTagToolStripMenuItem.Size = new Size(0xcb, 0x16);
            renameTagToolStripMenuItem.Text = "Rename Tag";
            renameTagToolStripMenuItem.Click += new EventHandler(renameTagToolStripMenuItem_Click);
            tagFilenameDatabaseToolStripMenuItem.Name = "tagFilenameDatabaseToolStripMenuItem";
            tagFilenameDatabaseToolStripMenuItem.Size = new Size(0xcb, 0x16);
            tagFilenameDatabaseToolStripMenuItem.Text = "Halo FileName Database";
            tagFilenameDatabaseToolStripMenuItem.Click += new EventHandler(tagFilenameDatabaseToolStripMenuItem_Click);
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { allFilesToolStripMenuItem, labledFilesToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(0xa6, 0x16);
            toolStripMenuItem1.Text = "View";
            allFilesToolStripMenuItem.Checked = true;
            allFilesToolStripMenuItem.CheckState = CheckState.Checked;
            allFilesToolStripMenuItem.Name = "allFilesToolStripMenuItem";
            allFilesToolStripMenuItem.Size = new Size(0x8b, 0x16);
            allFilesToolStripMenuItem.Text = "All Files";
            allFilesToolStripMenuItem.Click += new EventHandler(allFilesToolStripMenuItem_Click);
            labledFilesToolStripMenuItem.Name = "labledFilesToolStripMenuItem";
            labledFilesToolStripMenuItem.Size = new Size(0x8b, 0x16);
            labledFilesToolStripMenuItem.Text = "Named Files";
            labledFilesToolStripMenuItem.Click += new EventHandler(labledFilesToolStripMenuItem_Click);
            copyFilenameToolStripMenuItem.Name = "copyFilenameToolStripMenuItem";
            copyFilenameToolStripMenuItem.Size = new Size(0xa6, 0x16);
            copyFilenameToolStripMenuItem.Text = "Copy Filename";
            copyFilenameToolStripMenuItem.Click += new EventHandler(copyFilenameToolStripMenuItem_Click);
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(groupTagInformation);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(3, 0x146);
            panel2.Name = "panel2";
            panel2.Size = new Size(0xfb, 0xd9);
            panel2.TabIndex = 2;
            groupTagInformation.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            groupTagInformation.Controls.Add(lstTagInfoList);
            groupTagInformation.Location = new Point(5, 6);
            groupTagInformation.Name = "groupTagInformation";
            groupTagInformation.RightToLeft = RightToLeft.No;
            groupTagInformation.Size = new Size(0xef, 0xce);
            groupTagInformation.TabIndex = 11;
            groupTagInformation.TabStop = false;
            groupTagInformation.Text = "Tag Information";
            lstTagInfoList.BackColor = Color.White;
            lstTagInfoList.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            lstTagInfoList.ContextMenuStrip = contextMenuStrip1;
            lstTagInfoList.Dock = DockStyle.Fill;
            lstTagInfoList.ForeColor = Color.Black;
            lstTagInfoList.FullRowSelect = true;
            lstTagInfoList.Location = new Point(3, 0x10);
            lstTagInfoList.Name = "lstTagInfoList";
            lstTagInfoList.Scrollable = false;
            lstTagInfoList.Size = new Size(0xe9, 0xbb);
            lstTagInfoList.TabIndex = 5;
            lstTagInfoList.UseCompatibleStateImageBehavior = false;
            lstTagInfoList.View = View.Details;
            columnHeader1.Text = "Name";
            columnHeader2.Text = "Value";
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(searchTXT);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(3, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(0xfb, 30);
            panel3.TabIndex = 4;
            pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Resources.search;
            pictureBox1.Location = new Point(0xe0, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 20);
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            pictureBox1.Click += new EventHandler(searchBTN_Click);
            searchTXT.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            searchTXT.Location = new Point(8, 3);
            searchTXT.Name = "searchTXT";
            searchTXT.RightToLeft = RightToLeft.No;
            searchTXT.Size = new Size(210, 20);
            searchTXT.TabIndex = 1;
            searchTXT.Text = "Search...";
            searchTXT.Click += new EventHandler(searchTXT_Click);
            searchTXT.KeyDown += new KeyEventHandler(searchTXT_KeyDown);
            panel1.BackColor = SystemColors.Control;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtlocation);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x399, 0x23);
            panel1.TabIndex = 4;
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button1.Location = new Point(0x378, 7);
            button1.Name = "button1";
            button1.Size = new Size(0x1a, 20);
            button1.TabIndex = 1;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            txtlocation.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtlocation.BackColor = Color.White;
            txtlocation.Location = new Point(4, 8);
            txtlocation.Name = "txtlocation";
            txtlocation.ReadOnly = true;
            txtlocation.Size = new Size(880, 20);
            txtlocation.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x399, 0x260);
            base.Controls.Add(splitContainer1);
            base.Controls.Add(panel1);
            base.Icon = (Icon) manager.GetObject("$Icon");
            base.Name = "MapForm";
            Text = "Map";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel1.PerformLayout();
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.EndInit();
            splitContainer3.ResumeLayout(false);
            contextMenuStrip2.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            contextMenuStrip3.ResumeLayout(false);
            tabControlRight.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupMapInfo.ResumeLayout(false);
            contextMenuStrip1.ResumeLayout(false);
            tabTagsTree.ResumeLayout(false);
            contextMenuStrip4.ResumeLayout(false);
            panel2.ResumeLayout(false);
            groupTagInformation.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((ISupportInitialize) pictureBox1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void labledFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labledFilesToolStripMenuItem.CheckState = CheckState.Checked;
            allFilesToolStripMenuItem.CheckState = CheckState.Unchecked;
            treeView1.Nodes.Clear();
            Map.LoadTagsIntoTreeview(treeView1, true);
            TreeStyle();
            foreach (TreeNode node in treeView1.Nodes)
            {
            }
        }

        public void LoadEditor(int tagIndex)
        {
            if ((treeView1.SelectedNode != null) && (treeView1.SelectedNode.Parent != null))
            {
                string path = AppSettings.Settings.PluginPath + Map.Index_Items[tagIndex].Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
                if (!File.Exists(path))
                {
                    OutputMessenger.OutputMessage("Plugin not found for class \"" + Map.Index_Items[tagIndex].Class + "\"", (Control) this);
                }
                else
                {
                    TabPage currentSelectedTab = GetCurrentSelectedTab(true);
                    string[] strArray = treeView1.SelectedNode.Text.Split(new char[] { '\\' });
                    string str2 = strArray[strArray.Length - 1];
                    currentSelectedTab.Text = str2 + "." + Map.Index_Items[tagIndex].Class;
                    currentSelectedTab.Tag = tagIndex;
                    try
                    {
                        currentSelectedTab.ImageIndex = treeView1.SelectedNode.ImageIndex;
                    }
                    catch
                    {
                    }
                    Painting = false;
                    if (menuItemEditorTagGrid.Checked)
                    {
                        TagGridContainer container;
                        MetaParser metaParser = new MetaParser(Map, tagIndex);
                        XmlParser xmlParser = new XmlParser();
                        xmlParser.ParsePlugin(path);
                        metaParser.ParseMeta(xmlParser);
                        try
                        {
                            container = (TagGridContainer) currentSelectedTab.Controls[0];
                            container.SwitchToTag(metaParser);
                        }
                        catch
                        {
                            currentSelectedTab.Controls.Clear();
                            container = new TagGridContainer(metaParser) {
                                Dock = DockStyle.Fill
                            };
                            currentSelectedTab.Controls.Add(container);
                            container.Dock = DockStyle.Fill;
                        }
                        OutputMessenger.OutputMessage("Tag Grid loaded for tag: \"" + currentSelectedTab.Text + "\"", (Control) this);
                    }
                    if (menuItemEditorTagEditor.Checked)
                    {
                        try
                        {
                            ((TagEditorContainer) currentSelectedTab.Controls[0]).SwitchToTag(tagIndex);
                        }
                        catch
                        {
                            currentSelectedTab.Controls.Clear();
                            TagEditorContainer container3 = new TagEditorContainer(Map) {
                                Dock = DockStyle.Fill
                            };
                            currentSelectedTab.Controls.Add(container3);
                            container3.LoadTagEditor(tagIndex);
                        }
                        OutputMessenger.OutputMessage("Tag Editor loaded for tag: \"" + currentSelectedTab.Text + "\"", (Control) this);
                    }
                    bool flag = Map.Index_Items[tagIndex].Class == "bitm";
                    bitmapEditorToolStripMenuItem.Visible = true;
                    if (flag && bitmapEditorToolStripMenuItem.Checked)
                    {
                        currentSelectedTab.Controls.Clear();
                        BitmapViewer viewer = new BitmapViewer();
                        currentSelectedTab.Controls.Add(viewer);
                        viewer.LoadBitmapTag(Map, tagIndex);
                        viewer.Dock = DockStyle.Fill;
                        OutputMessenger.OutputMessage("Bitmap Editor loaded for tag: \"" + currentSelectedTab.Text + "\"", (Control) this);
                    }
                    Painting = true;
                }
            }
        }

        private void LoadMapInformationIntoList()
        {
            lstMapValueInfo.Items.Clear();
            AddItemToList(lstMapValueInfo, "Internal Name", Map.Map_Header.internalName);
            AddItemToList(lstMapValueInfo, "Scenario Name", Map.Map_Header.scenarioName);
            AddItemToList(lstMapValueInfo, "Map Type", Map.Map_Header.Map_Size_Type.ToString());
            AddItemToList(lstMapValueInfo, "Map Magic", "0x" + Map.Map_Header.mapMagic.ToString("X"));
            AddItemToList(lstMapValueInfo, "Index Offset", "0x" + Map.Map_Header.indexOffset.ToString("X"));
            AddItemToList(lstMapValueInfo, "Tag Count", "0x" + Map.Map_Header.fileTableCount.ToString("X"));
            AddItemToList(lstMapValueInfo, "Tag Offset", "0x" + Map.Map_Header.fileTableOffset.ToString("X"));
            AddItemToList(lstMapValueInfo, "Tag Size", "0x" + Map.Map_Header.fileTableSize.ToString("X"));
            AddItemToList(lstMapValueInfo, "String Count", "0x" + Map.Map_Header.stringTableCount.ToString("X"));
            AddItemToList(lstMapValueInfo, "String Offset", "0x" + Map.Map_Header.stringTableOffset.ToString("X"));
            AddItemToList(lstMapValueInfo, "String Size", "0x" + Map.Map_Header.stringTableSize.ToString("X"));
            AddItemToList(lstMapValueInfo, "Raw Table Start", "0x" + Map.Map_Header.RawTableOffset.ToString("X"));
            AddItemToList(lstMapValueInfo, "Raw Table Size", "0x" + Map.Map_Header.RawTableSize.ToString("X"));
            lstMapValueInfo.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void LoadTagInformationIntoList(int tagIndex)
        {
            lstTagInfoList.Items.Clear();
            AddItemToList(lstTagInfoList, "Class", Map.Index_Items[tagIndex].Class);
            AddItemToList(lstTagInfoList, "Name", treeView1.SelectedNode.Text);
            AddItemToList(lstTagInfoList, "Offset", "0x" + Map.Index_Items[tagIndex].Offset.ToString("X"));
            AddItemToList(lstTagInfoList, "Identifier", "0x" + Map.Index_Items[tagIndex].Ident.ToString("X"));
            lstTagInfoList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void menuItemEditor_ButtonClick(object sender, EventArgs e)
        {
            menuItemEditor.ShowDropDown();
        }

        private void menuItemEditorTagEditor_Click(object sender, EventArgs e)
        {
            SelectEditor(menuItemEditorTagEditor);
            LoadEditor(GetTagIndexForSelectedTag());
        }

        private void menuItemEditorTagGrid_Click(object sender, EventArgs e)
        {
            SelectEditor(menuItemEditorTagGrid);
            LoadEditor(GetTagIndexForSelectedTag());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void renameTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
        }

        private void searchBTN_Click(object sender, EventArgs e)
        {
            if (searchTXT.Text != "Search...")
            {
                string text = searchTXT.Text;
                if (!FindInTreeView(treeView1.Nodes, text))
                {
                    MessageBox.Show("Tag \"" + searchTXT.Text + "\" not found.");
                }
            }
        }

        private void searchTXT_Click(object sender, EventArgs e)
        {
            if (searchTXT.Text == "Search...")
            {
                searchTXT.Text = "";
            }
        }

        private void searchTXT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchBTN_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        private void SelectEditor(ToolStripMenuItem menuItem)
        {
            foreach (ToolStripMenuItem item in menuItemEditor.DropDownItems)
            {
                item.Checked = false;
            }
            menuItem.Checked = true;
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer1.Panel1.Width < (splitContainer1.Width - 400))
            {
                splitContainer1.SplitterDistance = splitContainer1.Width - 400;
            }
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void splitContainer3_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void swapTagHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tagIndexForSelectedTag = GetTagIndexForSelectedTag();
            if (tagIndexForSelectedTag != -1)
            {
                new SwapTagHeader(Map.Index_Items[tagIndexForSelectedTag], this).ShowDialog();
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int.Parse(tabControl2.SelectedTab.Tag.ToString());
            }
            catch
            {
                return;
            }
            ChangingTab = true;
            HaloMap.TagItem item = Map.Index_Items[int.Parse(tabControl2.SelectedTab.Tag.ToString())];
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                if (treeView1.Nodes[i].Tag.ToString() == item.Class)
                {
                    for (int j = 0; j < treeView1.Nodes[i].Nodes.Count; j++)
                    {
                        if (treeView1.Nodes[i].Nodes[j].Text == item.Name)
                        {
                            treeView1.SelectedNode = treeView1.Nodes[i].Nodes[j];
                            break;
                        }
                    }
                    break;
                }
            }
            ChangingTab = false;
        }

        private void tagFilenameDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TagNameDatabase { TopMost = true }.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void TreeStyle()
        {
            Dictionary<string, string> dictionary;
            string path = Application.StartupPath + @"\ClassLabels.txt";
            if (!File.Exists(path))
            {
                dictionary = null;
            }
            else
            {
                dictionary = new Dictionary<string, string>();
                StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open));
                char[] trimChars = new char[] { '"', ' ' };
                while (true)
                {
                    string str2 = reader.ReadLine();
                    if (str2 == null)
                    {
                        reader.Close();
                        break;
                    }
                    int index = str2.IndexOf(',');
                    string str3 = str2.Substring(0, index).Trim(trimChars);
                    string str4 = str2.Substring(index + 1).Trim(trimChars);
                    dictionary[str3] = str4;
                }
            }
            foreach (TreeNode node in treeView1.Nodes)
            {
                node.ImageIndex = 11;
                node.SelectedImageIndex = 12;
                int num2 = 15;
                if (node.Text == "mode")
                {
                    num2 = 0x10;
                }
                else if (node.Text == "phmo")
                {
                    num2 = 0x13;
                }
                else if (node.Text == "coll")
                {
                    num2 = 20;
                }
                else if (node.Text == "bitm")
                {
                    num2 = 0x11;
                }
                else if (node.Text == "snd!")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "lsnd")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "ssce")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "snmx")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "snde")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "sncl")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "sgp!")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "spk!!")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "srad")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "snmx")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "sfx+")
                {
                    num2 = 0x12;
                }
                else if (node.Text == "chdt")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "chad")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "chgd")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "chdg")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "wpdt")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "wrdt")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "wsdt")
                {
                    num2 = 0x15;
                }
                else if (node.Text == "wadt")
                {
                    num2 = 0x15;
                }
                foreach (TreeNode node2 in node.Nodes)
                {
                    node2.ImageIndex = num2;
                    node2.SelectedImageIndex = num2;
                    if ((dictionary != null) && dictionary.ContainsKey(node.Text))
                    {
                        node.Text = "[" + node.Text + "] - " + dictionary[node.Text];
                    }
                }
            }
        }

        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                int tag = (int) e.Node.Tag;
                string label = e.Label;
                tagNameList.SetPath(tag, label);
                tagNameList.Save();
            }
            catch
            {
                OutputMessenger.OutputMessage("Unable to rename file.", (Control) this);
            }
            treeView1.LabelEdit = false;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            int tagIndexForSelectedTag = GetTagIndexForSelectedTag();
            if (tagIndexForSelectedTag != -1)
            {
                LoadTagInformationIntoList(tagIndexForSelectedTag);
                try
                {
                    tabControl2.SelectedTab.ImageIndex = treeView1.SelectedNode.ImageIndex;
                }
                catch
                {
                }
            }
            if (!ChangingTab)
            {
                LoadEditor(GetTagIndexForSelectedTag());
            }
            treeView1.Focus();
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            searchTXT.Text = "Search...";
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.IsExpanded)
                {
                    node.ImageIndex = 12;
                }
                else
                {
                    node.ImageIndex = 11;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 15)
            {
                if (Painting)
                {
                    base.WndProc(ref m);
                }
                else
                {
                    m.Result = IntPtr.Zero;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public DateTime Initialized_Time
        {
            get
            {
                return _initializedTime;
            }
            set
            {
                _initializedTime = value;
            }
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
    }
}

