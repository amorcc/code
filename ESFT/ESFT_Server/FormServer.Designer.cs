namespace ESFT.Server
{
    partial class FormServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormServer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbNumConnections = new System.Windows.Forms.Label();
            this.lbWriteThreadNum = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTransfed = new System.Windows.Forms.Label();
            this.lbTransfering = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientFileFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RecevieOrSendLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentCompleteLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BeginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastPacketTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(916, 551);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 87);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(916, 407);
            this.panel3.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.key,
            this.TransferState,
            this.IP,
            this.ClientFileFullName,
            this.ServerPath,
            this.FileLenght,
            this.RecevieOrSendLenght,
            this.CurrentCompleteLenght,
            this.TSpeed,
            this.TransferTime,
            this.BeginTime,
            this.LastPacketTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(916, 407);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.lbNumConnections);
            this.panel4.Controls.Add(this.lbWriteThreadNum);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lbTransfed);
            this.panel4.Controls.Add(this.lbTransfering);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 494);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(916, 57);
            this.panel4.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(5, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(493, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(398, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 21;
            this.label7.Text = "异步连接使用：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(612, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "0";
            // 
            // lbNumConnections
            // 
            this.lbNumConnections.AutoSize = true;
            this.lbNumConnections.ForeColor = System.Drawing.Color.Red;
            this.lbNumConnections.Location = new System.Drawing.Point(52, 11);
            this.lbNumConnections.Name = "lbNumConnections";
            this.lbNumConnections.Size = new System.Drawing.Size(23, 12);
            this.lbNumConnections.TabIndex = 19;
            this.lbNumConnections.Text = "100";
            // 
            // lbWriteThreadNum
            // 
            this.lbWriteThreadNum.AutoSize = true;
            this.lbWriteThreadNum.ForeColor = System.Drawing.Color.Red;
            this.lbWriteThreadNum.Location = new System.Drawing.Point(366, 11);
            this.lbWriteThreadNum.Name = "lbWriteThreadNum";
            this.lbWriteThreadNum.Size = new System.Drawing.Size(11, 12);
            this.lbWriteThreadNum.TabIndex = 18;
            this.lbWriteThreadNum.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "并发数：";
            // 
            // lbTransfed
            // 
            this.lbTransfed.AutoSize = true;
            this.lbTransfed.ForeColor = System.Drawing.Color.Red;
            this.lbTransfed.Location = new System.Drawing.Point(276, 11);
            this.lbTransfed.Name = "lbTransfed";
            this.lbTransfed.Size = new System.Drawing.Size(11, 12);
            this.lbTransfed.TabIndex = 17;
            this.lbTransfed.Text = "0";
            // 
            // lbTransfering
            // 
            this.lbTransfering.AutoSize = true;
            this.lbTransfering.ForeColor = System.Drawing.Color.Red;
            this.lbTransfering.Location = new System.Drawing.Point(165, 11);
            this.lbTransfering.Name = "lbTransfering";
            this.lbTransfering.Size = new System.Drawing.Size(11, 12);
            this.lbTransfering.TabIndex = 16;
            this.lbTransfering.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "完成传输数：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "写进程：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "正在传输数：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(916, 87);
            this.panel2.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "清除已完成";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(204, 46);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(83, 16);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "断开未完成";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(105, 46);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "已经完成";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 46);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "正在进行";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始监听";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // key
            // 
            this.key.HeaderText = "传输号";
            this.key.Name = "key";
            // 
            // TransferState
            // 
            this.TransferState.HeaderText = "状态";
            this.TransferState.Name = "TransferState";
            this.TransferState.Width = 80;
            // 
            // IP
            // 
            this.IP.HeaderText = "IP";
            this.IP.Name = "IP";
            this.IP.Width = 120;
            // 
            // ClientFileFullName
            // 
            this.ClientFileFullName.HeaderText = "客户端文件地址";
            this.ClientFileFullName.Name = "ClientFileFullName";
            this.ClientFileFullName.Width = 120;
            // 
            // ServerPath
            // 
            this.ServerPath.HeaderText = "本地文件地址";
            this.ServerPath.Name = "ServerPath";
            this.ServerPath.Width = 120;
            // 
            // FileLenght
            // 
            this.FileLenght.HeaderText = "文件大小";
            this.FileLenght.Name = "FileLenght";
            // 
            // RecevieOrSendLenght
            // 
            this.RecevieOrSendLenght.HeaderText = "已接收或发送大小";
            this.RecevieOrSendLenght.Name = "RecevieOrSendLenght";
            this.RecevieOrSendLenght.Width = 150;
            // 
            // CurrentCompleteLenght
            // 
            this.CurrentCompleteLenght.HeaderText = "已经完成";
            this.CurrentCompleteLenght.Name = "CurrentCompleteLenght";
            // 
            // TSpeed
            // 
            this.TSpeed.HeaderText = "速度";
            this.TSpeed.Name = "TSpeed";
            // 
            // TransferTime
            // 
            this.TransferTime.HeaderText = "用时";
            this.TransferTime.Name = "TransferTime";
            // 
            // BeginTime
            // 
            this.BeginTime.HeaderText = "开始时间";
            this.BeginTime.Name = "BeginTime";
            // 
            // LastPacketTime
            // 
            this.LastPacketTime.HeaderText = "最后包时间";
            this.LastPacketTime.Name = "LastPacketTime";
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 551);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormServer";
            this.Text = "在成长文件接收端(端口8000)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormServer_FormClosing);
            this.Load += new System.EventHandler(this.FormServer_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbNumConnections;
        private System.Windows.Forms.Label lbWriteThreadNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTransfed;
        private System.Windows.Forms.Label lbTransfering;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn key;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferState;
        private System.Windows.Forms.DataGridViewTextBoxColumn IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClientFileFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn RecevieOrSendLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentCompleteLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn BeginTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastPacketTime;
    }
}

