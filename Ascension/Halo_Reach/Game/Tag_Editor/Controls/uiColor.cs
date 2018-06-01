namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Values;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiColor : UserControl
    {
        private bool _editted;
        private mColorBlock _valuedata;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        private ToolStripMenuItem editValueInPluginToolStripMenuItem;
        private int fullOffset;
        private HaloMap HMap;
        private Label lblValueName;
        private Label lblValueType;
        private PictureBox pictureBox1;
        private ToolStripSeparator toolStripSeparator1;

        public uiColor(mColorBlock val)
        {
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            ValueData = val;
            lblValueName.Text = ValueData.Name;
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = fullOffset + HMap.Map_Header.mapMagic;
            string str = "Color order: \n";
            for (int i = 0; i < ValueData.Color_Order.Count; i++)
            {
                str = str + ValueData.Color_Order[i].ToString() + "\n";
            }
            OutputMessenger.OutputMessage("Name: \"" + ValueData.Name + "\"\nType: \"" + ValueData.Attributes.ToString() + "\"\nPlugin Offset: \"" + ValueData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nMemory Pointer \"0x" + num.ToString("X") + "\"\n" + str, this);
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
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInformationToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            editValueInPluginToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize) pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            lblValueType.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            lblValueType.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueType.Location = new Point(150, 0);
            lblValueType.Name = "lblValueType";
            lblValueType.Size = new Size(50, 30);
            lblValueType.TabIndex = 15;
            lblValueType.Text = "color";
            lblValueType.TextAlign = ContentAlignment.MiddleLeft;
            lblValueName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblValueName.Location = new Point(3, 0);
            lblValueName.Name = "lblValueName";
            lblValueName.Size = new Size(0x66, 30);
            lblValueName.TabIndex = 14;
            lblValueName.Text = "name";
            lblValueName.TextAlign = ContentAlignment.MiddleLeft;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Location = new Point(0x7b, 6);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(20, 20);
            pictureBox1.TabIndex = 0x10;
            pictureBox1.TabStop = false;
            pictureBox1.Click += new EventHandler(pictureBox1_Click);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem, toolStripSeparator1, editValueInPluginToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x36);
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
            base.Controls.Add(lblValueName);
            base.Controls.Add(pictureBox1);
            base.Controls.Add(lblValueType);
            base.Name = "uiColor";
            base.Size = new Size(0xde, 30);
            ((ISupportInitialize) pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            int alpha = 0xff;
            int red = 0;
            int green = 0;
            int blue = 0;
            foreach (ColorBlockPart part in ValueData.Color_Order)
            {
                float num6;
                int num5 = 0;
                switch (ValueData.Attributes)
                {
                    case mValue.ObjectAttributes.ColorBlock8:
                        num5 = Map.IO.In.ReadByte();
                        goto Label_013B;

                    case mValue.ObjectAttributes.ColorBlock16:
                        num5 = Map.IO.In.ReadInt16();
                        goto Label_013B;

                    case mValue.ObjectAttributes.ColorBlock32:
                        num5 = Map.IO.In.ReadInt32();
                        goto Label_013B;

                    case mValue.ObjectAttributes.ColorBlockF:
                        num6 = Map.IO.In.ReadSingle();
                        if (!ValueData.Real_Color)
                        {
                            break;
                        }
                        num5 = (int) (num6 * 255f);
                        goto Label_0120;

                    default:
                        goto Label_013B;
                }
                num5 = (int) num6;
            Label_0120:
                if (num5 > 0xff)
                {
                    num5 = 0xff;
                }
            Label_013B:
                switch (part)
                {
                    case ColorBlockPart.Red:
                        red = num5;
                        break;

                    case ColorBlockPart.Green:
                        green = num5;
                        break;

                    case ColorBlockPart.Blue:
                        blue = num5;
                        break;

                    case ColorBlockPart.Alpha:
                        alpha = num5;
                        break;
                }
            }
            pictureBox1.BackColor = Color.FromArgb(alpha, red, green, blue);
            Editted = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = dialog.Color;
                Editted = true;
            }
        }

        public void SaveValue(EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + ValueData.Offset;
            int r = pictureBox1.BackColor.R;
            int g = pictureBox1.BackColor.G;
            int b = pictureBox1.BackColor.B;
            int a = pictureBox1.BackColor.A;
            foreach (ColorBlockPart part in ValueData.Color_Order)
            {
                int num5 = 0;
                switch (part)
                {
                    case ColorBlockPart.Red:
                        num5 = r;
                        break;

                    case ColorBlockPart.Green:
                        num5 = g;
                        break;

                    case ColorBlockPart.Blue:
                        num5 = b;
                        break;

                    case ColorBlockPart.Alpha:
                        num5 = a;
                        break;
                }
                switch (ValueData.Attributes)
                {
                    case mValue.ObjectAttributes.ColorBlock8:
                        IO.Out.Write((byte) num5);
                        break;

                    case mValue.ObjectAttributes.ColorBlock16:
                        IO.Out.Write((short) num5);
                        break;

                    case mValue.ObjectAttributes.ColorBlock32:
                        IO.Out.Write(num5);
                        break;

                    case mValue.ObjectAttributes.ColorBlockF:
                    {
                        float num6 = num5;
                        if (ValueData.Real_Color)
                        {
                            num6 /= 255f;
                        }
                        IO.Out.Write(num6);
                        break;
                    }
                }
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

        public mColorBlock ValueData
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

