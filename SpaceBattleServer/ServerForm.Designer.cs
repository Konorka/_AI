namespace SpaceBattle_Server
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpToolbox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lstClients = new System.Windows.Forms.ListBox();
            this.chkToolbox = new System.Windows.Forms.CheckBox();
            this.grpMapMask = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtMaskPlayer = new System.Windows.Forms.TextBox();
            this.chkMapMask = new System.Windows.Forms.CheckBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.chkMessages = new System.Windows.Forms.CheckBox();
            this.grpMessages = new System.Windows.Forms.GroupBox();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.chkTimer = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpToolbox.SuspendLayout();
            this.grpMapMask.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.grpMessages.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(750, 750);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // grpToolbox
            // 
            this.grpToolbox.Controls.Add(this.button1);
            this.grpToolbox.Controls.Add(this.lstClients);
            this.grpToolbox.Location = new System.Drawing.Point(645, 184);
            this.grpToolbox.Name = "grpToolbox";
            this.grpToolbox.Size = new System.Drawing.Size(348, 177);
            this.grpToolbox.TabIndex = 7;
            this.grpToolbox.TabStop = false;
            this.grpToolbox.Text = "Toolbox";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(132, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Start!";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Start_Click);
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.Location = new System.Drawing.Point(6, 19);
            this.lstClients.Name = "lstClients";
            this.lstClients.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstClients.Size = new System.Drawing.Size(120, 147);
            this.lstClients.TabIndex = 0;
            // 
            // chkToolbox
            // 
            this.chkToolbox.AutoSize = true;
            this.chkToolbox.Checked = true;
            this.chkToolbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkToolbox.Location = new System.Drawing.Point(929, 12);
            this.chkToolbox.Name = "chkToolbox";
            this.chkToolbox.Size = new System.Drawing.Size(52, 17);
            this.chkToolbox.TabIndex = 8;
            this.chkToolbox.Text = "Tools";
            this.chkToolbox.UseVisualStyleBackColor = true;
            this.chkToolbox.CheckedChanged += new System.EventHandler(this.chkToolbox_CheckedChanged);
            // 
            // grpMapMask
            // 
            this.grpMapMask.Controls.Add(this.pictureBox2);
            this.grpMapMask.Controls.Add(this.txtMaskPlayer);
            this.grpMapMask.Location = new System.Drawing.Point(645, 358);
            this.grpMapMask.Name = "grpMapMask";
            this.grpMapMask.Size = new System.Drawing.Size(348, 392);
            this.grpMapMask.TabIndex = 9;
            this.grpMapMask.TabStop = false;
            this.grpMapMask.Text = "MapMask";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(6, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(330, 330);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // txtMaskPlayer
            // 
            this.txtMaskPlayer.Location = new System.Drawing.Point(6, 19);
            this.txtMaskPlayer.Name = "txtMaskPlayer";
            this.txtMaskPlayer.Size = new System.Drawing.Size(330, 20);
            this.txtMaskPlayer.TabIndex = 0;
            // 
            // chkMapMask
            // 
            this.chkMapMask.AutoSize = true;
            this.chkMapMask.Checked = true;
            this.chkMapMask.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMapMask.Location = new System.Drawing.Point(871, 12);
            this.chkMapMask.Name = "chkMapMask";
            this.chkMapMask.Size = new System.Drawing.Size(52, 17);
            this.chkMapMask.TabIndex = 10;
            this.chkMapMask.Text = "Mask";
            this.chkMapMask.UseVisualStyleBackColor = true;
            this.chkMapMask.CheckedChanged += new System.EventHandler(this.chkMapMask_CheckedChanged);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(756, 35);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(231, 683);
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // chkMessages
            // 
            this.chkMessages.AutoSize = true;
            this.chkMessages.Checked = true;
            this.chkMessages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMessages.Location = new System.Drawing.Point(819, 12);
            this.chkMessages.Name = "chkMessages";
            this.chkMessages.Size = new System.Drawing.Size(46, 17);
            this.chkMessages.TabIndex = 12;
            this.chkMessages.Text = "Msg";
            this.chkMessages.UseVisualStyleBackColor = true;
            this.chkMessages.CheckedChanged += new System.EventHandler(this.chkMessages_CheckedChanged);
            // 
            // grpMessages
            // 
            this.grpMessages.Controls.Add(this.txtMessages);
            this.grpMessages.Location = new System.Drawing.Point(645, 44);
            this.grpMessages.Name = "grpMessages";
            this.grpMessages.Size = new System.Drawing.Size(348, 143);
            this.grpMessages.TabIndex = 13;
            this.grpMessages.TabStop = false;
            this.grpMessages.Text = "Messages";
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(6, 19);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessages.Size = new System.Drawing.Size(336, 118);
            this.txtMessages.TabIndex = 0;
            // 
            // chkTimer
            // 
            this.chkTimer.AutoSize = true;
            this.chkTimer.Checked = true;
            this.chkTimer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTimer.Location = new System.Drawing.Point(756, 12);
            this.chkTimer.Name = "chkTimer";
            this.chkTimer.Size = new System.Drawing.Size(52, 17);
            this.chkTimer.TabIndex = 14;
            this.chkTimer.Text = "Timer";
            this.chkTimer.UseVisualStyleBackColor = true;
            this.chkTimer.CheckedChanged += new System.EventHandler(this.chkTimer_CheckedChanged);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 728);
            this.ControlBox = false;
            this.Controls.Add(this.chkTimer);
            this.Controls.Add(this.grpMessages);
            this.Controls.Add(this.grpToolbox);
            this.Controls.Add(this.chkMessages);
            this.Controls.Add(this.chkMapMask);
            this.Controls.Add(this.grpMapMask);
            this.Controls.Add(this.chkToolbox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ServerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerForm_FormClosing);
            this.DoubleClick += new System.EventHandler(this.ServerForm_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpToolbox.ResumeLayout(false);
            this.grpMapMask.ResumeLayout(false);
            this.grpMapMask.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.grpMessages.ResumeLayout(false);
            this.grpMessages.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpToolbox;
        private System.Windows.Forms.CheckBox chkToolbox;
        private System.Windows.Forms.GroupBox grpMapMask;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtMaskPlayer;
        private System.Windows.Forms.CheckBox chkMapMask;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.CheckBox chkMessages;
        private System.Windows.Forms.GroupBox grpMessages;
        private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.CheckBox chkTimer;
    }
}

