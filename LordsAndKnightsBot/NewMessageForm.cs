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
    public partial class NewMessageForm : Form
    {
        Main parent;
        User user;

        public NewMessageForm(Main parentForm, User recipient)
        {
            InitializeComponent();

            parent = parentForm;
            user = recipient;
            labelRecipient.Text = recipient.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxSubject.Text == String.Empty)
            {
                MessageBox.Show("You have to enter a subject.");
                return;
            }
            if (textBoxMessage.Text == String.Empty)
            {
                MessageBox.Show("You have to enter a message content.");
                return;
            }
            parent.GetHtml(Properties.Settings.Default.world + "/wa/DiscussionAction/createDiscussion?callback=&receivingPlayerArray=" + user.id + "&subject=" + textBoxSubject.Text + "&content=" + textBoxMessage.Text + "&PropertyListVersion=3");
            MessageBox.Show("Sent message.");
            this.Close();
        }
    }
}
