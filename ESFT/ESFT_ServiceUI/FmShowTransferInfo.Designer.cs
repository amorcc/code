namespace ESFT_ServiceUI
{
    partial class FmShowTransferInfo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pMiddle = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serverPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recevieOrSendLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferTimeSecond = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastPacketTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lbWriteThreadNum = new System.Windows.Forms.Label();
            this.lbUsedNum = new System.Windows.Forms.Label();
            this.lbAvailableSocketNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.key2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 512);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 40);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1023, 472);
            this.panel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pMiddle);
            this.panel5.Controls.Add(this.dataGridView1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1023, 216);
            this.panel5.TabIndex = 1;
            // 
            // pMiddle
            // 
            this.pMiddle.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pMiddle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pMiddle.Location = new System.Drawing.Point(0, 211);
            this.pMiddle.Name = "pMiddle";
            this.pMiddle.Size = new System.Drawing.Size(1023, 5);
            this.pMiddle.TabIndex = 4;
            this.pMiddle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pMiddle_MouseMove);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key,
            this.transferType,
            this.serverPath,
            this.fileLenght,
            this.currentLenght,
            this.recevieOrSendLenght,
            this.stateStr,
            this.transferTimeSecond,
            this.speed,
            this.lastPacketTime,
            this.beginTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1023, 216);
            this.dataGridView1.TabIndex = 3;
            // 
            // key
            // 
            this.key.HeaderText = "编号";
            this.key.Name = "key";
            // 
            // transferType
            // 
            this.transferType.HeaderText = "传输类型";
            this.transferType.Name = "transferType";
            // 
            // serverPath
            // 
            this.serverPath.HeaderText = "服务器路径";
            this.serverPath.Name = "serverPath";
            // 
            // fileLenght
            // 
            this.fileLenght.HeaderText = "文件大小";
            this.fileLenght.Name = "fileLenght";
            // 
            // currentLenght
            // 
            this.currentLenght.HeaderText = "已完成";
            this.currentLenght.Name = "currentLenght";
            // 
            // recevieOrSendLenght
            // 
            this.recevieOrSendLenght.HeaderText = "接受长度";
            this.recevieOrSendLenght.Name = "recevieOrSendLenght";
            // 
            // stateStr
            // 
            this.stateStr.HeaderText = "传输状态";
            this.stateStr.Name = "stateStr";
            // 
            // transferTimeSecond
            // 
            this.transferTimeSecond.HeaderText = "传输时间";
            this.transferTimeSecond.Name = "transferTimeSecond";
            // 
            // speed
            // 
            this.speed.HeaderText = "速度";
            this.speed.Name = "speed";
            // 
            // lastPacketTime
            // 
            this.lastPacketTime.HeaderText = "最后包时间";
            this.lastPacketTime.Name = "lastPacketTime";
            // 
            // beginTime
            // 
            this.beginTime.HeaderText = "开始时间";
            this.beginTime.Name = "beginTime";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tabControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 216);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1023, 256);
            this.panel4.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1023, 256);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1015, 230);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置和统计";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.button5);
            this.panel6.Controls.Add(this.button4);
            this.panel6.Controls.Add(this.button3);
            this.panel6.Controls.Add(this.txtServerPort);
            this.panel6.Controls.Add(this.txtServerIP);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1009, 224);
            this.panel6.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(295, 84);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(116, 23);
            this.button5.TabIndex = 6;
            this.button5.Text = "立即子服务器信息";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(161, 84);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(116, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "立即获取任务信息";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(19, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "立即获取连接数信息";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(88, 39);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(169, 21);
            this.txtServerPort.TabIndex = 3;
            this.txtServerPort.Text = "8000";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(88, 12);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(169, 21);
            this.txtServerIP.TabIndex = 2;
            this.txtServerIP.Text = "192.168.0.66";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "端口号：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "服务器IP：";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtOutput);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1015, 230);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "连接信息";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(1009, 224);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.Text = "";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1015, 230);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "子服务器信息";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key2,
            this.ServerName,
            this.IP1,
            this.IP2,
            this.IP3});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(1015, 230);
            this.dataGridView2.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.checkBox2);
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.lbWriteThreadNum);
            this.panel2.Controls.Add(this.lbUsedNum);
            this.panel2.Controls.Add(this.lbAvailableSocketNum);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1023, 40);
            this.panel2.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(895, 11);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "立即获取任务信息";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(545, 15);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(120, 16);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "自动获取任务信息";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(390, 15);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(132, 16);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "自动获取可用连接数";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(763, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "立即获取连接数信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbWriteThreadNum
            // 
            this.lbWriteThreadNum.AutoSize = true;
            this.lbWriteThreadNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWriteThreadNum.ForeColor = System.Drawing.Color.Red;
            this.lbWriteThreadNum.Location = new System.Drawing.Point(337, 17);
            this.lbWriteThreadNum.Name = "lbWriteThreadNum";
            this.lbWriteThreadNum.Size = new System.Drawing.Size(19, 12);
            this.lbWriteThreadNum.TabIndex = 4;
            this.lbWriteThreadNum.Text = "-1";
            // 
            // lbUsedNum
            // 
            this.lbUsedNum.AutoSize = true;
            this.lbUsedNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUsedNum.ForeColor = System.Drawing.Color.Red;
            this.lbUsedNum.Location = new System.Drawing.Point(182, 17);
            this.lbUsedNum.Name = "lbUsedNum";
            this.lbUsedNum.Size = new System.Drawing.Size(19, 12);
            this.lbUsedNum.TabIndex = 3;
            this.lbUsedNum.Text = "-1";
            // 
            // lbAvailableSocketNum
            // 
            this.lbAvailableSocketNum.AutoSize = true;
            this.lbAvailableSocketNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAvailableSocketNum.ForeColor = System.Drawing.Color.Red;
            this.lbAvailableSocketNum.Location = new System.Drawing.Point(66, 17);
            this.lbAvailableSocketNum.Name = "lbAvailableSocketNum";
            this.lbAvailableSocketNum.Size = new System.Drawing.Size(19, 12);
            this.lbAvailableSocketNum.TabIndex = 2;
            this.lbAvailableSocketNum.Text = "-1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "写进程数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "已用数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "可用数：";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 1200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // key2
            // 
            this.key2.HeaderText = "编号";
            this.key2.Name = "key2";
            // 
            // ServerName
            // 
            this.ServerName.HeaderText = "服务器名称";
            this.ServerName.Name = "ServerName";
            // 
            // IP1
            // 
            this.IP1.HeaderText = "IP1";
            this.IP1.Name = "IP1";
            // 
            // IP2
            // 
            this.IP2.HeaderText = "IP2";
            this.IP2.Name = "IP2";
            // 
            // IP3
            // 
            this.IP3.HeaderText = "IP3";
            this.IP3.Name = "IP3";
            // 
            // FmShowTransferInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 512);
            this.Controls.Add(this.panel1);
            this.Name = "FmShowTransferInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "在成长传输信息显示";
            this.Load += new System.EventHandler(this.FmShowTransferInfo_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbWriteThreadNum;
        private System.Windows.Forms.Label lbUsedNum;
        private System.Windows.Forms.Label lbAvailableSocketNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn key;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferType;
        private System.Windows.Forms.DataGridViewTextBoxColumn serverPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn recevieOrSendLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferTimeSecond;
        private System.Windows.Forms.DataGridViewTextBoxColumn speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastPacketTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn beginTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel pMiddle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridViewTextBoxColumn key2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP3;
    }
}

