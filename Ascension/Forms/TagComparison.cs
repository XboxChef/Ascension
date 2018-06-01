namespace Ascension.Forms
{
    using Ascension.Settings;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TagComparison : Form
    {
        private TextBox a1;
        private TextBox a2;
        private TextBox a3;
        private TextBox a4;
        private TextBox b1;
        private TextBox b2;
        private TextBox b3;
        private TextBox b4;
        private Button button1;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem copyFilenameToolStripMenuItem;
        private ImageList imageList1;
        private Label label1;
        private Label label2;
        private Label label3;
        private HaloMap map;
        private HaloMap map2;
        private MenuStrip menuStrip1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem openMapAToolStripMenuItem;
        private ToolStripMenuItem openMapBToolStripMenuItem;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private ToolStripMenuItem renameToolStripMenuItem;
        private TagNameList tagNameList;
        private TagNameList tagNameList2;
        private TextBox textBox1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private TreeView treeView1;
        private TreeView treeView2;

        public TagComparison()
        {
            InitializeComponent();
        }

        private void a1_TextChanged(object sender, EventArgs e)
        {
            compare();
        }

        private void b1_TextChanged(object sender, EventArgs e)
        {
            compare();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                for (int i = 0; i < treeView1.SelectedNode.Nodes.Count; i++)
                {
                    map.OpenIO();
                    int ident = int.Parse(treeView1.SelectedNode.Nodes[i].Tag.ToString());
                    int num3 = int.Parse(textBox1.Text);
                    int tagIndexByIdent = map.GetTagIndexByIdent(ident);
                    int num5 = map.Index_Items[tagIndexByIdent].Offset + num3;
                    map.IO.In.BaseStream.Position = num5;
                    a1.Text = map.IO.In.ReadInt32().ToString();
                    a2.Text = map.IO.In.ReadInt32().ToString();
                    a3.Text = map.IO.In.ReadInt32().ToString();
                    a4.Text = map.IO.In.ReadInt32().ToString();
                    map.CloseIO();
                    for (int j = 0; j < treeView2.SelectedNode.Nodes.Count; j++)
                    {
                        map2.OpenIO();
                        ident = int.Parse(treeView2.SelectedNode.Nodes[j].Tag.ToString());
                        num3 = int.Parse(textBox1.Text);
                        tagIndexByIdent = map2.GetTagIndexByIdent(ident);
                        num5 = map2.Index_Items[tagIndexByIdent].Offset + num3;
                        map2.IO.In.BaseStream.Position = num5;
                        b1.Text = map2.IO.In.ReadInt32().ToString();
                        b2.Text = map2.IO.In.ReadInt32().ToString();
                        b3.Text = map2.IO.In.ReadInt32().ToString();
                        b4.Text = map2.IO.In.ReadInt32().ToString();
                        map2.CloseIO();
                        string text = a1.Text;
                        string str2 = a2.Text;
                        string str3 = a3.Text;
                        string str4 = a4.Text;
                        string str5 = b1.Text;
                        string str6 = b2.Text;
                        string str7 = b3.Text;
                        string str8 = b4.Text;
                        if ((((text == str5) && (str2 == str6)) && (str3 == str7)) && (str4 == str8))
                        {
                            treeView2.LabelEdit = true;
                            string str9 = treeView1.SelectedNode.Nodes[i].Text;
                            treeView2.SelectedNode.Nodes[j].Text = str9;
                            treeView2.SelectedNode.Nodes[j].BeginEdit();
                            treeView2.SelectedNode.Nodes[j].EndEdit(false);
                            int tag = (int) treeView2.SelectedNode.Nodes[j].Tag;
                            string tagPath = treeView2.SelectedNode.Nodes[j].Text;
                            tagNameList2.SetPath(tag, tagPath);
                            tagNameList2.Save();
                            treeView2.LabelEdit = false;
                            break;
                        }
                    }
                }
                if (treeView1.SelectedNode.Nodes.Count == 0)
                {
                    MessageBox.Show("Select a Class");
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            a1.Text = "";
            a2.Text = "";
            a3.Text = "";
            a4.Text = "";
            label2.Text = "";
        }

        private void closeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            treeView2.Nodes.Clear();
            b1.Text = "";
            b2.Text = "";
            b3.Text = "";
            b4.Text = "";
            label3.Text = "";
        }

        public void compare()
        {
            string text = a1.Text;
            string str2 = a2.Text;
            string str3 = a3.Text;
            string str4 = a4.Text;
            string strB = b1.Text;
            string str6 = b2.Text;
            string str7 = b3.Text;
            string str8 = b4.Text;
            text.CompareTo(strB);
            if ((((text == strB) && (str2 == str6)) && (str3 == str7)) && (str4 == str8))
            {
                panel3.BackColor = Color.Green;
            }
            else
            {
                panel3.BackColor = Color.Red;
            }
        }

        private void copyFilenameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(treeView1.SelectedNode.Text);
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
            components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TagComparison));
            panel1 = new Panel();
            treeView2 = new TreeView();
            contextMenuStrip2 = new ContextMenuStrip(components);
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            openMapBToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem1 = new ToolStripMenuItem();
            panel2 = new Panel();
            treeView1 = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            copyFilenameToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            menuStrip2 = new MenuStrip();
            openMapAToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            a1 = new TextBox();
            b1 = new TextBox();
            panel3 = new Panel();
            a2 = new TextBox();
            b2 = new TextBox();
            a3 = new TextBox();
            b3 = new TextBox();
            a4 = new TextBox();
            b4 = new TextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            button1 = new Button();
            imageList1 = new ImageList(components);
            panel1.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel2.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            menuStrip2.SuspendLayout();
            base.SuspendLayout();
            panel1.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
            panel1.Controls.Add(treeView2);
            panel1.Controls.Add(menuStrip1);
            panel1.Location = new Point(0x206, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x119, 0x1cb);
            panel1.TabIndex = 0;
            treeView2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            treeView2.BorderStyle = BorderStyle.FixedSingle;
            treeView2.ContextMenuStrip = contextMenuStrip2;
            treeView2.Location = new Point(3, 0x15);
            treeView2.Name = "treeView2";
            treeView2.RightToLeft = RightToLeft.Yes;
            treeView2.Size = new Size(0x119, 0x1b3);
            treeView2.TabIndex = 0;
            treeView2.AfterLabelEdit += new NodeLabelEditEventHandler(treeView2_AfterLabelEdit);
            treeView2.AfterSelect += new TreeViewEventHandler(treeView2_AfterSelect);
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2 });
            contextMenuStrip2.Name = "contextMenuStrip1";
            contextMenuStrip2.Size = new Size(0x9a, 0x30);
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(0x99, 0x16);
            toolStripMenuItem1.Text = "Copy Filename";
            toolStripMenuItem1.Click += new EventHandler(toolStripMenuItem1_Click);
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(0x99, 0x16);
            toolStripMenuItem2.Text = "Rename";
            toolStripMenuItem2.Click += new EventHandler(toolStripMenuItem2_Click);
            menuStrip1.AllowMerge = false;
            menuStrip1.Items.AddRange(new ToolStripItem[] { openMapBToolStripMenuItem, closeToolStripMenuItem1 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(0x119, 0x18);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            openMapBToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            openMapBToolStripMenuItem.Name = "openMapBToolStripMenuItem";
            openMapBToolStripMenuItem.Size = new Size(0x55, 20);
            openMapBToolStripMenuItem.Text = "Open Map B";
            openMapBToolStripMenuItem.Click += new EventHandler(openMapBToolStripMenuItem_Click);
            closeToolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            closeToolStripMenuItem1.Name = "closeToolStripMenuItem1";
            closeToolStripMenuItem1.Size = new Size(0x30, 20);
            closeToolStripMenuItem1.Text = "Close";
            closeToolStripMenuItem1.Click += new EventHandler(closeToolStripMenuItem1_Click);
            panel2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            panel2.Controls.Add(treeView1);
            panel2.Controls.Add(menuStrip2);
            panel2.Location = new Point(12, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(0x119, 0x1cb);
            panel2.TabIndex = 0;
            treeView1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            treeView1.BorderStyle = BorderStyle.FixedSingle;
            treeView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.Location = new Point(0, 0x18);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(0x119, 0x1b3);
            treeView1.TabIndex = 0;
            treeView1.AfterLabelEdit += new NodeLabelEditEventHandler(treeView1_AfterLabelEdit);
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyFilenameToolStripMenuItem, renameToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x9a, 0x30);
            copyFilenameToolStripMenuItem.Name = "copyFilenameToolStripMenuItem";
            copyFilenameToolStripMenuItem.Size = new Size(0x99, 0x16);
            copyFilenameToolStripMenuItem.Text = "Copy Filename";
            copyFilenameToolStripMenuItem.Click += new EventHandler(copyFilenameToolStripMenuItem_Click);
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(0x99, 0x16);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += new EventHandler(renameToolStripMenuItem_Click);
            menuStrip2.AllowMerge = false;
            menuStrip2.Items.AddRange(new ToolStripItem[] { openMapAToolStripMenuItem, closeToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(0x119, 0x18);
            menuStrip2.TabIndex = 1;
            menuStrip2.Text = "menuStrip2";
            openMapAToolStripMenuItem.Name = "openMapAToolStripMenuItem";
            openMapAToolStripMenuItem.Size = new Size(0x56, 20);
            openMapAToolStripMenuItem.Text = "Open Map A";
            openMapAToolStripMenuItem.Click += new EventHandler(openMapAToolStripMenuItem_Click);
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(0x30, 20);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += new EventHandler(closeToolStripMenuItem_Click);
            a1.Location = new Point(0x12a, 0x52);
            a1.Name = "a1";
            a1.Size = new Size(100, 20);
            a1.TabIndex = 2;
            a1.TextChanged += new EventHandler(a1_TextChanged);
            b1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            b1.Location = new Point(0x19c, 0x52);
            b1.Name = "b1";
            b1.Size = new Size(100, 20);
            b1.TabIndex = 2;
            b1.TextAlign = HorizontalAlignment.Right;
            b1.TextChanged += new EventHandler(b1_TextChanged);
            panel3.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            panel3.Location = new Point(0x12a, 0xba);
            panel3.Name = "panel3";
            panel3.Size = new Size(0xd6, 0xf7);
            panel3.TabIndex = 3;
            a2.Location = new Point(0x12a, 0x6c);
            a2.Name = "a2";
            a2.Size = new Size(100, 20);
            a2.TabIndex = 2;
            a2.TextChanged += new EventHandler(a1_TextChanged);
            b2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            b2.Location = new Point(0x19c, 0x6c);
            b2.Name = "b2";
            b2.Size = new Size(100, 20);
            b2.TabIndex = 2;
            b2.TextAlign = HorizontalAlignment.Right;
            b2.TextChanged += new EventHandler(a1_TextChanged);
            a3.Location = new Point(0x12a, 0x86);
            a3.Name = "a3";
            a3.Size = new Size(100, 20);
            a3.TabIndex = 2;
            a3.TextChanged += new EventHandler(a1_TextChanged);
            b3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            b3.Location = new Point(0x19c, 0x86);
            b3.Name = "b3";
            b3.Size = new Size(100, 20);
            b3.TabIndex = 2;
            b3.TextAlign = HorizontalAlignment.Right;
            b3.TextChanged += new EventHandler(a1_TextChanged);
            a4.Location = new Point(0x12a, 160);
            a4.Name = "a4";
            a4.Size = new Size(100, 20);
            a4.TabIndex = 2;
            a4.TextChanged += new EventHandler(a1_TextChanged);
            b4.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            b4.Location = new Point(0x19c, 160);
            b4.Name = "b4";
            b4.Size = new Size(100, 20);
            b4.TabIndex = 2;
            b4.TextAlign = HorizontalAlignment.Right;
            b4.TextChanged += new EventHandler(a1_TextChanged);
            label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(0x128, 40);
            label1.Name = "label1";
            label1.Size = new Size(0xbd, 13);
            label1.TabIndex = 4;
            label1.Text = "Position from header (can be negative)";
            textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            textBox1.Location = new Point(0x12b, 0x38);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0xd5, 20);
            textBox1.TabIndex = 2;
            textBox1.Text = "0";
            textBox1.TextAlign = HorizontalAlignment.Center;
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            label2.AutoSize = true;
            label2.Location = new Point(12, 0x1da);
            label2.Name = "label2";
            label2.Size = new Size(0, 13);
            label2.TabIndex = 7;
            label3.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            label3.AutoSize = true;
            label3.Location = new Point(0x203, 0x1da);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.No;
            label3.Size = new Size(0, 13);
            label3.TabIndex = 7;
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            button1.Location = new Point(0x12b, 0x1b7);
            button1.Name = "button1";
            button1.Size = new Size(0xd5, 0x17);
            button1.TabIndex = 8;
            button1.Text = "Auto Rename B's Class Tags Using A's";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
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
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x32b, 0x1fb);
            base.Controls.Add(label2);
            base.Controls.Add(label3);
            base.Controls.Add(panel3);
            base.Controls.Add(b3);
            base.Controls.Add(panel2);
            base.Controls.Add(button1);
            base.Controls.Add(label1);
            base.Controls.Add(b4);
            base.Controls.Add(panel1);
            base.Controls.Add(b2);
            base.Controls.Add(b1);
            base.Controls.Add(a1);
            base.Controls.Add(a4);
            base.Controls.Add(a3);
            base.Controls.Add(a2);
            base.Controls.Add(textBox1);
            base.MainMenuStrip = menuStrip1;
            MinimumSize = new Size(0x333, 0x216);
            base.Name = "TagComparison";
            base.ShowIcon = false;
            Text = "Tag Comparer";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void openMapAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Halo: Reach Map Files|*.map";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Map = new HaloMap(openFileDialog.FileName);
            map = Map;
            tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)map.Map_Header.internalName));
            treeView1.Nodes.Clear();
            if (AppSettings.Settings.Map_Folder != "")
                map.Map_Directory = AppSettings.Settings.Map_Folder;
            label2.Text = map.Map_Header.internalName + " Tag Count: " + map.Index_Items.Count.ToString();
            Map.tagNameList = tagNameList;
            treeView1.Nodes.Clear();
            map.LoadTagsIntoTreeview(treeView1, false);
            if (map.Map_Header.haloVersion == 12)
                renameToolStripMenuItem.Visible = true;
            if (map.Map_Header.haloVersion == 11)
                renameToolStripMenuItem.Visible = false;
        }

        private void openMapBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Halo: Reach Map Files|*.map";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            Map2 = new HaloMap(openFileDialog.FileName);
            Map2 = map2;
            tagNameList2 = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)map2.Map_Header.internalName));
            if (AppSettings.Settings.Map_Folder != "")
                map2.Map_Directory = AppSettings.Settings.Map_Folder;
            label3.Text = map2.Map_Header.internalName + " Tag Count: " + map2.Index_Items.Count.ToString();
            Map2.tagNameList = tagNameList2;
            treeView2.Nodes.Clear();
            map2.LoadTagsIntoTreeview(treeView2, false);
            if (map2.Map_Header.haloVersion == 12)
                toolStripMenuItem2.Visible = true;
            if (map2.Map_Header.haloVersion == 11)
                toolStripMenuItem2.Visible = false;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int num;
            int num2;
            int tagIndexByIdent;
            int num4;
            if (treeView2.Nodes.Count != 0)
            {
                try
                {
                    map2.OpenIO();
                    num = int.Parse(treeView2.SelectedNode.Tag.ToString());
                    num2 = int.Parse(textBox1.Text);
                    tagIndexByIdent = map2.GetTagIndexByIdent(num);
                    num4 = map2.Index_Items[tagIndexByIdent].Offset + num2;
                    map2.IO.In.BaseStream.Position = num4;
                    b1.Text = map2.IO.In.ReadInt32().ToString();
                    b2.Text = map2.IO.In.ReadInt32().ToString();
                    b3.Text = map2.IO.In.ReadInt32().ToString();
                    b4.Text = map2.IO.In.ReadInt32().ToString();
                    map2.CloseIO();
                }
                catch
                {
                }
            }
            if (treeView1.Nodes.Count != 0)
            {
                try
                {
                    map.OpenIO();
                    num = int.Parse(treeView1.SelectedNode.Tag.ToString());
                    num2 = int.Parse(textBox1.Text);
                    tagIndexByIdent = map.GetTagIndexByIdent(num);
                    num4 = map.Index_Items[tagIndexByIdent].Offset + num2;
                    map.IO.In.BaseStream.Position = num4;
                    a1.Text = map.IO.In.ReadInt32().ToString();
                    a2.Text = map.IO.In.ReadInt32().ToString();
                    a3.Text = map.IO.In.ReadInt32().ToString();
                    a4.Text = map.IO.In.ReadInt32().ToString();
                    map.CloseIO();
                }
                catch
                {
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(treeView2.SelectedNode.Text);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            treeView2.LabelEdit = true;
            treeView2.SelectedNode.BeginEdit();
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
            }
            treeView1.LabelEdit = false;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((treeView1.SelectedNode != null) && (treeView1.SelectedNode.Parent != null))
            {
                map.OpenIO();
                int ident = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int num2 = int.Parse(textBox1.Text);
                int tagIndexByIdent = map.GetTagIndexByIdent(ident);
                int num4 = map.Index_Items[tagIndexByIdent].Offset + num2;
                map.IO.In.BaseStream.Position = num4;
                a1.Text = map.IO.In.ReadInt32().ToString();
                a2.Text = map.IO.In.ReadInt32().ToString();
                a3.Text = map.IO.In.ReadInt32().ToString();
                a4.Text = map.IO.In.ReadInt32().ToString();
                map.CloseIO();
            }
        }

        private void treeView2_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                int tag = (int) e.Node.Tag;
                string label = e.Label;
                tagNameList2.SetPath(tag, label);
                tagNameList2.Save();
            }
            catch
            {
            }
            treeView2.LabelEdit = false;
        }

        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if ((treeView2.SelectedNode != null) && (treeView2.SelectedNode.Parent != null))
            {
                map2.OpenIO();
                int ident = int.Parse(treeView2.SelectedNode.Tag.ToString());
                int num2 = int.Parse(textBox1.Text);
                int tagIndexByIdent = map2.GetTagIndexByIdent(ident);
                int num4 = map2.Index_Items[tagIndexByIdent].Offset + num2;
                map2.IO.In.BaseStream.Position = num4;
                b1.Text = map2.IO.In.ReadInt32().ToString();
                b2.Text = map2.IO.In.ReadInt32().ToString();
                b3.Text = map2.IO.In.ReadInt32().ToString();
                b4.Text = map2.IO.In.ReadInt32().ToString();
                map2.CloseIO();
            }
        }

        public HaloMap Map
        {
            get
            {
                return map;
            }
            set
            {
                map = value;
            }
        }

        public HaloMap Map2
        {
            get
            {
                return map2;
            }
            set
            {
                map2 = value;
            }
        }
    }
}

