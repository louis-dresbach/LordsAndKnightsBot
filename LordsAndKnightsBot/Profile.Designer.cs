namespace LordsAndKnightsBot
{
    partial class Profile
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelAlliance = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPoints = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelCastles = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelRank = new System.Windows.Forms.Label();
            this.labelVacation = new System.Windows.Forms.Label();
            this.pictureBoxAlliedStatus = new System.Windows.Forms.PictureBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlliedStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // labelAlliance
            // 
            this.labelAlliance.AutoSize = true;
            this.labelAlliance.Font = new System.Drawing.Font("AngsanaUPC", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAlliance.Location = new System.Drawing.Point(89, 35);
            this.labelAlliance.Name = "labelAlliance";
            this.labelAlliance.Size = new System.Drawing.Size(89, 20);
            this.labelAlliance.TabIndex = 0;
            this.labelAlliance.Text = "ALLIANCE NAME";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("AngsanaUPC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.Location = new System.Drawing.Point(111, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(50, 26);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "NAME";
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.Location = new System.Drawing.Point(175, 83);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(29, 12);
            this.labelPoints.TabIndex = 2;
            this.labelPoints.Text = "0000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Points: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Castles: ";
            // 
            // labelCastles
            // 
            this.labelCastles.AutoSize = true;
            this.labelCastles.Location = new System.Drawing.Point(175, 95);
            this.labelCastles.Name = "labelCastles";
            this.labelCastles.Size = new System.Drawing.Size(29, 12);
            this.labelCastles.TabIndex = 4;
            this.labelCastles.Text = "0000";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 242);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(79, 12);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Send Message";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Rank: ";
            // 
            // labelRank
            // 
            this.labelRank.AutoSize = true;
            this.labelRank.Location = new System.Drawing.Point(175, 125);
            this.labelRank.Name = "labelRank";
            this.labelRank.Size = new System.Drawing.Size(29, 12);
            this.labelRank.TabIndex = 7;
            this.labelRank.Text = "0000";
            // 
            // labelVacation
            // 
            this.labelVacation.AutoSize = true;
            this.labelVacation.Location = new System.Drawing.Point(204, 242);
            this.labelVacation.Name = "labelVacation";
            this.labelVacation.Size = new System.Drawing.Size(68, 12);
            this.labelVacation.TabIndex = 9;
            this.labelVacation.Text = "On Vacation";
            // 
            // pictureBoxAlliedStatus
            // 
            this.pictureBoxAlliedStatus.Location = new System.Drawing.Point(14, 203);
            this.pictureBoxAlliedStatus.Name = "pictureBoxAlliedStatus";
            this.pictureBoxAlliedStatus.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxAlliedStatus.TabIndex = 10;
            this.pictureBoxAlliedStatus.TabStop = false;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(184, 215);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(88, 12);
            this.linkLabel2.TabIndex = 11;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Copy player link";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(284, 263);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.pictureBoxAlliedStatus);
            this.Controls.Add(this.labelVacation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelRank);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelCastles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelAlliance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Profile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Profile";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAlliedStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAlliance;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelCastles;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelRank;
        private System.Windows.Forms.Label labelVacation;
        private System.Windows.Forms.PictureBox pictureBoxAlliedStatus;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}