namespace PlayOthello
{
    partial class frmMain
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
            this.pnlInteraction = new System.Windows.Forms.Panel();
            this.lblWhiteScore = new System.Windows.Forms.Label();
            this.lblBlackScore = new System.Windows.Forms.Label();
            this.picCurrent = new System.Windows.Forms.PictureBox();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblPosY = new System.Windows.Forms.Label();
            this.lblPosX = new System.Windows.Forms.Label();
            this.lblDetails = new System.Windows.Forms.Label();
            this.tbxPosY = new System.Windows.Forms.TextBox();
            this.tbxPosX = new System.Windows.Forms.TextBox();
            this.btnPlay = new System.Windows.Forms.Button();
            this.flpPlayItems = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlInteraction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrent)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlInteraction
            // 
            this.pnlInteraction.Controls.Add(this.lblWhiteScore);
            this.pnlInteraction.Controls.Add(this.lblBlackScore);
            this.pnlInteraction.Controls.Add(this.picCurrent);
            this.pnlInteraction.Controls.Add(this.lblDisplay);
            this.pnlInteraction.Controls.Add(this.lblPosY);
            this.pnlInteraction.Controls.Add(this.lblPosX);
            this.pnlInteraction.Controls.Add(this.lblDetails);
            this.pnlInteraction.Controls.Add(this.tbxPosY);
            this.pnlInteraction.Controls.Add(this.tbxPosX);
            this.pnlInteraction.Controls.Add(this.btnPlay);
            this.pnlInteraction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInteraction.Location = new System.Drawing.Point(0, 465);
            this.pnlInteraction.Name = "pnlInteraction";
            this.pnlInteraction.Size = new System.Drawing.Size(452, 85);
            this.pnlInteraction.TabIndex = 1;
            // 
            // lblWhiteScore
            // 
            this.lblWhiteScore.AutoSize = true;
            this.lblWhiteScore.Location = new System.Drawing.Point(365, 56);
            this.lblWhiteScore.Name = "lblWhiteScore";
            this.lblWhiteScore.Size = new System.Drawing.Size(0, 13);
            this.lblWhiteScore.TabIndex = 10;
            // 
            // lblBlackScore
            // 
            this.lblBlackScore.AutoSize = true;
            this.lblBlackScore.Location = new System.Drawing.Point(262, 56);
            this.lblBlackScore.Name = "lblBlackScore";
            this.lblBlackScore.Size = new System.Drawing.Size(0, 13);
            this.lblBlackScore.TabIndex = 9;
            // 
            // picCurrent
            // 
            this.picCurrent.Location = new System.Drawing.Point(160, 32);
            this.picCurrent.Name = "picCurrent";
            this.picCurrent.Size = new System.Drawing.Size(45, 45);
            this.picCurrent.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCurrent.TabIndex = 8;
            this.picCurrent.TabStop = false;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.Location = new System.Drawing.Point(261, 12);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(133, 24);
            this.lblDisplay.TabIndex = 7;
            this.lblDisplay.Text = "Hit Start to Play";
            // 
            // lblPosY
            // 
            this.lblPosY.AutoSize = true;
            this.lblPosY.Location = new System.Drawing.Point(21, 63);
            this.lblPosY.Name = "lblPosY";
            this.lblPosY.Size = new System.Drawing.Size(15, 13);
            this.lblPosY.TabIndex = 6;
            this.lblPosY.Text = "y:";
            // 
            // lblPosX
            // 
            this.lblPosX.AutoSize = true;
            this.lblPosX.Location = new System.Drawing.Point(21, 35);
            this.lblPosX.Name = "lblPosX";
            this.lblPosX.Size = new System.Drawing.Size(15, 13);
            this.lblPosX.TabIndex = 5;
            this.lblPosX.Text = "x:";
            // 
            // lblDetails
            // 
            this.lblDetails.AutoSize = true;
            this.lblDetails.Location = new System.Drawing.Point(21, 12);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(74, 13);
            this.lblDetails.TabIndex = 4;
            this.lblDetails.Text = "Play Location:";
            // 
            // tbxPosY
            // 
            this.tbxPosY.Location = new System.Drawing.Point(42, 60);
            this.tbxPosY.Name = "tbxPosY";
            this.tbxPosY.Size = new System.Drawing.Size(35, 20);
            this.tbxPosY.TabIndex = 3;
            // 
            // tbxPosX
            // 
            this.tbxPosX.Location = new System.Drawing.Point(42, 32);
            this.tbxPosX.Name = "tbxPosX";
            this.tbxPosX.Size = new System.Drawing.Size(35, 20);
            this.tbxPosX.TabIndex = 2;
            this.tbxPosX.TextChanged += new System.EventHandler(this.tbxPosX_TextChanged);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(137, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(103, 23);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Start";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // flpPlayItems
            // 
            this.flpPlayItems.BackColor = System.Drawing.SystemColors.GrayText;
            this.flpPlayItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpPlayItems.Location = new System.Drawing.Point(0, 0);
            this.flpPlayItems.Name = "flpPlayItems";
            this.flpPlayItems.Padding = new System.Windows.Forms.Padding(2);
            this.flpPlayItems.Size = new System.Drawing.Size(452, 459);
            this.flpPlayItems.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 550);
            this.Controls.Add(this.flpPlayItems);
            this.Controls.Add(this.pnlInteraction);
            this.Name = "frmMain";
            this.Text = "Jenson\'s Othello";
            this.pnlInteraction.ResumeLayout(false);
            this.pnlInteraction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlInteraction;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Label lblPosY;
        private System.Windows.Forms.Label lblPosX;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.TextBox tbxPosY;
        private System.Windows.Forms.TextBox tbxPosX;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.FlowLayoutPanel flpPlayItems;
        private System.Windows.Forms.PictureBox picCurrent;
        private System.Windows.Forms.Label lblWhiteScore;
        private System.Windows.Forms.Label lblBlackScore;

    }
}

