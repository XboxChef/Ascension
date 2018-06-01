namespace Ascension.Halo_Reach.Tag_Grid.Dialog
{
    using Ascension.Communications.Output;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.Map;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;

    public class IdentSwapper : Form
    {
        private ListView _identgrid;
        private HaloMap _map;
        private Button button1;
        private Button button2;
        private ComboBox cmbxClass;
        private ComboBox cmbxName;
        private IContainer components = null;
        private Panel panel1;

        public IdentSwapper(HaloMap map, ListView listView)
        {
            InitializeComponent();
            IdentGrid = listView;
            Map = map;
            LoadClasses();
            SelectItem(cmbxClass, listView.SelectedItems[0].SubItems[3].Text);
            SelectItem(cmbxName, listView.SelectedItems[0].SubItems[4].Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AppSettings.Settings.IP_and_XDK_Name == "")
            {
                OutputMessenger.OutputMessage("XDK Name not set. Please set it in settings before continuing.", this);
            }
            else
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                communicator.Connect();
                communicator.Freeze();
                XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                EndianIO nio = new EndianIO(stream, EndianType.BigEndian);
                nio.Open();
                int tagIndexByClassAndName = Map.GetTagIndexByClassAndName(cmbxClass.Text, cmbxName.Text);
                string str = "Null";
                string name = "Null";
                if (tagIndexByClassAndName != -1)
                {
                    str = Map.Index_Items[tagIndexByClassAndName].Class;
                    name = Map.Index_Items[tagIndexByClassAndName].Name;
                }
                for (int i = 0; i < IdentGrid.SelectedItems.Count; i++)
                {
                    int num3 = int.Parse(IdentGrid.SelectedItems[i].SubItems[1].Text.Substring(2), NumberStyles.HexNumber) + Map.Map_Header.mapMagic;
                    if (tagIndexByClassAndName != -1)
                    {
                        nio.Out.BaseStream.Position = num3;
                        nio.Out.WriteAsciiString(str, 4);
                    }
                    nio.Out.BaseStream.Position = num3 + 12;
                    if (tagIndexByClassAndName != -1)
                    {
                        nio.Out.Write(Map.Index_Items[tagIndexByClassAndName].Ident);
                    }
                    else
                    {
                        nio.Out.Write(-1);
                    }
                }
                nio.Close();
                stream.Close();
                communicator.Unfreeze();
                communicator.Disconnect();
                OutputMessenger.OutputMessage("Sucessfully poked " + IdentGrid.SelectedItems.Count.ToString() + " tag references to: \"" + name + "." + str + "\"", IdentGrid);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Map.OpenIO();
            int tagIndexByClassAndName = Map.GetTagIndexByClassAndName(cmbxClass.Text, cmbxName.Text);
            string str = "Null";
            string name = "Null";
            if (tagIndexByClassAndName != -1)
            {
                str = Map.Index_Items[tagIndexByClassAndName].Class;
                name = Map.Index_Items[tagIndexByClassAndName].Name;
            }
            for (int i = 0; i < IdentGrid.SelectedItems.Count; i++)
            {
                int num3 = int.Parse(IdentGrid.SelectedItems[i].SubItems[1].Text.Substring(2), NumberStyles.HexNumber);
                if (tagIndexByClassAndName != -1)
                {
                    Map.IO.Out.BaseStream.Position = num3;
                    Map.IO.Out.WriteAsciiString(str, 4);
                }
                Map.IO.Out.BaseStream.Position = num3 + 12;
                if (tagIndexByClassAndName != -1)
                {
                    Map.IO.Out.Write(Map.Index_Items[tagIndexByClassAndName].Ident);
                    IdentGrid.SelectedItems[i].SubItems[2].Text = "0x" + Map.Index_Items[tagIndexByClassAndName].Ident.ToString("X");
                    IdentGrid.SelectedItems[i].SubItems[3].Text = str;
                    IdentGrid.SelectedItems[i].SubItems[4].Text = name;
                }
                else
                {
                    Map.IO.Out.Write(-1);
                    IdentGrid.SelectedItems[i].SubItems[2].Text = "0xFFFFFFFF";
                    IdentGrid.SelectedItems[i].SubItems[3].Text = "Null";
                    IdentGrid.SelectedItems[i].SubItems[4].Text = "Null";
                }
            }
            Map.CloseIO();
            OutputMessenger.OutputMessage("Sucessfully swapped " + IdentGrid.SelectedItems.Count.ToString() + " tag references to: \"" + name + "." + str + "\"", IdentGrid);
            base.Close();
        }

        private void cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmbx_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cmbxName.Items.Clear();
            for (int i = 0; i < Map.Index_Header.tagCount; i++)
            {
                if (Map.Index_Items[i].Class == cmbxClass.Text)
                {
                    cmbxName.Items.Add(Map.Index_Items[i].Name);
                }
            }
            cmbxName.Sorted = true;
            cmbxName.Sorted = false;
            cmbxName.Items.Add("<<Null>>");
        }

        private void cmbxName_SelectedIndexChanged(object sender, EventArgs e)
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
            panel1 = new Panel();
            button1 = new Button();
            cmbxName = new ComboBox();
            cmbxClass = new ComboBox();
            button2 = new Button();
            panel1.SuspendLayout();
            base.SuspendLayout();
            panel1.BackColor = SystemColors.ControlLight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(cmbxName);
            panel1.Controls.Add(cmbxClass);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x1db, 0x4d);
            panel1.TabIndex = 0;
            button1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            button1.Location = new Point(13, 0x2a);
            button1.Name = "button1";
            button1.Size = new Size(0x16a, 0x17);
            button1.TabIndex = 5;
            button1.Text = "Swap";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(buttonX1_Click);
            cmbxName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            cmbxName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxName.FormattingEnabled = true;
            cmbxName.Location = new Point(0x74, 15);
            cmbxName.Name = "cmbxName";
            cmbxName.Size = new Size(0x162, 0x15);
            cmbxName.TabIndex = 4;
            cmbxName.SelectedIndexChanged += new EventHandler(cmbxName_SelectedIndexChanged);
            cmbxClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxClass.FormattingEnabled = true;
            cmbxClass.Location = new Point(13, 15);
            cmbxClass.Name = "cmbxClass";
            cmbxClass.Size = new Size(0x61, 0x15);
            cmbxClass.TabIndex = 3;
            cmbxClass.SelectedIndexChanged += new EventHandler(cmbx_SelectedIndexChanged_1);
            button2.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            button2.Location = new Point(0x17d, 0x2a);
            button2.Name = "button2";
            button2.Size = new Size(0x59, 0x17);
            button2.TabIndex = 6;
            button2.Text = "Poke";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            base.ClientSize = new Size(0x1f3, 0x67);
            base.Controls.Add(panel1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "IdentSwapper";
            Text = "Ident Swapper";
            panel1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadClasses()
        {
            int num;
            List<string> list = new List<string>();
            for (num = 0; num < Map.Index_Items.Count; num++)
            {
                int num2 = -1;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == Map.Index_Items[num].Class)
                    {
                        num2 = i;
                        break;
                    }
                }
                if (num2 == -1)
                {
                    list.Add(Map.Index_Items[num].Class);
                }
            }
            cmbxClass.Items.Clear();
            for (num = 0; num < list.Count; num++)
            {
                cmbxClass.Items.Add(list[num]);
            }
            cmbxClass.Sorted = true;
        }

        private void SelectItem(ComboBox comboBox, string text)
        {
            for (int i = 0; i < comboBox.Items.Count; i++)
            {
                if (comboBox.Items[i].ToString() == text)
                {
                    comboBox.SelectedIndex = i;
                    break;
                }
            }
        }

        public ListView IdentGrid
        {
            get
            {
                return _identgrid;
            }
            set
            {
                _identgrid = value;
            }
        }

        public HaloMap Map
        {
            get
            {
                return _map;
            }
            set
            {
                _map = value;
            }
        }
    }
}

