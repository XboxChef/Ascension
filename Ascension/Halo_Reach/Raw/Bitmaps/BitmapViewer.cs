namespace Ascension.Halo_Reach.Raw.Bitmaps
{
    using Ascension.Communications.Output;
    using HaloReach3d.Map;
    using HaloReach3d.Raw.Bitmaps;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class BitmapViewer : UserControl
    {
        private BitmapInfo bi;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ComboBox comboBox1;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem extractBitmapToolStripMenuItem;
        private ToolStripMenuItem extractRawToolStripMenuItem;
        private Label label1;
        private ListView listView1;
        private HaloMap map;
        private PictureBox pictureBox1;
        private int tagIndex;

        public BitmapViewer()
        {
            InitializeComponent();
            string[] names = Enum.GetNames(typeof(TextureFormat));
            foreach (string str in names)
            {
                comboBox1.Items.Add(str);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] names = Enum.GetNames(typeof(TextureFormat));
            for (int i = 0; i < names.Length; i++)
            {
                if (comboBox1.Text == names[i].ToString())
                {
                    if (listView1.FocusedItem != null)
                    {
                        map.RawInformation.ExternalMaps.OpenIOs();
                        map.OpenIO();
                        int tag = (int) listView1.FocusedItem.Tag;
                        TextureFormat textureFormat = (TextureFormat) Enum.Parse(typeof(TextureFormat), names[i]);
                        try
                        {
                            OutputMessenger.OutputMessage("Trying to view bitmap as format: " + textureFormat.ToString(), this);
                            pictureBox1.Image = bi.GeneratePreview(tag, textureFormat);
                        }
                        catch
                        {
                            Bitmap image = new Bitmap(200, 200);
                            Graphics graphics = Graphics.FromImage(image);
                            graphics.Clear(Color.Gray);
                            string text = "Error Previewing!";
                            SizeF ef = graphics.MeasureString(text, new Font(FontFamily.GenericSerif, 15f));
                            graphics.DrawString(text, new Font(FontFamily.GenericSerif, 15f), Brushes.Black, new PointF(100f - (ef.Width / 2f), 100f - (ef.Height / 2f)));
                            pictureBox1.Image = image;
                        }
                        map.CloseIO();
                        map.RawInformation.ExternalMaps.CloseIOs();
                    }
                    break;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void extractBitmapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                int tag = (int) listView1.FocusedItem.Tag;
                SaveFileDialog dialog = new SaveFileDialog {
                    Filter = "DDS File (*.dds)|*.dds|Tif Image (*.tif)|*.tif"
                };
                string[] strArray = map.Index_Items[tagIndex].Name.Split(new char[] { '\\' });
                dialog.FileName = strArray[strArray.Length - 1];
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    map.RawInformation.ExternalMaps.OpenIOs();
                    if (dialog.FilterIndex == 1)
                    {
                        bi.Extract(dialog.FileName, tag);
                    }
                    else
                    {
                        bi.GeneratePreview(tag).Save(dialog.FileName, ImageFormat.Tiff);
                    }
                    map.RawInformation.ExternalMaps.CloseIOs();
                    OutputMessenger.OutputMessage("Texture extracted to: \"" + dialog.FileName + "\"", this);
                }
            }
        }

        private void extractRawToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                int tag = (int) listView1.FocusedItem.Tag;
                SaveFileDialog dialog = new SaveFileDialog {
                    Filter = "Bin File (*.bin)|*.bin"
                };
                string[] strArray = map.Index_Items[tagIndex].Name.Split(new char[] { '\\' });
                dialog.FileName = strArray[strArray.Length - 1];
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    map.RawInformation.ExternalMaps.OpenIOs();
                    bi.ExtractRaw(dialog.FileName, tag);
                    map.RawInformation.ExternalMaps.CloseIOs();
                    OutputMessenger.OutputMessage("Texture extracted to: \"" + dialog.FileName + "\"", this);
                }
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            pictureBox1 = new PictureBox();
            listView1 = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            extractRawToolStripMenuItem = new ToolStripMenuItem();
            extractBitmapToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip2 = new ContextMenuStrip(components);
            label1 = new Label();
            comboBox1 = new ComboBox();
            ((ISupportInitialize) pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            pictureBox1.BackColor = Color.Black;
            pictureBox1.ContextMenuStrip = contextMenuStrip2;
            pictureBox1.Location = new Point(0x16, 0x8e);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(0x18b, 0x18b);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += new EventHandler(pictureBox1_Click);
            listView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3, columnHeader4, columnHeader5, columnHeader2, columnHeader6 });
            listView1.ContextMenuStrip = contextMenuStrip1;
            listView1.FullRowSelect = true;
            listView1.Location = new Point(0x16, 14);
            listView1.Name = "listView1";
            listView1.Size = new Size(400, 0x5f);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            columnHeader1.Text = "Index";
            columnHeader3.Text = "Height";
            columnHeader4.Text = "Width";
            columnHeader5.Text = "Size";
            columnHeader2.Text = "Format";
            columnHeader6.Text = "Type";
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { extractRawToolStripMenuItem, extractBitmapToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x97, 0x30);
            extractRawToolStripMenuItem.Name = "extractRawToolStripMenuItem";
            extractRawToolStripMenuItem.Size = new Size(150, 0x16);
            extractRawToolStripMenuItem.Text = "Extract Raw";
            extractRawToolStripMenuItem.Click += new EventHandler(extractRawToolStripMenuItem_Click);
            extractBitmapToolStripMenuItem.Name = "extractBitmapToolStripMenuItem";
            extractBitmapToolStripMenuItem.Size = new Size(150, 0x16);
            extractBitmapToolStripMenuItem.Text = "Extract Bitmap";
            extractBitmapToolStripMenuItem.Click += new EventHandler(extractBitmapToolStripMenuItem_Click);
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(0x3d, 4);
            label1.AutoSize = true;
            label1.Location = new Point(0x13, 0x76);
            label1.Name = "label1";
            label1.Size = new Size(0x7e, 13);
            label1.TabIndex = 3;
            label1.Text = "Attempt to view in format:";
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(0x97, 0x73);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x10f, 0x15);
            comboBox1.TabIndex = 4;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            base.Controls.Add(pictureBox1);
            base.Controls.Add(listView1);
            base.Controls.Add(comboBox1);
            base.Controls.Add(label1);
            base.Name = "BitmapViewer";
            base.Size = new Size(0x1bf, 0x229);
            ((ISupportInitialize) pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                map.RawInformation.ExternalMaps.OpenIOs();
                map.OpenIO();
                int tag = (int) listView1.FocusedItem.Tag;
                try
                {
                    pictureBox1.Image = bi.GeneratePreview(tag);
                }
                catch
                {
                    Bitmap image = new Bitmap(200, 200);
                    Graphics graphics = Graphics.FromImage(image);
                    graphics.Clear(Color.Gray);
                    string text = "No Preview Available";
                    SizeF ef = graphics.MeasureString(text, new Font(FontFamily.GenericSerif, 15f));
                    graphics.DrawString(text, new Font(FontFamily.GenericSerif, 15f), Brushes.Black, new PointF(100f - (ef.Width / 2f), 100f - (ef.Height / 2f)));
                    pictureBox1.Image = image;
                }
                map.CloseIO();
                map.RawInformation.ExternalMaps.CloseIOs();
            }
        }

        public void LoadBitmapTag(HaloMap Map, int TagIndex)
        {
            map = Map;
            Map.OpenIO();
            map.RawInformation.ExternalMaps.CreateIOs();
            map.RawInformation.ExternalMaps.OpenIOs();
            tagIndex = TagIndex;
            bi = BitmapFunctions.GetBitmapInfo(map, tagIndex);
            listView1.Items.Clear();
            for (int i = 0; i < bi.bitmapList.Count; i++)
            {
                string[] items = new string[] { i.ToString(), bi.bitmapList[i].Height.ToString(), bi.bitmapList[i].Width.ToString(), $"0x{bi.bitmapList[i].RawLength:X}", bi.bitmapList[i].Format.ToString(), bi.bitmapList[i].Type.ToString() };
                ListViewItem item = new ListViewItem(items) {
                    Tag = i
                };
                listView1.Items.Add(item);
            }
            if (listView1.Items.Count > 0)
            {
                listView1.FocusedItem = listView1.Items[0];
                listView1_SelectedIndexChanged(null, null);
            }
            map.RawInformation.ExternalMaps.CloseIOs();
            Map.CloseIO();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.BackColor == Color.White)
            {
                pictureBox1.BackColor = Color.Black;
            }
            else
            {
                pictureBox1.BackColor = Color.White;
            }
        }
    }
}

