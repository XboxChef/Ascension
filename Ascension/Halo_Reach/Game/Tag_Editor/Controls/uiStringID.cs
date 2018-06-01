namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class uiStringID : UserControl
    {
        private bool _editted;
        private mStringID _stringdata;
        private Button btnSelectIdent;
        private Button button1;
        private ComboBox cmbxOptions;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private int fullOffset;
        private GroupBox groupBox1;
        private HaloMap HMap;
        public string ipath;
        private Label label1;
        private List<string> labelEntry;
        private Label lblValueName;
        private TextBox txtStringID;
        private TextBox txtStringIDint;

        public uiStringID()
        {
            ipath = "";
            components = null;
            InitializeComponent();
        }

        public uiStringID(mStringID stringID, List<string> SID)
        {
            ipath = "";
            components = null;
            InitializeComponent();
            String_Data = stringID;
            lblValueName.Text = String_Data.Name;
            labelEntry = SID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringsSave();
        }

        private void cmbxOptions_Click(object sender, EventArgs e)
        {
            if (cmbxOptions.Text == "Add New")
            {
                cmbxOptions.Text = "";
            }
        }

        private void cmbxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxOptions.Text == "Add New")
            {
                button1.Visible = true;
                cmbxOptions.DropDownStyle = ComboBoxStyle.DropDown;
            }
            if (cmbxOptions.Text == "Null")
            {
                txtStringIDint.Text = "0";
            }
            if (cmbxOptions.Text != "Add New")
            {
                button1.Visible = false;
                cmbxOptions.DropDownStyle = ComboBoxStyle.DropDownList;
                string str = cmbxOptions.Text.Split(new char[] { '-' })[0];
                txtStringIDint.Text = str;
                Editted = true;
            }
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void GetSID()
        {
            for (int i = 0; i < cmbxOptions.Items.Count; i++)
            {
                if (cmbxOptions.Items[i].ToString().Split(new char[] { '-' })[0].Replace("\"", "") == txtStringIDint.Text)
                {
                    cmbxOptions.SelectedIndex = i;
                    break;
                }
                cmbxOptions.SelectedIndex = 0;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            components = new Container();
            txtStringIDint = new TextBox();
            lblValueName = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            cmbxOptions = new ComboBox();
            button1 = new Button();
            groupBox1 = new GroupBox();
            btnSelectIdent = new Button();
            txtStringID = new TextBox();
            label1 = new Label();
            contextMenuStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            base.SuspendLayout();
            txtStringIDint.BackColor = SystemColors.Window;
            txtStringIDint.Location = new Point(0x68, 13);
            txtStringIDint.Name = "txtStringIDint";
            txtStringIDint.ReadOnly = true;
            txtStringIDint.Size = new Size(0x59, 20);
            txtStringIDint.TabIndex = 0x16;
            txtStringIDint.TextChanged += new EventHandler(txtStringIDint_TextChanged);
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(1, 14);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(100, 0x13);
            lblValueName.TabIndex = 0x15;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x1a);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            cmbxOptions.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            cmbxOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxOptions.FormattingEnabled = true;
            cmbxOptions.Location = new Point(0xc6, 13);
            cmbxOptions.Name = "cmbxOptions";
            cmbxOptions.Size = new Size(0xcb, 0x15);
            cmbxOptions.TabIndex = 0x17;
            cmbxOptions.SelectedIndexChanged += new EventHandler(cmbxOptions_SelectedIndexChanged);
            cmbxOptions.Click += new EventHandler(cmbxOptions_Click);
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            button1.Location = new Point(0x193, 13);
            button1.Name = "button1";
            button1.Size = new Size(0x27, 0x15);
            button1.TabIndex = 0x18;
            button1.Text = "save";
            button1.UseVisualStyleBackColor = true;
            button1.Visible = false;
            button1.Click += new EventHandler(button1_Click);
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(btnSelectIdent);
            groupBox1.Controls.Add(txtStringID);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(lblValueName);
            groupBox1.Controls.Add(cmbxOptions);
            groupBox1.Controls.Add(txtStringIDint);
            groupBox1.Controls.Add(button1);
            groupBox1.FlatStyle = FlatStyle.System;
            groupBox1.Location = new Point(3, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(0x1bf, 0x2d);
            groupBox1.TabIndex = 0x19;
            groupBox1.TabStop = false;
            groupBox1.Enter += new EventHandler(groupBox1_Enter);
            btnSelectIdent.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            btnSelectIdent.Location = new Point(0x13d, 0x3a);
            btnSelectIdent.Name = "btnSelectIdent";
            btnSelectIdent.Size = new Size(0x36, 20);
            btnSelectIdent.TabIndex = 0x1b;
            btnSelectIdent.Text = "Copy";
            btnSelectIdent.UseVisualStyleBackColor = true;
            txtStringID.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtStringID.BackColor = SystemColors.Window;
            txtStringID.Location = new Point(0xb6, 0x3a);
            txtStringID.Name = "txtStringID";
            txtStringID.ReadOnly = true;
            txtStringID.Size = new Size(0x81, 20);
            txtStringID.TabIndex = 0x1a;
            label1.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(1, 0x35);
            label1.Name = "label1";
            label1.Size = new Size(0xaf, 30);
            label1.TabIndex = 0x19;
            label1.Text = "name";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(groupBox1);
            base.Name = "uiStringID";
            base.Size = new Size(0x1c5, 50);
            contextMenuStrip1.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void LoadSID()
        {
            if (!File.Exists(ipath))
            {
                File.Create(ipath);
            }
            cmbxOptions.Items.Clear();
            cmbxOptions.Items.Add("Add New");
            for (int i = 0; i < labelEntry.Count; i++)
            {
                string[] strArray = labelEntry[i].Split(new char[] { '-' });
                string str = strArray[0].Replace("\"", "");
                string item = "";
                for (int j = 1; j < strArray.Length; j++)
                {
                    item = item + strArray[j].Replace("\"", "");
                    item = str + "-" + item;
                }
                if (item != "")
                {
                    cmbxOptions.Items.Add(item);
                }
            }
        }
        public void LoadValue(HaloMap Map, int parentOffset)
        {
            if (Map.Map_Header.haloVersion == 12)
            {
                Map.IO.In.BaseStream.Position = (long)(parentOffset + String_Data.Offset);
                fullOffset = (int)Map.IO.In.BaseStream.Position;
                HMap = Map;
                txtStringIDint.Text = Map.IO.In.ReadInt32().ToString();
                ipath = string.Format("{0}\\StringID Lists\\{1}.sidlist", (object)Application.StartupPath, (object)Map.Map_Header.internalName);
                LoadSID();
                GetSID();
                Editted = false;
            }
            if (Map.Map_Header.haloVersion != 11)
                return;
            lblValueName.Visible = false;
            txtStringIDint.Visible = false;
            cmbxOptions.Visible = false;
            button1.Visible = false;
            label1.Location = new Point(1, 14);
            txtStringID.Location = new Point(182, 14);
            btnSelectIdent.Location = new Point(317, 14);
            Map.IO.In.BaseStream.Position = (long)(parentOffset + String_Data.Offset);
            fullOffset = (int)Map.IO.In.BaseStream.Position;
            int num = Map.IO.In.ReadInt32();
            int index = num;
            if (num > 1024)
                index = num + 3983;
            txtStringID.Text = Map.String_Table.StringItems[index].Name;
            HMap = Map;
            Editted = false;
        }

        public void PokeValue(EndianIO IO)
        {
            IO.Out.BaseStream.Position = fullOffset + HMap.Map_Header.mapMagic;
            IO.Out.Write(int.Parse(txtStringIDint.Text));
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + String_Data.Offset;
            IO.Out.Write(int.Parse(txtStringIDint.Text));
        }

        public void StringsSave()
        {
            if (File.Exists(ipath))
            {
                StreamReader reader = new StreamReader(ipath);
                string str = reader.ReadToEnd();
                RichTextBox box = new RichTextBox {
                    Text = reader.ReadToEnd()
                };
                List<string> list = new List<string>(box.Lines);
                for (int i = 0; i < list.Count; i++)
                {
                    string str2 = list[i].Split(new char[] { '-' })[0].Replace("\"", "");
                    if (txtStringIDint.Text.Contains(str2))
                    {
                        return;
                    }
                }
                reader.Close();
                string item = "";
                item = "\"" + txtStringIDint.Text + "\"-\"" + cmbxOptions.Text + "\"";
                StreamWriter writer = new StreamWriter(ipath);
                writer.WriteLine(str + item);
                writer.Close();
                labelEntry.Add(item);
                LoadSID();
                GetSID();
            }
        }

        private void txtStringIDint_TextChanged(object sender, EventArgs e)
        {
            GetSID();
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

        public mStringID String_Data
        {
            get
            {
                return _stringdata;
            }
            set
            {
                _stringdata = value;
            }
        }
    }
}

