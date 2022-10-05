using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace LordsAndKnightsBot
{
    public partial class Profile : Form
    {
        Main parentForm;
        String username;
        String userid;

        public Profile(String id, Main parent)
        {
            InitializeComponent();

            parentForm = parent;
            String response = parent.GetHtml(Properties.Settings.Default.world + "/wa/ProfileAction/playerInformation?callback=&id=" + id + "&PropertyListVersion=3");
            response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
            Clipboard.SetText(response);
            if (!response.Contains("error"))
            {
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response);
                labelName.Text = data["Player"]["nick"];
                labelAlliance.Text = data["Player"]["alliance"]["name"];
                labelPoints.Text = data["Player"]["points"];
                int castles = 0;
                foreach (var v in data["Player"]["habitatDictionary"]) castles++;
                labelCastles.Text = castles.ToString();
                labelVacation.Visible = Convert.ToBoolean(data["Player"]["isOnVacation"].ToString());
                labelRank.Text = data["Player"]["rank"].ToString();

                userid = id;
                username = data["Player"]["nick"];

                if (id == Properties.Settings.Default.user_id) drawCircle("5");
                else
                {
                    if (data["Player"]["alliance"]["id"] == parent.playerAllianceId) drawCircle("4");
                    else
                    {
                        Diplomacy dip = parent.diplomacies.Find(delegate(Diplomacy d) { return d.Id == data["Player"]["alliance"]["id"].ToString(); });
                        if (dip != null) drawCircle(dip.Relation);
                        else drawCircle("0");
                    }
                }

                reposition();
            }
            else
            {
                labelName.Text = "Unable to call info.";
                linkLabel1.Enabled = false;
                labelVacation.Visible = false;
            }
        }

        private void drawCircle(String Relationship)
        {
            Color c;

            switch (Relationship)
            {
                case "-1": //enemy
                    c = Color.Red;
                    break;
                case "0": //Yourself
                    c = Color.Black;
                    break;
                case "1": //NAP
                    c = Color.DodgerBlue;
                    break;
                case "2": //allied
                    c = Color.GreenYellow;
                    break;
                case "3": //vassal
                    c = Color.DarkOrange;
                    break;
                case "4": //Same alliance
                    c = Color.OrangeRed;
                    break;
                case "5": //Yourself
                    c = Color.Gray;
                    break;
                default:
                    return;
            }

            Bitmap b = new Bitmap(24, 24);
            Graphics g = Graphics.FromImage(b);
            Brush br = new SolidBrush(c);
            g.FillEllipse(br, 0, 0, 24, 24);
            pictureBoxAlliedStatus.Image = b;
        }

        private void reposition()
        {
            labelName.Left = (this.Width - labelName.Width) / 2;
            labelAlliance.Left = (this.Width - labelAlliance.Width) / 2;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new NewMessageForm(parentForm, new User() { Name = username, id = userid }).Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("l+k://player?" + userid + "&" + Properties.Settings.Default.world_id);
        }
    }
}
