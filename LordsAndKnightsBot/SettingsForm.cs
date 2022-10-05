using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LordsAndKnightsBot
{
    public partial class SettingsForm : Form
    {
        Main parent = null;
        public SettingsForm(Main Parent)
        {
            InitializeComponent();

            Rectangle rect = new Rectangle(tabPage1.Left, tabPage1.Top, tabPage1.Width, tabPage1.Height);
            tabControl1.Region = new Region(rect);

            textBoxUserAgent.Text = Properties.Settings.Default.useragent;
            checkBoxProxy.Checked = Properties.Settings.Default.proxyEnabled;
            textBoxProxyIp.Text = Properties.Settings.Default.proxyIp;
            textBoxProxyPort.Text = Properties.Settings.Default.proxyPort.ToString();

            pictureBox1.BackColor = Properties.Settings.Default.ResourceAlmostFull;
            pictureBox2.BackColor = Properties.Settings.Default.ResourceFull;

            checkBox1.Checked = Properties.Settings.Default.autoUpgrade;

            parent = Parent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            listBox1.Items.Clear();
            foreach (String s in parent.log)
            {
                listBox1.Items.Add(s);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ResourceAlmostFull = c.Color;
                pictureBox1.BackColor = c.Color;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ResourceFull = c.Color;
                pictureBox2.BackColor = c.Color;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoUpgrade = checkBox1.Checked;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            parent.log.Clear();
        }
    }
}
