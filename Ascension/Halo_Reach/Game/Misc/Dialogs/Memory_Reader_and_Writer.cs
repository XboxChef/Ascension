namespace Ascension.Halo_Reach.Game.Misc.Dialogs
{
    using Ascension.Halo_Reach.Game.Misc;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Memory_Reader_and_Writer : Form
    {
        private Button button1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private Panel panel2;
        private uiXvalue uiXvalue1;

        public Memory_Reader_and_Writer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uiXvalue xvalue = new uiXvalue {
                Dock = DockStyle.Top
            };
            panel1.Controls.Add(xvalue);
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
            panel1 = new Panel();
            uiXvalue1 = new uiXvalue();
            panel2 = new Panel();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            base.SuspendLayout();
            panel1.AutoScroll = true;
            panel1.Controls.Add(uiXvalue1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0x2e);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x20f, 0xda);
            panel1.TabIndex = 0;
            uiXvalue1.Dock = DockStyle.Top;
            uiXvalue1.Location = new Point(0, 0);
            uiXvalue1.Name = "uiXvalue1";
            uiXvalue1.Size = new Size(0x20f, 0x20);
            uiXvalue1.TabIndex = 0;
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(button1);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(0x20f, 0x2e);
            panel2.TabIndex = 0;
            label3.AutoSize = true;
            label3.Location = new Point(1, 0x1d);
            label3.Name = "label3";
            label3.Size = new Size(0x1f, 13);
            label3.TabIndex = 1;
            label3.Text = "Type";
            label2.AutoSize = true;
            label2.Location = new Point(0xb2, 30);
            label2.Name = "label2";
            label2.Size = new Size(0x23, 13);
            label2.TabIndex = 1;
            label2.Text = "Offset";
            label1.AutoSize = true;
            label1.Location = new Point(0x51, 30);
            label1.Name = "label1";
            label1.Size = new Size(0x22, 13);
            label1.TabIndex = 1;
            label1.Text = "Value";
            button1.Location = new Point(2, 3);
            button1.Name = "button1";
            button1.Size = new Size(0x4b, 0x17);
            button1.TabIndex = 0;
            button1.Text = "Add New";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x20f, 0x108);
            base.Controls.Add(panel1);
            base.Controls.Add(panel2);
            base.Name = "Memory_Reader_and_Writer";
            base.ShowIcon = false;
            Text = "Memory Writer";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            base.ResumeLayout(false);
        }
    }
}

