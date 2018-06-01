namespace Ascension.Halo_Reach.Game.Tag_Grid
{
    using Ascension.Communications.Output;
    using Ascension.Forms;
    using Ascension.Halo_Reach.Meta_Parser;
    using Ascension.Halo_Reach.Tag_Grid.Dialog;
    using Ascension.Settings;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TagGridContainer : UserControl
    {
        private HaloMap _map;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader20;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private IContainer components = null;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem goToInCurrentTabToolStripMenuItem;
        private ToolStripMenuItem goToInNewTabToolStripMenuItem;
        private ListView lstIdentGrid;
        private ListView lstStringIdentifiersGrid;
        private ListView lstStructuresGrid;
        private ListView lstVoidsGrid;
        private ToolStripSplitButton menuItemShow;
        private Panel panel1;
        private ToolStripMenuItem stringIdentifiersToolStripMenuItem;
        private ToolStripMenuItem structuresAndVoidsToolStripMenuItem;
        private ToolStripMenuItem swapSelectedToolStripMenuItem;
        private ToolStripMenuItem tagDataVoidsToolStripMenuItem;
        private ToolStripMenuItem tagReferencesToolStripMenuItem;
        private ToolStrip toolStrip1;

        public TagGridContainer(MetaParser metaParser)
        {
            InitializeComponent();
            SwitchToTag(metaParser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void goToInCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToTag(false);
        }

        private void goToInNewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToTag(true);
        }

        private void GoToTag(bool inNewTab)
        {
            if (lstIdentGrid.SelectedItems[0].SubItems[2].Text != "0xFFFFFFFF")
            {
                Control parent = this;
                MapForm form = null;
                while (true)
                {
                    if (parent.Parent == null)
                    {
                        break;
                    }
                    parent = parent.Parent;
                    if (parent.GetType() == typeof(MapForm))
                    {
                        form = (MapForm) parent;
                        break;
                    }
                }
                if (form != null)
                {
                    if (inNewTab)
                    {
                        form.AddTab("", "", "", true);
                    }
                    form.GotoTag(lstIdentGrid.SelectedItems[0].SubItems[3].Text, lstIdentGrid.SelectedItems[0].SubItems[4].Text);
                }
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(TagGridContainer));
            lstStructuresGrid = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            lstIdentGrid = new ListView();
            columnHeader7 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader11 = new ColumnHeader();
            columnHeader12 = new ColumnHeader();
            panel1 = new Panel();
            lstStringIdentifiersGrid = new ListView();
            columnHeader8 = new ColumnHeader();
            columnHeader13 = new ColumnHeader();
            columnHeader14 = new ColumnHeader();
            columnHeader15 = new ColumnHeader();
            toolStrip1 = new ToolStrip();
            menuItemShow = new ToolStripSplitButton();
            structuresAndVoidsToolStripMenuItem = new ToolStripMenuItem();
            tagReferencesToolStripMenuItem = new ToolStripMenuItem();
            stringIdentifiersToolStripMenuItem = new ToolStripMenuItem();
            tagDataVoidsToolStripMenuItem = new ToolStripMenuItem();
            lstVoidsGrid = new ListView();
            columnHeader16 = new ColumnHeader();
            columnHeader18 = new ColumnHeader();
            columnHeader20 = new ColumnHeader();
            columnHeader21 = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            swapSelectedToolStripMenuItem = new ToolStripMenuItem();
            goToInCurrentTabToolStripMenuItem = new ToolStripMenuItem();
            goToInNewTabToolStripMenuItem = new ToolStripMenuItem();
            panel1.SuspendLayout();
            toolStrip1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            lstStructuresGrid.BackColor = Color.White;
            lstStructuresGrid.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3, columnHeader4, columnHeader5, columnHeader6 });
            lstStructuresGrid.ForeColor = Color.Black;
            lstStructuresGrid.GridLines = true;
            lstStructuresGrid.Location = new Point(3, 0x18);
            lstStructuresGrid.Name = "lstStructuresGrid";
            lstStructuresGrid.Size = new Size(0x1aa, 0x146);
            lstStructuresGrid.TabIndex = 4;
            lstStructuresGrid.UseCompatibleStateImageBehavior = false;
            lstStructuresGrid.View = View.Details;
            columnHeader1.Text = "Name";
            columnHeader3.Text = "Offset";
            columnHeader4.Text = "Count";
            columnHeader5.Text = "Size";
            columnHeader6.Text = "Pointer";
            lstIdentGrid.BackColor = Color.White;
            lstIdentGrid.Columns.AddRange(new ColumnHeader[] { columnHeader7, columnHeader9, columnHeader10, columnHeader11, columnHeader12 });
            lstIdentGrid.ContextMenuStrip = contextMenuStrip1;
            lstIdentGrid.ForeColor = Color.Black;
            lstIdentGrid.GridLines = true;
            lstIdentGrid.Location = new Point(0x45, 0x18);
            lstIdentGrid.Name = "lstIdentGrid";
            lstIdentGrid.Size = new Size(0x1aa, 0x146);
            lstIdentGrid.TabIndex = 5;
            lstIdentGrid.UseCompatibleStateImageBehavior = false;
            lstIdentGrid.View = View.Details;
            columnHeader7.Text = "Name";
            columnHeader9.Text = "Offset";
            columnHeader10.Text = "Identifier";
            columnHeader11.Text = "Class";
            columnHeader12.Text = "Name";
            panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            panel1.Controls.Add(lstIdentGrid);
            panel1.Controls.Add(lstVoidsGrid);
            panel1.Controls.Add(lstStringIdentifiersGrid);
            panel1.Controls.Add(lstStructuresGrid);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x21b, 0x171);
            panel1.TabIndex = 7;
            lstStringIdentifiersGrid.BackColor = Color.White;
            lstStringIdentifiersGrid.Columns.AddRange(new ColumnHeader[] { columnHeader8, columnHeader13, columnHeader14, columnHeader15 });
            lstStringIdentifiersGrid.ForeColor = Color.Black;
            lstStringIdentifiersGrid.GridLines = true;
            lstStringIdentifiersGrid.Location = new Point(0x17, 0x18);
            lstStringIdentifiersGrid.Name = "lstStringIdentifiersGrid";
            lstStringIdentifiersGrid.Size = new Size(0x1aa, 0x146);
            lstStringIdentifiersGrid.TabIndex = 6;
            lstStringIdentifiersGrid.UseCompatibleStateImageBehavior = false;
            lstStringIdentifiersGrid.View = View.Details;
            columnHeader8.Text = "Name";
            columnHeader13.Text = "Offset";
            columnHeader14.Text = "Value";
            columnHeader15.Text = "String";
            toolStrip1.Dock = DockStyle.Bottom;
            toolStrip1.Items.AddRange(new ToolStripItem[] { menuItemShow });
            toolStrip1.Location = new Point(0, 0x171);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(0x21b, 0x19);
            toolStrip1.TabIndex = 7;
            toolStrip1.Text = "toolStrip1";
            menuItemShow.DisplayStyle = ToolStripItemDisplayStyle.Text;
            menuItemShow.DropDownItems.AddRange(new ToolStripItem[] { structuresAndVoidsToolStripMenuItem, tagDataVoidsToolStripMenuItem, tagReferencesToolStripMenuItem, stringIdentifiersToolStripMenuItem });
            menuItemShow.Image = (Image) manager.GetObject("menuItemShow.Image");
            menuItemShow.ImageTransparentColor = Color.Magenta;
            menuItemShow.Name = "menuItemShow";
            menuItemShow.Size = new Size(0x34, 0x16);
            menuItemShow.Text = "Show";
            menuItemShow.ButtonClick += new EventHandler(menuItemShow_ButtonClick);
            structuresAndVoidsToolStripMenuItem.CheckOnClick = true;
            structuresAndVoidsToolStripMenuItem.Name = "structuresAndVoidsToolStripMenuItem";
            structuresAndVoidsToolStripMenuItem.Size = new Size(0xa1, 0x16);
            structuresAndVoidsToolStripMenuItem.Text = "Structures";
            structuresAndVoidsToolStripMenuItem.Click += new EventHandler(structuresAndVoidsToolStripMenuItem_Click);
            tagReferencesToolStripMenuItem.CheckOnClick = true;
            tagReferencesToolStripMenuItem.Name = "tagReferencesToolStripMenuItem";
            tagReferencesToolStripMenuItem.Size = new Size(0xa1, 0x16);
            tagReferencesToolStripMenuItem.Text = "Tag References";
            tagReferencesToolStripMenuItem.Click += new EventHandler(tagReferencesToolStripMenuItem_Click);
            stringIdentifiersToolStripMenuItem.CheckOnClick = true;
            stringIdentifiersToolStripMenuItem.Name = "stringIdentifiersToolStripMenuItem";
            stringIdentifiersToolStripMenuItem.Size = new Size(0xa1, 0x16);
            stringIdentifiersToolStripMenuItem.Text = "String Identifiers";
            stringIdentifiersToolStripMenuItem.Click += new EventHandler(stringIdentifiersToolStripMenuItem_Click);
            tagDataVoidsToolStripMenuItem.Name = "tagDataVoidsToolStripMenuItem";
            tagDataVoidsToolStripMenuItem.Size = new Size(0xa1, 0x16);
            tagDataVoidsToolStripMenuItem.Text = "Tag Data / Voids";
            tagDataVoidsToolStripMenuItem.Click += new EventHandler(tagDataVoidsToolStripMenuItem_Click);
            lstVoidsGrid.BackColor = Color.White;
            lstVoidsGrid.Columns.AddRange(new ColumnHeader[] { columnHeader16, columnHeader18, columnHeader20, columnHeader21 });
            lstVoidsGrid.ForeColor = Color.Black;
            lstVoidsGrid.GridLines = true;
            lstVoidsGrid.Location = new Point(0x17, 0x18);
            lstVoidsGrid.Name = "lstVoidsGrid";
            lstVoidsGrid.Size = new Size(0x1aa, 0x146);
            lstVoidsGrid.TabIndex = 7;
            lstVoidsGrid.UseCompatibleStateImageBehavior = false;
            lstVoidsGrid.View = View.Details;
            columnHeader16.Text = "Name";
            columnHeader18.Text = "Offset";
            columnHeader20.Text = "Size";
            columnHeader21.Text = "Pointer";
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { swapSelectedToolStripMenuItem, goToInCurrentTabToolStripMenuItem, goToInNewTabToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(0xba, 0x5c);
            swapSelectedToolStripMenuItem.Name = "swapSelectedToolStripMenuItem";
            swapSelectedToolStripMenuItem.Size = new Size(0x98, 0x16);
            swapSelectedToolStripMenuItem.Text = "Swap Selected";
            swapSelectedToolStripMenuItem.Click += new EventHandler(swapSelectedToolStripMenuItem_Click);
            goToInCurrentTabToolStripMenuItem.Name = "goToInCurrentTabToolStripMenuItem";
            goToInCurrentTabToolStripMenuItem.Size = new Size(0xb9, 0x16);
            goToInCurrentTabToolStripMenuItem.Text = "Go to: In Current Tab";
            goToInCurrentTabToolStripMenuItem.Click += new EventHandler(goToInCurrentTabToolStripMenuItem_Click);
            goToInNewTabToolStripMenuItem.Name = "goToInNewTabToolStripMenuItem";
            goToInNewTabToolStripMenuItem.Size = new Size(0xb9, 0x16);
            goToInNewTabToolStripMenuItem.Text = "Go to: In New Tab";
            goToInNewTabToolStripMenuItem.Click += new EventHandler(goToInNewTabToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(toolStrip1);
            base.Controls.Add(panel1);
            base.Name = "TagGridContainer";
            base.Size = new Size(0x21b, 0x18a);
            panel1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void menuItemShow_ButtonClick(object sender, EventArgs e)
        {
            menuItemShow.ShowDropDown();
        }

        private void SelectGridToShow(ToolStripMenuItem buttonItem)
        {
            foreach (ToolStripMenuItem item in menuItemShow.DropDownItems)
            {
                item.Checked = false;
            }
            buttonItem.Checked = true;
            if (structuresAndVoidsToolStripMenuItem.Checked)
            {
                lstStructuresGrid.Dock = DockStyle.Fill;
                lstStructuresGrid.BringToFront();
                AppSettings.Settings.LastGridView = Ascension.Settings.Settings.LastGridViews.Structure;
                OutputMessenger.OutputMessage("Loaded \"structure & voids\" grid.", this);
            }
            if (tagDataVoidsToolStripMenuItem.Checked)
            {
                lstVoidsGrid.Dock = DockStyle.Fill;
                lstVoidsGrid.BringToFront();
                AppSettings.Settings.LastGridView = Ascension.Settings.Settings.LastGridViews.Voids;
                OutputMessenger.OutputMessage("Loaded \"tagdata/voids\" grid.", this);
            }
            if (tagReferencesToolStripMenuItem.Checked)
            {
                lstIdentGrid.Dock = DockStyle.Fill;
                lstIdentGrid.BringToFront();
                AppSettings.Settings.LastGridView = Ascension.Settings.Settings.LastGridViews.Ident;
                OutputMessenger.OutputMessage("Loaded \"tag references\" grid.", this);
            }
            if (stringIdentifiersToolStripMenuItem.Checked)
            {
                lstStringIdentifiersGrid.Dock = DockStyle.Fill;
                lstStringIdentifiersGrid.BringToFront();
                AppSettings.Settings.LastGridView = Ascension.Settings.Settings.LastGridViews.Strings;
                OutputMessenger.OutputMessage("Loaded \"string identifiers\" grid.", this);
            }
        }

        private void stringIdentifiersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectGridToShow(stringIdentifiersToolStripMenuItem);
        }

        private void structuresAndVoidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectGridToShow(structuresAndVoidsToolStripMenuItem);
        }

        private void swapSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstIdentGrid.SelectedItems.Count > 0)
            {
                new IdentSwapper(Map, lstIdentGrid).ShowDialog();
            }
        }

        public void SwitchToTag(MetaParser metaParser)
        {
            lstIdentGrid.Items.Clear();
            lstStringIdentifiersGrid.Items.Clear();
            lstStructuresGrid.Items.Clear();
            lstVoidsGrid.Items.Clear();
            int num;
            for (int index = 0; index < metaParser.Structures.Count; ++index)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems[0].Text = metaParser.Structures[index].Name;
                ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
                string str1 = "0x";
                num = metaParser.Structures[index].Offset;
                string str2 = num.ToString("X");
                string text1 = str1 + str2;
                subItems1.Add(text1);
                ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
                num = metaParser.Structures[index].Count;
                string text2 = num.ToString();
                subItems2.Add(text2);
                ListViewItem.ListViewSubItemCollection subItems3 = listViewItem.SubItems;
                num = metaParser.Structures[index].Size;
                string text3 = num.ToString();
                subItems3.Add(text3);
                ListViewItem.ListViewSubItemCollection subItems4 = listViewItem.SubItems;
                string str3 = "0x";
                num = metaParser.Structures[index].Pointer;
                string str4 = num.ToString("X");
                string text4 = str3 + str4;
                subItems4.Add(text4);
                lstStructuresGrid.Items.Add(listViewItem);
            }
            for (int index = 0; index < metaParser.Idents.Count; ++index)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems[0].Text = metaParser.Idents[index].Name;
                ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
                string str1 = "0x";
                num = metaParser.Idents[index].Offset;
                string str2 = num.ToString("X");
                string text1 = str1 + str2;
                subItems1.Add(text1);
                ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
                string str3 = "0x";
                num = metaParser.Idents[index].ID;
                string str4 = num.ToString("X");
                string text2 = str3 + str4;
                subItems2.Add(text2);
                listViewItem.SubItems.Add(metaParser.Idents[index].TagClass);
                listViewItem.SubItems.Add(metaParser.Idents[index].TagName);
                lstIdentGrid.Items.Add(listViewItem);
            }
            for (int index = 0; index < metaParser.Strings.Count; ++index)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems[0].Text = metaParser.Strings[index].Name;
                ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
                string str1 = "0x";
                num = metaParser.Strings[index].Offset;
                string str2 = num.ToString("X");
                string text1 = str1 + str2;
                subItems1.Add(text1);
                ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
                string str3 = "0x";
                num = metaParser.Strings[index].Identifier;
                string str4 = num.ToString("X");
                string text2 = str3 + str4;
                subItems2.Add(text2);
                listViewItem.SubItems.Add(metaParser.Strings[index].StringName);
                lstStringIdentifiersGrid.Items.Add(listViewItem);
            }
            for (int index = 0; index < metaParser.Tag_Data_Blocks.Count; ++index)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.SubItems[0].Text = metaParser.Tag_Data_Blocks[index].Name;
                ListViewItem.ListViewSubItemCollection subItems1 = listViewItem.SubItems;
                string str1 = "0x";
                num = metaParser.Tag_Data_Blocks[index].Offset;
                string str2 = num.ToString("X");
                string text1 = str1 + str2;
                subItems1.Add(text1);
                ListViewItem.ListViewSubItemCollection subItems2 = listViewItem.SubItems;
                string str3 = "0x";
                num = metaParser.Tag_Data_Blocks[index].Size;
                string str4 = num.ToString("X");
                string text2 = str3 + str4;
                subItems2.Add(text2);
                ListViewItem.ListViewSubItemCollection subItems3 = listViewItem.SubItems;
                string str5 = "0x";
                num = metaParser.Tag_Data_Blocks[index].Pointer;
                string str6 = num.ToString("X");
                string text3 = str5 + str6;
                subItems3.Add(text3);
                lstVoidsGrid.Items.Add(listViewItem);
            }
            switch (AppSettings.Settings.LastGridView)
            {
                case Ascension.Settings.Settings.LastGridViews.Structure:
                    SelectGridToShow(structuresAndVoidsToolStripMenuItem);
                    break;
                case Ascension.Settings.Settings.LastGridViews.Ident:
                    SelectGridToShow(tagReferencesToolStripMenuItem);
                    break;
                case Ascension.Settings.Settings.LastGridViews.Strings:
                    SelectGridToShow(stringIdentifiersToolStripMenuItem);
                    break;
                case Ascension.Settings.Settings.LastGridViews.Voids:
                    SelectGridToShow(tagDataVoidsToolStripMenuItem);
                    break;
                default:
                    SelectGridToShow(structuresAndVoidsToolStripMenuItem);
                    break;
            }
            lstStructuresGrid.Dock = DockStyle.Fill;
            lstIdentGrid.Dock = DockStyle.Fill;
            lstStringIdentifiersGrid.Dock = DockStyle.Fill;
            lstVoidsGrid.Dock = DockStyle.Fill;
            lstStructuresGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstIdentGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstStringIdentifiersGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lstVoidsGrid.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            Map = metaParser.Map;
        }

        private void tagDataVoidsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectGridToShow(tagDataVoidsToolStripMenuItem);
        }

        private void tagReferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectGridToShow(tagReferencesToolStripMenuItem);
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

