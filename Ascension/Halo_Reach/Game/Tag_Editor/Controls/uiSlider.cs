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

    public class uiSlider : UserControl
    {
        private bool _editted;
        private mValue _valuedata;
        private ToolStripMenuItem applyToolStripMenuItem;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem displayInformationToolStripMenuItem;
        public bool floater;
        private int fullOffset;
        private HaloMap HMap;
        private Label lblValueName;
        private Panel panel1;
        private float restore;
        private ToolStripMenuItem setMinimumValueToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox1;
        private ToolStripTextBox toolStripTextBox2;
        private TrackBar trackBar1;
        private TextBox txtValue;
        public bool viewing;

        public uiSlider()
        {
            components = null;
            floater = false;
            viewing = false;
            InitializeComponent();
        }

        public uiSlider(mValue valdata)
        {
            components = null;
            floater = false;
            viewing = false;
            InitializeComponent();
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            ValueData = valdata;
            lblValueName.Text = ValueData.Name;
            Editted = false;
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
            trackBar1 = new TrackBar();
            setMinimumValueToolStripMenuItem = new ToolStripMenuItem();
            applyToolStripMenuItem = new ToolStripMenuItem();
            toolStripTextBox1 = new ToolStripTextBox();
            toolStripTextBox2 = new ToolStripTextBox();
            contextMenuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            trackBar1.BeginInit();
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
            txtValue.Location = new Point(0x174, 5);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(90, 20);
            txtValue.TabIndex = 0x10;
            txtValue.TextChanged += new EventHandler(textBox1_TextChanged);
            txtValue.MouseLeave += new EventHandler(uiValue_MouseLeave);
            txtValue.MouseHover += new EventHandler(uiValue_MouseHover);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInformationToolStripMenuItem, setMinimumValueToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xb3, 0x30);
            displayInformationToolStripMenuItem.Name = "displayInformationToolStripMenuItem";
            displayInformationToolStripMenuItem.Size = new Size(0xb2, 0x16);
            displayInformationToolStripMenuItem.Text = "Display Information";
            displayInformationToolStripMenuItem.Click += new EventHandler(displayInformationToolStripMenuItem_Click);
            panel1.Controls.Add(txtValue);
            panel1.Controls.Add(lblValueName);
            panel1.Controls.Add(trackBar1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x1d1, 0x21);
            panel1.TabIndex = 0x11;
            panel1.MouseLeave += new EventHandler(uiValue_MouseLeave);
            panel1.MouseHover += new EventHandler(uiValue_MouseHover);
            trackBar1.Location = new Point(0xb8, 5);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(0xb6, 0x2d);
            trackBar1.TabIndex = 0x11;
            trackBar1.TickStyle = TickStyle.None;
            setMinimumValueToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripTextBox1, toolStripTextBox2, applyToolStripMenuItem });
            setMinimumValueToolStripMenuItem.Name = "setMinimumValueToolStripMenuItem";
            setMinimumValueToolStripMenuItem.Size = new Size(0xb2, 0x16);
            setMinimumValueToolStripMenuItem.Text = "SetMinimum Value";
            applyToolStripMenuItem.Name = "applyToolStripMenuItem";
            applyToolStripMenuItem.Size = new Size(160, 0x16);
            applyToolStripMenuItem.Text = "Apply";
            toolStripTextBox1.BackColor = SystemColors.ScrollBar;
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new Size(100, 0x17);
            toolStripTextBox1.Text = "Minimum";
            toolStripTextBox2.BackColor = SystemColors.ScrollBar;
            toolStripTextBox2.Name = "toolStripTextBox2";
            toolStripTextBox2.Size = new Size(100, 0x17);
            toolStripTextBox2.Text = "Maximum";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(panel1);
            base.Name = "uiValue";
            base.Size = new Size(0x1d1, 0x21);
            base.MouseLeave += new EventHandler(uiValue_MouseLeave);
            base.MouseHover += new EventHandler(uiValue_MouseHover);
            contextMenuStrip1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            trackBar1.EndInit();
            base.ResumeLayout(false);
        }

        public void LoadValue(HaloMap Map, int parentOffset)
        {
            Map.IO.In.BaseStream.Position = parentOffset + ValueData.Offset;
            fullOffset = (int) Map.IO.In.BaseStream.Position;
            HMap = Map;
            floater = true;
            txtValue.Text = Map.IO.In.ReadSingle().ToString();
            Editted = false;
        }

        public void PokeValue(HaloReach3d.IO.EndianIO IO)
        {
            IO.Out.BaseStream.Position = fullOffset + HMap.Map_Header.mapMagic;
            IO.Out.Write(float.Parse(txtValue.Text));
        }

        public void SaveValue(HaloReach3d.IO.EndianIO IO, int parentOffset)
        {
            IO.Out.BaseStream.Position = parentOffset + ValueData.Offset;
            IO.Out.Write(float.Parse(txtValue.Text));
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

