namespace Ascension.Halo_Reach.Game.Tag_Editor.Dialog
{
    using Ascension.Halo_Reach.Game.Tag_Editor.Classes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class TagEditorSettingsDialog : Form
    {
        private IContainer components = null;
        private PropertyGrid propertyGrid1;

        public TagEditorSettingsDialog()
        {
            InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagEditorSettingsDialog));
            propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            SuspendLayout();
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            propertyGrid1.Location = new System.Drawing.Point(0, 0);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.Size = new System.Drawing.Size(389, 245);
            propertyGrid1.TabIndex = 1;
            // 
            // TagEditorSettingsDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(389, 245);
            Controls.Add(propertyGrid1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TagEditorSettingsDialog";
            Text = "Tag Editor Settings";
            Load += new System.EventHandler(TagEditorSettingsDialog_Load);
            ResumeLayout(false);

        }

        private void TagEditorSettingsDialog_Load(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = TagEditorSettings.Visibility_Settings;
        }
    }
}

