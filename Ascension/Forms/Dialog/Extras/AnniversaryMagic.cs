namespace Ascension.Forms.Dialog.Extras
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class AnniversaryMagic : Form
    {
        private Button button1;
        private ComboBox comboBox1;
        private IContainer components = null;
        public Dictionary<string, int> MapMagics = new Dictionary<string, int>();
        private TextBox textBox1;
        private TextBox textBox2;

        public AnniversaryMagic()
        {
            InitializeComponent();
            MapMagics["A10"] = 0x4d3bfc0;
            MapMagics["A30"] = 0x15a256c;
            MapMagics["A50"] = 0x23abf14;
            MapMagics["B30"] = 0x1312728;
            MapMagics["B40"] = 0x3a4eb00;
            MapMagics["C10"] = 0x2b6cc70;
            MapMagics["C20"] = 0x28fb414;
            MapMagics["C40"] = 0x3b13810;
            MapMagics["D40"] = 0x4a3676c;
            foreach (KeyValuePair<string, int> pair in MapMagics)
            {
                comboBox1.Items.Add(pair.Key);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int num;
                if (textBox1.Text.StartsWith("0x"))
                {
                    num = Convert.ToInt32(textBox1.Text, 0x10) - MapMagics[comboBox1.Text];
                }
                else
                {
                    num = int.Parse(textBox1.Text) - MapMagics[comboBox1.Text];
                }
                textBox2.Text = "0x" + num.ToString("X");
            }
            catch
            {
                MessageBox.Show("Couldnt convert offset. Possible wrong Decimal Size or Wrong Hex Format 0x...");
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
            comboBox1 = new ComboBox();
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            base.SuspendLayout();
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x11c, 0x15);
            comboBox1.TabIndex = 0;
            comboBox1.Text = "Maps";
            textBox1.Location = new Point(12, 0x27);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0x11c, 20);
            textBox1.TabIndex = 1;
            textBox1.Text = "Offset";
            button1.Location = new Point(12, 0x41);
            button1.Name = "button1";
            button1.Size = new Size(0x11c, 20);
            button1.TabIndex = 2;
            button1.Text = "Magic Offset";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox2.Location = new Point(12, 0x5b);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(0x11c, 20);
            textBox2.TabIndex = 3;
            textBox2.Text = "Memory Offset";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x134, 0x7c);
            base.Controls.Add(textBox2);
            base.Controls.Add(button1);
            base.Controls.Add(textBox1);
            base.Controls.Add(comboBox1);
            base.Name = "AnniversaryMagic";
            Text = "AnniversaryMagic";
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}

