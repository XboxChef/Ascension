namespace Ascension.Forms.Dialog.Extras
{
    using Ascension.Settings;
    using HaloDevelopmentExtender;
    using HaloReach3d.IO;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class PlayerInfo : Form
    {
        private IContainer components = null;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private LinkLabel linkLabel1;
        private Label posx;
        private Label posy;
        private Label posz;
        private Label rotx;
        private Label roty;
        private Timer timer1;

        public PlayerInfo()
        {
            InitializeComponent();
            updatepos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updatepos();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string getValue(uint offset)
        {
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
                try
                {
                    nio.In.BaseStream.Position = offset;
                    obj2 = nio.In.ReadSingle();
                }
                catch
                {
                    return "N/A";
                }
                nio.Close();
                stream.Close();
                communicator.Disconnect();
                return obj2.ToString();
            }
            return "No Console Detected";
        }

        private void InitializeComponent()
        {
            components = new Container();
            groupBox1 = new GroupBox();
            posz = new Label();
            posy = new Label();
            posx = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox2 = new GroupBox();
            roty = new Label();
            rotx = new Label();
            label5 = new Label();
            label4 = new Label();
            linkLabel1 = new LinkLabel();
            timer1 = new Timer(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            base.SuspendLayout();
            groupBox1.Controls.Add(posz);
            groupBox1.Controls.Add(posy);
            groupBox1.Controls.Add(posx);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(0x88, 60);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Position";
            posz.AutoSize = true;
            posz.Location = new Point(0x26, 0x2a);
            posz.Name = "posz";
            posz.Size = new Size(0x1d, 13);
            posz.TabIndex = 1;
            posz.Text = "posz";
            posy.AutoSize = true;
            posy.Location = new Point(0x26, 0x1d);
            posy.Name = "posy";
            posy.Size = new Size(0x1d, 13);
            posy.TabIndex = 1;
            posy.Text = "posy";
            posx.AutoSize = true;
            posx.Location = new Point(0x26, 0x10);
            posx.Name = "posx";
            posx.Size = new Size(0x1d, 13);
            posx.TabIndex = 1;
            posx.Text = "posx";
            label3.AutoSize = true;
            label3.Location = new Point(6, 0x2a);
            label3.Name = "label3";
            label3.Size = new Size(0x1a, 13);
            label3.TabIndex = 0;
            label3.Text = "Z = ";
            label2.AutoSize = true;
            label2.Location = new Point(6, 0x1d);
            label2.Name = "label2";
            label2.Size = new Size(0x1a, 13);
            label2.TabIndex = 0;
            label2.Text = "Y = ";
            label1.AutoSize = true;
            label1.Location = new Point(6, 0x10);
            label1.Name = "label1";
            label1.Size = new Size(0x1a, 13);
            label1.TabIndex = 0;
            label1.Text = "X = ";
            groupBox2.Controls.Add(roty);
            groupBox2.Controls.Add(rotx);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label4);
            groupBox2.Location = new Point(12, 0x4e);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(0x88, 0x31);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Rotation";
            roty.AutoSize = true;
            roty.Location = new Point(0x20, 0x1d);
            roty.Name = "roty";
            roty.Size = new Size(0x18, 13);
            roty.TabIndex = 1;
            roty.Text = "roty";
            rotx.AutoSize = true;
            rotx.Location = new Point(0x20, 0x10);
            rotx.Name = "rotx";
            rotx.Size = new Size(0x18, 13);
            rotx.TabIndex = 1;
            rotx.Text = "rotx";
            label5.AutoSize = true;
            label5.Location = new Point(6, 0x1d);
            label5.Name = "label5";
            label5.Size = new Size(0x1a, 13);
            label5.TabIndex = 0;
            label5.Text = "Y = ";
            label4.AutoSize = true;
            label4.Location = new Point(6, 0x10);
            label4.Name = "label4";
            label4.Size = new Size(0x1a, 13);
            label4.TabIndex = 0;
            label4.Text = "X = ";
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(0x12, 130);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(130, 13);
            linkLabel1.TabIndex = 2;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Copy Co-ords to ClipBoard";
            linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
            timer1.Enabled = true;
            timer1.Tick += new EventHandler(timer1_Tick);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0xa4, 0x93);
            base.Controls.Add(linkLabel1);
            base.Controls.Add(groupBox2);
            base.Controls.Add(groupBox1);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.Name = "PlayerInfo";
            Text = "PlayerInfo";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText($"Position: X = {posx.Text}, Y = {posy.Text}, Z = {posz.Text}");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatepos();
        }

        public void updatepos()
        {
            posx.Text = getValue(0x83223b90);
            posy.Text = getValue(0x83223b94);
            posz.Text = getValue(0x83223b98);
            rotx.Text = getValue(0x82b7f6c0);
            roty.Text = getValue(0x82b7f6c4);
        }
    }
}

