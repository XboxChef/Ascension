namespace Ascension.Halo_Reach.Game.Misc.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class Finder : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private IContainer components = null;
        private Panel panel1;
        public bool reading = false;
        private TextBox textBox1;
        private TextBox textBox10;
        private TextBox textBox11;
        private TextBox textBox12;
        private TextBox textBox13;
        private TextBox textBox14;
        private TextBox textBox15;
        private TextBox textBox16;
        private TextBox textBox17;
        private TextBox textBox18;
        private TextBox textBox19;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private TextBox textBox6;
        private TextBox textBox7;
        private TextBox textBox8;
        private TextBox textBox9;

        public Finder()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uint num = uint.Parse(textBox1.Text);
            textBox1.Text = (num + 0x48).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uint num = uint.Parse(textBox1.Text);
            textBox1.Text = (num - 0x48).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reading = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Finder_Load(object sender, EventArgs e)
        {
        }

        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            panel1 = new Panel();
            textBox19 = new TextBox();
            textBox16 = new TextBox();
            textBox13 = new TextBox();
            textBox10 = new TextBox();
            textBox7 = new TextBox();
            textBox4 = new TextBox();
            textBox18 = new TextBox();
            textBox17 = new TextBox();
            textBox15 = new TextBox();
            textBox14 = new TextBox();
            textBox12 = new TextBox();
            textBox11 = new TextBox();
            textBox9 = new TextBox();
            textBox8 = new TextBox();
            textBox6 = new TextBox();
            textBox5 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            panel1.SuspendLayout();
            base.SuspendLayout();
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(80, 20);
            button1.TabIndex = 0;
            button1.Text = "Start Reading";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox1.Location = new Point(0x62, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 20);
            textBox1.TabIndex = 1;
            panel1.BackColor = Color.Silver;
            panel1.Controls.Add(textBox19);
            panel1.Controls.Add(textBox16);
            panel1.Controls.Add(textBox13);
            panel1.Controls.Add(textBox10);
            panel1.Controls.Add(textBox7);
            panel1.Controls.Add(textBox4);
            panel1.Controls.Add(textBox18);
            panel1.Controls.Add(textBox17);
            panel1.Controls.Add(textBox15);
            panel1.Controls.Add(textBox14);
            panel1.Controls.Add(textBox12);
            panel1.Controls.Add(textBox11);
            panel1.Controls.Add(textBox9);
            panel1.Controls.Add(textBox8);
            panel1.Controls.Add(textBox6);
            panel1.Controls.Add(textBox5);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Location = new Point(12, 0x26);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x27d, 0x51);
            panel1.TabIndex = 2;
            textBox19.Location = new Point(0x216, 0x37);
            textBox19.Name = "textBox19";
            textBox19.Size = new Size(100, 20);
            textBox19.TabIndex = 0;
            textBox16.Location = new Point(0x1ac, 0x37);
            textBox16.Name = "textBox16";
            textBox16.Size = new Size(100, 20);
            textBox16.TabIndex = 0;
            textBox13.Location = new Point(0x142, 0x37);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(100, 20);
            textBox13.TabIndex = 0;
            textBox10.Location = new Point(0xd8, 0x37);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(100, 20);
            textBox10.TabIndex = 0;
            textBox7.Location = new Point(110, 0x37);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 20);
            textBox7.TabIndex = 0;
            textBox4.Location = new Point(4, 0x37);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 20);
            textBox4.TabIndex = 0;
            textBox18.Location = new Point(0x216, 0x1d);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(100, 20);
            textBox18.TabIndex = 0;
            textBox17.Location = new Point(0x216, 3);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(100, 20);
            textBox17.TabIndex = 0;
            textBox15.Location = new Point(0x1ac, 0x1d);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(100, 20);
            textBox15.TabIndex = 0;
            textBox14.Location = new Point(0x1ac, 3);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(100, 20);
            textBox14.TabIndex = 0;
            textBox12.Location = new Point(0x142, 0x1d);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(100, 20);
            textBox12.TabIndex = 0;
            textBox11.Location = new Point(0x142, 3);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(100, 20);
            textBox11.TabIndex = 0;
            textBox9.Location = new Point(0xd8, 0x1d);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 20);
            textBox9.TabIndex = 0;
            textBox8.Location = new Point(0xd8, 3);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 20);
            textBox8.TabIndex = 0;
            textBox6.Location = new Point(110, 0x1d);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 20);
            textBox6.TabIndex = 0;
            textBox5.Location = new Point(110, 3);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 20);
            textBox5.TabIndex = 0;
            textBox3.Location = new Point(4, 0x1d);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 20);
            textBox3.TabIndex = 0;
            textBox2.Location = new Point(4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 20);
            textBox2.TabIndex = 0;
            button2.Location = new Point(0xcc, 11);
            button2.Name = "button2";
            button2.Size = new Size(20, 20);
            button2.TabIndex = 0;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button3.Location = new Point(0xdf, 11);
            button3.Name = "button3";
            button3.Size = new Size(20, 20);
            button3.TabIndex = 0;
            button3.Text = "-";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            button4.Location = new Point(0x239, 12);
            button4.Name = "button4";
            button4.Size = new Size(80, 20);
            button4.TabIndex = 0;
            button4.Text = "Stop Reading";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(660, 0x84);
            base.Controls.Add(panel1);
            base.Controls.Add(textBox1);
            base.Controls.Add(button3);
            base.Controls.Add(button2);
            base.Controls.Add(button4);
            base.Controls.Add(button1);
            base.Name = "Finder";
            Text = "Finder";
            base.Load += new EventHandler(Finder_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

