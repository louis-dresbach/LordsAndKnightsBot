using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace LordsAndKnightsBot
{
    public partial class Messages : Form
    {
        Main parent;
        List<message> messages = new List<message>();
        message currentMessage = new message();
        List<User> currentPlayers = new List<User>();
        RichTextBoxEx richTextBox = new RichTextBoxEx();

        public Messages(Main parentForm)
        {
            InitializeComponent();
            reposition();

            parent = parentForm;

            richTextBox.Width = 370;
            richTextBox.Height = 261;
            richTextBox.ReadOnly = true;
            richTextBox.BackColor = Color.White;
            richTextBox.Location = new Point(12, 56);
            richTextBox.DetectUrls = true;
            richTextBox.LinkClicked += new LinkClickedEventHandler(richTextBox_LinkClicked);

            this.Controls.Add(richTextBox);
        }

        private void reposition()
        {
            labelTitle.Left = (this.Width - labelTitle.Width) / 2;
            labelReply.Left = (this.Width - labelReply.Width) / 2;
        }

        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                User player = currentPlayers.Find(delegate(User u) { return u.Name.StartsWith(e.LinkText); });
                new Profile(player.id, parent).Show();
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start(e.LinkText);
                }
                catch { }
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadMessage();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Top = parent.Top;
            this.Left = parent.Left + parent.Width + 8;
        }

        private void loadMessage(message msg=null)
        {
            message mess = new message();
            if (msg == null)
            {
                mess = messages.Find(delegate(message m) { return m.Title == listBox1.Text; });
                currentMessage = mess;
            }
            else mess = msg;
            String id = mess.Id;
            if (id != String.Empty)
            {
                String response = parent.GetHtml(Properties.Settings.Default.world + "/wa/DiscussionAction/discussion?callback=&discussionId=" + id + "&PropertyListVersion=3");
                response = response.TrimStart('(').TrimEnd(')').Replace(" = ", " : ").Replace(";", "");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response);

                richTextBox.ResetText();
                richTextBox.Refresh();
                List<discussionEntry> entries = new List<discussionEntry>();

                foreach (var v in data["discussion"]["discussionMemberArray"])
                {
                    currentPlayers.Add(new User() { Name = v["nick"], id = v["id"] });
                }

                foreach (var v in data["discussion"]["discussionEntryArray"])
                {
                    String dateString = v["creationDate"].ToString().Replace("Z", "").Replace("T", " ");
                    try
                    {
                        DateTime time = DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", null);
                        entries.Add(new discussionEntry() { Time = time, message = v["content"], userId = v["playerId"].ToString() });
                    }
                    catch { }
                }
                 
                IOrderedEnumerable<discussionEntry> ordered = entries.OrderBy(x => x.Time);
                foreach (var v in ordered)
                {
                    User p = currentPlayers.Find(delegate(User u) { return u.id == v.userId; }); 
                    richTextBox.InsertLink(p.Name.ToString());
                    richTextBox.AppendText("  -  " + v.Time + Environment.NewLine + v.message + Environment.NewLine + Environment.NewLine);
                }
                labelTitle.Text = mess.Title;
            }
            else
            {
                labelTitle.Text = "-----";
                richTextBox.Text = "Unable to load message(s).";
            }
            reposition();
        }

        protected override void OnShown(EventArgs e)
        {
            messages = parent.loadMessages();

            foreach (message m in messages)
            {
                listBox1.Items.Add(m.Title);
            }
            listBox1.SelectedIndex = 0;
            loadMessage();

            base.OnShown(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parent.GetHtml(Properties.Settings.Default.world + "/wa/DiscussionAction/addDiscussionEntry?callback=&discussionId=" + currentMessage.Id + "&content=" + textBox1.Text + "&PropertyListVersion=3");
            loadMessage(currentMessage);
            textBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this message?", "L&K Bot", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                parent.GetHtml(Properties.Settings.Default.world + "/wa/DiscussionAction/releaseFromDiscussionV2?callback=&discussionId=" + currentMessage.Id + "&PropertyListVersion=3");
            }
        }
    }
    public class message
    {
        public String Title
        {
            get;
            set;
        }
        public String Id
        {
            get;
            set;
        }
    }
    class discussionEntry
    {
        public DateTime Time
        {
            get;
            set;
        }
        public String message
        {
            get;
            set;
        }
        public String userId
        {
            get;
            set;
        }
    }
    public class User
    {
        public String id
        {
            get;
            set;
        }
        public String Name
        {
            get;
            set;
        }
    }
}
