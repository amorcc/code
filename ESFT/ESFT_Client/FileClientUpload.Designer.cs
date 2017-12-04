namespace ESFT.Client
{
    partial class FileClientUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileClientUpload));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ServerFuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerRealFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServerPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurrentCompleteLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransferTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WriteLenght = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastPacketTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 515);
            this.panel1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dataGridView1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 43);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(818, 424);
            this.panel4.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btn,
            this.ServerFuleName,
            this.ServerRealFullName,
            this.TransferState,
            this.ServerIp,
            this.ServerPort,
            this.FileFullName,
            this.key,
            this.FileLenght,
            this.CurrentCompleteLenght,
            this.TSpeed,
            this.TransferTime,
            this.WriteLenght,
            this.LastPacketTime});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(818, 424);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_1);
            // 
            // btn
            // 
            this.btn.HeaderText = "停止";
            this.btn.Name = "btn";
            this.btn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btn.Width = 60;
            // 
            // ServerFuleName
            // 
            this.ServerFuleName.HeaderText = "服务器保存文件全路径";
            this.ServerFuleName.Name = "ServerFuleName";
            // 
            // ServerRealFullName
            // 
            this.ServerRealFullName.HeaderText = "服务器真实保存路径";
            this.ServerRealFullName.Name = "ServerRealFullName";
            // 
            // TransferState
            // 
            this.TransferState.HeaderText = "状态";
            this.TransferState.Name = "TransferState";
            // 
            // ServerIp
            // 
            this.ServerIp.HeaderText = "服务器IP";
            this.ServerIp.Name = "ServerIp";
            // 
            // ServerPort
            // 
            this.ServerPort.HeaderText = "服务器端口";
            this.ServerPort.Name = "ServerPort";
            // 
            // FileFullName
            // 
            this.FileFullName.HeaderText = "文件位置";
            this.FileFullName.Name = "FileFullName";
            // 
            // key
            // 
            this.key.HeaderText = "传输号";
            this.key.Name = "key";
            // 
            // FileLenght
            // 
            this.FileLenght.HeaderText = "文件大小";
            this.FileLenght.Name = "FileLenght";
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
            // WriteLenght
            // 
            this.WriteLenght.HeaderText = "已经写入";
            this.WriteLenght.Name = "WriteLenght";
            // 
            // LastPacketTime
            // 
            this.LastPacketTime.HeaderText = "最后包时间";
            this.LastPacketTime.Name = "LastPacketTime";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button10);
            this.panel3.Controls.Add(this.button9);
            this.panel3.Controls.Add(this.button8);
            this.panel3.Controls.Add(this.button7);
            this.panel3.Controls.Add(this.button6);
            this.panel3.Controls.Add(this.button5);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 467);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(818, 48);
            this.panel3.TabIndex = 1;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(174, 22);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 20;
            this.button10.Text = "test";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(273, 6);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 19;
            this.button9.Text = "初始化";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(563, 13);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 18;
            this.button8.Text = "测试接收";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(482, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 17;
            this.button7.Text = "测试发送";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(75, 13);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "下载文件";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(385, 13);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "全部开始";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(703, 13);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "GC";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.textBox3);
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 43);
            this.panel2.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(174, 10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "全部停止";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "全部开始";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(453, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "服务器IP：";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(739, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(69, 21);
            this.textBox3.TabIndex = 11;
            this.textBox3.Text = "8000";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(521, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(111, 21);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "192.168.1.6";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(644, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "服务器端口号：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 950;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FileClientUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 515);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileClientUpload";
            this.Text = "FileClientUpload";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileClientUpload_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewLinkColumn btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerFuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerRealFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerIp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServerPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn key;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentCompleteLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn TSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransferTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn WriteLenght;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastPacketTime;
    }
}