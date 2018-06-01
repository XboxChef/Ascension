namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Values;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.Helpers;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class uiTagData : UserControl
    {
        private bool _editted;
        private mTagData _valuedata;
        private Button button1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private int fullOffset;
        private HaloMap HMap;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label lblValueName;
        private Label lblValueType;
        private TextBox txtArrayBox;
        private TextBox txtbyte;
        private TextBox txtfloat;
        private TextBox txtint16;
        private TextBox txtint32;
        private TextBox txtuint16;
        private TextBox txtuint32;

        public uiTagData(mTagData tagData)
        {
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            ValueData = tagData;
            lblValueName.Text = ValueData.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                if (txtArrayBox.Text.Length != txtArrayBox.MaxLength)
                {
                    OutputMessenger.OutputMessage("Data array \"" + ValueData.Name + "\" could not be saved due to an invalid length of bytes.", this);
                }
                try
                {
                    if (AppSettings.Settings.IP_and_XDK_Name == "")
                    {
                        MessageBox.Show("XDK Name/IP not set");
                    }
                    else
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
                        HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                        nio.Open();
                        nio.Out.BaseStream.Position = ValueData.Pointer + HMap.Map_Header.mapMagic;
                        try
                        {
                            nio.Out.Write(ExtraFunctions.HexStringToBytes(txtArrayBox.Text), 0, txtArrayBox.Text.Length / 2);
                        }
                        catch
                        {
                            OutputMessenger.OutputMessage("Some invalid byte characters were intered in data array \"" + ValueData.Name + "\".\n They will not be saved.", this);
                            return;
                        }
                        nio.Close();
                        stream.Close();
                        communicator.Disconnect();
                        OutputMessenger.OutputMessage("Poked Chunk Values", this);
                    }
                }
                catch
                {
                    OutputMessenger.OutputMessage("Couldn't Poke Chunks", this);
                }
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
            lblValueType = new Label();
            lblValueName = new Label();
            txtArrayBox = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            txtbyte = new TextBox();
            txtint16 = new TextBox();
            txtuint16 = new TextBox();
            txtint32 = new TextBox();
            txtuint32 = new TextBox();
            txtfloat = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            button1 = new Button();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            lblValueType.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueType.Location = new Point(0xdb, 0);
            lblValueType.Name = "lblValueType";
            lblValueType.Size = new Size(70, 30);
            lblValueType.TabIndex = 0x11;
            lblValueType.Text = "type";
            lblValueType.TextAlign = ContentAlignment.MiddleRight;
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(13, 0);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(200, 30);
            lblValueName.TabIndex = 0x10;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            txtArrayBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            txtArrayBox.Location = new Point(0x10, 0x1d);
            txtArrayBox.Multiline = true;
            txtArrayBox.Name = "txtArrayBox";
            txtArrayBox.Size = new Size(0x117, 150);
            txtArrayBox.TabIndex = 20;
            txtArrayBox.TextChanged += new EventHandler(txtArrayBox_TextChanged);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x1a);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            txtbyte.Location = new Point(0x12d, 0x1d);
            txtbyte.Name = "txtbyte";
            txtbyte.Size = new Size(0x43, 20);
            txtbyte.TabIndex = 0x15;
            txtint16.Location = new Point(0x12d, 0x37);
            txtint16.Name = "txtint16";
            txtint16.Size = new Size(0x43, 20);
            txtint16.TabIndex = 0x15;
            txtuint16.Location = new Point(0x12d, 0x51);
            txtuint16.Name = "txtuint16";
            txtuint16.Size = new Size(0x43, 20);
            txtuint16.TabIndex = 0x15;
            txtint32.Location = new Point(0x12d, 0x6b);
            txtint32.Name = "txtint32";
            txtint32.Size = new Size(0x43, 20);
            txtint32.TabIndex = 0x15;
            txtuint32.Location = new Point(0x12d, 0x85);
            txtuint32.Name = "txtuint32";
            txtuint32.Size = new Size(0x43, 20);
            txtuint32.TabIndex = 0x15;
            txtfloat.Location = new Point(0x12d, 0x9f);
            txtfloat.Name = "txtfloat";
            txtfloat.Size = new Size(0x43, 20);
            txtfloat.TabIndex = 0x15;
            label1.AutoSize = true;
            label1.Location = new Point(0x176, 0x24);
            label1.Name = "label1";
            label1.Size = new Size(0x1b, 13);
            label1.TabIndex = 0x16;
            label1.Text = "byte";
            label2.AutoSize = true;
            label2.Location = new Point(0x176, 0x3e);
            label2.Name = "label2";
            label2.Size = new Size(30, 13);
            label2.TabIndex = 0x16;
            label2.Text = "int16";
            label3.AutoSize = true;
            label3.Location = new Point(0x175, 0x58);
            label3.Name = "label3";
            label3.Size = new Size(0x24, 13);
            label3.TabIndex = 0x16;
            label3.Text = "uint16";
            label4.AutoSize = true;
            label4.Location = new Point(0x176, 0x72);
            label4.Name = "label4";
            label4.Size = new Size(30, 13);
            label4.TabIndex = 0x16;
            label4.Text = "int32";
            label5.AutoSize = true;
            label5.Location = new Point(0x175, 140);
            label5.Name = "label5";
            label5.Size = new Size(0x24, 13);
            label5.TabIndex = 0x16;
            label5.Text = "uint32";
            label6.AutoSize = true;
            label6.Location = new Point(0x176, 0xa6);
            label6.Name = "label6";
            label6.Size = new Size(0x1b, 13);
            label6.TabIndex = 0x16;
            label6.Text = "float";
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button1.Location = new Point(0x12d, 5);
            button1.Name = "button1";
            button1.Size = new Size(100, 20);
            button1.TabIndex = 0x17;
            button1.Text = "Manual Poke";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(button1);
            base.Controls.Add(label6);
            base.Controls.Add(label5);
            base.Controls.Add(label4);
            base.Controls.Add(label3);
            base.Controls.Add(label2);
            base.Controls.Add(label1);
            base.Controls.Add(txtfloat);
            base.Controls.Add(txtuint32);
            base.Controls.Add(txtint32);
            base.Controls.Add(txtuint16);
            base.Controls.Add(txtint16);
            base.Controls.Add(txtbyte);
            base.Controls.Add(txtArrayBox);
            base.Controls.Add(lblValueName);
            base.Controls.Add(lblValueType);
            base.Name = "uiTagData";
            base.Size = new Size(0x196, 0xb9);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            base.Enabled = false;
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            ValueData.Size = Map.IO.In.ReadUInt32();
            Stream baseStream = Map.IO.In.BaseStream;
            baseStream.Position += 8L;
            ValueData.Pointer = Map.IO.In.ReadInt32() - Map.Map_Header.mapMagic;
            lblValueType.Text = "data{" + ValueData.Size.ToString() + "}";
            txtArrayBox.MaxLength = (int) (ValueData.Size * 2);
            if (ValueData.Size > 0x400)
            {
                txtArrayBox.Text = "Data too long. Reading skipped.";
            }
            else if (((ValueData.Size > 0) && (ValueData.Pointer > 0)) && (ValueData.Pointer < Map.Map_Header.fileSize))
            {
                Map.IO.In.BaseStream.Position = ValueData.Pointer;
                txtArrayBox.Text = ExtraFunctions.BytesToHexString(Map.IO.In.ReadBytes((int) ValueData.Size));
                base.Enabled = true;
            }
            Editted = false;
        }

        public void PokeValue(HaloReach3d.IO.EndianIO IO, int parentOffset)
        {
            if (base.Enabled)
            {
                if (txtArrayBox.Text.Length != txtArrayBox.MaxLength)
                {
                    OutputMessenger.OutputMessage("Data array \"" + ValueData.Name + "\" could not be saved due to an invalid length of bytes.", this);
                }
                IO.Out.BaseStream.Position = ValueData.Pointer + HMap.Map_Header.mapMagic;
                try
                {
                    IO.Out.Write(ExtraFunctions.HexStringToBytes(txtArrayBox.Text), 0, ExtraFunctions.HexStringToBytes(txtArrayBox.Text).Length);
                }
                catch
                {
                    OutputMessenger.OutputMessage("Some invalid byte characters were intered in data array \"" + ValueData.Name + "\".\n They will not be saved.", this);
                }
            }
        }

        public void ReadForward()
        {
        }

        public void SaveValue(HaloReach3d.IO.EndianIO IO, int parentOffset)
        {
            if (base.Enabled)
            {
                if (txtArrayBox.Text.Length != txtArrayBox.MaxLength)
                {
                    OutputMessenger.OutputMessage("Data array \"" + ValueData.Name + "\" could not be saved due to an invalid length of bytes.", this);
                }
                byte[] buffer = null;
                try
                {
                    buffer = ExtraFunctions.HexStringToBytes(txtArrayBox.Text);
                }
                catch
                {
                    OutputMessenger.OutputMessage("Some invalid byte characters were intered in data array \"" + ValueData.Name + "\".\n They will not be saved.", this);
                    return;
                }
                IO.Out.BaseStream.Position = ValueData.Pointer;
                IO.Out.Write(buffer);
            }
        }

        private void txtArrayBox_TextChanged(object sender, EventArgs e)
        {
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

        public mTagData ValueData
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

