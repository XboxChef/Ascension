namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Game.Tag_Editor;
    using Ascension.Halo_Reach.Values;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiValue : UserControl
    {
        private bool _editted;
        private mValue _valuedata;
        private Button button1;
        private Button button2;
        private bool calculating;
        private ComboBox comboBox1;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        public bool floater;
        private int fullOffset;
        private HaloMap HMap;
        private Label lbltype;
        private Label lblValueName;
        private TextBox MD_value;
        private Panel panel1;
        private float restore;
        private TextBox txtValue;
        public bool viewing;

        public uiValue()
        {
            floater = false;
            viewing = false;
            calculating = true;
            components = null;
            InitializeComponent();
        }

        public uiValue(mValue valdata)
        {
            floater = false;
            viewing = false;
            calculating = true;
            components = null;
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            ValueData = valdata;
            lblValueName.Text = ValueData.Name;
            Editted = false;
            if (valdata.Attributes == mValue.ObjectAttributes.Float)
            {
                MD_value.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (floater)
            {
                float num = float.Parse(txtValue.Text);
                txtValue.Text = (num + 0.01f).ToString();
            }
            else
            {
                int num2 = int.Parse(txtValue.Text);
                txtValue.Text = (num2 + 1).ToString();
            }
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                try
                {
                    XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                    communicator.Connect();
                    XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                    communicator.Disconnect();
                    HaloReach3d.IO.EndianIO iO = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                    iO.Open();
                    PokeValue(iO);
                    iO.Close();
                    stream.Close();
                }
                catch
                {
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (floater)
            {
                float num = float.Parse(txtValue.Text);
                txtValue.Text = (num - 0.01f).ToString();
            }
            else
            {
                int num2 = int.Parse(txtValue.Text);
                txtValue.Text = (num2 - 1).ToString();
            }
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                try
                {
                    XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                    communicator.Connect();
                    XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                    communicator.Disconnect();
                    HaloReach3d.IO.EndianIO iO = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                    iO.Open();
                    PokeValue(iO);
                    iO.Close();
                    stream.Close();
                }
                catch
                {
                }
            }
        }

        public void calculate(bool multiplayer)
        {
            try
            {
                if (!_editted)
                {
                    restore = float.Parse(txtValue.Text);
                }
                _editted = true;
                if (calculating)
                {
                    calculating = false;
                    if (multiplayer)
                    {
                        if (MD_value.Text != "0")
                        {
                            float num = restore * float.Parse(MD_value.Text);
                            txtValue.Text = num.ToString();
                        }
                    }
                    else
                    {
                        MD_value.Text = (float.Parse(txtValue.Text) / restore).ToString();
                    }
                }
                calculating = true;
            }
            catch
            {
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewing = true;
            HMap.IO.In.BaseStream.Position = fullOffset;
            if (comboBox1.SelectedIndex == 0)
            {
                txtValue.Text = HMap.IO.In.ReadByte().ToString();
                floater = true;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                txtValue.Text = HMap.IO.In.ReadSingle().ToString();
                floater = false;
            }
            if (comboBox1.SelectedIndex == 2)
            {
                txtValue.Text = HMap.IO.In.ReadInt16().ToString();
                floater = false;
            }
            if (comboBox1.SelectedIndex == 3)
            {
                txtValue.Text = HMap.IO.In.ReadInt32().ToString();
                floater = false;
            }
            if (comboBox1.SelectedIndex == 4)
            {
                txtValue.Text = HMap.IO.In.ReadUInt16().ToString();
                floater = false;
            }
            if (comboBox1.SelectedIndex == 5)
            {
                txtValue.Text = HMap.IO.In.ReadUInt32().ToString();
                floater = false;
            }
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = fullOffset + HMap.Map_Header.mapMagic;
            OutputMessenger.OutputMessage("Name: \"" + ValueData.Name + "\"\nType: \"" + ValueData.Attributes.ToString() + "\"\nPlugin Offset: \"" + ValueData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nMemory Pointer \"0x" + num.ToString("X") + "\"\n", this);
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
            lblValueName = new Label();
            txtValue = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            panel1 = new Panel();
            MD_value = new TextBox();
            button2 = new Button();
            button1 = new Button();
            comboBox1 = new ComboBox();
            lbltype = new Label();
            contextMenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            base.SuspendLayout();
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 5);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0xb9, 20);
            lblValueName.TabIndex = 14;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            lblValueName.MouseLeave += new EventHandler(uiValue_MouseLeave);
            lblValueName.MouseHover += new EventHandler(uiValue_MouseHover);
            txtValue.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtValue.Location = new Point(0xb8, 5);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(90, 20);
            txtValue.TabIndex = 0x10;
            txtValue.TextChanged += new EventHandler(textBox1_TextChanged);
            txtValue.MouseLeave += new EventHandler(uiValue_MouseLeave);
            txtValue.MouseHover += new EventHandler(uiValue_MouseHover);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x1a);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            panel1.Controls.Add(MD_value);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(txtValue);
            panel1.Controls.Add(lblValueName);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(lbltype);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x1d1, 30);
            panel1.TabIndex = 0x11;
            panel1.MouseLeave += new EventHandler(uiValue_MouseLeave);
            panel1.MouseHover += new EventHandler(uiValue_MouseHover);
            MD_value.Location = new Point(0x14f, 5);
            MD_value.Name = "MD_value";
            MD_value.Size = new Size(0x23, 20);
            MD_value.TabIndex = 0x13;
            MD_value.Text = "1";
            MD_value.Visible = false;
            MD_value.TextChanged += new EventHandler(MD_value_TextChanged);
            button2.Location = new Point(0x135, 5);
            button2.Name = "button2";
            button2.Size = new Size(20, 20);
            button2.TabIndex = 0x11;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button1.Location = new Point(0x121, 5);
            button1.Name = "button1";
            button1.Size = new Size(20, 20);
            button1.TabIndex = 0x11;
            button1.Text = "+";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            comboBox1.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Byte", "Float", "Int16", "Int32", "UInt16", "UInt32" });
            comboBox1.Location = new Point(370, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x4f, 20);
            comboBox1.TabIndex = 0x12;
            comboBox1.Text = "type";
            comboBox1.Visible = false;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            lbltype.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbltype.Location = new Point(370, 5);
            lbltype.Name = "lbltype";
            lbltype.Size = new Size(0x4f, 20);
            lbltype.TabIndex = 14;
            lbltype.Text = "type";
            lbltype.TextAlign = ContentAlignment.MiddleLeft;
            lbltype.MouseLeave += new EventHandler(uiValue_MouseLeave);
            lbltype.MouseHover += new EventHandler(uiValue_MouseHover);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(panel1);
            base.Name = "uiValue";
            base.Size = new Size(0x1d1, 30);
            base.MouseLeave += new EventHandler(uiValue_MouseLeave);
            base.MouseHover += new EventHandler(uiValue_MouseHover);
            contextMenuStrip1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            int length;
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Unicode:
                    button1.Visible = false;
                    button2.Visible = false;
                    length = ((mUnicode) ValueData).Length;
                    txtValue.Text = Map.IO.In.ReadUnicodeString(length);
                    txtValue.MaxLength = length;
                    break;

                case mValue.ObjectAttributes.String:
                    button1.Visible = false;
                    button2.Visible = false;
                    length = ((mString) ValueData).Length;
                    txtValue.Text = Map.IO.In.ReadAsciiString(length);
                    txtValue.MaxLength = length;
                    break;

                case mValue.ObjectAttributes.Float:
                    floater = true;
                    txtValue.Text = Map.IO.In.ReadSingle().ToString();
                    break;

                case mValue.ObjectAttributes.Int16:
                    txtValue.Text = Map.IO.In.ReadInt16().ToString();
                    break;

                case mValue.ObjectAttributes.UInt16:
                    txtValue.Text = Map.IO.In.ReadUInt16().ToString();
                    break;

                case mValue.ObjectAttributes.Int32:
                    txtValue.Text = Map.IO.In.ReadInt32().ToString();
                    break;

                case mValue.ObjectAttributes.UInt32:
                    txtValue.Text = Map.IO.In.ReadUInt32().ToString();
                    break;

                case mValue.ObjectAttributes.Byte:
                    txtValue.Text = Map.IO.In.ReadByte().ToString();
                    break;

                case mValue.ObjectAttributes.Undefined:
                    floater = true;
                    txtValue.Text = Map.IO.In.ReadSingle().ToString();
                    break;
            }
            Editted = false;
            lbltype.Text = ValueData.Attributes.ToString().ToLower();
            comboBox1.Text = ValueData.Attributes.ToString().ToLower();
            MD_value.Text = "1";
        }

        private void MD_value_TextChanged(object sender, EventArgs e)
        {
            calculate(true);
        }

        public void PokeValue(HaloReach3d.IO.EndianIO IO)
        {
            IO.Out.BaseStream.Position = fullOffset + HMap.Map_Header.mapMagic;
            if (!viewing)
            {
                switch (ValueData.Attributes)
                {
                    case mValue.ObjectAttributes.Unicode:
                        IO.Out.WriteUnicodeString(txtValue.Text, txtValue.MaxLength);
                        break;

                    case mValue.ObjectAttributes.String:
                        IO.Out.WriteAsciiString(txtValue.Text, txtValue.MaxLength);
                        break;

                    case mValue.ObjectAttributes.Float:
                        IO.Out.Write(float.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.Int16:
                        IO.Out.Write(short.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.UInt16:
                        IO.Out.Write(ushort.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.Int32:
                        IO.Out.Write(int.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.UInt32:
                        IO.Out.Write(uint.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.Byte:
                        IO.Out.Write(byte.Parse(txtValue.Text));
                        break;

                    case mValue.ObjectAttributes.Undefined:
                        IO.Out.Write(float.Parse(txtValue.Text));
                        break;
                }
            }
            if (viewing)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    IO.Out.Write(byte.Parse(txtValue.Text));
                }
                if (comboBox1.SelectedIndex == 1)
                {
                    IO.Out.Write(float.Parse(txtValue.Text));
                }
                if (comboBox1.SelectedIndex == 2)
                {
                    IO.Out.Write(short.Parse(txtValue.Text));
                }
                if (comboBox1.SelectedIndex == 4)
                {
                    IO.Out.Write(ushort.Parse(txtValue.Text));
                }
                if (comboBox1.SelectedIndex == 3)
                {
                    IO.Out.Write(int.Parse(txtValue.Text));
                }
                if (comboBox1.SelectedIndex == 5)
                {
                    IO.Out.Write(uint.Parse(txtValue.Text));
                }
            }
        }

        public void SaveValue(HaloReach3d.IO.EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + ValueData.Offset;
            switch (ValueData.Attributes)
            {
                case mValue.ObjectAttributes.Unicode:
                    IO.Out.WriteUnicodeString(txtValue.Text, txtValue.MaxLength);
                    break;

                case mValue.ObjectAttributes.String:
                    IO.Out.WriteAsciiString(txtValue.Text, txtValue.MaxLength);
                    break;

                case mValue.ObjectAttributes.Float:
                    IO.Out.Write(float.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.Int16:
                    IO.Out.Write(short.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.UInt16:
                    IO.Out.Write(ushort.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.Int32:
                    IO.Out.Write(int.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.UInt32:
                    IO.Out.Write(uint.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.Byte:
                    IO.Out.Write(byte.Parse(txtValue.Text));
                    break;

                case mValue.ObjectAttributes.Undefined:
                    IO.Out.Write(float.Parse(txtValue.Text));
                    break;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Editted = true;
        }

        private void uiValue_MouseHover(object sender, EventArgs e)
        {
            TagEditorContainer.ValOffset = fullOffset;
        }

        private void uiValue_MouseLeave(object sender, EventArgs e)
        {
        }

        public void VVA(bool viewing)
        {
            if (viewing)
            {
                comboBox1.Visible = true;
                lbltype.Visible = false;
            }
            if (!viewing)
            {
                comboBox1.Visible = false;
                lbltype.Visible = true;
            }
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

        public mValue ValueData
        {
            get
            {
                return _valuedata;
            }
            set
            {
                _valuedata = value;
            }
        }
    }
}

