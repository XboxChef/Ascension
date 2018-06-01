namespace Ascension.Forms.Dialog
{
    using Ascension.Halo_Reach;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class ScreenshotForm : Form
    {
        private ToolStripMenuItem closeToolStripMenuItem;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private PictureBox pictureBox1;
        private ToolStripMenuItem saveToolStripMenuItem;

        public ScreenshotForm()
        {
            InitializeComponent();
            Text = "Screenshot: " + DateTime.Now;
            XboxDebugCommunicator xdc = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
            xdc.Connect();
            Image image = XboxScreenshot.TakeScreenshot(xdc);
            try
            {
                xdc.Disconnect();
            }
            catch
            {
            }
            pictureBox1.Image = XboxScreenshot.ResizeImage((Bitmap) image);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            if (AppSettings.Settings.SaveScreenshots)
            {
                if (AppSettings.Settings.ScreenShotLocation == "")
                {
                    MessageBox.Show("Please set a Save Location");
                    new SettingsForm().ShowDialog();
                }
                string str = $"{DateTime.Now:yyyy-MM-dd,hh-mm-ss}";
                pictureBox1.Image.Save(AppSettings.Settings.ScreenShotLocation + str + ".jpg", ImageFormat.Jpeg);
            }
            xdc.Disconnect();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            base.Close();
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
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            saveToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            ((ISupportInitialize) pictureBox1).BeginInit();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.ContextMenuStrip = contextMenuStrip1;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(640, 480);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { saveToolStripMenuItem, closeToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x68, 0x30);
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(0x67, 0x16);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += new EventHandler(saveToolStripMenuItem_Click);
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(0x67, 0x16);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += new EventHandler(closeToolStripMenuItem_Click);
            base.ClientSize = new Size(640, 480);
            base.Controls.Add(pictureBox1);
            base.Name = "ScreenshotForm";
            ((ISupportInitialize) pictureBox1).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = "PNG Image File (*.png)|*.png|JPEG File (*.jpg)|*.jpg|BMP File (*.bmp)|*.bmp|TIFF File (*.tiff)|*.tiff",
                FilterIndex = 0
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                switch (dialog.FilterIndex)
                {
                    case 0:
                        pictureBox1.Image.Save(dialog.FileName, ImageFormat.Png);
                        break;

                    case 1:
                        pictureBox1.Image.Save(dialog.FileName, ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(dialog.FileName, ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox1.Image.Save(dialog.FileName, ImageFormat.Tiff);
                        break;
                }
            }
        }
    }
}

