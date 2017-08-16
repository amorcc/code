namespace SSDC
{
    partial class ImportDataMulti
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel11111 = new System.Windows.Forms.Panel();
            this.dgvDataInfo = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FullFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.IdNumberCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.StatusStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cBoxID = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cBoxName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnReturn = new SSDC.CCButton();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ccButton1 = new SSDC.CCButton();
            this.ccButton2 = new SSDC.CCButton();
            this.pChildMain.SuspendLayout();
            this.panel11111.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pChildMain
            // 
            this.pChildMain.Controls.Add(this.panel11111);
            this.pChildMain.Controls.Add(this.panel1);
            this.pChildMain.Controls.Add(this.panel2);
            this.pChildMain.Size = new System.Drawing.Size(1179, 630);
            // 
            // panel11111
            // 
            this.panel11111.Controls.Add(this.dgvDataInfo);
            this.panel11111.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11111.Location = new System.Drawing.Point(0, 87);
            this.panel11111.Name = "panel11111";
            this.panel11111.Size = new System.Drawing.Size(1179, 482);
            this.panel11111.TabIndex = 0;
            // 
            // dgvDataInfo
            // 
            this.dgvDataInfo.AllowUserToAddRows = false;
            this.dgvDataInfo.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDataInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDataInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDataInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvDataInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDataInfo.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDataInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.FileName,
            this.FullFileName,
            this.NameCol,
            this.IdNumberCol,
            this.StatusStr,
            this.Status});
            this.dgvDataInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDataInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvDataInfo.Name = "dgvDataInfo";
            this.dgvDataInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDataInfo.RowHeadersVisible = false;
            this.dgvDataInfo.RowTemplate.Height = 23;
            this.dgvDataInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataInfo.Size = new System.Drawing.Size(1179, 482);
            this.dgvDataInfo.TabIndex = 28;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 25F;
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "文件名";
            this.FileName.Name = "FileName";
            // 
            // FullFileName
            // 
            this.FullFileName.DataPropertyName = "FullFileName";
            this.FullFileName.HeaderText = "文件路径";
            this.FullFileName.Name = "FullFileName";
            // 
            // NameCol
            // 
            this.NameCol.DataPropertyName = "NameCol";
            this.NameCol.HeaderText = "姓名列";
            this.NameCol.Items.AddRange(new object[] {
            "1231",
            "123",
            "123",
            "123",
            "123123"});
            this.NameCol.Name = "NameCol";
            // 
            // IdNumberCol
            // 
            this.IdNumberCol.DataPropertyName = "IdNumberCol";
            this.IdNumberCol.HeaderText = "身份证列";
            this.IdNumberCol.Name = "IdNumberCol";
            // 
            // StatusStr
            // 
            this.StatusStr.DataPropertyName = "StatusStr";
            this.StatusStr.HeaderText = "状态";
            this.StatusStr.Name = "StatusStr";
            this.StatusStr.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StatusStr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Status
            // 
            this.Status.HeaderText = "";
            this.Status.Name = "Status";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(461, 14);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(87, 21);
            this.checkBox1.TabIndex = 27;
            this.checkBox1.Text = "清空后导入";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // cBoxID
            // 
            this.cBoxID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cBoxID.FormattingEnabled = true;
            this.cBoxID.Location = new System.Drawing.Point(351, 12);
            this.cBoxID.Name = "cBoxID";
            this.cBoxID.Size = new System.Drawing.Size(87, 25);
            this.cBoxID.TabIndex = 25;
            this.cBoxID.SelectedIndexChanged += new System.EventHandler(this.cBoxID_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(281, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 17);
            this.label7.TabIndex = 24;
            this.label7.Text = "身份证列：";
            // 
            // cBoxName
            // 
            this.cBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cBoxName.FormattingEnabled = true;
            this.cBoxName.Location = new System.Drawing.Point(170, 12);
            this.cBoxName.Name = "cBoxName";
            this.cBoxName.Size = new System.Drawing.Size(87, 25);
            this.cBoxName.TabIndex = 23;
            this.cBoxName.SelectedIndexChanged += new System.EventHandler(this.cBoxName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(697, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(114, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "姓名列：";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(212)))));
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1179, 87);
            this.panel2.TabIndex = 2;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(111)))), ((int)(((byte)(189)))), ((int)(((byte)(255)))));
            this.panel5.Controls.Add(this.btnReturn);
            this.panel5.Controls.Add(this.checkBox1);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.cBoxID);
            this.panel5.Controls.Add(this.cBoxName);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 36);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1179, 50);
            this.panel5.TabIndex = 29;
            // 
            // btnReturn
            // 
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.BackgroundImage = global::SSDC.Properties.Resources._05通用按钮_1;
            this.btnReturn.BtnText = "选择文件";
            this.btnReturn.Down = false;
            this.btnReturn.DownBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.btnReturn.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReturn.GroupNum = 0;
            this.btnReturn.Location = new System.Drawing.Point(16, 11);
            this.btnReturn.MouseMoveBgColor = global::SSDC.Properties.Resources._05通用按钮_3;
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.NormalBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.btnReturn.Size = new System.Drawing.Size(90, 26);
            this.btnReturn.TabIndex = 16;
            this.btnReturn.TextColor = System.Drawing.Color.White;
            this.btnReturn.TextTop = 5;
            this.btnReturn.BtnClick += new SSDC.CCButton.BtnClickEventHandler(this.btnSelectFile_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::SSDC.Properties.Resources.btn0;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(594, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 27);
            this.button1.TabIndex = 26;
            this.button1.Text = "选择文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(212)))));
            this.panel4.BackgroundImage = global::SSDC.Properties.Resources._00002按钮背景;
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1179, 36);
            this.panel4.TabIndex = 28;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 35);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1179, 1);
            this.panel3.TabIndex = 6;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(8, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 17);
            this.label13.TabIndex = 5;
            this.label13.Text = "导入数据";
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 86);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(1179, 1);
            this.panel14.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ccButton1);
            this.panel1.Controls.Add(this.ccButton2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 569);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1179, 61);
            this.panel1.TabIndex = 3;
            // 
            // ccButton1
            // 
            this.ccButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ccButton1.BackColor = System.Drawing.Color.Transparent;
            this.ccButton1.BackgroundImage = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton1.BtnText = "立即导入";
            this.ccButton1.Down = false;
            this.ccButton1.DownBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ccButton1.GroupNum = 0;
            this.ccButton1.Location = new System.Drawing.Point(490, 18);
            this.ccButton1.MouseMoveBgColor = global::SSDC.Properties.Resources._05通用按钮_3;
            this.ccButton1.Name = "ccButton1";
            this.ccButton1.NormalBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton1.Size = new System.Drawing.Size(90, 26);
            this.ccButton1.TabIndex = 28;
            this.ccButton1.TextColor = System.Drawing.Color.White;
            this.ccButton1.TextTop = 5;
            this.ccButton1.BtnClick += new SSDC.CCButton.BtnClickEventHandler(this.btnImport_Click);
            // 
            // ccButton2
            // 
            this.ccButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ccButton2.BackColor = System.Drawing.Color.Transparent;
            this.ccButton2.BackgroundImage = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton2.BtnText = "返回";
            this.ccButton2.Down = false;
            this.ccButton2.DownBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ccButton2.GroupNum = 0;
            this.ccButton2.Location = new System.Drawing.Point(594, 18);
            this.ccButton2.MouseMoveBgColor = global::SSDC.Properties.Resources._05通用按钮_3;
            this.ccButton2.Name = "ccButton2";
            this.ccButton2.NormalBgColor = global::SSDC.Properties.Resources._05通用按钮_1;
            this.ccButton2.Size = new System.Drawing.Size(90, 26);
            this.ccButton2.TabIndex = 29;
            this.ccButton2.TextColor = System.Drawing.Color.White;
            this.ccButton2.TextTop = 5;
            this.ccButton2.BtnClick += new SSDC.CCButton.BtnClickEventHandler(this.button2_Click);
            // 
            // ImportDataMulti
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 630);
            this.Name = "ImportDataMulti";
            this.Text = "ImportDataMulti";
            this.Load += new System.EventHandler(this.ImportDataMulti_Load);
            this.pChildMain.ResumeLayout(false);
            this.panel11111.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel11111;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cBoxID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cBoxName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dgvDataInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FullFileName;
        private System.Windows.Forms.DataGridViewComboBoxColumn NameCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn IdNumberCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn StatusStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel5;
        public CCButton btnReturn;
        public CCButton ccButton2;
        public CCButton ccButton1;
    }
}