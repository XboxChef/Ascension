namespace Ascension.Halo_Reach.Game.Misc
{
    using Ascension.Settings;
    using HaloReach3d.IO;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class uiXvalue : UserControl
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private IContainer components = null;
        public bool Continue = false;
        private TextBox textBox1;
        private TextBox textBox2;

        public uiXvalue()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Continue = false;
            base.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uint num;
            while (Continue)
            {
                num = uint.Parse(textBox2.Text);
                uint num2 = num + 4;
                textBox2.Text = num2.ToString();
                Write();
                Application.DoEvents();
            }
            if (!Continue)
            {
                num = uint.Parse(textBox2.Text);
                textBox2.Text = (num + 4).ToString();
                Write();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            uint num;
            while (Continue)
            {
                num = uint.Parse(textBox2.Text);
                uint num2 = num - 4;
                textBox2.Text = num2.ToString();
                Write();
                Application.DoEvents();
            }
            if (!Continue)
            {
                num = uint.Parse(textBox2.Text);
                textBox2.Text = (num - 4).ToString();
                Write();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                try
                {
                }
                catch
                {
                    MessageBox.Show("Out of range.");
                    Continue = false;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Write();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Continue = true;
            }
            if (!checkBox1.Checked)
            {
                Continue = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void InitializeComponent()
        {
            button1 = new Button();
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            button5 = new Button();
            checkBox1 = new CheckBox();
            button4 = new Button();
            base.SuspendLayout();
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button1.Location = new Point(0x27e, 6);
            button1.Name = "button1";
            button1.Size = new Size(0x39, 20);
            button1.TabIndex = 0;
            button1.Text = "Remove";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "bytes", "int16s", "uint16s", "int", "uint", "float" });
            comboBox1.Location = new Point(3, 6);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x48, 0x15);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            textBox1.Location = new Point(0x51, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0x5f, 20);
            textBox1.TabIndex = 2;
            textBox1.Text = "0";
            textBox2.Location = new Point(0xb3, 6);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(0x5f, 20);
            textBox2.TabIndex = 2;
            textBox2.Text = "0";
            button2.Location = new Point(280, 6);
            button2.Name = "button2";
            button2.Size = new Size(20, 20);
            button2.TabIndex = 0;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button3.Location = new Point(0x12b, 6);
            button3.Name = "button3";
            button3.Size = new Size(20, 20);
            button3.TabIndex = 0;
            button3.Text = "-";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            button5.Location = new Point(0x144, 6);
            button5.Name = "button5";
            button5.Size = new Size(0x2a, 20);
            button5.TabIndex = 0;
            button5.Text = "Write";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(0x174, 8);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(0x56, 0x11);
            checkBox1.TabIndex = 3;
            checkBox1.Text = "Continuously";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            button4.Location = new Point(0x1f1, 5);
            button4.Name = "button4";
            button4.Size = new Size(0x4b, 0x17);
            button4.TabIndex = 4;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(button4);
            base.Controls.Add(checkBox1);
            base.Controls.Add(textBox2);
            base.Controls.Add(textBox1);
            base.Controls.Add(comboBox1);
            base.Controls.Add(button3);
            base.Controls.Add(button2);
            base.Controls.Add(button5);
            base.Controls.Add(button1);
            base.Name = "uiXvalue";
            base.Size = new Size(0x2ba, 0x22);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void Write()
        {
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                try
                {
                }
                catch
                {
                    MessageBox.Show("Out of range.");
                    Continue = false;
                }
            }
        }

        public void WriteTypes(EndianIO IO)
        {
            uint num = uint.Parse(textBox2.Text);
            IO.Out.BaseStream.Position = num;
            if (comboBox1.SelectedIndex == 0)
            {
                IO.Out.Write(byte.Parse(textBox1.Text));
            }
            if (comboBox1.SelectedIndex == 1)
            {
                IO.Out.Write(short.Parse(textBox1.Text));
            }
            if (comboBox1.SelectedIndex == 2)
            {
                IO.Out.Write(ushort.Parse(textBox1.Text));
            }
            if (comboBox1.SelectedIndex == 3)
            {
                IO.Out.Write(uint.Parse(textBox1.Text));
            }
            if (comboBox1.SelectedIndex == 4)
            {
                IO.Out.Write(int.Parse(textBox1.Text));
            }
            if (comboBox1.SelectedIndex == 5)
            {
                IO.Out.Write(float.Parse(textBox1.Text));
            }
        }
    }
}

