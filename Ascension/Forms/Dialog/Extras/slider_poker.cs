namespace Ascension.Forms.Dialog.Extras
{
    using Ascension.Forms.Dialog.Extras.Controls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class slider_poker : Form
    {
        private IContainer components = null;
        private List<ControlItem> Items = new List<ControlItem>();
        private Panel panel1;

        public slider_poker(string nm)
        {
            InitializeComponent();
            Text = nm;
            if (nm == "Scenery")
            {
                Items.Add(new ControlItem(0x8204812c, -10f, 20f, "Thermal", "Float"));
                Items.Add(new ControlItem(0x83150da8, -10f, 10f, "Brightness", "Float"));
                Items.Add(new ControlItem(0x82047d08, 0f, 100f, "Screen Grains", "Float"));
                Items.Add(new ControlItem(0x82026aa0, 0f, 10f, "TV Style", "Float"));
                Items.Add(new ControlItem(0x82047d48, 0f, 2f, "Red / Blue", "Double"));
                Items.Add(new ControlItem(0x82046c74, 0f, 7f, "Screen Zoom", "Float"));
                Items.Add(new ControlItem(0x820271d8, -10f, 100f, "LOD Distance", "Float"));
            }
            if (nm == "Vehicles")
            {
                Items.Add(new ControlItem(0x8203f9ac, -1f, 5f, "Friction", "Float"));
                Items.Add(new ControlItem(0x82035ee4, -1f, 1f, "Air Controls", "Float"));
                Items.Add(new ControlItem(0x82000978, -1f, 5000f, "Turn Delay", "Float"));
                Items.Add(new ControlItem(0x82000970, -1f, 500f, "Turn Response", "Float"));
            }
            if (nm == "Player Hud")
            {
                Items.Add(new ControlItem(0x82a47824, -2f, 2f, "HUD Stretch X", "Float"));
                Items.Add(new ControlItem(0x82a47828, -2f, 2f, "HUD Stretch Y", "Float"));
                Items.Add(new ControlItem(0x83150d1c, -1500f, 1500f, "HUD Offset X", "Float"));
                Items.Add(new ControlItem(0x83150d20, -1000f, 1000f, "HUD Offset Y", "Float"));
            }
            foreach (ControlItem item in Items)
            {
                int count = panel1.Controls.Count;
                xex_slider _slider = new xex_slider();
                _slider.setValues(item.Name, item.Offset, item.Start, item.Range, item.PType);
                _slider.Location = new Point(0, count * 0x23);
                panel1.Controls.Add(_slider);
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
            panel1 = new Panel();
            base.SuspendLayout();
            panel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            panel1.AutoScroll = true;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(0x228, 150);
            panel1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x240, 0xae);
            base.Controls.Add(panel1);
            base.Name = "slider_poker";
            Text = "Sliders";
            base.ResumeLayout(false);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ControlItem
        {
            public uint Offset;
            public float Start;
            public float Range;
            public string Name;
            public string PType;
            public ControlItem(uint off, float srt, float range, string nm, string tp)
            {
                Offset = off;
                Start = srt;
                Range = range;
                Name = nm;
                PType = tp;
            }
        }
    }
}

