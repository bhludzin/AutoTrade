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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.lblLowAskHTTP = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.lblHighBidHTTP = new System.Windows.Forms.Label();
			this.lblDepthCountHTTP = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.lblLastOrderDepthHTTP = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.btnManualRequestOrderHTTP = new System.Windows.Forms.Button();
			this.lblLastResultOrderHTTP = new System.Windows.Forms.Label();
			this.label24 = new System.Windows.Forms.Label();
			this.lblLastAskHTTP = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.label27 = new System.Windows.Forms.Label();
			this.lblLastBidHTTP = new System.Windows.Forms.Label();
			this.lblOrderCountHTTP = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.lblLastResultOrder = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblAsk = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblBid = new System.Windows.Forms.Label();
			this.lblBidAskCount = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnManualRequestPriceHTTP = new System.Windows.Forms.Button();
			this.lblLastResultPriceHTTP = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.lblLastPriceHTTP = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.lblPriceCountHTTP = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lblLastResult = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lblPrice = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblPriceCount = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblCurrentTime = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lblElapsedTime = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.lblPollingStarted = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.lblPollingStatus = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.timerCurrent = new System.Windows.Forms.Timer(this.components);
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.tabPage1.Controls.Add(this.groupBox5);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Controls.Add(this.lblCurrentTime);
			this.tabPage1.Controls.Add(this.label11);
			this.tabPage1.Controls.Add(this.lblElapsedTime);
			this.tabPage1.Controls.Add(this.label12);
			this.tabPage1.Controls.Add(this.lblPollingStarted);
			this.tabPage1.Controls.Add(this.label10);
			this.tabPage1.Controls.Add(this.lblPollingStatus);
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
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.lblLowAskHTTP);
			this.groupBox5.Controls.Add(this.label19);
			this.groupBox5.Controls.Add(this.label20);
			this.groupBox5.Controls.Add(this.lblHighBidHTTP);
			this.groupBox5.Controls.Add(this.lblDepthCountHTTP);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Controls.Add(this.lblLastOrderDepthHTTP);
			this.groupBox5.Controls.Add(this.label15);
			this.groupBox5.Location = new System.Drawing.Point(342, 385);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(261, 126);
			this.groupBox5.TabIndex = 37;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Full Market Depth from HTTP API";
			// 
			// lblLowAskHTTP
			// 
			this.lblLowAskHTTP.Location = new System.Drawing.Point(77, 70);
			this.lblLowAskHTTP.Name = "lblLowAskHTTP";
			this.lblLowAskHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblLowAskHTTP.TabIndex = 38;
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(29, 70);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(51, 13);
			this.label19.TabIndex = 37;
			this.label19.Text = "Low Ask:";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(31, 48);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(50, 13);
			this.label20.TabIndex = 36;
			this.label20.Text = "High Bid:";
			// 
			// lblHighBidHTTP
			// 
			this.lblHighBidHTTP.Location = new System.Drawing.Point(76, 48);
			this.lblHighBidHTTP.Name = "lblHighBidHTTP";
			this.lblHighBidHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblHighBidHTTP.TabIndex = 35;
			// 
			// lblDepthCountHTTP
			// 
			this.lblDepthCountHTTP.Location = new System.Drawing.Point(81, 27);
			this.lblDepthCountHTTP.Name = "lblDepthCountHTTP";
			this.lblDepthCountHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblDepthCountHTTP.TabIndex = 34;
			this.lblDepthCountHTTP.Text = "0";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(12, 27);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(70, 13);
			this.label17.TabIndex = 33;
			this.label17.Text = "Depth Count:";
			// 
			// lblLastOrderDepthHTTP
			// 
			this.lblLastOrderDepthHTTP.Location = new System.Drawing.Point(80, 90);
			this.lblLastOrderDepthHTTP.Name = "lblLastOrderDepthHTTP";
			this.lblLastOrderDepthHTTP.Size = new System.Drawing.Size(167, 18);
			this.lblLastOrderDepthHTTP.TabIndex = 32;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(15, 91);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(65, 13);
			this.label15.TabIndex = 31;
			this.label15.Text = "Last Result:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.btnManualRequestOrderHTTP);
			this.groupBox3.Controls.Add(this.lblLastResultOrderHTTP);
			this.groupBox3.Controls.Add(this.label24);
			this.groupBox3.Controls.Add(this.lblLastAskHTTP);
			this.groupBox3.Controls.Add(this.label26);
			this.groupBox3.Controls.Add(this.label27);
			this.groupBox3.Controls.Add(this.lblLastBidHTTP);
			this.groupBox3.Controls.Add(this.lblOrderCountHTTP);
			this.groupBox3.Controls.Add(this.label30);
			this.groupBox3.Location = new System.Drawing.Point(342, 247);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(405, 122);
			this.groupBox3.TabIndex = 36;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Orders (Bid/Ask) from HTTP API";
			// 
			// btnManualRequestOrderHTTP
			// 
			this.btnManualRequestOrderHTTP.Location = new System.Drawing.Point(280, 48);
			this.btnManualRequestOrderHTTP.Name = "btnManualRequestOrderHTTP";
			this.btnManualRequestOrderHTTP.Size = new System.Drawing.Size(98, 23);
			this.btnManualRequestOrderHTTP.TabIndex = 39;
			this.btnManualRequestOrderHTTP.Text = "Manual Request";
			this.btnManualRequestOrderHTTP.UseVisualStyleBackColor = true;
			this.btnManualRequestOrderHTTP.Click += new System.EventHandler(this.btnManualRequestOrderHTTP_Click);
			// 
			// lblLastResultOrderHTTP
			// 
			this.lblLastResultOrderHTTP.Location = new System.Drawing.Point(87, 91);
			this.lblLastResultOrderHTTP.Name = "lblLastResultOrderHTTP";
			this.lblLastResultOrderHTTP.Size = new System.Drawing.Size(167, 18);
			this.lblLastResultOrderHTTP.TabIndex = 38;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(22, 92);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(65, 13);
			this.label24.TabIndex = 37;
			this.label24.Text = "Last Result:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblLastAskHTTP
			// 
			this.lblLastAskHTTP.Location = new System.Drawing.Point(85, 70);
			this.lblLastAskHTTP.Name = "lblLastAskHTTP";
			this.lblLastAskHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblLastAskHTTP.TabIndex = 36;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.Location = new System.Drawing.Point(37, 70);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(51, 13);
			this.label26.TabIndex = 35;
			this.label26.Text = "Last Ask:";
			// 
			// label27
			// 
			this.label27.AutoSize = true;
			this.label27.Location = new System.Drawing.Point(39, 48);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(48, 13);
			this.label27.TabIndex = 34;
			this.label27.Text = "Last Bid:";
			// 
			// lblLastBidHTTP
			// 
			this.lblLastBidHTTP.Location = new System.Drawing.Point(84, 48);
			this.lblLastBidHTTP.Name = "lblLastBidHTTP";
			this.lblLastBidHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblLastBidHTTP.TabIndex = 33;
			// 
			// lblOrderCountHTTP
			// 
			this.lblOrderCountHTTP.Location = new System.Drawing.Point(90, 27);
			this.lblOrderCountHTTP.Name = "lblOrderCountHTTP";
			this.lblOrderCountHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblOrderCountHTTP.TabIndex = 32;
			this.lblOrderCountHTTP.Text = "0";
			// 
			// label30
			// 
			this.label30.AutoSize = true;
			this.label30.Location = new System.Drawing.Point(6, 27);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(85, 13);
			this.label30.TabIndex = 31;
			this.label30.Text = "Bid / Ask Count:";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.lblLastResultOrder);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.lblAsk);
			this.groupBox4.Controls.Add(this.label4);
			this.groupBox4.Controls.Add(this.label3);
			this.groupBox4.Controls.Add(this.lblBid);
			this.groupBox4.Controls.Add(this.lblBidAskCount);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Location = new System.Drawing.Point(25, 247);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(273, 122);
			this.groupBox4.TabIndex = 35;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Orders (Bid/Ask) from socket";
			// 
			// lblLastResultOrder
			// 
			this.lblLastResultOrder.Location = new System.Drawing.Point(92, 89);
			this.lblLastResultOrder.Name = "lblLastResultOrder";
			this.lblLastResultOrder.Size = new System.Drawing.Size(167, 18);
			this.lblLastResultOrder.TabIndex = 30;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(27, 90);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(65, 13);
			this.label13.TabIndex = 29;
			this.label13.Text = "Last Result:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblAsk
			// 
			this.lblAsk.Location = new System.Drawing.Point(90, 68);
			this.lblAsk.Name = "lblAsk";
			this.lblAsk.Size = new System.Drawing.Size(73, 18);
			this.lblAsk.TabIndex = 23;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(42, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(51, 13);
			this.label4.TabIndex = 22;
			this.label4.Text = "Last Ask:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(44, 46);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 21;
			this.label3.Text = "Last Bid:";
			// 
			// lblBid
			// 
			this.lblBid.Location = new System.Drawing.Point(89, 46);
			this.lblBid.Name = "lblBid";
			this.lblBid.Size = new System.Drawing.Size(73, 18);
			this.lblBid.TabIndex = 20;
			// 
			// lblBidAskCount
			// 
			this.lblBidAskCount.Location = new System.Drawing.Point(95, 25);
			this.lblBidAskCount.Name = "lblBidAskCount";
			this.lblBidAskCount.Size = new System.Drawing.Size(73, 18);
			this.lblBidAskCount.TabIndex = 15;
			this.lblBidAskCount.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(11, 25);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(85, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Bid / Ask Count:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnManualRequestPriceHTTP);
			this.groupBox2.Controls.Add(this.lblLastResultPriceHTTP);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.lblLastPriceHTTP);
			this.groupBox2.Controls.Add(this.label18);
			this.groupBox2.Controls.Add(this.lblPriceCountHTTP);
			this.groupBox2.Controls.Add(this.label22);
			this.groupBox2.Location = new System.Drawing.Point(342, 123);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(405, 100);
			this.groupBox2.TabIndex = 34;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Prices from HTTP API";
			// 
			// btnManualRequestPriceHTTP
			// 
			this.btnManualRequestPriceHTTP.Location = new System.Drawing.Point(280, 40);
			this.btnManualRequestPriceHTTP.Name = "btnManualRequestPriceHTTP";
			this.btnManualRequestPriceHTTP.Size = new System.Drawing.Size(98, 23);
			this.btnManualRequestPriceHTTP.TabIndex = 35;
			this.btnManualRequestPriceHTTP.Text = "Manual Request";
			this.btnManualRequestPriceHTTP.UseVisualStyleBackColor = true;
			this.btnManualRequestPriceHTTP.Click += new System.EventHandler(this.btnManualRequestPriceHTTP_Click);
			// 
			// lblLastResultPriceHTTP
			// 
			this.lblLastResultPriceHTTP.Location = new System.Drawing.Point(79, 67);
			this.lblLastResultPriceHTTP.Name = "lblLastResultPriceHTTP";
			this.lblLastResultPriceHTTP.Size = new System.Drawing.Size(167, 18);
			this.lblLastResultPriceHTTP.TabIndex = 34;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(14, 68);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(65, 13);
			this.label14.TabIndex = 33;
			this.label14.Text = "Last Result:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblLastPriceHTTP
			// 
			this.lblLastPriceHTTP.Location = new System.Drawing.Point(79, 45);
			this.lblLastPriceHTTP.Name = "lblLastPriceHTTP";
			this.lblLastPriceHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblLastPriceHTTP.TabIndex = 32;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(22, 45);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(57, 13);
			this.label18.TabIndex = 31;
			this.label18.Text = "Last Price:";
			// 
			// lblPriceCountHTTP
			// 
			this.lblPriceCountHTTP.Location = new System.Drawing.Point(82, 25);
			this.lblPriceCountHTTP.Name = "lblPriceCountHTTP";
			this.lblPriceCountHTTP.Size = new System.Drawing.Size(73, 18);
			this.lblPriceCountHTTP.TabIndex = 30;
			this.lblPriceCountHTTP.Text = "0";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(16, 25);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(65, 13);
			this.label22.TabIndex = 29;
			this.label22.Text = "Price Count:";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblLastResult);
			this.groupBox1.Controls.Add(this.label8);
			this.groupBox1.Controls.Add(this.lblPrice);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.lblPriceCount);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Location = new System.Drawing.Point(25, 123);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(273, 100);
			this.groupBox1.TabIndex = 33;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Prices from socket";
			// 
			// lblLastResult
			// 
			this.lblLastResult.Location = new System.Drawing.Point(82, 67);
			this.lblLastResult.Name = "lblLastResult";
			this.lblLastResult.Size = new System.Drawing.Size(167, 18);
			this.lblLastResult.TabIndex = 28;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(17, 68);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 13);
			this.label8.TabIndex = 27;
			this.label8.Text = "Last Result:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPrice
			// 
			this.lblPrice.Location = new System.Drawing.Point(82, 45);
			this.lblPrice.Name = "lblPrice";
			this.lblPrice.Size = new System.Drawing.Size(73, 18);
			this.lblPrice.TabIndex = 26;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(25, 45);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(57, 13);
			this.label9.TabIndex = 25;
			this.label9.Text = "Last Price:";
			// 
			// lblPriceCount
			// 
			this.lblPriceCount.Location = new System.Drawing.Point(85, 25);
			this.lblPriceCount.Name = "lblPriceCount";
			this.lblPriceCount.Size = new System.Drawing.Size(73, 18);
			this.lblPriceCount.TabIndex = 17;
			this.lblPriceCount.Text = "0";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 25);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 13);
			this.label7.TabIndex = 16;
			this.label7.Text = "Price Count:";
			// 
			// lblCurrentTime
			// 
			this.lblCurrentTime.Location = new System.Drawing.Point(478, 66);
			this.lblCurrentTime.Name = "lblCurrentTime";
			this.lblCurrentTime.Size = new System.Drawing.Size(167, 18);
			this.lblCurrentTime.TabIndex = 32;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(401, 66);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(77, 13);
			this.label11.TabIndex = 31;
			this.label11.Text = "Current Time:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblElapsedTime
			// 
			this.lblElapsedTime.Location = new System.Drawing.Point(478, 41);
			this.lblElapsedTime.Name = "lblElapsedTime";
			this.lblElapsedTime.Size = new System.Drawing.Size(167, 18);
			this.lblElapsedTime.TabIndex = 30;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(401, 41);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(77, 13);
			this.label12.TabIndex = 29;
			this.label12.Text = "Elapsed Time:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPollingStarted
			// 
			this.lblPollingStarted.Location = new System.Drawing.Point(478, 17);
			this.lblPollingStarted.Name = "lblPollingStarted";
			this.lblPollingStarted.Size = new System.Drawing.Size(167, 18);
			this.lblPollingStarted.TabIndex = 28;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(397, 17);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 13);
			this.label10.TabIndex = 27;
			this.label10.Text = "Polling Started:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblPollingStatus
			// 
			this.lblPollingStatus.BackColor = System.Drawing.Color.DarkRed;
			this.lblPollingStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPollingStatus.ForeColor = System.Drawing.SystemColors.Window;
			this.lblPollingStatus.Location = new System.Drawing.Point(184, 17);
			this.lblPollingStatus.Name = "lblPollingStatus";
			this.lblPollingStatus.Size = new System.Drawing.Size(166, 88);
			this.lblPollingStatus.TabIndex = 16;
			this.lblPollingStatus.Text = "Polling Off";
			this.lblPollingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerDepth;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblPollingStatus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Timer timerCurrent;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblLowAskHTTP;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblHighBidHTTP;
        private System.Windows.Forms.Label lblDepthCountHTTP;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblLastOrderDepthHTTP;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnManualRequestOrderHTTP;
        private System.Windows.Forms.Label lblLastResultOrderHTTP;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblLastAskHTTP;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblLastBidHTTP;
        private System.Windows.Forms.Label lblOrderCountHTTP;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblLastResultOrder;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblAsk;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblBid;
        private System.Windows.Forms.Label lblBidAskCount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnManualRequestPriceHTTP;
        private System.Windows.Forms.Label lblLastResultPriceHTTP;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblLastPriceHTTP;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblPriceCountHTTP;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLastResult;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPriceCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblPollingStarted;
		private System.Windows.Forms.Label label10;
    }
}

