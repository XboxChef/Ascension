namespace Ascension.Halo_Reach.Game.Plugins.Forms
{
    using Ascension.Halo_Reach.Plugins;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PluginEditorForm : Form
    {
        private IContainer components = null;

        public PluginEditorForm(XmlParser parser)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginEditorForm));
            SuspendLayout();
            // 
            // PluginEditorForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(418, 146);
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            Name = "PluginEditorForm";
            Text = "Plugin Editor";
            Load += new System.EventHandler(PluginEditorForm_Load);
            ResumeLayout(false);

        }

        private void PluginEditorForm_Load(object sender, EventArgs e)
        {
        }
    }
}

