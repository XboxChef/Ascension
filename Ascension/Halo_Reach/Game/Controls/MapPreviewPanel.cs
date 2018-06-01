namespace Ascension.Halo_Reach.Game.Controls
{
    using Ascension.Halo_Reach.Map_Package.Info;
    using Ascension.Halo_Reach.Map_Package.Package_Classes;
    using Ascension.Settings;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class MapPreviewPanel : UserControl
    {
        private IContainer components = null;
        private Label label1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;

        public MapPreviewPanel(HaloMap Map)
        {
            InitializeComponent();
            label1.Text = Map.Map_Header.internalName;
            if (AppSettings.Settings.MapInfo_Folder != "")
            {
                string[] files = Directory.GetFiles(AppSettings.Settings.MapInfo_Folder);
                foreach (string str in files)
                {
                    try
                    {
                        FileStream stream = new FileStream(str, FileMode.Open);
                        InfoFile file = new InfoFile(stream);
                        stream.Close();
                        if (file.MapFileName == Map.Map_Header.internalName)
                        {
                            label1.Text = file.EnglishName;
                            richTextBox1.Text = file.EnglishDescription;
                            if (AppSettings.Settings.Images_Folder != "")
                            {
                                string path = AppSettings.Settings.Images_Folder + @"\" + file.MapImageFileName + ".blf";
                                if (File.Exists(path))
                                {
                                    stream = new FileStream(path, FileMode.Open);
                                    BLFImageFile file2 = new BLFImageFile(stream);
                                    stream.Close();
                                    pictureBox1.Image = file2.BLFImage;
                                }
                            }
                            break;
                        }
                    }
                    catch
                    {
                    }
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

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            label1 = new Label();
            richTextBox1 = new RichTextBox();
            ((ISupportInitialize) pictureBox1).BeginInit();
            base.SuspendLayout();
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(0x2f, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 160);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            label1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(0, 0xa4);
            label1.Name = "label1";
            label1.Size = new Size(500, 0x1b);
            label1.TabIndex = 1;
            label1.Text = "{Map_Name}";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            richTextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            richTextBox1.BackColor = SystemColors.Control;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(3, 0xc2);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(0x1df, 0xa6);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(richTextBox1);
            base.Controls.Add(label1);
            base.Controls.Add(pictureBox1);
            base.Name = "MapPreviewPanel";
            base.Size = new Size(500, 0x16b);
            ((ISupportInitialize) pictureBox1).EndInit();
            base.ResumeLayout(false);
        }
    }
}

