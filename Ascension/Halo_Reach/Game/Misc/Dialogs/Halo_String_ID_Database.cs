namespace Ascension.Halo_Reach.Game.Misc.Dialogs
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class Halo_String_ID_Database : Form
    {
        private Button button1;
        private IContainer components = null;
        private ListBox listBox1;
        private Panel panel1;
        private TextBox textBox1;

        public Halo_String_ID_Database()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader(Application.StartupPath + @"\halo_string_id_database.txt");
            string str = "";
            while ((str = reader.ReadLine()) != null)
            {
                string item = str;
                listBox1.Items.Add(item);
            }
            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            StreamReader reader = new StreamReader(Application.StartupPath + @"\halo_string_id_database.txt");
            string str = "";
            while ((str = reader.ReadLine()) != null)
            {
                string item = str;
                if (item.Contains(textBox1.Text))
                {
                    listBox1.Items.Add(item);
                }
            }
            reader.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text + listBox1.SelectedItem.ToString();
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
            listBox1 = new ListBox();
            panel1 = new Panel();
            button1 = new Button();
            textBox1 = new TextBox();
            panel1.SuspendLayout();
            base.SuspendLayout();
            listBox1.Dock = DockStyle.Fill;
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(0, 0x29);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(0x24f, 0x13f);
            listBox1.TabIndex = 0;
            listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x24f, 0x29);
            panel1.TabIndex = 1;
            button1.Location = new Point(0x1fa, 12);
            button1.Name = "button1";
            button1.Size = new Size(0x4b, 20);
            button1.TabIndex = 1;
            button1.Text = "Filter";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(0x1e8, 20);
            textBox1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x24f, 360);
            base.Controls.Add(listBox1);
            base.Controls.Add(panel1);
            base.Name = "Halo_String_ID_Database";
            Text = "Halo String ID Database";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            base.ResumeLayout(false);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

