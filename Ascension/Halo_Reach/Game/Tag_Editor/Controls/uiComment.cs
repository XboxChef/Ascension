namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Halo_Reach.Values;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiComment : UserControl
    {
        private mComment _valuedata;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private RichTextBox lblDescription;
        private Label lblTitle;

        public uiComment(mComment val)
        {
            InitializeComponent();
            ValueData = val;
            lblTitle.Text = ValueData.Title;
            lblDescription.Text = ValueData.Description;
            if (ValueData.Description == "")
            {
                base.Size = new Size(0x192, 0x1a);
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
            components = new Container();
            lblTitle = new Label();
            lblDescription = new RichTextBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            base.SuspendLayout();
            lblTitle.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Bold, GraphicsUnit.Pixel, 0);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(7, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(0x184, 15);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "TITLE OF COMMENT HERE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblDescription.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            lblDescription.BackColor = SystemColors.ActiveCaption;
            lblDescription.BorderStyle = BorderStyle.None;
            lblDescription.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblDescription.ForeColor = Color.Black;
            lblDescription.Location = new Point(8, 0x17);
            lblDescription.Name = "lblDescription";
            lblDescription.ReadOnly = true;
            lblDescription.Size = new Size(0x183, 0x62);
            lblDescription.TabIndex = 1;
            lblDescription.Text = "";
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x3d, 4);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.ActiveCaption;
            base.BorderStyle = BorderStyle.FixedSingle;
            base.Controls.Add(lblTitle);
            base.Controls.Add(lblDescription);
            base.Name = "uiComment";
            base.Size = new Size(0x192, 0x7f);
            base.ResumeLayout(false);
        }

        public mComment ValueData
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

