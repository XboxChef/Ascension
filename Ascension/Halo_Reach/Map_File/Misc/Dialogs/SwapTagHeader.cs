namespace Ascension.Halo_Reach.Map_File.Misc.Dialogs
{
    using Ascension.Communications.Output;
    using Ascension.Forms;
    using Ascension.Halo_Reach.Plugins;
    using Ascension.Settings;
    using HaloReach3d.Helpers;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class SwapTagHeader : Form
    {
        private HaloMap.TagItem _meta;
        private Button button1;
        private ComboBox comboBox1;
        private IContainer components = null;
        private MapForm form;
        private Label label1;

        public SwapTagHeader(HaloMap.TagItem meta, MapForm mf)
        {
            InitializeComponent();
            Meta = meta;
            form = mf;
            foreach (TagHierarchy.TagHClass class2 in Meta.Map.Tag_Hierarchy.TagClasses)
            {
                if (class2.TagClass == Meta.Class)
                {
                    foreach (TagHierarchy.TagHName name in class2.Tags)
                    {
                        comboBox1.Items.Add(name.TagName);
                    }
                }
            }
            comboBox1.Sorted = true;
            comboBox1.SelectedIndex = 0;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string fileName = AppSettings.Settings.PluginPath + Meta.Class.Replace(" ", "").Replace("<", "_").Replace(">", "_") + ".asc";
            XmlParser parser = new XmlParser();
            parser.ParsePlugin(fileName);
            int headerSize = parser.HeaderSize;
            HaloMap.TagItem item = Meta.Map.Index_Items[Meta.Map.GetTagIndexByClassAndName(Meta.Class, comboBox1.Text)];
            Meta.Map.OpenIO();
            Meta.Map.IO.In.BaseStream.Position = item.Offset;
            byte[] buffer = Meta.Map.IO.In.ReadBytes(headerSize);
            Meta.Map.IO.Out.BaseStream.Position = Meta.Offset;
            Meta.Map.IO.Out.Write(buffer);
            Meta.Map.CloseIO();
            base.Close();
            OutputMessenger.OutputMessage("Tag header successfully swapped to: \"" + Meta.Name + "." + Meta.Class + "\"", (Control) form);
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
            label1 = new Label();
            comboBox1 = new ComboBox();
            button1 = new Button();
            base.SuspendLayout();
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(0x5b, 13);
            label1.TabIndex = 0;
            label1.Text = "Tag To Swap To:";
            comboBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(15, 0x19);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x19c, 0x15);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            button1.Location = new Point(15, 0x34);
            button1.Name = "button1";
            button1.Size = new Size(0x19c, 0x17);
            button1.TabIndex = 3;
            button1.Text = "Swap";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(buttonX1_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1b7, 0x53);
            base.Controls.Add(button1);
            base.Controls.Add(comboBox1);
            base.Controls.Add(label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SwapTagHeader";
            Text = "Tag Header Swapper";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public HaloMap.TagItem Meta
        {
            get
            {
                return _meta;
            }
            set
            {
                _meta = value;
            }
        }
    }
}

