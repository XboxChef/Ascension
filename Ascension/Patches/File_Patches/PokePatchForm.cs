using Ascension.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ascension.Patches.File_Patches
{
    public partial class PokePatchForm : Form
    {
        private DateTime _initializedTime;

        public PokePatchForm()
        {
            InitializeComponent();
            Initialized_Time = DateTime.Now;
            label3.Text = getfilename(AppSettings.Settings.Patch_1_Map, 4);
            label4.Text = getfilename(AppSettings.Settings.Patch_2_Map, 4);
            label5.Text = getfilename(AppSettings.Settings.Patch_3_Map, 4);
            label6.Text = AppSettings.Settings.Patch_1_Description;
            label7.Text = AppSettings.Settings.Patch_2_Description;
            label8.Text = AppSettings.Settings.Patch_3_Description;
            patch1.Text = getfilename(AppSettings.Settings.Patch_1, 9);
            patch2.Text = getfilename(AppSettings.Settings.Patch_2, 9);
            patch3.Text = getfilename(AppSettings.Settings.Patch_3, 9);
            if (patch1.Text == "NULL")
            {
                patch1.Enabled = false;
            }
            if (patch2.Text == "NULL")
            {
                patch2.Enabled = false;
            }
            if (patch3.Text == "NULL")
            {
                patch3.Enabled = false;
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Ascension Patch File(.ascpatch)|*.ascpatch"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                patchtext.Text = dialog.FileName;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((patchtext.Text == "") | (maptext.Text == ""))
            {
                MessageBox.Show("One of the fields were not entered correctly. Please enter them before continuing.");
            }
            else
            {
                tabControl1.Enabled = false;
                AscendedPatch.PokePatch(maptext.Text, patchtext.Text);
                Output(getfilename(patchtext.Text, 9), this);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Halo Map Files(.map)|*.map"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                maptext.Text = dialog.FileName;
            }
        }

        private string getfilename(string filepath, int remove_end)
        {
            if (filepath != "")
            {
                string[] source = filepath.Split(new char[] { '\\' });
                string str = source[source.Count<string>() - 1];
                return str.Remove(str.Length - remove_end, remove_end);
            }
            return "NULL";
        }


        private void Output(string msg, PokePatchForm form)
        {
            DateTime time = form.Initialized_Time;
            TimeSpan span = DateTime.Now.Subtract(time);
            string str = span.Hours.ToString();
            string str2 = span.Minutes.ToString();
            string str3 = span.Seconds.ToString();
            while (str.Length != 2)
            {
                str = "0" + str;
            }
            while (str2.Length != 2)
            {
                str2 = "0" + str2;
            }
            while (str3.Length != 2)
            {
                str3 = "0" + str3;
            }
            bool flag = form.textBox1.SelectionStart >= (form.textBox1.Text.Length - 1);
            string text = form.textBox1.Text;
            form.textBox1.Text = text + "[" + str + ":" + str2 + ":" + str3 + "] Poked Patch " + msg + "\r\n";
            if (flag)
            {
                form.textBox1.SelectionStart = form.textBox1.Text.Length - 1;
                form.textBox1.ScrollToCaret();
            }
            tabControl1.Enabled = true;
        }

        private void patch1_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            AscendedPatch.PokePatch(AppSettings.Settings.Patch_1_Map, AppSettings.Settings.Patch_1);
            Output(patch1.Text, this);
        }

        private void patch2_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            AscendedPatch.PokePatch(AppSettings.Settings.Patch_2_Map, AppSettings.Settings.Patch_2);
            Output(patch2.Text, this);
        }

        private void patch3_Click(object sender, EventArgs e)
        {
            tabControl1.Enabled = false;
            AscendedPatch.PokePatch(AppSettings.Settings.Patch_3_Map, AppSettings.Settings.Patch_3);
            Output(patch3.Text, this);
        }

        public DateTime Initialized_Time
        {
            get
            {
                return _initializedTime;
            }
            set
            {
                _initializedTime = value;
            }
        }
    }
}
