namespace Ascension.Halo_Reach.Game.Tag_Editor.Controls.Dialog
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class tagblockview : Form
    {
        private IContainer components = null;

        public tagblockview()
        {
            InitializeComponent();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x20c, 0x177);
            base.Name = "tagblockview";
            base.ShowIcon = false;
            Text = "Researching";
            base.ResumeLayout(false);
        }
    }
}

