namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiEnum : UserControl
    {
        private bool _editted;
        private mEnum _enumdata;
        private List<mEnumOption> _enumoptions;
        private ComboBox cmbxOptions;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private int fullOffset;
        private HaloMap HMap;
        private Label lblValueName;
        private Label lblValueType;

        public uiEnum()
        {
            components = null;
            InitializeComponent();
        }

        public uiEnum(mValue enumdata)
        {
            components = null;
            InitializeComponent();
            EnumData = (mEnum) enumdata;
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            lblValueName.Text = EnumData.Name;
            EnumOptions = EnumData.Options;
            switch (enumdata.Attributes)
            {
                case mValue.ObjectAttributes.Enum8:
                    lblValueType.Text = "enum8";
                    break;

                case mValue.ObjectAttributes.Enum16:
                    lblValueType.Text = "enum16";
                    break;

                case mValue.ObjectAttributes.Enum32:
                    lblValueType.Text = "enum32";
                    break;
            }
            LoadOptions();
            Editted = false;
        }

        private void cmbxOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            Editted = true;
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (mEnumOption option in EnumData.Options)
            {
                string str2 = str;
                str = str2 + "Option Name: \"" + option.Name + "\" / Value: \"" + option.Value.ToString() + "\"\n";
            }
            int num = fullOffset + HMap.Map_Header.mapMagic;
            OutputMessenger.OutputMessage("Name: \"" + EnumData.Name + "\"\nType: \"" + EnumData.Attributes.ToString() + "\"\nPlugin Offset: \"" + EnumData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nMemory Pointer \"0x" + num.ToString("X") + "\"\nEnum options: \n" + str, this);
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
            lblValueType = new Label();
            cmbxOptions = new ComboBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 0);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0xaf, 30);
            lblValueName.TabIndex = 0;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            lblValueType.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            lblValueType.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueType.Location = new Point(0x15b, -1);
            lblValueType.Name = "lblValueType";
            lblValueType.Size = new Size(50, 30);
            lblValueType.TabIndex = 12;
            lblValueType.Text = "enum00";
            lblValueType.TextAlign = ContentAlignment.MiddleLeft;
            cmbxOptions.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            cmbxOptions.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxOptions.FormattingEnabled = true;
            cmbxOptions.Location = new Point(0xb8, 5);
            cmbxOptions.Name = "cmbxOptions";
            cmbxOptions.Size = new Size(0x9d, 0x15);
            cmbxOptions.TabIndex = 14;
            cmbxOptions.SelectedIndexChanged += new EventHandler(cmbxOptions_SelectedIndexChanged);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x30);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(cmbxOptions);
            base.Controls.Add(lblValueType);
            base.Controls.Add(lblValueName);
            base.Name = "uiEnum";
            base.Size = new Size(400, 30);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void LoadOptions()
        {
            cmbxOptions.Items.Clear();
            for (int i = 0; i < EnumOptions.Count; i++)
            {
                cmbxOptions.Items.Add(EnumOptions[i].Name);
            }
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            Map.IO.In.BaseStream.Position = parentOffset + EnumData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            switch (EnumData.Attributes)
            {
                case mValue.ObjectAttributes.Enum8:
                    SelectOption(Map.IO.In.ReadByte());
                    break;

                case mValue.ObjectAttributes.Enum16:
                    SelectOption(Map.IO.In.ReadInt16());
                    break;

                case mValue.ObjectAttributes.Enum32:
                    SelectOption(Map.IO.In.ReadInt32());
                    break;
            }
            Editted = false;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            if (cmbxOptions.SelectedIndex != -1)
            {
                IO.Out.BaseStream.Position = parentOffset + EnumData.Offset;
                switch (EnumData.Attributes)
                {
                    case mValue.ObjectAttributes.Enum8:
                        IO.Out.Write((byte) EnumOptions[cmbxOptions.SelectedIndex].Value);
                        break;

                    case mValue.ObjectAttributes.Enum16:
                        IO.Out.Write((short) EnumOptions[cmbxOptions.SelectedIndex].Value);
                        break;

                    case mValue.ObjectAttributes.Enum32:
                        IO.Out.Write(EnumOptions[cmbxOptions.SelectedIndex].Value);
                        break;
                }
            }
        }

        public void SelectOption(int value)
        {
            for (int i = 0; i < EnumOptions.Count; i++)
            {
                if (EnumOptions[i].Value == value)
                {
                    cmbxOptions.SelectedIndex = i;
                    return;
                }
            }
            mEnumOption item = new mEnumOption {
                Name = "<unknown>(" + value + ")",
                Value = value
            };
            EnumOptions.Add(item);
            cmbxOptions.Items.Add(item.Name);
            cmbxOptions.SelectedIndex = cmbxOptions.Items.Count - 1;
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

        public mEnum EnumData
        {
            get
            {
                return _enumdata;
            }
            set
            {
                _enumdata = value;
            }
        }

        public List<mEnumOption> EnumOptions
        {
            get
            {
                return _enumoptions;
            }
            set
            {
                _enumoptions = value;
            }
        }
    }
}

