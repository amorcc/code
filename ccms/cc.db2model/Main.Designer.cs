namespace cc.db2model
{
    partial class Main
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cboxDBConnStrs = new System.Windows.Forms.ComboBox();
            this.btnQueryTableFromDB = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtTableNameFilter = new System.Windows.Forms.TextBox();
            this.btnTableNameFilter = new System.Windows.Forms.Button();
            this.checkListTableName = new System.Windows.Forms.CheckedListBox();
            this.cboxTableSelectedAll = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtModelSavePath = new System.Windows.Forms.TextBox();
            this.btnSelectModelSavePath = new System.Windows.Forms.Button();
            this.btnSelectDalSavePath = new System.Windows.Forms.Button();
            this.txtDalSavePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModelNamespace = new System.Windows.Forms.TextBox();
            this.txtDalNamespace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtModelBaseClassName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDalBaseClassName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtModelUsing = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDalUsing = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1231, 646);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel8);
            this.panel2.Controls.Add(this.btnQueryTableFromDB);
            this.panel2.Controls.Add(this.cboxDBConnStrs);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1231, 56);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox1);
            this.panel3.Controls.Add(this.panel9);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 56);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1231, 590);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库";
            // 
            // cboxDBConnStrs
            // 
            this.cboxDBConnStrs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxDBConnStrs.FormattingEnabled = true;
            this.cboxDBConnStrs.Location = new System.Drawing.Point(57, 15);
            this.cboxDBConnStrs.Name = "cboxDBConnStrs";
            this.cboxDBConnStrs.Size = new System.Drawing.Size(121, 20);
            this.cboxDBConnStrs.TabIndex = 1;
            // 
            // btnQueryTableFromDB
            // 
            this.btnQueryTableFromDB.Location = new System.Drawing.Point(204, 12);
            this.btnQueryTableFromDB.Name = "btnQueryTableFromDB";
            this.btnQueryTableFromDB.Size = new System.Drawing.Size(75, 23);
            this.btnQueryTableFromDB.TabIndex = 2;
            this.btnQueryTableFromDB.Text = "查询";
            this.btnQueryTableFromDB.UseVisualStyleBackColor = true;
            this.btnQueryTableFromDB.Click += new System.EventHandler(this.btnQueryTableFromDB_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(246, 590);
            this.panel4.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cboxTableSelectedAll);
            this.panel5.Controls.Add(this.btnTableNameFilter);
            this.panel5.Controls.Add(this.txtTableNameFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(246, 61);
            this.panel5.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.checkListTableName);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 61);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(246, 529);
            this.panel6.TabIndex = 1;
            // 
            // txtTableNameFilter
            // 
            this.txtTableNameFilter.Location = new System.Drawing.Point(3, 15);
            this.txtTableNameFilter.Name = "txtTableNameFilter";
            this.txtTableNameFilter.Size = new System.Drawing.Size(164, 21);
            this.txtTableNameFilter.TabIndex = 1;
            this.txtTableNameFilter.Text = "zmm_";
            // 
            // btnTableNameFilter
            // 
            this.btnTableNameFilter.Location = new System.Drawing.Point(173, 14);
            this.btnTableNameFilter.Name = "btnTableNameFilter";
            this.btnTableNameFilter.Size = new System.Drawing.Size(58, 23);
            this.btnTableNameFilter.TabIndex = 3;
            this.btnTableNameFilter.Text = "过滤";
            this.btnTableNameFilter.UseVisualStyleBackColor = true;
            this.btnTableNameFilter.Click += new System.EventHandler(this.btnTableNameFilter_Click);
            // 
            // checkListTableName
            // 
            this.checkListTableName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkListTableName.FormattingEnabled = true;
            this.checkListTableName.Location = new System.Drawing.Point(0, 0);
            this.checkListTableName.Name = "checkListTableName";
            this.checkListTableName.Size = new System.Drawing.Size(246, 529);
            this.checkListTableName.TabIndex = 2;
            // 
            // cboxTableSelectedAll
            // 
            this.cboxTableSelectedAll.AutoSize = true;
            this.cboxTableSelectedAll.Location = new System.Drawing.Point(3, 42);
            this.cboxTableSelectedAll.Name = "cboxTableSelectedAll";
            this.cboxTableSelectedAll.Size = new System.Drawing.Size(48, 16);
            this.cboxTableSelectedAll.TabIndex = 1;
            this.cboxTableSelectedAll.Text = "全选";
            this.cboxTableSelectedAll.UseVisualStyleBackColor = true;
            this.cboxTableSelectedAll.CheckedChanged += new System.EventHandler(this.cboxTableSelectedAll_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Location = new System.Drawing.Point(246, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1, 590);
            this.panel7.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Black;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 55);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1231, 1);
            this.panel8.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btnStart);
            this.panel9.Controls.Add(this.txtDalUsing);
            this.panel9.Controls.Add(this.txtDalBaseClassName);
            this.panel9.Controls.Add(this.txtDalNamespace);
            this.panel9.Controls.Add(this.label9);
            this.panel9.Controls.Add(this.label7);
            this.panel9.Controls.Add(this.label5);
            this.panel9.Controls.Add(this.txtModelUsing);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Controls.Add(this.txtModelBaseClassName);
            this.panel9.Controls.Add(this.label6);
            this.panel9.Controls.Add(this.txtModelNamespace);
            this.panel9.Controls.Add(this.label4);
            this.panel9.Controls.Add(this.btnSelectDalSavePath);
            this.panel9.Controls.Add(this.txtDalSavePath);
            this.panel9.Controls.Add(this.label3);
            this.panel9.Controls.Add(this.btnSelectModelSavePath);
            this.panel9.Controls.Add(this.txtModelSavePath);
            this.panel9.Controls.Add(this.label2);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(247, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(984, 137);
            this.panel9.TabIndex = 2;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.Black;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel10.Location = new System.Drawing.Point(0, 136);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(984, 1);
            this.panel10.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Model保存路径";
            // 
            // txtModelSavePath
            // 
            this.txtModelSavePath.Location = new System.Drawing.Point(95, 21);
            this.txtModelSavePath.Name = "txtModelSavePath";
            this.txtModelSavePath.Size = new System.Drawing.Size(164, 21);
            this.txtModelSavePath.TabIndex = 6;
            // 
            // btnSelectModelSavePath
            // 
            this.btnSelectModelSavePath.Location = new System.Drawing.Point(265, 19);
            this.btnSelectModelSavePath.Name = "btnSelectModelSavePath";
            this.btnSelectModelSavePath.Size = new System.Drawing.Size(24, 23);
            this.btnSelectModelSavePath.TabIndex = 7;
            this.btnSelectModelSavePath.Text = "…";
            this.btnSelectModelSavePath.UseVisualStyleBackColor = true;
            this.btnSelectModelSavePath.Click += new System.EventHandler(this.btnSelectModelSavePath_Click);
            // 
            // btnSelectDalSavePath
            // 
            this.btnSelectDalSavePath.Location = new System.Drawing.Point(579, 19);
            this.btnSelectDalSavePath.Name = "btnSelectDalSavePath";
            this.btnSelectDalSavePath.Size = new System.Drawing.Size(24, 23);
            this.btnSelectDalSavePath.TabIndex = 10;
            this.btnSelectDalSavePath.Text = "…";
            this.btnSelectDalSavePath.UseVisualStyleBackColor = true;
            this.btnSelectDalSavePath.Click += new System.EventHandler(this.btnSelectDalSavePath_Click);
            // 
            // txtDalSavePath
            // 
            this.txtDalSavePath.Location = new System.Drawing.Point(409, 21);
            this.txtDalSavePath.Name = "txtDalSavePath";
            this.txtDalSavePath.Size = new System.Drawing.Size(164, 21);
            this.txtDalSavePath.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(320, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "Dal保存路径";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(247, 137);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(984, 453);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "Model命名空间";
            // 
            // txtModelNamespace
            // 
            this.txtModelNamespace.Location = new System.Drawing.Point(95, 49);
            this.txtModelNamespace.Name = "txtModelNamespace";
            this.txtModelNamespace.Size = new System.Drawing.Size(164, 21);
            this.txtModelNamespace.TabIndex = 12;
            // 
            // txtDalNamespace
            // 
            this.txtDalNamespace.Location = new System.Drawing.Point(409, 52);
            this.txtDalNamespace.Name = "txtDalNamespace";
            this.txtDalNamespace.Size = new System.Drawing.Size(164, 21);
            this.txtDalNamespace.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(320, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "Dal命名空间";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "Model基类";
            // 
            // txtModelBaseClassName
            // 
            this.txtModelBaseClassName.Location = new System.Drawing.Point(95, 76);
            this.txtModelBaseClassName.Name = "txtModelBaseClassName";
            this.txtModelBaseClassName.Size = new System.Drawing.Size(164, 21);
            this.txtModelBaseClassName.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(320, 84);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "Dal基类";
            // 
            // txtDalBaseClassName
            // 
            this.txtDalBaseClassName.Location = new System.Drawing.Point(409, 79);
            this.txtDalBaseClassName.Name = "txtDalBaseClassName";
            this.txtDalBaseClassName.Size = new System.Drawing.Size(164, 21);
            this.txtDalBaseClassName.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "Model的using";
            // 
            // txtModelUsing
            // 
            this.txtModelUsing.Location = new System.Drawing.Point(95, 103);
            this.txtModelUsing.Name = "txtModelUsing";
            this.txtModelUsing.Size = new System.Drawing.Size(164, 21);
            this.txtModelUsing.TabIndex = 12;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(320, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 13;
            this.label9.Text = "Dal的using";
            // 
            // txtDalUsing
            // 
            this.txtDalUsing.Location = new System.Drawing.Point(409, 106);
            this.txtDalUsing.Name = "txtDalUsing";
            this.txtDalUsing.Size = new System.Drawing.Size(164, 21);
            this.txtDalUsing.TabIndex = 14;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(689, 19);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(123, 110);
            this.btnStart.TabIndex = 15;
            this.btnStart.Text = "开始生成";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 646);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cboxDBConnStrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQueryTableFromDB;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnTableNameFilter;
        private System.Windows.Forms.TextBox txtTableNameFilter;
        private System.Windows.Forms.CheckedListBox checkListTableName;
        private System.Windows.Forms.CheckBox cboxTableSelectedAll;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btnSelectModelSavePath;
        private System.Windows.Forms.TextBox txtModelSavePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectDalSavePath;
        private System.Windows.Forms.TextBox txtDalSavePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtDalNamespace;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtModelNamespace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDalBaseClassName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtModelBaseClassName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDalUsing;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtModelUsing;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnStart;
    }
}