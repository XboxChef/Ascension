namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls
{
    using Ascension.Halo_Reach.Values;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiUnused : UserControl
    {
        private mUnused _unuseddata;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblUnusedDescription;

        public uiUnused()
        {
            components = null;
            InitializeComponent();
        }

        public uiUnused(mUnused unuseddata)
        {
            components = null;
            InitializeComponent();
            UnusedData = unuseddata;
            lblUnusedDescription.Text = string.Concat(new object[] { "unused data {offset =", UnusedData.Offset, ", size=", UnusedData.Size, "}" });
        }

        private void displayInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editValueInPluginToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            components = new Container();
            lblUnusedDescription = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            base.SuspendLayout();
            lblUnusedDescription.BackColor = SystemColors.Control;
            lblUnusedDescription.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUnusedDescription.ForeColor = SystemColors.ControlText;
            lblUnusedDescription.Location = new Point(4, 3);
            lblUnusedDescription.Name = "lblUnusedDescription";
            lblUnusedDescription.Size = new Size(0x141, 0x19);
            lblUnusedDescription.TabIndex = 2;
            lblUnusedDescription.Text = "unused data";
            lblUnusedDescription.TextAlign = ContentAlignment.MiddleLeft;
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x99, 0x1a);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.Controls.Add(lblUnusedDescription);
            base.Name = "uiUnused";
            base.Size = new Size(400, 30);
            base.ResumeLayout(false);
        }

        public mUnused UnusedData
        {
            get
            {
                return _unuseddata;
            }
            set
            {
                _unuseddata = value;
            }
        }
    }
}

