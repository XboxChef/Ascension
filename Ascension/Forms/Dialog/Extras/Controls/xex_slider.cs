namespace Ascension.Forms.Dialog.Extras.Controls
{
    using Ascension;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class xex_slider : UserControl
    {
        private IContainer components = null;
        private Label label1;
        private uint Offset;
        private TrackBar trackBar1;
        private string Type;

        public xex_slider()
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
            label1 = new Label();
            trackBar1 = new TrackBar();
            trackBar1.BeginInit();
            base.SuspendLayout();
            label1.AutoSize = true;
            label1.Location = new Point(12, 11);
            label1.Name = "label1";
            label1.Size = new Size(0x21, 13);
            label1.TabIndex = 0;
            label1.Text = "name";
            trackBar1.Location = new Point(0xcc, 7);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(0x144, 0x2d);
            trackBar1.TabIndex = 1;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(trackBar1);
            base.Controls.Add(label1);
            base.Name = "xex_slider";
            base.Size = new Size(0x213, 0x23);
            trackBar1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void setValues(string name, uint offset, float start, float range, string type)
        {
            Offset = offset;
            label1.Text = name;
            Type = type;
            trackBar1.SetRange(((int) start) * 10, ((int) range) * 10);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float num = trackBar1.Value;
            num /= 10f;
            ((Form1) Application.OpenForms[0]).streampoke(Offset, Type, num.ToString());
        }
    }
}

