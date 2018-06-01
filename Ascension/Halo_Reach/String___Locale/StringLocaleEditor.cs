namespace Ascension.Halo_Reach.String___Locale
{
    using HaloReach3d.Locale;
    using HaloReach3d.Map;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class StringLocaleEditor : UserControl
    {
        private LocaleHandler _localehandler;
        private HaloMap _map;
        private Button btnApplyFilter;
        private Button btnSaveChanges;
        private ComboBox cmbxLanguage;
        internal ColumnHeader colIndex;
        private ColumnHeader colLength;
        internal ColumnHeader colName;
        internal ColumnHeader colOffset;
        private IContainer components = null;
        private Label label1;
        private Label lblLanguage;
        private Label lblLocaleCount;
        private Label lblLocaleIndexOffset;
        private Label lblLocaleTableOffset;
        private Label lblLocaleTableSize;
        public string LocaleFilter = "";
        internal ListView localeGrid;
        private Panel pnlLocaleDetails;
        public string StringFilter = "";
        private TextBox txtFilter;
        private TextBox txtLocaleCount;
        private TextBox txtLocaleIndexOffset;
        private TextBox txtLocaleTableOffset;
        private TextBox txtLocaleTableSize;
        private TextBox txtSelectedLocale;

        public StringLocaleEditor(HaloMap map)
        {
            InitializeComponent();
            Map = map;
            Locale_Handler = new LocaleHandler(Map);
            cmbxLanguage.SelectedIndex = 0;
            localeGrid.Focus();
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            LocaleFilter = txtFilter.Text;
            LoadLocales(LocaleFilter);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (localeGrid.SelectedItems.Count != 0)
            {
                bool flag = txtSelectedLocale.Text.Length == localeGrid.SelectedItems[0].SubItems[1].Text.Length;
                int stringIndex = int.Parse(localeGrid.SelectedItems[0].Text);
                Locale_Handler.SaveLocale(cmbxLanguage.SelectedIndex, stringIndex, txtSelectedLocale.Text);
                localeGrid.SelectedItems[0].SubItems[1].Text = txtSelectedLocale.Text;
                if (!flag)
                {
                    Locale_Handler = new LocaleHandler(Map);
                    int selectedIndex = cmbxLanguage.SelectedIndex;
                    LocaleHandler.LocaleTable table = Locale_Handler.LocaleTables[selectedIndex];
                    txtLocaleCount.Text = table.LocaleCount.ToString();
                    txtLocaleTableOffset.Text = table.LocaleTableOffset.ToString();
                    txtLocaleTableSize.Text = table.LocaleTableSize.ToString();
                    txtLocaleIndexOffset.Text = table.LocaleTableIndexOffset.ToString();
                    localeGrid.SelectedItems[0].SubItems[3].Text = table.LocaleStrings[stringIndex].Length.ToString();
                    for (int i = localeGrid.SelectedItems[0].Index; i < localeGrid.Items.Count; i++)
                    {
                        localeGrid.Items[i].SubItems[2].Text = table.LocaleStrings[i].Offset.ToString();
                    }
                }
            }
        }

        private void cmbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLocales(LocaleFilter);
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
            localeGrid = new ListView();
            colIndex = new ColumnHeader();
            colName = new ColumnHeader();
            colOffset = new ColumnHeader();
            colLength = new ColumnHeader();
            pnlLocaleDetails = new Panel();
            btnSaveChanges = new Button();
            txtSelectedLocale = new TextBox();
            txtLocaleIndexOffset = new TextBox();
            txtLocaleTableSize = new TextBox();
            txtLocaleCount = new TextBox();
            txtLocaleTableOffset = new TextBox();
            btnApplyFilter = new Button();
            txtFilter = new TextBox();
            cmbxLanguage = new ComboBox();
            label1 = new Label();
            lblLocaleIndexOffset = new Label();
            lblLocaleTableSize = new Label();
            lblLocaleTableOffset = new Label();
            lblLocaleCount = new Label();
            lblLanguage = new Label();
            pnlLocaleDetails.SuspendLayout();
            base.SuspendLayout();
            localeGrid.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            localeGrid.BackColor = Color.White;
            localeGrid.Columns.AddRange(new ColumnHeader[] { colIndex, colName, colOffset, colLength });
            localeGrid.ForeColor = Color.Black;
            localeGrid.FullRowSelect = true;
            localeGrid.GridLines = true;
            localeGrid.Location = new Point(0, 0);
            localeGrid.MultiSelect = false;
            localeGrid.Name = "localeGrid";
            localeGrid.Size = new Size(0x18a, 0x18a);
            localeGrid.TabIndex = 9;
            localeGrid.UseCompatibleStateImageBehavior = false;
            localeGrid.View = View.Details;
            localeGrid.SelectedIndexChanged += new EventHandler(localeGrid_SelectedIndexChanged);
            colIndex.Text = "Index";
            colIndex.Width = 0x3b;
            colName.Text = "Name";
            colName.Width = 220;
            colOffset.Text = "Offset";
            colOffset.Width = 0x35;
            colLength.Text = "Length";
            colLength.Width = 0x34;
            pnlLocaleDetails.Controls.Add(txtLocaleIndexOffset);
            pnlLocaleDetails.Controls.Add(txtLocaleTableSize);
            pnlLocaleDetails.Controls.Add(txtLocaleCount);
            pnlLocaleDetails.Controls.Add(txtLocaleTableOffset);
            pnlLocaleDetails.Controls.Add(btnApplyFilter);
            pnlLocaleDetails.Controls.Add(txtFilter);
            pnlLocaleDetails.Controls.Add(cmbxLanguage);
            pnlLocaleDetails.Controls.Add(label1);
            pnlLocaleDetails.Controls.Add(lblLocaleIndexOffset);
            pnlLocaleDetails.Controls.Add(lblLocaleTableSize);
            pnlLocaleDetails.Controls.Add(lblLocaleTableOffset);
            pnlLocaleDetails.Controls.Add(lblLocaleCount);
            pnlLocaleDetails.Controls.Add(lblLanguage);
            pnlLocaleDetails.Dock = DockStyle.Right;
            pnlLocaleDetails.Location = new Point(0x18a, 0);
            pnlLocaleDetails.Name = "pnlLocaleDetails";
            pnlLocaleDetails.Size = new Size(200, 0x1fa);
            pnlLocaleDetails.TabIndex = 0;
            btnSaveChanges.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            btnSaveChanges.Location = new Point(0, 480);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(0xbf, 0x17);
            btnSaveChanges.TabIndex = 10;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += new EventHandler(buttonX1_Click);
            txtSelectedLocale.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            txtSelectedLocale.Location = new Point(0, 400);
            txtSelectedLocale.Multiline = true;
            txtSelectedLocale.Name = "txtSelectedLocale";
            txtSelectedLocale.ScrollBars = ScrollBars.Vertical;
            txtSelectedLocale.Size = new Size(0x18a, 0x4d);
            txtSelectedLocale.TabIndex = 0x1a;
            txtLocaleIndexOffset.Location = new Point(6, 0x127);
            txtLocaleIndexOffset.Name = "txtLocaleIndexOffset";
            txtLocaleIndexOffset.ReadOnly = true;
            txtLocaleIndexOffset.Size = new Size(0xbf, 20);
            txtLocaleIndexOffset.TabIndex = 0x19;
            txtLocaleTableSize.Location = new Point(6, 0xf9);
            txtLocaleTableSize.Name = "txtLocaleTableSize";
            txtLocaleTableSize.ReadOnly = true;
            txtLocaleTableSize.Size = new Size(0xbf, 20);
            txtLocaleTableSize.TabIndex = 0x18;
            txtLocaleCount.Location = new Point(6, 0x9e);
            txtLocaleCount.Name = "txtLocaleCount";
            txtLocaleCount.ReadOnly = true;
            txtLocaleCount.Size = new Size(0xbf, 20);
            txtLocaleCount.TabIndex = 0x17;
            txtLocaleTableOffset.Location = new Point(6, 0xca);
            txtLocaleTableOffset.Name = "txtLocaleTableOffset";
            txtLocaleTableOffset.ReadOnly = true;
            txtLocaleTableOffset.Size = new Size(0xbf, 20);
            txtLocaleTableOffset.TabIndex = 0x16;
            btnApplyFilter.Location = new Point(6, 0x61);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(0xbf, 0x17);
            btnApplyFilter.TabIndex = 0x15;
            btnApplyFilter.Text = "Apply Filter";
            btnApplyFilter.UseVisualStyleBackColor = true;
            btnApplyFilter.Click += new EventHandler(btnApplyFilter_Click);
            txtFilter.Location = new Point(6, 0x47);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(0xbf, 20);
            txtFilter.TabIndex = 20;
            txtFilter.TextChanged += new EventHandler(txtFilter_TextChanged);
            cmbxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbxLanguage.FormattingEnabled = true;
            cmbxLanguage.Items.AddRange(new object[] { "English", "Japanese", "German", "French", "Spanish", "Latin American Spanish", "Italian", "Korean", "Chinese", "Unknown 0", "Portuguese", "Unknown 1" });
            cmbxLanguage.Location = new Point(6, 0x15);
            cmbxLanguage.Name = "cmbxLanguage";
            cmbxLanguage.Size = new Size(0xbf, 0x15);
            cmbxLanguage.TabIndex = 0x13;
            cmbxLanguage.SelectedIndexChanged += new EventHandler(cmbxLanguage_SelectedIndexChanged);
            label1.AutoSize = true;
            label1.Location = new Point(3, 0x37);
            label1.Name = "label1";
            label1.Size = new Size(0x20, 13);
            label1.TabIndex = 0x10;
            label1.Text = "Filter:";
            lblLocaleIndexOffset.AutoSize = true;
            lblLocaleIndexOffset.Location = new Point(3, 0x117);
            lblLocaleIndexOffset.Name = "lblLocaleIndexOffset";
            lblLocaleIndexOffset.Size = new Size(0x66, 13);
            lblLocaleIndexOffset.TabIndex = 8;
            lblLocaleIndexOffset.Text = "Locale Index Offset:";
            lblLocaleTableSize.AutoSize = true;
            lblLocaleTableSize.Location = new Point(3, 0xe9);
            lblLocaleTableSize.Name = "lblLocaleTableSize";
            lblLocaleTableSize.Size = new Size(0x5f, 13);
            lblLocaleTableSize.TabIndex = 6;
            lblLocaleTableSize.Text = "Locale Table Size:";
            lblLocaleTableOffset.AutoSize = true;
            lblLocaleTableOffset.Location = new Point(3, 0xba);
            lblLocaleTableOffset.Name = "lblLocaleTableOffset";
            lblLocaleTableOffset.Size = new Size(0x67, 13);
            lblLocaleTableOffset.TabIndex = 4;
            lblLocaleTableOffset.Text = "Locale Table Offset:";
            lblLocaleCount.AutoSize = true;
            lblLocaleCount.Location = new Point(3, 0x8e);
            lblLocaleCount.Name = "lblLocaleCount";
            lblLocaleCount.Size = new Size(0x49, 13);
            lblLocaleCount.TabIndex = 2;
            lblLocaleCount.Text = "Locale Count:";
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(3, 5);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(0x3a, 13);
            lblLanguage.TabIndex = 0;
            lblLanguage.Text = "Language:";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(btnSaveChanges);
            base.Controls.Add(localeGrid);
            base.Controls.Add(txtSelectedLocale);
            base.Controls.Add(pnlLocaleDetails);
            base.Name = "StringLocaleEditor";
            base.Size = new Size(0x252, 0x1fa);
            pnlLocaleDetails.ResumeLayout(false);
            pnlLocaleDetails.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void LoadLocales(string filter)
        {
            int selectedIndex = cmbxLanguage.SelectedIndex;
            LocaleHandler.LocaleTable table = Locale_Handler.LocaleTables[selectedIndex];
            txtLocaleCount.Text = table.LocaleCount.ToString();
            txtLocaleTableOffset.Text = table.LocaleTableOffset.ToString();
            txtLocaleTableSize.Text = table.LocaleTableSize.ToString();
            txtLocaleIndexOffset.Text = table.LocaleTableIndexOffset.ToString();
            localeGrid.Items.Clear();
            for (int i = 0; i < table.LocaleCount; i++)
            {
                if (table.LocaleStrings[i].Name.ToLower().Contains(filter.ToLower()))
                {
                    ListViewItem item = new ListViewItem {
                        Text = i.ToString(),
                        SubItems = { 
                            table.LocaleStrings[i].Name,
                            table.LocaleStrings[i].Offset.ToString(),
                            table.LocaleStrings[i].Length.ToString()
                        }
                    };
                    localeGrid.Items.Add(item);
                }
            }
        }

        private void localeGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localeGrid.SelectedItems.Count != 0)
            {
                txtSelectedLocale.Text = localeGrid.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
        }

        public LocaleHandler Locale_Handler
        {
            get
            {
                return _localehandler;
            }
            set
            {
                _localehandler = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

