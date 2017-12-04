namespace ESFT_ServiceUI
{
    partial class FmMain
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("服务器");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
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
            this.panel8 = new System.Windows.Forms.Panel();
            this.lbUnfinishCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbFinishCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbSelecetServerIP = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lbTaskCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbSelecetServerPort = new System.Windows.Forms.TextBox();
            this.lbSelecetServerName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.lbWriteThreadNum = new System.Windows.Forms.Label();
            this.lbUsedNum = new System.Windows.Forms.Label();
            this.lbAvailableSocketNum = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtServerIP = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1099, 650);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1099, 550);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel9);
            this.panel4.Controls.Add(this.panel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(302, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(797, 550);
            this.panel4.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.dataGridView1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 88);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(797, 462);
            this.panel9.TabIndex = 2;
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
            this.dataGridView1.Size = new System.Drawing.Size(797, 462);
            this.dataGridView1.TabIndex = 4;
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
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.lbUnfinishCount);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Controls.Add(this.lbFinishCount);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.lbSelecetServerIP);
            this.panel8.Controls.Add(this.checkBox1);
            this.panel8.Controls.Add(this.lbTaskCount);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.lbSelecetServerPort);
            this.panel8.Controls.Add(this.lbSelecetServerName);
            this.panel8.Controls.Add(this.button2);
            this.panel8.Controls.Add(this.checkBox2);
            this.panel8.Controls.Add(this.lbWriteThreadNum);
            this.panel8.Controls.Add(this.lbUsedNum);
            this.panel8.Controls.Add(this.lbAvailableSocketNum);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label6);
            this.panel8.Controls.Add(this.label5);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(797, 88);
            this.panel8.TabIndex = 1;
            // 
            // lbUnfinishCount
            // 
            this.lbUnfinishCount.AutoSize = true;
            this.lbUnfinishCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUnfinishCount.ForeColor = System.Drawing.Color.Red;
            this.lbUnfinishCount.Location = new System.Drawing.Point(332, 67);
            this.lbUnfinishCount.Name = "lbUnfinishCount";
            this.lbUnfinishCount.Size = new System.Drawing.Size(19, 12);
            this.lbUnfinishCount.TabIndex = 27;
            this.lbUnfinishCount.Text = "-1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(264, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "未完成数：";
            // 
            // lbFinishCount
            // 
            this.lbFinishCount.AutoSize = true;
            this.lbFinishCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbFinishCount.ForeColor = System.Drawing.Color.Red;
            this.lbFinishCount.Location = new System.Drawing.Point(177, 65);
            this.lbFinishCount.Name = "lbFinishCount";
            this.lbFinishCount.Size = new System.Drawing.Size(19, 12);
            this.lbFinishCount.TabIndex = 25;
            this.lbFinishCount.Text = "-1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(123, 66);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "完成数：";
            // 
            // lbSelecetServerIP
            // 
            this.lbSelecetServerIP.FormattingEnabled = true;
            this.lbSelecetServerIP.Items.AddRange(new object[] {
            "192.168.0.21",
            "116.255.249.67",
            "218.28.142.30",
            "218.29.67.197",
            "192.168.0.5",
            "61.163.1.178"});
            this.lbSelecetServerIP.Location = new System.Drawing.Point(325, 9);
            this.lbSelecetServerIP.Name = "lbSelecetServerIP";
            this.lbSelecetServerIP.Size = new System.Drawing.Size(128, 20);
            this.lbSelecetServerIP.TabIndex = 23;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(510, 57);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "过滤完成";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // lbTaskCount
            // 
            this.lbTaskCount.AutoSize = true;
            this.lbTaskCount.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTaskCount.ForeColor = System.Drawing.Color.Red;
            this.lbTaskCount.Location = new System.Drawing.Point(59, 66);
            this.lbTaskCount.Name = "lbTaskCount";
            this.lbTaskCount.Size = new System.Drawing.Size(19, 12);
            this.lbTaskCount.TabIndex = 21;
            this.lbTaskCount.Text = "-1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "任务数：";
            // 
            // lbSelecetServerPort
            // 
            this.lbSelecetServerPort.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSelecetServerPort.ForeColor = System.Drawing.Color.Red;
            this.lbSelecetServerPort.Location = new System.Drawing.Point(551, 9);
            this.lbSelecetServerPort.Name = "lbSelecetServerPort";
            this.lbSelecetServerPort.Size = new System.Drawing.Size(128, 26);
            this.lbSelecetServerPort.TabIndex = 18;
            this.lbSelecetServerPort.Text = "8000";
            // 
            // lbSelecetServerName
            // 
            this.lbSelecetServerName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSelecetServerName.ForeColor = System.Drawing.Color.Red;
            this.lbSelecetServerName.Location = new System.Drawing.Point(90, 9);
            this.lbSelecetServerName.Name = "lbSelecetServerName";
            this.lbSelecetServerName.Size = new System.Drawing.Size(128, 26);
            this.lbSelecetServerName.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(714, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 67);
            this.button2.TabIndex = 15;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(607, 57);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(72, 16);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "自动刷新";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // lbWriteThreadNum
            // 
            this.lbWriteThreadNum.AutoSize = true;
            this.lbWriteThreadNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWriteThreadNum.ForeColor = System.Drawing.Color.Red;
            this.lbWriteThreadNum.Location = new System.Drawing.Point(332, 39);
            this.lbWriteThreadNum.Name = "lbWriteThreadNum";
            this.lbWriteThreadNum.Size = new System.Drawing.Size(19, 12);
            this.lbWriteThreadNum.TabIndex = 12;
            this.lbWriteThreadNum.Text = "-1";
            // 
            // lbUsedNum
            // 
            this.lbUsedNum.AutoSize = true;
            this.lbUsedNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbUsedNum.ForeColor = System.Drawing.Color.Red;
            this.lbUsedNum.Location = new System.Drawing.Point(177, 39);
            this.lbUsedNum.Name = "lbUsedNum";
            this.lbUsedNum.Size = new System.Drawing.Size(19, 12);
            this.lbUsedNum.TabIndex = 11;
            this.lbUsedNum.Text = "-1";
            // 
            // lbAvailableSocketNum
            // 
            this.lbAvailableSocketNum.AutoSize = true;
            this.lbAvailableSocketNum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbAvailableSocketNum.ForeColor = System.Drawing.Color.Red;
            this.lbAvailableSocketNum.Location = new System.Drawing.Point(61, 39);
            this.lbAvailableSocketNum.Name = "lbAvailableSocketNum";
            this.lbAvailableSocketNum.Size = new System.Drawing.Size(19, 12);
            this.lbAvailableSocketNum.TabIndex = 9;
            this.lbAvailableSocketNum.Text = "-1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "写进程数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(123, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "已用数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "可用数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(468, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "服务器PORT：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "服务器IP：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器名称：";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(302, 550);
            this.panel5.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.treeView1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 88);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(302, 462);
            this.panel7.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "MainNode";
            treeNode1.Text = "服务器";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(302, 462);
            this.treeView1.TabIndex = 0;
            this.treeView1.Click += new System.EventHandler(this.treeView1_Click);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtServerIP);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Controls.Add(this.txtServerPort);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(302, 88);
            this.panel6.TabIndex = 0;
            // 
            // txtServerIP
            // 
            this.txtServerIP.FormattingEnabled = true;
            this.txtServerIP.Items.AddRange(new object[] {
            "192.168.0.21",
            "116.255.249.67",
            "218.28.142.30",
            "218.29.67.197",
            "192.168.0.5",
            "61.163.1.178"});
            this.txtServerIP.Location = new System.Drawing.Point(13, 9);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(157, 20);
            this.txtServerIP.TabIndex = 3;
            this.txtServerIP.Text = "116.255.249.67";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "获取所有服务器信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(13, 34);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(157, 21);
            this.txtServerPort.TabIndex = 1;
            this.txtServerPort.Text = "8000";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtOutput);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 550);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1099, 100);
            this.panel3.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(1099, 100);
            this.txtOutput.TabIndex = 2;
            this.txtOutput.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 650);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FmMain";
            this.Text = "FmMain";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbWriteThreadNum;
        private System.Windows.Forms.Label lbUsedNum;
        private System.Windows.Forms.Label lbAvailableSocketNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox lbSelecetServerPort;
        private System.Windows.Forms.TextBox lbSelecetServerName;
        private System.Windows.Forms.Label lbTaskCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox lbSelecetServerIP;
        private System.Windows.Forms.ComboBox txtServerIP;
        private System.Windows.Forms.Label lbFinishCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbUnfinishCount;
        private System.Windows.Forms.Timer timer1;
    }
}