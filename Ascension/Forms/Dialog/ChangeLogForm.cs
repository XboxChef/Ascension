namespace Ascension.Forms.Dialog
{
    using Ascension.Details;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChangeLogForm : Form
    {
        private IContainer components = null;
        private Label lblTitle;
        private PictureBox pbxLogo;
        private RichTextBox richTextBox1;

        public ChangeLogForm(bool justUpdated)
        {
            InitializeComponent();
            if (justUpdated)
            {
                lblTitle.Text = "Update applied!";
                richTextBox1.Text = ChangeLogs.GetChangeLogString();
                Text = "Update successfully applied.";
            }
            else
            {
                richTextBox1.Text = ChangeLogs.GetAllChangeLogsString();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChangeLogForm));
            richTextBox1 = new RichTextBox();
            lblTitle = new Label();
            pbxLogo = new PictureBox();
            ((ISupportInitialize) pbxLogo).BeginInit();
            base.SuspendLayout();
            richTextBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Location = new Point(0xa9, 0x25);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(0x1c7, 0xf6);
            richTextBox1.TabIndex = 5;
            richTextBox1.Text = "";
            lblTitle.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            lblTitle.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(0xa9, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0x1c7, 20);
            lblTitle.TabIndex = 4;
            lblTitle.Text = "Ascension Build Logs";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            pbxLogo.Image = (Image) manager.GetObject("pbxLogo.Image");
            pbxLogo.Location = new Point(10, 14);
            pbxLogo.Name = "pbxLogo";
            pbxLogo.Size = new Size(0x98, 150);
            pbxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pbxLogo.TabIndex = 3;
            pbxLogo.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x27b, 0x129);
            base.Controls.Add(richTextBox1);
            base.Controls.Add(lblTitle);
            base.Controls.Add(pbxLogo);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.MinimizeBox = false;
            base.Name = "ChangeLogForm";
            Text = "Ascension Build Logs";
            ((ISupportInitialize) pbxLogo).EndInit();
            base.ResumeLayout(false);
        }
    }
}

