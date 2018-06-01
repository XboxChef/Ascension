namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls.Dialog
{
    using Ascension.Halo_Reach.Game.Tag_Editor.Controls;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiIdentSwapper : Form
    {
        private uiIdent _ident;
        private Button button1;
        private Button button2;
        private IContainer components = null;
        private int IDENTIFIER;
        private TagNameList tagNameList;
        private string texT = "";
        private TreeView treeView1;

        public uiIdentSwapper(uiIdent ident, int ID, string Text)
        {
            texT = Text;
            InitializeComponent();
            IDENTIFIER = ID;
            Ident_UI = ident;
            tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)ident.HMap.Map_Header.internalName));
            ident.HMap.tagNameList = tagNameList;
            ident.HMap.LoadTagsIntoTreeview(treeView1, false);
            int tagIndexByIdent = ident.HMap.GetTagIndexByIdent(IDENTIFIER);
            if (tagIndexByIdent == -1)
                return;
            HaloMap.TagItem indexItem = ident.HMap.Index_Items[tagIndexByIdent];
            for (int index1 = 0; index1 < treeView1.Nodes.Count; ++index1)
            {
                if (treeView1.Nodes[index1].Tag.ToString() == indexItem.Class)
                {
                    for (int index2 = 0; index2 < treeView1.Nodes[index1].Nodes.Count; ++index2)
                    {
                        if (treeView1.Nodes[index1].Nodes[index2].Text.Contains(texT))
                        {
                            treeView1.SelectedNode = treeView1.Nodes[index1].Nodes[index2];
                            break;
                        }
                    }
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ident_UI.SetIdent(-1);
            base.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((treeView1.SelectedNode != null) && (treeView1.SelectedNode.Parent != null))
            {
                Ident_UI.SetIdent(int.Parse(treeView1.SelectedNode.Tag.ToString()));
                base.Close();
            }
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
            treeView1 = new TreeView();
            button1 = new Button();
            button2 = new Button();
            base.SuspendLayout();
            treeView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            treeView1.Location = new Point(12, 12);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(270, 0xe9);
            treeView1.TabIndex = 0;
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Location = new Point(12, 0xfb);
            button1.Name = "button1";
            button1.Size = new Size(0x55, 0x19);
            button1.TabIndex = 1;
            button1.Text = "Null";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            button2.Location = new Point(0xc6, 0xfb);
            button2.Name = "button2";
            button2.Size = new Size(0x55, 0x19);
            button2.TabIndex = 2;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x127, 0x11a);
            base.Controls.Add(button2);
            base.Controls.Add(button1);
            base.Controls.Add(treeView1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "uiIdentSwapper";
            Text = "Browse..";
            base.ResumeLayout(false);
        }

        public uiIdent Ident_UI
        {
            get
            {
                return _ident;
            }
            set
            {
                _ident = value;
            }
        }
    }
}

