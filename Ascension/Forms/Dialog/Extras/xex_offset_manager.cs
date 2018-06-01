namespace Ascension.Forms.Dialog.Extras
{
    using Ascension;
    using Ascension.Forms.Dialog.Extras.Controls;
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.Helpers;
    using HaloReach3d.IO;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Windows.Forms;

    public class xex_offset_manager : Form
    {
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private TextBox Class;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox DefaultValue;
        private ToolStripMenuItem deleteItemToolStripMenuItem;
        private TextBox Description;
        private TextBox endoff;
        private TextBox filter;
        private ComboBox GameSelector;
        private Button get_value;
        public Dictionary<string, Items> ItemsList = new Dictionary<string, Items>();
        private TextBox mapmagic;
        private CheckBox memcheck;
        private TreeNode OFF = new TreeNode();
        private TreeNode OFF2 = new TreeNode();
        private TextBox Offset;
        private TextBox OffsetName;
        private Panel panel1;
        private Button poke_changes;
        private TextBox poke_offset;
        private TextBox poke_value;
        private TextBox pokeall_value;
        private Dictionary<string, Items> privateList = new Dictionary<string, Items>();
        private Button save_add;
        private TextBox startoff;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TrackBar trackBar1;
        private TreeView treeView1;
        private ComboBox Value_type;

        public xex_offset_manager()
        {
            InitializeComponent();
            GetGames();
            LoadItems();
            updateList();
        }

        public void addtxtbxs(string o, string t)
        {
            if (t.Contains(comboBox2.Text) | (comboBox2.Text == ""))
            {
                int count = panel1.Controls.Count;
                string v = getValue(Convert.ToUInt32(o, 0x10), t);
                xex_value _value = new xex_value();
                _value.setvalues(o, v, t, ItemsList);
                _value.Location = new Point(0, count * 0x19);
                if (v.Contains(filter.Text) | (filter.Text == ""))
                {
                    panel1.Controls.Add(_value);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Dictionary<string, Items> dictionary = new Dictionary<string, Items>();
            OpenFileDialog dialog = new OpenFileDialog {
                Title = "Open"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    TextReader reader = new StreamReader(dialog.FileName);
                    string str = reader.ReadToEnd();
                    reader.Close();
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, Items>>(str);
                    foreach (KeyValuePair<string, Items> pair in dictionary)
                    {
                        ItemsList[pair.Key] = pair.Value;
                    }
                    SaveItems();
                    updateList();
                    MessageBox.Show("Done");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            poke_offset.Text = "0x" + ((Convert.ToUInt32(poke_offset.Text, 0x10) - 4)).ToString("X");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            poke_offset.Text = "0x" + ((Convert.ToUInt32(poke_offset.Text, 0x10) + 4)).ToString("X");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int num;
            if (!memcheck.Checked)
            {
                panel1.Controls.Clear();
                string[] source = textBox1.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                string t = "";
                string o = "";
                for (num = 0; num < source.Count<string>(); num++)
                {
                    try
                    {
                        try
                        {
                            t = source[num].Split(new char[] { '.' })[2];
                            t = t.Split(new char[] { ' ' })[0];
                            o = source[num].Split(new char[] { ':' })[1];
                            o = o.Split(new char[] { ' ' })[0];
                            addtxtbxs(o, t);
                        }
                        catch
                        {
                            if (source[num].Contains("bl "))
                            {
                                t = "float";
                                o = source[num].Split(new char[] { ':' })[1];
                                o = o.Split(new char[] { ' ' })[0];
                                addtxtbxs(o, t);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                uint num2;
                panel1.Controls.Clear();
                try
                {
                    num2 = uint.Parse(startoff.Text) - uint.Parse(mapmagic.Text);
                }
                catch
                {
                    num2 = Convert.ToUInt32(startoff.Text) - Convert.ToUInt32(mapmagic.Text);
                }
                for (num = int.Parse(startoff.Text); num < int.Parse(endoff.Text); num += 4)
                {
                    num2++;
                    addtxtbxs(num2.ToString("X"), comboBox1.Text);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (xex_value _value in panel1.Controls)
            {
                pokeXbox(Convert.ToUInt32(_value.offset, 0x10), comboBox1.Text, pokeall_value.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((comboBox1.SelectedItem.ToString() == "ASCII String") | (comboBox1.SelectedItem.ToString() == "Unicode String")) | (comboBox1.SelectedItem.ToString() == "Bytes"))
            {
                textBox4.ReadOnly = false;
            }
            else
            {
                textBox4.ReadOnly = true;
            }
        }

        public string ConvertToHex(string asciiString)
        {
            string str = "";
            foreach (char ch in asciiString)
            {
                int num = ch;
                str = str + "00" + $"{Convert.ToUInt32(num.ToString()):x2}";
            }
            return str;
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = OFF.Nodes[treeView1.SelectedNode.Parent.Index].Nodes[treeView1.SelectedNode.Index].Text;
            if (DialogResult.OK == MessageBox.Show("Ensure that you have the item selected that you wish to delete " + ItemsList[text].Name, "Warning", MessageBoxButtons.OKCancel))
            {
                ItemsList.Remove(text);
                SaveItems();
                updateList();
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

        private void GameSelector_TextChanged(object sender, EventArgs e)
        {
            ItemsList.Clear();
            treeView1.Nodes.Clear();
            LoadItems();
            updateList();
        }

        private void get_value_Click(object sender, EventArgs e)
        {
            poke_value.Text = getValue(Convert.ToUInt32(poke_offset.Text, 0x10), comboBox1.Text);
        }

        public void GetGames()
        {
            string[] strArray;
            try
            {
                WebClient client = new WebClient();
                StreamReader reader = new StreamReader(client.OpenRead("http://www.xboxchaos.com/ascension/GamesList.txt"));
                string str = reader.ReadToEnd();
                reader.Close();
                strArray = str.Replace("\n", ";").Split(new char[] { ';' });
                for (int i = 0; i < strArray.Count<string>(); i++)
                {
                    GameSelector.Items.Add(strArray[i]);
                }
                GameSelector.Sorted = true;
            }
            catch
            {
                MessageBox.Show("Could Not Collect Games from Server.");
            }
            try
            {
                strArray = AppSettings.Settings.Additional_Games.Split(new char[] { '^' });
                foreach (string str2 in strArray)
                {
                    GameSelector.Items.Add(str2);
                }
            }
            catch
            {
            }
        }

        public string getValue(uint offset, string type)
        {
            string format = "";
            if (checkBox1.Checked)
            {
                format = "X";
            }
            else
            {
                format = "";
            }
            object obj2 = null;
            if (AppSettings.Settings.IP_and_XDK_Name != "")
            {
                XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                if (!communicator.Connected)
                {
                    try
                    {
                        communicator.Connect();
                    }
                    catch
                    {
                    }
                }
                XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                nio.Open();
                nio.In.BaseStream.Position = offset;
                if ((type == "String") | (type == "string"))
                {
                    obj2 = nio.In.ReadString();
                }
                if (type == "Unicode String")
                {
                    obj2 = nio.In.ReadUnicodeString(int.Parse(textBox4.Text));
                }
                if (type == "ASCII String")
                {
                    obj2 = nio.In.ReadAsciiString(int.Parse(textBox4.Text));
                }
                if ((type == "Float") | (type == "float"))
                {
                    obj2 = nio.In.ReadSingle();
                }
                if ((type == "Double") | (type == "double"))
                {
                    obj2 = nio.In.ReadDouble();
                }
                if ((type == "Short") | (type == "short"))
                {
                    obj2 = nio.In.ReadInt16().ToString(format);
                }
                if ((type == "Byte") | (type == "byte"))
                {
                    obj2 = nio.In.ReadByte().ToString(format);
                }
                if ((type == "Long") | (type == "long"))
                {
                    obj2 = nio.In.ReadInt32().ToString(format);
                }
                if ((type == "Quad") | (type == "quad"))
                {
                    obj2 = nio.In.ReadInt64().ToString(format);
                }
                if ((type == "Bytes") | (type == "bytes"))
                {
                    if (textBox4.Text == "")
                    {
                        textBox4.Text = "4";
                    }
                    obj2 = ExtraFunctions.BytesToHexString(nio.In.ReadBytes(int.Parse(textBox4.Text)));
                }
                nio.Close();
                stream.Close();
                communicator.Disconnect();
                return obj2.ToString();
            }
            MessageBox.Show("XDK Name/IP not set");
            return "No Console Detected";
        }

        private void InitializeComponent()
        {
            components = new Container();
            treeView1 = new TreeView();
            contextMenuStrip1 = new ContextMenuStrip(components);
            deleteItemToolStripMenuItem = new ToolStripMenuItem();
            poke_changes = new Button();
            save_add = new Button();
            Description = new TextBox();
            DefaultValue = new TextBox();
            OffsetName = new TextBox();
            Class = new TextBox();
            Offset = new TextBox();
            poke_value = new TextBox();
            poke_offset = new TextBox();
            comboBox1 = new ComboBox();
            button1 = new Button();
            Value_type = new ComboBox();
            get_value = new Button();
            checkBox1 = new CheckBox();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            button4 = new Button();
            textBox1 = new TextBox();
            filter = new TextBox();
            comboBox2 = new ComboBox();
            checkBox2 = new CheckBox();
            GameSelector = new ComboBox();
            trackBar1 = new TrackBar();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            pokeall_value = new TextBox();
            button5 = new Button();
            textBox4 = new TextBox();
            mapmagic = new TextBox();
            startoff = new TextBox();
            endoff = new TextBox();
            memcheck = new CheckBox();
            contextMenuStrip1.SuspendLayout();
            trackBar1.BeginInit();
            base.SuspendLayout();
            treeView1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            treeView1.ContextMenuStrip = contextMenuStrip1;
            treeView1.Location = new Point(12, 0x25);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(0x101, 0x153);
            treeView1.TabIndex = 14;
            treeView1.AfterSelect += new TreeViewEventHandler(treeView1_AfterSelect);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { deleteItemToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0x99, 0x30);
            deleteItemToolStripMenuItem.Name = "deleteItemToolStripMenuItem";
            deleteItemToolStripMenuItem.Size = new Size(0x98, 0x16);
            deleteItemToolStripMenuItem.Text = "Delete Item";
            deleteItemToolStripMenuItem.Click += new EventHandler(deleteItemToolStripMenuItem_Click);
            poke_changes.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            poke_changes.Location = new Point(0x1fc, 0x164);
            poke_changes.Name = "poke_changes";
            poke_changes.Size = new Size(0x72, 20);
            poke_changes.TabIndex = 12;
            poke_changes.Text = "Poke Changes";
            poke_changes.UseVisualStyleBackColor = true;
            poke_changes.Click += new EventHandler(poke_changes_Click);
            save_add.Location = new Point(0x113, 11);
            save_add.Name = "save_add";
            save_add.Size = new Size(0x15b, 20);
            save_add.TabIndex = 13;
            save_add.Text = "Save/Add To List";
            save_add.UseVisualStyleBackColor = true;
            save_add.Click += new EventHandler(save_add_Click_1);
            Description.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            Description.Location = new Point(0x113, 0x73);
            Description.Multiline = true;
            Description.Name = "Description";
            Description.Size = new Size(0x15b, 0xd1);
            Description.TabIndex = 11;
            Description.Text = "Description";
            DefaultValue.Location = new Point(0x113, 0x59);
            DefaultValue.Name = "DefaultValue";
            DefaultValue.Size = new Size(0xad, 20);
            DefaultValue.TabIndex = 6;
            DefaultValue.Text = "Default Value";
            OffsetName.Location = new Point(0x113, 0x3f);
            OffsetName.Name = "OffsetName";
            OffsetName.Size = new Size(0x15b, 20);
            OffsetName.TabIndex = 9;
            OffsetName.Text = "Name";
            Class.Location = new Point(0x113, 0x25);
            Class.Name = "Class";
            Class.Size = new Size(0xad, 20);
            Class.TabIndex = 8;
            Class.Text = "Class";
            Offset.Location = new Point(0x1c0, 0x25);
            Offset.Name = "Offset";
            Offset.Size = new Size(0xae, 20);
            Offset.TabIndex = 7;
            Offset.Text = "Offset";
            Offset.TextChanged += new EventHandler(Offset_TextChanged);
            poke_value.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            poke_value.Location = new Point(0x1c1, 330);
            poke_value.Name = "poke_value";
            poke_value.Size = new Size(0xad, 20);
            poke_value.TabIndex = 7;
            poke_value.Text = "Value";
            poke_offset.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            poke_offset.Location = new Point(0x114, 330);
            poke_offset.Name = "poke_offset";
            poke_offset.Size = new Size(0x72, 20);
            poke_offset.TabIndex = 8;
            poke_offset.Text = "Offset";
            poke_offset.TextChanged += new EventHandler(poke_offset_TextChanged);
            comboBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "String", "Unicode String", "ASCII String", "Float", "Double", "Long", "Quad", "Byte", "Short", "Bytes" });
            comboBox1.Location = new Point(0x114, 0x163);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(0x52, 0x15);
            comboBox1.TabIndex = 15;
            comboBox1.Text = "Float";
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            button1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button1.Location = new Point(12, 0x17e);
            button1.Name = "button1";
            button1.Size = new Size(0x101, 20);
            button1.TabIndex = 0x10;
            button1.Text = "Update Offsets from File";
            button1.UseVisualStyleBackColor = true;
            button1.Click += new EventHandler(button1_Click_1);
            Value_type.FormattingEnabled = true;
            Value_type.Items.AddRange(new object[] { "String", "Unicode String", "ASCII String", "Float", "Double", "Long", "Quad", "Byte", "Bytes", "Short" });
            Value_type.Location = new Point(450, 0x58);
            Value_type.Name = "Value_type";
            Value_type.Size = new Size(0xac, 0x15);
            Value_type.TabIndex = 15;
            Value_type.Text = "Value Type";
            Value_type.SelectedIndexChanged += new EventHandler(Value_type_SelectedIndexChanged);
            get_value.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            get_value.Location = new Point(0x188, 0x164);
            get_value.Name = "get_value";
            get_value.Size = new Size(0x72, 20);
            get_value.TabIndex = 0x11;
            get_value.Text = "Get Value";
            get_value.UseVisualStyleBackColor = true;
            get_value.Click += new EventHandler(get_value_Click);
            checkBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(0x1ac, 0x14d);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 0x12;
            checkBox1.UseVisualStyleBackColor = true;
            button2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button2.Location = new Point(0x18a, 330);
            button2.Name = "button2";
            button2.Size = new Size(14, 20);
            button2.TabIndex = 0x13;
            button2.Text = "-";
            button2.UseVisualStyleBackColor = true;
            button2.Click += new EventHandler(button2_Click);
            button3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button3.Location = new Point(410, 330);
            button3.Name = "button3";
            button3.Size = new Size(14, 20);
            button3.TabIndex = 0x13;
            button3.Text = "+";
            button3.UseVisualStyleBackColor = true;
            button3.Click += new EventHandler(button3_Click);
            panel1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            panel1.AutoScroll = true;
            panel1.Location = new Point(0x39f, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x1a2, 0x169);
            panel1.TabIndex = 0x17;
            button4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button4.Location = new Point(0x286, 0x17e);
            button4.Name = "button4";
            button4.Size = new Size(0x86, 20);
            button4.TabIndex = 0x16;
            button4.Text = "Parse Values";
            button4.UseVisualStyleBackColor = true;
            button4.Click += new EventHandler(button4_Click);
            textBox1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            textBox1.Location = new Point(0x274, 0x25);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Both;
            textBox1.Size = new Size(0x125, 0x150);
            textBox1.TabIndex = 20;
            textBox1.Click += new EventHandler(textBox1_Click);
            filter.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            filter.Location = new Point(0x343, 0x17f);
            filter.Name = "filter";
            filter.Size = new Size(0x56, 20);
            filter.TabIndex = 0x18;
            comboBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "", "string", "float", "double", "long", "quad", "byte", "short" });
            comboBox2.Location = new Point(0x30e, 0x17e);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(50, 0x15);
            comboBox2.TabIndex = 0x19;
            checkBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(0x274, 0x181);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(15, 14);
            checkBox2.TabIndex = 0x1a;
            checkBox2.UseVisualStyleBackColor = true;
            GameSelector.FormattingEnabled = true;
            GameSelector.Location = new Point(13, 10);
            GameSelector.Name = "GameSelector";
            GameSelector.Size = new Size(0x100, 0x15);
            GameSelector.TabIndex = 0x1b;
            GameSelector.Text = "Halo Reach";
            GameSelector.TextChanged += new EventHandler(GameSelector_TextChanged);
            trackBar1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            trackBar1.Location = new Point(0x188, 0x17d);
            trackBar1.Maximum = 5;
            trackBar1.Minimum = -5;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(230, 0x2d);
            trackBar1.TabIndex = 0x1c;
            trackBar1.TickStyle = TickStyle.None;
            trackBar1.Scroll += new EventHandler(trackBar1_Scroll);
            textBox2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            textBox2.Location = new Point(0x113, 0x17e);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(0x38, 20);
            textBox2.TabIndex = 0x1d;
            textBox2.Text = "-5";
            textBox2.TextChanged += new EventHandler(textBox2_TextChanged);
            textBox3.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            textBox3.Location = new Point(0x14e, 0x17e);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(0x38, 20);
            textBox3.TabIndex = 0x1d;
            textBox3.Text = "5";
            textBox3.TextChanged += new EventHandler(textBox3_TextChanged);
            pokeall_value.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            pokeall_value.Location = new Point(0x39f, 0x17f);
            pokeall_value.Name = "pokeall_value";
            pokeall_value.Size = new Size(0x139, 20);
            pokeall_value.TabIndex = 30;
            button5.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            button5.Location = new Point(0x4de, 0x17f);
            button5.Name = "button5";
            button5.Size = new Size(0x63, 0x13);
            button5.TabIndex = 0x1f;
            button5.Text = "Poke All";
            button5.UseVisualStyleBackColor = true;
            button5.Click += new EventHandler(button5_Click);
            textBox4.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            textBox4.Location = new Point(360, 0x163);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new Size(30, 20);
            textBox4.TabIndex = 0x20;
            mapmagic.Location = new Point(0x274, 12);
            mapmagic.Name = "mapmagic";
            mapmagic.Size = new Size(0x5b, 20);
            mapmagic.TabIndex = 0x21;
            mapmagic.Text = "Magic";
            startoff.Location = new Point(720, 12);
            startoff.Name = "startoff";
            startoff.Size = new Size(0x5b, 20);
            startoff.TabIndex = 0x21;
            endoff.Location = new Point(0x32c, 12);
            endoff.Name = "endoff";
            endoff.Size = new Size(0x5b, 20);
            endoff.TabIndex = 0x21;
            memcheck.AutoSize = true;
            memcheck.Location = new Point(0x389, 15);
            memcheck.Name = "memcheck";
            memcheck.Size = new Size(15, 14);
            memcheck.TabIndex = 0x22;
            memcheck.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x274, 0x19c);
            base.Controls.Add(memcheck);
            base.Controls.Add(endoff);
            base.Controls.Add(startoff);
            base.Controls.Add(mapmagic);
            base.Controls.Add(textBox4);
            base.Controls.Add(button5);
            base.Controls.Add(pokeall_value);
            base.Controls.Add(textBox3);
            base.Controls.Add(textBox2);
            base.Controls.Add(trackBar1);
            base.Controls.Add(GameSelector);
            base.Controls.Add(checkBox2);
            base.Controls.Add(comboBox2);
            base.Controls.Add(filter);
            base.Controls.Add(panel1);
            base.Controls.Add(button4);
            base.Controls.Add(textBox1);
            base.Controls.Add(button3);
            base.Controls.Add(button2);
            base.Controls.Add(checkBox1);
            base.Controls.Add(Value_type);
            base.Controls.Add(get_value);
            base.Controls.Add(button1);
            base.Controls.Add(comboBox1);
            base.Controls.Add(treeView1);
            base.Controls.Add(poke_changes);
            base.Controls.Add(save_add);
            base.Controls.Add(Description);
            base.Controls.Add(DefaultValue);
            base.Controls.Add(OffsetName);
            base.Controls.Add(poke_offset);
            base.Controls.Add(Class);
            base.Controls.Add(poke_value);
            base.Controls.Add(Offset);
            MaximumSize = new Size(0x55d, 850);
            MinimumSize = new Size(0x284, 450);
            base.Name = "xex_offset_manager";
            Text = "Memory Offset Manager";
            contextMenuStrip1.ResumeLayout(false);
            trackBar1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void LoadItems()
        {
            try
            {
                string str = "";
                string str2 = "";
                try
                {
                    WebClient client = new WebClient();
                    StreamReader reader = new StreamReader(client.OpenRead("http://www.xboxchaos.com/ascension/" + GameSelector.Text + ".val"));
                    str = reader.ReadToEnd();
                    reader.Close();
                }
                catch
                {
                }
                try
                {
                    Dictionary<string, Items> dictionary = new Dictionary<string, Items>();
                    TextReader reader2 = new StreamReader(@"values\" + GameSelector.Text + ".val");
                    str2 = reader2.ReadToEnd();
                    reader2.Close();
                }
                catch
                {
                }
                try
                {
                    if (str2 != "")
                    {
                        privateList = JsonConvert.DeserializeObject<Dictionary<string, Items>>(str2);
                        foreach (KeyValuePair<string, Items> pair in privateList)
                        {
                            ItemsList[pair.Key] = pair.Value;
                        }
                    }
                    if (str != "")
                    {
                        privateList = JsonConvert.DeserializeObject<Dictionary<string, Items>>(str);
                        foreach (KeyValuePair<string, Items> pair in privateList)
                        {
                            ItemsList[pair.Key] = pair.Value;
                        }
                    }
                }
                catch
                {
                }
            }
            catch
            {
                MessageBox.Show("Cannot Load Values from Online or Currently no Values available");
            }
        }

        private void Offset_TextChanged(object sender, EventArgs e)
        {
            poke_offset.Text = Offset.Text;
        }

        private void poke_changes_Click(object sender, EventArgs e)
        {
            try
            {
                pokeXbox(Convert.ToUInt32(poke_offset.Text, 0x10), comboBox1.SelectedItem.ToString(), poke_value.Text);
            }
            catch
            {
                MessageBox.Show("Could not poke Changes.");
            }
        }

        private void poke_offset_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextReader reader = new StreamReader(@"values\" + GameSelector.Text + ".val");
                string str = reader.ReadToEnd();
                reader.Close();
                if (str.Contains(poke_offset.Text))
                {
                    Offset.BackColor = Color.LightGreen;
                }
                else
                {
                    Offset.BackColor = SystemColors.Window;
                }
            }
            catch
            {
            }
        }

        public void pokeXbox(uint offset, string poketype, string ammount)
        {
            if (!checkBox1.Checked)
            {
                ammount = int.Parse(ammount).ToString("X");
            }
            try
            {
                if (AppSettings.Settings.IP_and_XDK_Name == "")
                {
                    MessageBox.Show("XDK Name/IP not set");
                }
                else
                {
                    XboxDebugCommunicator communicator = new XboxDebugCommunicator(AppSettings.Settings.IP_and_XDK_Name);
                    if (!communicator.Connected)
                    {
                        try
                        {
                            communicator.Connect();
                        }
                        catch
                        {
                        }
                    }
                    XboxMemoryStream stream = communicator.ReturnXboxMemoryStream();
                    HaloReach3d.IO.EndianIO nio = new HaloReach3d.IO.EndianIO(stream, HaloReach3d.IO.EndianType.BigEndian);
                    nio.Open();
                    nio.Out.BaseStream.Position = offset;
                    if (poketype == "Unicode String")
                    {
                        nio.Out.WriteUnicodeString(ammount, ammount.Length);
                    }
                    if (poketype == "ASCII String")
                    {
                        nio.Out.WriteUnicodeString(ammount, ammount.Length);
                    }
                    if ((poketype == "String") | (poketype == "string"))
                    {
                        nio.Out.Write(ammount);
                    }
                    if ((poketype == "Float") | (poketype == "float"))
                    {
                        nio.Out.Write(float.Parse(ammount));
                    }
                    if ((poketype == "Double") | (poketype == "double"))
                    {
                        nio.Out.Write(double.Parse(ammount));
                    }
                    if ((poketype == "Short") | (poketype == "short"))
                    {
                        nio.Out.Write((short) Convert.ToUInt32(ammount, 0x10));
                    }
                    if ((poketype == "Byte") | (poketype == "byte"))
                    {
                        nio.Out.Write((byte) Convert.ToUInt32(ammount, 0x10));
                    }
                    if ((poketype == "Long") | (poketype == "long"))
                    {
                        nio.Out.Write((long) Convert.ToUInt32(ammount, 0x10));
                    }
                    if ((poketype == "Quad") | (poketype == "quad"))
                    {
                        nio.Out.Write((long) Convert.ToUInt64(ammount, 0x10));
                    }
                    if ((poketype == "Int") | (poketype == "int"))
                    {
                        nio.Out.Write(Convert.ToUInt32(ammount, 0x10));
                    }
                    if ((poketype == "Bytes") | (poketype == "bytes"))
                    {
                        nio.Out.Write(ExtraFunctions.HexStringToBytes(ammount), 0, ExtraFunctions.HexStringToBytes(ammount).Count<byte>());
                    }
                    nio.Close();
                    stream.Close();
                    communicator.Disconnect();
                }
            }
            catch
            {
            }
        }

        private void save_add_Click_1(object sender, EventArgs e)
        {
            if (Offset.Text.StartsWith("0x"))
            {
                ItemsList[Offset.Text] = new Items(Class.Text, Offset.Text, OffsetName.Text, DefaultValue.Text, Value_type.Text, Description.Text);
                updateList();
                SaveItems();
            }
            else
            {
                MessageBox.Show("Invalid Offset");
            }
        }

        public void SaveItems()
        {
            if (!Directory.Exists(@"values\"))
            {
                Directory.CreateDirectory(@"values\");
            }
            TextWriter writer = new StreamWriter(@"values\" + GameSelector.Text + ".val", false);
            string str = JsonConvert.SerializeObject(ItemsList);
            writer.WriteLine(str);
            writer.Flush();
            writer.Close();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                textBox1.Text = "";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            trackBar1.SetRange(int.Parse(textBox2.Text) * 10, int.Parse(textBox3.Text) * 10);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            trackBar1.SetRange(int.Parse(textBox2.Text) * 10, int.Parse(textBox3.Text) * 10);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float num = trackBar1.Value;
            num /= 10f;
            poke_value.Text = num.ToString();
            ((Form1) Application.OpenForms[0]).streampoke(Convert.ToUInt32(poke_offset.Text, 0x10), comboBox1.Text, num.ToString());
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                string text = OFF.Nodes[treeView1.SelectedNode.Parent.Index].Nodes[treeView1.SelectedNode.Index].Text;
                Class.Text = ItemsList[text].Class;
                OffsetName.Text = ItemsList[text].Name;
                DefaultValue.Text = ItemsList[text].DefaultV;
                Value_type.SelectedItem = ItemsList[text].RecommendedV;
                Description.Text = ItemsList[text].Description;
                Offset.Text = text;
            }
            catch
            {
            }
        }

        public void updateList()
        {
            OFF.Nodes.Clear();
            OFF2.Nodes.Clear();
            IOrderedEnumerable<KeyValuePair<string, Items>> enumerable = from entry in ItemsList
                orderby entry.Value.Class
                select entry;
            TreeNode node = new TreeNode();
            treeView1.Nodes.Clear();
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, Items> pair in enumerable)
            {
                if (!list.Contains(pair.Value.Class))
                {
                    node = treeView1.Nodes.Add(pair.Value.Class);
                    OFF2 = OFF.Nodes.Add(pair.Value.Class);
                }
                list.Add(pair.Value.Class);
                node.Nodes.Add(pair.Value.Name);
                OFF2.Nodes.Add(pair.Value.Offset);
            }
            treeView1.Refresh();
            Text = "Xex Memory Offsets = " + ItemsList.Count;
        }

        private void Value_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = Value_type.SelectedItem;
        }
    }
}

