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
    public partial class changeResources : Form
    {
        Main parent;
        Castle castle;
        String resource;
        
        public changeResources(Main parentForm, Castle castle, String resource="Silver")
        {
            InitializeComponent();
            parent = parentForm;
            this.Text = "Exchange " + resource + " in " + castle.name;
            this.castle = castle;
            this.resource = resource;

            trackBarSpearmen.Maximum = Convert.ToInt32(castle.spearmen);
            trackBarSwordsmen.Maximum = Convert.ToInt32(castle.swordsmen);
            trackBarArchers.Maximum = Convert.ToInt32(castle.archers);
            trackBarCrossbowmen.Maximum = Convert.ToInt32(castle.crossbowmen);
            trackBarScorpions.Maximum = Convert.ToInt32(castle.scorpions);
            trackBarLancers.Maximum = Convert.ToInt32(castle.lancers);
            trackBarPushCarts.Maximum = Convert.ToInt32(castle.pushCarts);
            trackBarOxCarts.Maximum = Convert.ToInt32(castle.oxCarts);
            numericUpDownSpearmen.Maximum = Convert.ToInt32(castle.spearmen);
            numericUpDownSwordsmen.Maximum = Convert.ToInt32(castle.swordsmen);
            numericUpDownArchers.Maximum = Convert.ToInt32(castle.archers);
            numericUpDownCrossbowmen.Maximum = Convert.ToInt32(castle.crossbowmen);
            numericUpDownScorpions.Maximum = Convert.ToInt32(castle.scorpions);
            numericUpDownLancers.Maximum = Convert.ToInt32(castle.lancers);
            numericUpDownPushCarts.Maximum = Convert.ToInt32(castle.pushCarts);
            numericUpDownOxCarts.Maximum = Convert.ToInt32(castle.oxCarts);

            update();
        }

        private void update()
        {
            label1.Text = resource + ": " + "0";
        }

        private void trackBarPushCarts_Scroll(object sender, EventArgs e)
        {
            numericUpDownPushCarts.Value = trackBarPushCarts.Value;
        }

        private void numericUpDownPushCarts_ValueChanged(object sender, EventArgs e)
        {
            trackBarPushCarts.Value = Convert.ToInt32(numericUpDownPushCarts.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.exchangeResource(castle, resource);
            this.Close();
        }
    }
}
