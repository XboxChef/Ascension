namespace Ascension.Halo_Reach.Plugins
{
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class PluginGeneratorForm : Form
    {
        private PluginLayoutCreator _pluginlayoutcreator;
        private Button button1;
        private Button button2;
        private Button button3;
        private CheckBox checkBox1;
        private IContainer components = null;
        private Label label1;
        private Label label2;
        private TextBox txtOpenMap;
        private TextBox txtOutputFolder;

        public PluginGeneratorForm()
        {
            InitializeComponent();
        }

        private void btnGeneratePlugins_Click(object sender, EventArgs e)
        {
            if (!((txtOpenMap.Text == "") | (txtOutputFolder.Text == "")))
            {
                if (!checkBox1.Checked)
                {
                    Plugin_Layout_Creator = new PluginLayoutCreator(new HaloMap(txtOpenMap.Text));
                    Plugin_Layout_Creator.GeneratePlugins(txtOutputFolder.Text, true);
                    MessageBox.Show("Done.");
                }
                else
                {
                    string[] strArray = txtOpenMap.Text.Split(new char[] { '\\' });
                    string[] files = Directory.GetFiles(txtOpenMap.Text.Substring(0, txtOpenMap.Text.Length - strArray[strArray.Length - 1].Length));
                    List<PluginLayoutCreator.MetaHeader_DataBlock> list = new List<PluginLayoutCreator.MetaHeader_DataBlock>();
                    foreach (string str2 in files)
                    {
                        if (str2.Contains(".map") && !str2.Contains("shared.map"))
                        {
                            try
                            {
                                HaloMap map = new HaloMap(str2);
                                PluginLayoutCreator creator = new PluginLayoutCreator(map);
                                list.AddRange(creator.GetMetaHeaderDataBlocksMapped(true, true, true));
                            }
                            catch
                            {
                            }
                        }
                    }
                    new PluginLayoutCreator(null) { MetaHeader_Data_Blocks = list }.GeneratePlugins(txtOutputFolder.Text, false);
                }
            }
        }

        private void btnOpenMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = "Halo 3 Map Files(.map)|*.map"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtOpenMap.Text = dialog.FileName;
            }
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = dialog.SelectedPath;
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
            label2 = new Label();
            label1 = new Label();
            txtOpenMap = new TextBox();
            txtOutputFolder = new TextBox();
            checkBox1 = new CheckBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            base.SuspendLayout();
            label2.AutoSize = true;
            label2.Location = new Point(12, 0x30);
            label2.Name = "label2";
            label2.Size = new Size(0x6f, 13);
            label2.TabIndex = 12;
            label2.Text = "Output Plugins Folder:";
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(0x54, 13);
            label1.TabIndex = 0;
            label1.Text = "Open a map file:";
            txtOpenMap.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtOpenMap.Location = new Point(15, 0x19);
            txtOpenMap.Name = "txtOpenMap";
            txtOpenMap.ReadOnly = true;
            txtOpenMap.Size = new Size(0xdb, 20);
            txtOpenMap.TabIndex = 0x11;
            txtOutputFolder.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            txtOutputFolder.Location = new Point(15, 0x40);
            txtOutputFolder.Name = "txtOutputFolder";
            txtOutputFolder.ReadOnly = true;
            txtOutputFolder.Size = new Size(0xdb, 20);
            txtOutputFolder.TabIndex = 0x12;
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(15, 90);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(0x95, 0x11);
            checkBox1.TabIndex = 0x11;
            checkBox1.Text = "Use all maps in map folder";
            checkBox1.UseVisualStyleBackColor = true;
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            button1.Location = new Point(15, 0x71);
            button1.Name = "button1";
            button1.Size = new Size(0x109, 0x17);
            button1.TabIndex = 0x10;
            button1.Text = "Generate Plugins";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(btnGeneratePlugins_Click);
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button2.Location = new Point(240, 0x19);
            button2.Name = "button2";
            button2.Size = new Size(40, 20);
            button2.TabIndex = 0x13;
            button2.Text = "...";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(btnOpenMap_Click);
            button3.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button3.Location = new Point(240, 0x3f);
            button3.Name = "button3";
            button3.Size = new Size(40, 20);
            button3.TabIndex = 20;
            button3.Text = "...";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(btnOutputFolder_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x98);
            base.Controls.Add(button3);
            base.Controls.Add(button2);
            base.Controls.Add(button1);
            base.Controls.Add(checkBox1);
            base.Controls.Add(txtOutputFolder);
            base.Controls.Add(txtOpenMap);
            base.Controls.Add(label1);
            base.Controls.Add(label2);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PluginGeneratorForm";
            Text = "Plugin Generator";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        public PluginLayoutCreator Plugin_Layout_Creator
        {
            get
            {
                return _pluginlayoutcreator;
            }
            set
            {
                _pluginlayoutcreator = value;
            }
        }
    }
}

