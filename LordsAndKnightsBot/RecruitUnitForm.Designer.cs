namespace LordsAndKnightsBot
{
    partial class RecruitUnitForm
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
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.numericUpDownUnitCount = new System.Windows.Forms.NumericUpDown();
            this.trackBarUnitCount = new System.Windows.Forms.TrackBar();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUnitCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUnitCount)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::LordsAndKnightsBot.Properties.Resources.Spearman;
            this.pictureBox7.Location = new System.Drawing.Point(5, 14);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(37, 61);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 25;
            this.pictureBox7.TabStop = false;
            // 
            // numericUpDownUnitCount
            // 
            this.numericUpDownUnitCount.Location = new System.Drawing.Point(251, 24);
            this.numericUpDownUnitCount.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.numericUpDownUnitCount.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericUpDownUnitCount.Name = "numericUpDownUnitCount";
            this.numericUpDownUnitCount.Size = new System.Drawing.Size(158, 19);
            this.numericUpDownUnitCount.TabIndex = 24;
            this.numericUpDownUnitCount.ValueChanged += new System.EventHandler(this.numericUpDownUnitCount_ValueChanged);
            // 
            // trackBarUnitCount
            // 
            this.trackBarUnitCount.Location = new System.Drawing.Point(50, 24);
            this.trackBarUnitCount.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.trackBarUnitCount.Maximum = 0;
            this.trackBarUnitCount.Name = "trackBarUnitCount";
            this.trackBarUnitCount.Size = new System.Drawing.Size(191, 45);
            this.trackBarUnitCount.TabIndex = 23;
            this.trackBarUnitCount.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarUnitCount.Scroll += new System.EventHandler(this.trackBarUnitCount_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 67);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(158, 23);
            this.button1.TabIndex = 26;
            this.button1.Text = "Recruit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RecruitUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 100);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.numericUpDownUnitCount);
            this.Controls.Add(this.trackBarUnitCount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RecruitUnitForm";
            this.Text = "Recruit UNIT";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownUnitCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarUnitCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.NumericUpDown numericUpDownUnitCount;
        private System.Windows.Forms.TrackBar trackBarUnitCount;
        private System.Windows.Forms.Button button1;


    }
}