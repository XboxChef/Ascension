namespace Ascension.Halo_Reach.Game.Misc.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class TagNameDatabase : Form
    {
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyFilenameToolStripMenuItem;
        private TextBox textBox1;
        private TreeView treeView1;

        public TagNameDatabase()
        {
            InitializeComponent();
            classNodes = new Dictionary<string, TreeNode>();
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

        private TreeNode GetClassNode(string tagClass)
        {
            if (!classNodes.ContainsKey(tagClass))
            {
                classNodes.Add(tagClass, new TreeNode(tagClass));
            }
            return classNodes[tagClass];
        }

        private void InitializeComponent()
        {
            components = new Container();
            treeView1 = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            copyFilenameToolStripMenuItem = new ToolStripMenuItem();
            textBox1 = new TextBox();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            treeView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 20);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(0x1a1, 0x26e);
            treeView1.TabIndex = 0;
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyFilenameToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x9a, 0x1a);
            copyFilenameToolStripMenuItem.Name = "copyFilenameToolStripMenuItem";
            copyFilenameToolStripMenuItem.Size = new Size(0x99, 0x16);
            copyFilenameToolStripMenuItem.Text = "Copy Filename";
            copyFilenameToolStripMenuItem.Click += new EventHandler(copyFilenameToolStripMenuItem_Click);
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0x1a1, 20);
            textBox1.TabIndex = 1;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a1, 0x282);
            base.Controls.Add(treeView1);
            base.Controls.Add(textBox1);
            base.Name = "TagNameDatabase";
            Text = "Halo FileName Database";
            base.Load += new EventHandler(TagNameDatabase_Load);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void TagNameDatabase_Load(object sender, EventArgs e)
        {
            StreamReader reader = new StreamReader(Application.StartupPath + @"\halo_filename_database.txt");
            string str = "";
            while ((str = reader.ReadLine()) != null)
            {
                string tagClass = str.Substring(str.Length - 4, 4);
                string text = str.Substring(0, str.Length - 5);
                GetClassNode(tagClass).Nodes.Add(text);
            }
            reader.Close();
            List<TreeNode> list = new List<TreeNode>();
            foreach (KeyValuePair<string, TreeNode> pair in classNodes)
            {
                list.Add(pair.Value);
            }
            list.Sort(new TreenodeCompare());
            foreach (TreeNode node in list)
            {
                treeView1.Nodes.Add(node);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = treeView1.SelectedNode.Text;
        }

        private Dictionary<string, TreeNode> classNodes { get; set; }
    }
}

