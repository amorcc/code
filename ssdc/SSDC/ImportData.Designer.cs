namespace SSDC
{
    partial class ImportData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvDataInfo = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RowCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifyTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.dgvExcel = new System.Windows.Forms.DataGridView();
            this.panel9 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.cBoxDataName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel20 = new System.Windows.Forms.Panel();
            this.cBoxID = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cBoxName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pChildMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataInfo)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel)).BeginInit();
            this.panel9.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pChildMain
            // 
            this.pChildMain.Controls.Add(this.panel1);
            this.pChildMain.Size = new System.Drawing.Size(939, 733);
            this.pChildMain.VisibleChanged += new System.EventHandler(this.pChildMain_VisibleChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 733);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(939, 697);
            this.panel3.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvDataInfo);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(939, 578);
            this.panel4.TabIndex = 0;
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
            this.DataName,
            this.RowCount,
            this.ModifyTime,
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvDataInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataInfo.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvDataInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvDataInfo.Name = "dgvDataInfo";
            this.dgvDataInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvDataInfo.RowHeadersVisible = false;
            this.dgvDataInfo.RowTemplate.Height = 23;
            this.dgvDataInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataInfo.Size = new System.Drawing.Size(939, 578);
            this.dgvDataInfo.TabIndex = 2;
            this.dgvDataInfo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDataInfo_CellContentClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.FillWeight = 25F;
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            // 
            // DataName
            // 
            this.DataName.DataPropertyName = "DataName";
            this.DataName.FillWeight = 90.668F;
            this.DataName.HeaderText = "数据名称";
            this.DataName.Name = "DataName";
            // 
            // RowCount
            // 
            this.RowCount.DataPropertyName = "RowCount";
            this.RowCount.FillWeight = 90.668F;
            this.RowCount.HeaderText = "数据行数";
            this.RowCount.Name = "RowCount";
            // 
            // ModifyTime
            // 
            this.ModifyTime.DataPropertyName = "ModifyTime";
            this.ModifyTime.FillWeight = 90.668F;
            this.ModifyTime.HeaderText = "最后修改时间";
            this.ModifyTime.Name = "ModifyTime";
            // 
            // Column1
            // 
            this.Column1.FillWeight = 22F;
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.Text = "清空";
            this.Column1.UseColumnTextForLinkValue = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 22F;
            this.Column2.HeaderText = "";
            this.Column2.Name = "Column2";
            this.Column2.Text = "导入";
            this.Column2.UseColumnTextForLinkValue = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 22F;
            this.Column3.HeaderText = "";
            this.Column3.Name = "Column3";
            this.Column3.Text = "设置";
            this.Column3.UseColumnTextForLinkValue = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel8);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 578);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(939, 119);
            this.panel5.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel16);
            this.panel8.Controls.Add(this.panel12);
            this.panel8.Controls.Add(this.panel9);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(939, 119);
            this.panel8.TabIndex = 2;
            this.panel8.Visible = false;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel16.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel16.Location = new System.Drawing.Point(0, 118);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(939, 1);
            this.panel16.TabIndex = 7;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.dgvExcel);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 117);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(939, 2);
            this.panel12.TabIndex = 3;
            // 
            // dgvExcel
            // 
            this.dgvExcel.AllowUserToAddRows = false;
            this.dgvExcel.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvExcel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvExcel.BackgroundColor = System.Drawing.Color.White;
            this.dgvExcel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvExcel.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvExcel.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgvExcel.Location = new System.Drawing.Point(0, 0);
            this.dgvExcel.Name = "dgvExcel";
            this.dgvExcel.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvExcel.RowHeadersVisible = false;
            this.dgvExcel.RowTemplate.Height = 23;
            this.dgvExcel.Size = new System.Drawing.Size(939, 2);
            this.dgvExcel.TabIndex = 1;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(203)))), ((int)(((byte)(254)))));
            this.panel9.Controls.Add(this.checkBox1);
            this.panel9.Controls.Add(this.btnImport);
            this.panel9.Controls.Add(this.cBoxDataName);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.panel20);
            this.panel9.Controls.Add(this.cBoxID);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.cBoxName);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.btnSelectFile);
            this.panel9.Controls.Add(this.txtFileName);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(939, 117);
            this.panel9.TabIndex = 2;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(816, 54);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(84, 16);
            this.checkBox1.TabIndex = 18;
            this.checkBox1.Text = "清空后导入";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnImport
            // 
            this.btnImport.BackgroundImage = global::SSDC.Properties.Resources.btn0;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.Location = new System.Drawing.Point(430, 85);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(87, 27);
            this.btnImport.TabIndex = 17;
            this.btnImport.Text = "立即导入";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // cBoxDataName
            // 
            this.cBoxDataName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxDataName.FormattingEnabled = true;
            this.cBoxDataName.Location = new System.Drawing.Point(633, 9);
            this.cBoxDataName.Name = "cBoxDataName";
            this.cBoxDataName.Size = new System.Drawing.Size(204, 20);
            this.cBoxDataName.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(550, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "导入数据表：";
            // 
            // panel20
            // 
            this.panel20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel20.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel20.Location = new System.Drawing.Point(0, 116);
            this.panel20.Name = "panel20";
            this.panel20.Size = new System.Drawing.Size(939, 1);
            this.panel20.TabIndex = 14;
            // 
            // cBoxID
            // 
            this.cBoxID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxID.FormattingEnabled = true;
            this.cBoxID.Location = new System.Drawing.Point(706, 51);
            this.cBoxID.Name = "cBoxID";
            this.cBoxID.Size = new System.Drawing.Size(87, 20);
            this.cBoxID.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(636, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "身份证列：";
            // 
            // cBoxName
            // 
            this.cBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cBoxName.FormattingEnabled = true;
            this.cBoxName.Location = new System.Drawing.Point(525, 50);
            this.cBoxName.Name = "cBoxName";
            this.cBoxName.Size = new System.Drawing.Size(87, 20);
            this.cBoxName.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(475, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "姓名列：";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(432, 49);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(31, 23);
            this.btnSelectFile.TabIndex = 10;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(102, 51);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(324, 21);
            this.txtFileName.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "选择文件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "导入数据";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(212)))));
            this.panel2.BackgroundImage = global::SSDC.Properties.Resources._00002按钮背景;
            this.panel2.Controls.Add(this.panel14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 36);
            this.panel2.TabIndex = 1;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(0, 35);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(939, 1);
            this.panel14.TabIndex = 6;
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 733);
            this.Name = "ImportData";
            this.Text = "ImportData";
            this.Load += new System.EventHandler(this.ImportData_Load);
            this.pChildMain.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataInfo)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvExcel)).EndInit();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvDataInfo;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.DataGridView dgvExcel;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel20;
        private System.Windows.Forms.ComboBox cBoxID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cBoxName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cBoxDataName;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifyTime;
        private System.Windows.Forms.DataGridViewLinkColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn Column2;
        private System.Windows.Forms.DataGridViewLinkColumn Column3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}