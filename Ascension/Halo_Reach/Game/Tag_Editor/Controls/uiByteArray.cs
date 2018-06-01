namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.Helpers;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiByteArray : UserControl
    {
        private bool _editted;
        private mByteArray _valuedata;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private ToolStripMenuItem editValueInPluginToolStripMenuItem;
        private int fullOffset;
        private HaloMap HMap;
        private Label lblByteCount;
        private Label lblValueName;
        private ToolStripSeparator toolStripSeparator1;
        private TextBox txtArrayBox;

        public uiByteArray(mByteArray val)
        {
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            ValueData = val;
            lblValueName.Text = ValueData.Name;
            lblByteCount.Text = "(" + ValueData.Length.ToString() + ")";
            txtArrayBox.MaxLength = ValueData.Length * 2;
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
            txtArrayBox = new TextBox();
            lblByteCount = new Label();
            lblValueName = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editValueInPluginToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            txtArrayBox.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            txtArrayBox.Location = new Point(0xb8, 5);
            txtArrayBox.Multiline = true;
            txtArrayBox.Name = "txtArrayBox";
            txtArrayBox.Size = new Size(0x9d, 0x45);
            txtArrayBox.TabIndex = 0x13;
            txtArrayBox.TextChanged += new EventHandler(txtArrayBox_TextChanged);
            lblByteCount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            lblByteCount.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblByteCount.Location = new Point(0x15b, 5);
            lblByteCount.Name = "lblByteCount";
            lblByteCount.Size = new Size(50, 0x18);
            lblByteCount.TabIndex = 0x12;
            lblByteCount.Text = "(32)";
            lblByteCount.TextAlign = ContentAlignment.MiddleLeft;
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 0);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0xaf, 30);
            lblValueName.TabIndex = 0x11;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem, toolStripSeparator1, editValueInPluginToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x4c);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(0xaf, 6);
            editValueInPluginToolStripMenuItem.Name = "editValueInPluginToolStripMenuItem";
            editValueInPluginToolStripMenuItem.Size = new Size(0xb2, 0x16);
            editValueInPluginToolStripMenuItem.Text = "Edit value in Plugin";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(txtArrayBox);
            base.Controls.Add(lblByteCount);
            base.Controls.Add(lblValueName);
            base.Name = "uiByteArray";
            base.Size = new Size(400, 0x4f);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            byte[] data = Map.IO.In.ReadBytes(ValueData.Length);
            txtArrayBox.Text = ExtraFunctions.BytesToHexString(data);
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
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
            IO.Out.BaseStream.Position = parentOffset + ValueData.Offset;
            IO.Out.Write(buffer);
        }

        private void txtArrayBox_TextChanged(object sender, EventArgs e)
        {
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

        public mByteArray ValueData
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

