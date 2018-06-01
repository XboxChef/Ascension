namespace Ascension.Forms.Dialog
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class AboutForm : Form
    {
        private IContainer components = null;
        private LinkLabel linkLabel1;
        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;

        public AboutForm()
        {
            InitializeComponent();
            linkLabel1.Text = Application.StartupPath;
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(AboutForm));
            pictureBox1 = new PictureBox();
            richTextBox1 = new RichTextBox();
            linkLabel1 = new LinkLabel();
            ((ISupportInitialize) pictureBox1).BeginInit();
            base.SuspendLayout();
            pictureBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(0x264, 0x57);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            richTextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            richTextBox1.BackColor = SystemColors.Control;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(12, 0x69);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(0x24c, 0xf7);
            richTextBox1.TabIndex = 6;
            richTextBox1.Text = manager.GetString("richTextBox1.Text");
            linkLabel1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(9, 0x167);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(90, 13);
            linkLabel1.TabIndex = 8;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Program Location";
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x264, 0x17d);
            base.Controls.Add(linkLabel1);
            base.Controls.Add(richTextBox1);
            base.Controls.Add(pictureBox1);
            base.Name = "AboutForm";
            Text = "About..";
            ((ISupportInitialize) pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Process { StartInfo = { FileName = linkLabel1.Text } }.Start();
        }
    }
}

