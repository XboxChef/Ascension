namespace Ascension.Forms.Dialog
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class Tag_merger : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;

        public Tag_merger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "TagList A";
            openFileDialog.InitialDirectory = "c:\\Desktop";
            openFileDialog.Filter = "Tag (*.taglist*)|*.taglist*";
            openFileDialog.InitialDirectory = Application.StartupPath + "\\Tag Lists";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            textBox1.Text = openFileDialog.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "TagList B";
            openFileDialog.InitialDirectory = "c:\\Desktop";
            openFileDialog.Filter = "Tag (*.taglist*)|*.taglist*";
            openFileDialog.InitialDirectory = Application.StartupPath + "\\Tag Lists";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;
            textBox2.Text = openFileDialog.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                File.Copy(textBox1.Text, textBox1.Text + ".bak", true);
                string[] collection = File.ReadAllLines(textBox1.Text);
                string[] strArray2 = File.ReadAllLines(textBox2.Text);
                List<string> list = new List<string>(collection);
                List<string> list2 = new List<string>(strArray2);
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                List<string> list3 = new List<string>();
                for (int i = 0; i < list.Count; i++)
                {
                    string[] strArray3 = list[i].Split(new char[] { '=' });
                    dictionary.Add(strArray3[0], strArray3[1]);
                }
                for (int j = 0; j < list2.Count; j++)
                {
                    string[] strArray4 = list2[j].Split(new char[] { '=' });
                    dictionary2.Add(strArray4[0], strArray4[1]);
                }
                foreach (KeyValuePair<string, string> pair in dictionary)
                {
                    dictionary2[pair.Key] = pair.Value;
                }
                foreach (KeyValuePair<string, string> pair in dictionary2)
                {
                    if (pair.Value == "0")
                    {
                        dictionary2.Remove(pair.Key);
                    }
                }
                foreach (KeyValuePair<string, string> pair2 in dictionary2)
                {
                    list3.Add(pair2.Key + "=" + pair2.Value);
                }
                OutputQueryResults(list3);
                MessageBox.Show("Lists Merged");
            }
            catch
            {
                MessageBox.Show("Couldn't Merge Lists");
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label3 = new Label();
            base.SuspendLayout();
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(0x34, 13);
            label1.TabIndex = 0;
            label1.Text = "TagList A";
            label2.AutoSize = true;
            label2.Location = new Point(12, 0x23);
            label2.Name = "label2";
            label2.Size = new Size(0x34, 13);
            label2.TabIndex = 0;
            label2.Text = "TagList B";
            textBox1.Location = new Point(70, 6);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(0x105, 20);
            textBox1.TabIndex = 1;
            textBox2.Location = new Point(70, 0x20);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(0x105, 20);
            textBox2.TabIndex = 1;
            button1.Location = new Point(0x150, 6);
            button1.Name = "button1";
            button1.Size = new Size(0x18, 20);
            button1.TabIndex = 2;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click);
            button2.Location = new Point(0x150, 0x20);
            button2.Name = "button2";
            button2.Size = new Size(0x18, 20);
            button2.TabIndex = 2;
            button2.Text = "...";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button3.Location = new Point(0x16c, 6);
            button3.Name = "button3";
            button3.Size = new Size(0x2f, 0x2e);
            button3.TabIndex = 3;
            button3.Text = "Merge";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            label3.Location = new Point(12, 0x40);
            label3.Name = "label3";
            label3.Size = new Size(0x18f, 30);
            label3.TabIndex = 4;
            label3.Text = "Tags will be moved from List B into List A. A backup will be made of List A.\r\nBoth taglists must be from the same map.";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1a7, 0x65);
            base.Controls.Add(label3);
            base.Controls.Add(button3);
            base.Controls.Add(button2);
            base.Controls.Add(button1);
            base.Controls.Add(textBox2);
            base.Controls.Add(textBox1);
            base.Controls.Add(label2);
            base.Controls.Add(label1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Tag_merger";
            Text = "Taglist Merger";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void OutputQueryResults(List<string> list)
        {
            StreamWriter writer = new StreamWriter(textBox1.Text);
            foreach (string str in list)
            {
                writer.WriteLine(str.ToString());
            }
            writer.Close();
        }
    }
}

