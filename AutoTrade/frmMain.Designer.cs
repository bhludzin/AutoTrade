namespace AutoTrade
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
            this.components = new System.ComponentModel.Container();
            this.timerDepth = new System.Windows.Forms.Timer(this.components);
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblBidAskCount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPriceCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblPollingStarted = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblLastResult = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblPollingStatus = new System.Windows.Forms.Label();
            this.lblAsk = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblBid = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.timerCurrent = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerDepth
            // 
            this.timerDepth.Interval = 60000;
            this.timerDepth.Tick += new System.EventHandler(this.timerDepth_Tick);
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(23, 64);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(142, 41);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop Polling";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("Meiryo", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(23, 17);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(142, 41);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "Start Polling";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblBidAskCount
            // 
            this.lblBidAskCount.Location = new System.Drawing.Point(461, 21);
            this.lblBidAskCount.Name = "lblBidAskCount";
            this.lblBidAskCount.Size = new System.Drawing.Size(73, 18);
            this.lblBidAskCount.TabIndex = 13;
            this.lblBidAskCount.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(377, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Bid / Ask Count:";
            // 
            // lblPriceCount
            // 
            this.lblPriceCount.Location = new System.Drawing.Point(461, 42);
            this.lblPriceCount.Name = "lblPriceCount";
            this.lblPriceCount.Size = new System.Drawing.Size(73, 18);
            this.lblPriceCount.TabIndex = 15;
            this.lblPriceCount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(395, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Price Count:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1335, 662);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCurrentTime);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.lblPrice);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.lblElapsedTime);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.lblPollingStarted);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.lblLastResult);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.lblPollingStatus);
            this.tabPage1.Controls.Add(this.lblAsk);
            this.tabPage1.Controls.Add(this.lblPriceCount);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.lblBidAskCount);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.lblBid);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1327, 636);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Market Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Location = new System.Drawing.Point(611, 90);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(167, 18);
            this.lblCurrentTime.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(534, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Current Time:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(428, 114);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(73, 18);
            this.lblPrice.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(375, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Last Price:";
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.Location = new System.Drawing.Point(611, 65);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(167, 18);
            this.lblElapsedTime.TabIndex = 22;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(534, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "Elapsed Time:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPollingStarted
            // 
            this.lblPollingStarted.Location = new System.Drawing.Point(611, 21);
            this.lblPollingStarted.Name = "lblPollingStarted";
            this.lblPollingStarted.Size = new System.Drawing.Size(167, 18);
            this.lblPollingStarted.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(530, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Polling Started:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblLastResult
            // 
            this.lblLastResult.Location = new System.Drawing.Point(611, 42);
            this.lblLastResult.Name = "lblLastResult";
            this.lblLastResult.Size = new System.Drawing.Size(167, 18);
            this.lblLastResult.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(546, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Last Result:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPollingStatus
            // 
            this.lblPollingStatus.BackColor = System.Drawing.Color.Red;
            this.lblPollingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollingStatus.ForeColor = System.Drawing.SystemColors.Window;
            this.lblPollingStatus.Location = new System.Drawing.Point(184, 17);
            this.lblPollingStatus.Name = "lblPollingStatus";
            this.lblPollingStatus.Size = new System.Drawing.Size(166, 88);
            this.lblPollingStatus.TabIndex = 16;
            this.lblPollingStatus.Text = "Not Polling";
            this.lblPollingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAsk
            // 
            this.lblAsk.Location = new System.Drawing.Point(430, 90);
            this.lblAsk.Name = "lblAsk";
            this.lblAsk.Size = new System.Drawing.Size(73, 18);
            this.lblAsk.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Last Ask:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Last Bid:";
            // 
            // lblBid
            // 
            this.lblBid.Location = new System.Drawing.Point(429, 68);
            this.lblBid.Name = "lblBid";
            this.lblBid.Size = new System.Drawing.Size(73, 18);
            this.lblBid.TabIndex = 12;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1327, 636);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Chart";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1327, 636);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Test";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // timerCurrent
            // 
            this.timerCurrent.Interval = 1000;
            this.timerCurrent.Tick += new System.EventHandler(this.timerCurrent_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 686);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoTrade";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerDepth;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblBidAskCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPriceCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblPollingStatus;
        private System.Windows.Forms.Label lblAsk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBid;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPollingStarted;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblLastResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Timer timerCurrent;
    }
}

