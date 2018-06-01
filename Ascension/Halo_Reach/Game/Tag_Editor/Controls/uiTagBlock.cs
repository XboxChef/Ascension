namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Communications.Output;
    using Ascension.Halo_Reach.Game.Tag_Editor;
    using Ascension.Halo_Reach.Game.Tag_Editor.Classes;
    using Ascension.Halo_Reach.Values;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.IO;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiTagBlock : UserControl
    {
        private HaloMap _map;
        private mTagBlock _reflexivedata;
        private Button button1;
        private Button button2;
        private ComboBox cmbxChunks;
        public bool cnkbxx;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem displayInfoToolStripMenuItem;
        private ToolStripMenuItem dragToolStripMenuItem;
        private int fullOffset;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblStructureText;
        private int parentoffset;
        private Panel pnlHeader;
        public Point pnllo;
        public int pnlsize;
        private Panel pnlValues;
        public int SIZE;
        private TextBox textBox1;
        private TextBox txtcount;
        private TextBox txtoffset;

        public uiTagBlock()
        {
            components = null;
            pnllo = new Point(0, 0);
            parentoffset = 0;
            SIZE = 0;
            cnkbxx = false;
            pnlsize = 0;
            InitializeComponent();
        }

        public uiTagBlock(mTagBlock mReflex)
        {
            components = null;
            pnllo = new Point(0, 0);
            parentoffset = 0;
            SIZE = 0;
            cnkbxx = false;
            pnlsize = 0;
            InitializeComponent();
            ReflexiveData = mReflex;
            foreach (Control control in base.Controls)
            {
                control.ContextMenuStrip = contextMenuStrip1;
            }
            lblStructureText.Text = ReflexiveData.Name.ToUpper();
            pnllo = pnlValues.Location;
            VIZ();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
                    nio.Out.BaseStream.Position = (parentoffset + ReflexiveData.Offset) + Map.Map_Header.mapMagic;
                    nio.Out.Write(int.Parse(txtcount.Text));
                    nio.Out.Write((int) (int.Parse(txtoffset.Text) + Map.Map_Header.mapMagic));
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

        private void button2_Click(object sender, EventArgs e)
        {
            Map.OpenIO();
            Map.IO.Out.BaseStream.Position = parentoffset + ReflexiveData.Offset;
            Map.IO.Out.Write(int.Parse(txtcount.Text));
            Map.IO.Out.Write((int) (int.Parse(txtoffset.Text) + Map.Map_Header.mapMagic));
            Map.CloseIO();
            LoadStructure(Map, parentoffset);
            OutputMessenger.OutputMessage("Saved Chunk Values", this);
        }

        private void cmbxChunks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbxChunks.SelectedIndex >= 0)
            {
                try
                {
                    int parentOffset = ReflexiveData.Pointer + (cmbxChunks.SelectedIndex * SIZE);
                    TagEditorHandler.LoadPluginValues(Map, pnlValues, parentOffset);
                    Map.CloseIO();
                }
                catch
                {
                    OutputMessenger.OutputMessage("Could not load values for Reflexive/Structure: " + ReflexiveData.Name, this);
                }
            }
        }

        private void displayInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmbxChunks.Enabled)
            {
                int num = ReflexiveData.Pointer + (cmbxChunks.SelectedIndex * ReflexiveData.Size);
                OutputMessenger.OutputMessage("Name: \"" + ReflexiveData.Name + "\"\nType: \"" + ReflexiveData.Attributes.ToString() + "\"\nPlugin Offset: \"" + ReflexiveData.Offset.ToString() + "\"\nFile Offset: \"0x" + fullOffset.ToString("X") + "\"\nChunk Count: \"" + ReflexiveData.ChunkCount.ToString() + "\"\nPointer: \"0x" + ReflexiveData.Pointer.ToString("X") + "\"\nMemory Pointer \"0x" + ReflexiveData.MemoryPointer.ToString("X") + "\"\nCurrent Chunk Offset: \"0x" + num.ToString("X") + "\"\n", this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            try
            {
                UnloadMetaEditor();
            }
            catch
            {
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void dragToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point point;
            cnkbxx = false;
            if (!dragToolStripMenuItem.Checked && (base.Parent != this))
            {
                while (!cnkbxx)
                {
                    while (!cnkbxx)
                    {
                        int y = TagEditorContainer.Scrolloffset.Y;
                        cmbxChunks.Location = new Point(0, y - base.Location.Y);
                        pnlValues.Width = pnlsize - 0x45;
                        dragToolStripMenuItem.Checked = true;
                        point = new Point(pnllo.X + 0x45, pnllo.Y);
                        pnlValues.Location = point;
                        Application.DoEvents();
                    }
                    Application.DoEvents();
                }
            }
            else
            {
                pnlValues.Width = pnlsize;
                cnkbxx = true;
                cmbxChunks.Location = new Point(1, 0x22);
                dragToolStripMenuItem.Checked = false;
                point = new Point(pnllo.X, pnllo.Y);
                pnlValues.Width = pnlsize;
                pnlValues.Location = point;
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            pnlValues = new Panel();
            pnlHeader = new Panel();
            contextMenuStrip1 = new ContextMenuStrip(components);
            displayInfoToolStripMenuItem = new ToolStripMenuItem();
            dragToolStripMenuItem = new ToolStripMenuItem();
            cmbxChunks = new ComboBox();
            textBox1 = new TextBox();
            button1 = new Button();
            label3 = new Label();
            button2 = new Button();
            label2 = new Label();
            label1 = new Label();
            txtoffset = new TextBox();
            txtcount = new TextBox();
            lblStructureText = new Label();
            contextMenuStrip2 = new ContextMenuStrip(components);
            pnlHeader.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            pnlValues.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            pnlValues.BackColor = SystemColors.Control;
            pnlValues.Location = new Point(3, 0x3b);
            pnlValues.Name = "pnlValues";
            pnlValues.Size = new Size(0x1de, 250);
            pnlValues.TabIndex = 8;
            pnlValues.Visible = false;
            pnlHeader.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            pnlHeader.BackColor = Color.FromArgb(0xa2, 180, 0xd1);
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlHeader.ContextMenuStrip = contextMenuStrip1;
            pnlHeader.Controls.Add(cmbxChunks);
            pnlHeader.Controls.Add(textBox1);
            pnlHeader.Controls.Add(button1);
            pnlHeader.Controls.Add(label3);
            pnlHeader.Controls.Add(button2);
            pnlHeader.Controls.Add(label2);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Controls.Add(txtoffset);
            pnlHeader.Controls.Add(txtcount);
            pnlHeader.Controls.Add(pnlValues);
            pnlHeader.Controls.Add(lblStructureText);
            pnlHeader.Location = new Point(0, 1);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(0x1e9, 0x13d);
            pnlHeader.TabIndex = 9;
            pnlHeader.Paint += new PaintEventHandler(panel2_Paint);
            pnlHeader.MouseLeave += new EventHandler(pnlHeader_MouseLeave);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { displayInfoToolStripMenuItem, dragToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(190, 0x30);
            displayInfoToolStripMenuItem.Name = "displayInfoToolStripMenuItem";
            displayInfoToolStripMenuItem.Size = new Size(0xbd, 0x16);
            displayInfoToolStripMenuItem.Text = "Display Information";
            displayInfoToolStripMenuItem.Click += new EventHandler(displayInfoToolStripMenuItem_Click);
            dragToolStripMenuItem.Name = "dragToolStripMenuItem";
            dragToolStripMenuItem.Size = new Size(0xbd, 0x16);
            dragToolStripMenuItem.Text = "Move Count UI Along";
            dragToolStripMenuItem.Click += new EventHandler(dragToolStripMenuItem_Click);
            cmbxChunks.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxChunks.FormattingEnabled = true;
            cmbxChunks.Location = new Point(1, 0x22);
            cmbxChunks.Name = "cmbxChunks";
            cmbxChunks.Size = new Size(70, 0x15);
            cmbxChunks.TabIndex = 0;
            cmbxChunks.SelectedIndexChanged += new EventHandler(cmbxChunks_SelectedIndexChanged);
            textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            textBox1.Location = new Point(0x1af, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0x2e, 20);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += new EventHandler(textBox1_TextChanged);
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button1.Location = new Point(0x1b4, 0x20);
            button1.Name = "button1";
            button1.Size = new Size(0x29, 0x17);
            button1.TabIndex = 0;
            button1.Text = "Poke";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            label3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label3.AutoSize = true;
            label3.BackColor = Color.Gray;
            label3.Location = new Point(0x195, 7);
            label3.Name = "label3";
            label3.Size = new Size(0x1b, 13);
            label3.TabIndex = 14;
            label3.Text = "Size";
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button2.FlatAppearance.BorderSize = 2;
            button2.FlatStyle = FlatStyle.System;
            button2.Location = new Point(0x18c, 0x20);
            button2.Name = "button2";
            button2.Size = new Size(0x29, 0x17);
            button2.TabIndex = 0;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            label2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(0xb2, 0x24);
            label2.Name = "label2";
            label2.Size = new Size(0x41, 13);
            label2.TabIndex = 14;
            label2.Text = "Block Count";
            label1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(0x11c, 0x24);
            label1.Name = "label1";
            label1.Size = new Size(0x23, 13);
            label1.TabIndex = 14;
            label1.Text = "Offset";
            txtoffset.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            txtoffset.Location = new Point(0x13f, 0x21);
            txtoffset.Name = "txtoffset";
            txtoffset.Size = new Size(0x4c, 20);
            txtoffset.TabIndex = 0;
            txtcount.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            txtcount.Location = new Point(0xf3, 0x21);
            txtcount.Name = "txtcount";
            txtcount.Size = new Size(0x29, 20);
            txtcount.TabIndex = 0;
            lblStructureText.BackColor = Color.Gray;
            lblStructureText.Dock = DockStyle.Top;
            lblStructureText.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStructureText.Location = new Point(0, 0);
            lblStructureText.Name = "lblStructureText";
            lblStructureText.Size = new Size(0x1e7, 0x1c);
            lblStructureText.TabIndex = 12;
            lblStructureText.Text = "STRUCTURE";
            lblStructureText.TextAlign = ContentAlignment.MiddleLeft;
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(0x3d, 4);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ContextMenuStrip = contextMenuStrip1;
            base.Controls.Add(pnlHeader);
            base.Name = "uiTagBlock";
            base.Size = new Size(0x1e1, 0x135);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public void LoadStructure(HaloMap map, int parentOffset)
        {
            Map = map;
            if (!((Map.IO != null) && Map.IO.Opened))
            {
                Map.OpenIO();
            }
            Map.IO.In.BaseStream.Position = parentOffset + ReflexiveData.Offset;
            parentoffset = parentOffset;
            int num = Map.IO.In.ReadInt32();
            ReflexiveData.ChunkCount = num;
            ReflexiveData.MemoryPointer = Map.IO.In.ReadInt32();
            int num2 = ReflexiveData.MemoryPointer - Map.Map_Header.mapMagic;
            ReflexiveData.Pointer = num2;
            SIZE = ReflexiveData.Size;
            textBox1.Text = ReflexiveData.Size.ToString();
            txtcount.Text = num.ToString();
            txtoffset.Text = num2.ToString();
            cmbxChunks.Items.Clear();
            if ((((ReflexiveData.ChunkCount <= 0) | (ReflexiveData.ChunkCount > 0x186a0)) | (ReflexiveData.Pointer < 0)) | (ReflexiveData.Pointer >= ((int) Map.IO.In.BaseStream.Length)))
            {
                pnlHeader.Enabled = true;
                cmbxChunks.Enabled = false;
                cmbxChunks.Text = "";
                pnlValues.Enabled = false;
                txtcount.Text = "0";
                txtoffset.Text = "0";
            }
            else if (ReflexiveData.ChunkCount > 0)
            {
                fullOffset = parentOffset + ReflexiveData.Offset;
                cmbxChunks.Enabled = true;
                pnlValues.Enabled = true;
                pnlHeader.Enabled = true;
                for (int i = 0; i < ReflexiveData.ChunkCount; i++)
                {
                    cmbxChunks.Items.Add(i + " - " + (ReflexiveData.ChunkCount - 1));
                }
                cmbxChunks.SelectedIndex = 0;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pnlHeader_MouseLeave(object sender, EventArgs e)
        {
        }

        public void PokeStructure(HaloReach3d.IO.EndianIO IO, int magic, bool onlyChanged)
        {
            if (cmbxChunks.Enabled)
            {
                int parentOffset = ReflexiveData.Pointer + (cmbxChunks.SelectedIndex * ReflexiveData.Size);
                TagEditorHandler.PokeValues(IO, pnlValues, parentOffset, magic, onlyChanged);
            }
        }

        private void resetCountUIToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        public void ResizeTagBlock()
        {
            int width = base.Width;
            int height = 80;
            for (int i = 0; i < pnlValues.Controls.Count; i++)
            {
                height += pnlValues.Controls[i].Height;
                if (pnlValues.Controls[i].GetType() == typeof(uiTagBlock))
                {
                    height += 2;
                }
                if (pnlValues.Controls[i].Width > width)
                {
                    width = pnlValues.Controls[i].Width;
                }
            }
            base.Size = new Size(width + 13, height);
            pnlsize = width + 13;
            if (base.Height > 0xfa0)
            {
                dragToolStripMenuItem.Visible = true;
            }
            else
            {
                dragToolStripMenuItem.Visible = false;
            }
        }

        public ComboBox returnComboBox() => 
            cmbxChunks;

        public Panel returnValuePanel() => 
            pnlValues;

        public void SaveStructure(HaloMap map, int parentOffset)
        {
            if (cmbxChunks.Enabled)
            {
                int num = ReflexiveData.Pointer + (cmbxChunks.SelectedIndex * ReflexiveData.Size);
                TagEditorHandler.SaveChangedValues(map, pnlValues, num);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SIZE = int.Parse(textBox1.Text);
        }

        public void UnloadMetaEditor()
        {
            foreach (Control control in pnlValues.Controls)
            {
                cnkbxx = true;
                control.Dispose();
            }
        }

        public void VIZ()
        {
            try
            {
                pnlValues.Visible = true;
                TagEditorHandler.Panels(pnlValues);
            }
            catch
            {
            }
        }

        public void VVA(bool viewing)
        {
            TagEditorHandler.VVA(pnlValues, viewing);
        }

        public HaloMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }

        public mTagBlock ReflexiveData
        {
            get
            {
                return _reflexivedata;
            }
            set
            {
                _reflexivedata = value;
            }
        }
    }
}

