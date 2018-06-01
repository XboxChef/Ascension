namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Values;
    using Ascension.Helpers;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiBitmask : UserControl
    {
        private mBitmask _bitmaskdata;
        private bool _editted;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private int fullOffset;
        private HaloMap HMap;
        private Label lblCount;
        private Label lblValueName;
        private CheckedListBox lstBitOptions;

        public uiBitmask()
        {
            components = null;
            InitializeComponent();
        }

        public uiBitmask(mBitmask bitmaskdata)
        {
            components = null;
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            BitmaskData = bitmaskdata;
            lblValueName.Text = bitmaskdata.Name;
            int num = 8;
            switch (bitmaskdata.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    num = 8;
                    break;

                case mValue.ObjectAttributes.Bitmask16:
                    num = 0x10;
                    break;

                case mValue.ObjectAttributes.Bitmask32:
                    num = 0x20;
                    break;
            }
            lblCount.Text = "{" + num.ToString() + "}";
            for (int i = 0; i < num; i++)
            {
                bool flag = false;
                for (int j = 0; j < bitmaskdata.Options.Count; j++)
                {
                    if (bitmaskdata.Options[j].BitIndex == i)
                    {
                        lstBitOptions.Items.Add(bitmaskdata.Options[j].Name);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    lstBitOptions.Items.Add("bit " + i.ToString());
                }
            }
            Editted = false;
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (mBitOption option in BitmaskData.Options)
            {
                string str2 = str;
                str = str2 + "Option Name: \"" + option.Name + "\" / Index: \"" + option.BitIndex.ToString() + "\"\n";
            }
            int num = fullOffset + HMap.Map_Header.mapMagic;
            OutputMessenger.OutputMessage("Name: \"" + BitmaskData.Name + "\"\nType: \"" + BitmaskData.Attributes.ToString() + "\"\nPlugin Offset: \"" + BitmaskData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nMemory Pointer \"0x" + num.ToString("X") + "\"\nBit options: \n" + str, this);
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
            lblCount = new Label();
            lblValueName = new Label();
            lstBitOptions = new CheckedListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            lblCount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            lblCount.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCount.Location = new Point(410, 3);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(50, 0x6d);
            lblCount.TabIndex = 15;
            lblCount.Text = "{32}";
            lblCount.TextAlign = ContentAlignment.MiddleLeft;
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 3);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0xaf, 0x6d);
            lblValueName.TabIndex = 14;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            lstBitOptions.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            lstBitOptions.FormattingEnabled = true;
            lstBitOptions.HorizontalScrollbar = true;
            lstBitOptions.Location = new Point(0xb8, 3);
            lstBitOptions.Name = "lstBitOptions";
            lstBitOptions.Size = new Size(220, 0x6d);
            lstBitOptions.TabIndex = 0x11;
            lstBitOptions.ItemCheck += new ItemCheckEventHandler(lstBitOptions_ItemCheck);
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
            base.Controls.Add(lstBitOptions);
            base.Controls.Add(lblCount);
            base.Controls.Add(lblValueName);
            base.Name = "uiBitmask";
            base.Size = new Size(0x1cf, 0x73);
            base.Load += new EventHandler(uiBitmask_Load);
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            uint num;
            fullOffset = parentOffset + BitmaskData.Offset;
            Map.IO.In.BaseStream.Position = fullOffset;
            HMap = Map;
            switch (BitmaskData.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    num = Map.IO.In.ReadByte();
                    break;

                case mValue.ObjectAttributes.Bitmask16:
                    num = Map.IO.In.ReadUInt16();
                    break;

                case mValue.ObjectAttributes.Bitmask32:
                    num = Map.IO.In.ReadUInt32();
                    break;

                default:
                    num = 0;
                    break;
            }
            uint num2 = 1;
            for (int i = 0; i < _bitmaskdata.Options.Count; i++)
            {
                if ((num & num2) == num2)
                {
                    lstBitOptions.SetItemChecked(i, true);
                }
                else
                {
                    lstBitOptions.SetItemChecked(i, false);
                }
                num2 = num2 << 1;
            }
            Editted = false;
        }

        private void lstBitOptions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Editted = true;
        }

        private List<bool> ReturnCheckedList()
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < lstBitOptions.Items.Count; i++)
            {
                list.Add(lstBitOptions.GetItemChecked(i));
            }
            return list;
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + BitmaskData.Offset;
            switch (BitmaskData.Attributes)
            {
                case mValue.ObjectAttributes.Bitmask8:
                    IO.Out.Write((byte) Ascension.Helpers.BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                    break;

                case mValue.ObjectAttributes.Bitmask16:
                    IO.Out.Write((short) Ascension.Helpers.BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                    break;

                case mValue.ObjectAttributes.Bitmask32:
                    IO.Out.Write(Ascension.Helpers.BitHelper.ConvertToWriteableInteger(ReturnCheckedList()));
                    break;
            }
        }

        private void uiBitmask_Load(object sender, EventArgs e)
        {
        }

        public mBitmask BitmaskData
        {
            get
            {
                return _bitmaskdata;
            }
            set
            {
                _bitmaskdata = value;
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
    }
}

