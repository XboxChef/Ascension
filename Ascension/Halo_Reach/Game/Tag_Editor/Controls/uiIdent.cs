namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Game.Tag_Editor.Controls.Dialog;
    using Ascension.Halo_Reach.Values;
    using Ascension.Settings;
    using HaloReach3d.Helpers;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class uiIdent : UserControl
    {
        private bool _editted;
        private mTagReference _identdata;
        private Button btnSelectIdent;
        private ComboBox cmbxClass;
        private ComboBox cmbxName;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private int fullOffset;
        public HaloMap HMap;
        private int IDENTIFIER;
        private Label lblValueName;
        private string oldIdentName;
        public string renamedCLASS;
        private ToolStripMenuItem renameToolStripMenuItem;
        private TagNameList tagNameList;
        public Dictionary<string, string> tags;
        private TextBox txtIdent;

        public uiIdent()
        {
            components = null;
            tags = new Dictionary<string, string>();
            oldIdentName = "";
            renamedCLASS = "";
            InitializeComponent();
        }

        public uiIdent(mTagReference identdata)
        {
            components = null;
            tags = new Dictionary<string, string>();
            oldIdentName = "";
            renamedCLASS = "";
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            IdentData = identdata;
            lblValueName.Text = IdentData.Name;
            Editted = false;
            if (AppSettings.Settings.Old_Ident_Swapper)
            {
                renameToolStripMenuItem.Visible = false;
            }
        }

        private void btnSelectIdent_Click(object sender, EventArgs e)
        {
            if (btnSelectIdent.Text == "...")
            {
                string text = txtIdent.Text;
                try
                {
                    string str2 = text.Substring(0, text.Length - 5);
                    uiIdentSwapper swapper = new uiIdentSwapper(this, IDENTIFIER, str2);
                    swapper.ShowDialog();
                }
                catch
                {
                    string str3 = text;
                    new uiIdentSwapper(this, IDENTIFIER, str3).ShowDialog();
                }
            }
            if (btnSelectIdent.Text == "Save")
            {
                string tagPath = "";
                try
                {
                    int iDENTIFIER = IDENTIFIER;
                    if (AppSettings.Settings.Old_Ident_Swapper)
                    {
                        tagPath = cmbxName.Text;
                        OutputMessenger.OutputMessage("Current Ident Editor Cant Save Tags. Please edit in Tree.", this);
                    }
                    else
                    {
                        tagPath = txtIdent.Text;
                        tagNameList.SetPath(iDENTIFIER, tagPath);
                    }
                    tagNameList.Save();
                }
                catch
                {
                    OutputMessenger.OutputMessage("Unable to rename file.", this);
                }
                if (AppSettings.Settings.Old_Ident_Swapper)
                {
                    btnSelectIdent.Visible = false;
                    LoadNames();
                    cmbxName.Text = "";
                    cmbxName.SelectedText = tagPath;
                    cmbxName.BackColor = Color.White;
                }
                else
                {
                    btnSelectIdent.Text = "...";
                    txtIdent.ReadOnly = true;
                    txtIdent.Text = txtIdent.Text + renamedCLASS;
                    txtIdent.BackColor = Color.White;
                }
            }
        }

        private void cmbxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadNames();
        }

        private void cmbxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editted = true;
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = fullOffset + HMap.Map_Header.mapMagic;
            OutputMessenger.OutputMessage("Name: \"" + IdentData.Name + "\"\nType: \"" + IdentData.Attributes.ToString() + "\"\nPlugin Offset: \"" + IdentData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nMemory Pointer \"0x" + num.ToString("X") + "\"\n", this);
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
            txtIdent = new TextBox();
            lblValueName = new Label();
            btnSelectIdent = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            cmbxClass = new ComboBox();
            cmbxName = new ComboBox();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            txtIdent.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtIdent.BackColor = Color.White;
            txtIdent.Location = new Point(0x93, 5);
            txtIdent.Name = "txtIdent";
            txtIdent.ReadOnly = true;
            txtIdent.Size = new Size(0x18e, 20);
            txtIdent.TabIndex = 0x13;
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 0);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0x8a, 30);
            lblValueName.TabIndex = 0x11;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            btnSelectIdent.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnSelectIdent.Location = new Point(0x227, 5);
            btnSelectIdent.Name = "btnSelectIdent";
            btnSelectIdent.Size = new Size(0x30, 20);
            btnSelectIdent.TabIndex = 0x15;
            btnSelectIdent.Text = "...";
            btnSelectIdent.UseVisualStyleBackColor = true;
            btnSelectIdent.Click += new EventHandler(btnSelectIdent_Click);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem, renameToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x30);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Size = new Size(0xb2, 0x16);
            renameToolStripMenuItem.Text = "Rename";
            renameToolStripMenuItem.Click += new EventHandler(renameToolStripMenuItem_Click);
            cmbxClass.FormattingEnabled = true;
            cmbxClass.Location = new Point(0x93, 4);
            cmbxClass.Name = "cmbxClass";
            cmbxClass.Size = new Size(0x3b, 0x15);
            cmbxClass.TabIndex = 0x16;
            cmbxClass.SelectedIndexChanged += new EventHandler(cmbxClass_SelectedIndexChanged);
            cmbxName.FormattingEnabled = true;
            cmbxName.Location = new Point(0xd4, 4);
            cmbxName.Name = "cmbxName";
            cmbxName.Size = new Size(0x14d, 0x15);
            cmbxName.TabIndex = 0x17;
            cmbxName.SelectedIndexChanged += new EventHandler(cmbxName_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(cmbxClass);
            base.Controls.Add(cmbxName);
            base.Controls.Add(btnSelectIdent);
            base.Controls.Add(txtIdent);
            base.Controls.Add(lblValueName);
            base.Name = "uiIdent";
            base.Size = new Size(0x25a, 0x20);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadClasses()
        {
            cmbxClass.Items.Clear();
            for (int i = 0; i < HMap.Tag_Hierarchy.TagClasses.Count; i++)
            {
                cmbxClass.Items.Add(HMap.Tag_Hierarchy.TagClasses[i].TagClass);
            }
            cmbxClass.Sorted = true;
            cmbxClass.Sorted = false;
            cmbxClass.Items.Insert(0, "\x00ff\x00ff\x00ff\x00ff");
        }

        private void LoadNames()
        {
            cmbxName.Items.Clear();
            tags.Clear();
            TagHierarchy.TagHClass class2 = HMap.Tag_Hierarchy.TagClasses.ReturnTagHClass(cmbxClass.Text);
            if (class2 != null)
            {
                for (int i = 0; i < class2.Tags.Count; i++)
                {
                    string tagName = class2.Tags[i].TagName;
                    if (tagNameList.TagPaths.ContainsKey(class2.Tags[i].TagInstance.Ident))
                    {
                        tagName = tagNameList.TagPaths[class2.Tags[i].TagInstance.Ident];
                    }
                    tags[tagName] = class2.Tags[i].TagName;
                    cmbxName.Items.Add(tagName);
                }
                cmbxName.Sorted = true;
                cmbxName.Sorted = false;
            }
            cmbxName.Items.Add("<<null>>");
            cmbxName.SelectedIndex = cmbxName.Items.Count - 1;
        }

        public void LoadValue(HaloMap map, int parentOffset)
        {
            HMap = map;
            if (map.Map_Header.haloVersion == 11)
                btnSelectIdent.Visible = false;
            tagNameList = new TagNameList(string.Format("{0}\\Tag Lists\\{1}.taglist", (object)Application.StartupPath, (object)HMap.Map_Header.internalName));
            HMap.tagNameList = tagNameList;
            HMap.IO.In.BaseStream.Position = (long)(parentOffset + IdentData.Offset);
            fullOffset = (int)HMap.IO.In.BaseStream.Position;
            HMap.IO.In.BaseStream.Position += 12L;
            int ident = HMap.IO.In.ReadInt32();
            if (AppSettings.Settings.Old_Ident_Swapper)
            {
                txtIdent.Visible = false;
                btnSelectIdent.Visible = false;
                int tagIndexByIdent = HMap.GetTagIndexByIdent(ident);
                if (tagIndexByIdent != -1)
                    SelectItem(cmbxClass, map.Index_Items[tagIndexByIdent].Class);
                else
                    cmbxClass.SelectedIndex = 0;
                if (tagIndexByIdent != -1)
                    SelectItem(cmbxName, map.Index_Items[tagIndexByIdent].Name);
                else
                    SelectItem(cmbxName, "<<null>>");
            }
            else
            {
                cmbxClass.Visible = false;
                cmbxName.Visible = false;
            }
            IDENTIFIER = ident;
            SetIdent(IDENTIFIER);
            Editted = false;
            if (!(btnSelectIdent.Text == "Save"))
                return;
            btnSelectIdent.Text = "...";
            txtIdent.ReadOnly = true;
            txtIdent.Text += renamedCLASS;
            txtIdent.BackColor = Color.White;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AppSettings.Settings.Old_Ident_Swapper)
            {
                oldIdentName = cmbxName.Text;
                if (cmbxName.Text != "Null")
                {
                    cmbxName.Text = "";
                    btnSelectIdent.Visible = true;
                    btnSelectIdent.Text = "Save";
                    cmbxName.BackColor = Color.LightGray;
                }
            }
            else if (txtIdent.Text != "Null")
            {
                txtIdent.ReadOnly = false;
                txtIdent.Text = "";
                btnSelectIdent.Text = "Save";
                txtIdent.BackColor = Color.LightGray;
            }
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + IdentData.Offset;
            if (IDENTIFIER != -1)
            {
                int tagIndexByClassAndName = -1;
                if (AppSettings.Settings.Old_Ident_Swapper)
                {
                    if (cmbxName.Text != "<<null>>")
                    {
                        try
                        {
                            tagIndexByClassAndName = HMap.GetTagIndexByClassAndName(cmbxClass.Text, tags[cmbxName.SelectedText]);
                            if (tagIndexByClassAndName != -1)
                            {
                                IO.Out.WriteAsciiString(HMap.Index_Items[tagIndexByClassAndName].Class, 4);
                                Stream stream1 = IO.Out.BaseStream;
                                stream1.Position += 8L;
                                IO.Out.Write(HMap.Index_Items[tagIndexByClassAndName].Ident);
                                return;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    tagIndexByClassAndName = HMap.GetTagIndexByIdent(IDENTIFIER);
                    if (tagIndexByClassAndName != -1)
                    {
                        try
                        {
                            IO.Out.WriteAsciiString(HMap.Index_Items[tagIndexByClassAndName].Class, 4);
                            Stream stream2 = IO.Out.BaseStream;
                            stream2.Position += 8L;
                            IO.Out.Write(IDENTIFIER);
                        }
                        catch
                        {
                        }
                        return;
                    }
                }
            }
            Stream baseStream = IO.Out.BaseStream;
            baseStream.Position += 12L;
            IO.Out.Write(-1);
        }

        private void SelectItem(ComboBox comboBox, string text)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public void SetIdent(int ID)
        {
            IDENTIFIER = ID;
            int tagIndexByIdent = HMap.GetTagIndexByIdent(IDENTIFIER);
            if (tagIndexByIdent != -1)
            {
                string[] strArray = HMap.Index_Items[tagIndexByIdent].Name.Split(new char[] { '\\' });
                string str = strArray[strArray.Length - 1];
                if (HMap.tagNameList.TagPaths.ContainsKey(IDENTIFIER))
                {
                    str = tagNameList.TagPaths[IDENTIFIER];
                }
                cmbxName.SelectedItem = str;
                txtIdent.Text = str + "." + HMap.Index_Items[tagIndexByIdent].Class;
                renamedCLASS = "." + HMap.Index_Items[tagIndexByIdent].Class;
            }
            else
            {
                txtIdent.Text = "Null";
                IDENTIFIER = 0;
            }
            Editted = true;
        }

        public bool Editted
        {
            get
            {
                return _editted;
            }
            set
            {
                _editted = value;
            }
        }

        public mTagReference IdentData
        {
            get
            {
                return _identdata;
            }
            set
            {
                _identdata = value;
            }
        }
    }
}

