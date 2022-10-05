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
    public partial class RecruitUnitForm : Form
    {
        Unit unit = new Unit();
        Castle castle = new Castle();
        Main parent;

        public RecruitUnitForm(Castle castle, Unit unitToUpgrade, Main thisForm)
        {
            InitializeComponent();

            this.unit = unitToUpgrade;
            this.castle = castle;
            this.parent = thisForm;

            this.Text = "Recruit " + unit.Name;

            double maximum = 0;
            maximum = Convert.ToInt32(castle.peopleStoreAmount) - Convert.ToInt32(castle.people);
            if (Math.Floor(Convert.ToDouble(castle.wood) / Convert.ToDouble(unit.WoodCost)) < maximum) maximum = Math.Floor(Convert.ToDouble(castle.wood) / Convert.ToDouble(unit.WoodCost));
            if (Math.Floor(Convert.ToDouble(castle.stone) / Convert.ToDouble(unit.StoneCost)) < maximum) maximum = Math.Floor(Convert.ToDouble(castle.stone) / Convert.ToDouble(unit.StoneCost));
            if (Math.Floor(Convert.ToDouble(castle.ore) / Convert.ToDouble(unit.OreCost)) < maximum) maximum = Math.Floor(Convert.ToDouble(castle.ore) / Convert.ToDouble(unit.OreCost));
            trackBarUnitCount.Maximum = Convert.ToInt32(maximum);
            numericUpDownUnitCount.Maximum = Convert.ToDecimal(maximum);

            switch (unit.Name)
            {
                case "Archer":
                    setData(Properties.Resources.Archer);
                    break;
                case "Armoured Horseman":
                    setData(Properties.Resources.Scorpion_Rider);
                    break;
                case "Crossbowman":
                    setData(Properties.Resources.Crossbowman);
                    break;
                case "Handcart":
                    setData(Properties.Resources.Pushcart);
                    break;
                case "Lancer Horseman":
                    setData(Properties.Resources.Lancer);
                    break;
                case "Ox Cart":
                    setData(Properties.Resources.Oxcart);
                    break;
                case "Spearman":
                    setData(Properties.Resources.Spearman);
                    break;
                case "Swordsman":
                    setData(Properties.Resources.Swordman);
                    break;
                default:
                    this.Close();
                    break;
            }
        }

        private void setData(Image image)
        {
            pictureBox7.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.recruitUnits(castle, unit, trackBarUnitCount.Value);
            this.Close();
        }

        private void trackBarUnitCount_Scroll(object sender, EventArgs e)
        {
            numericUpDownUnitCount.Value = trackBarUnitCount.Value;
        }

        private void numericUpDownUnitCount_ValueChanged(object sender, EventArgs e)
        {
            trackBarUnitCount.Value = Convert.ToInt32(numericUpDownUnitCount.Value);
        }
    }
}
