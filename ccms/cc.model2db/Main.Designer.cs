namespace cc.model2db
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSelectModelDLLFile = new System.Windows.Forms.Button();
            this.txtModelDLLFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.cboxDBConnStrs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboxDBConnStrs);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnSelectModelDLLFile);
            this.panel1.Controls.Add(this.txtModelDLLFile);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1039, 54);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(629, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSelectModelDLLFile
            // 
            this.btnSelectModelDLLFile.Location = new System.Drawing.Point(536, 25);
            this.btnSelectModelDLLFile.Name = "btnSelectModelDLLFile";
            this.btnSelectModelDLLFile.Size = new System.Drawing.Size(24, 23);
            this.btnSelectModelDLLFile.TabIndex = 10;
            this.btnSelectModelDLLFile.Text = "…";
            this.btnSelectModelDLLFile.UseVisualStyleBackColor = true;
            this.btnSelectModelDLLFile.Click += new System.EventHandler(this.btnSelectModelDLLFile_Click);
            // 
            // txtModelDLLFile
            // 
            this.txtModelDLLFile.Location = new System.Drawing.Point(366, 27);
            this.txtModelDLLFile.Name = "txtModelDLLFile";
            this.txtModelDLLFile.Size = new System.Drawing.Size(164, 21);
            this.txtModelDLLFile.TabIndex = 9;
            this.txtModelDLLFile.Text = "E:\\cc_space\\code\\ccms\\cc.model\\bin\\Debug\\cc.model.dll";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Model保存路径";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBox1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1039, 642);
            this.panel2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 54);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1039, 588);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // cboxDBConnStrs
            // 
            this.cboxDBConnStrs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxDBConnStrs.FormattingEnabled = true;
            this.cboxDBConnStrs.Location = new System.Drawing.Point(78, 22);
            this.cboxDBConnStrs.Name = "cboxDBConnStrs";
            this.cboxDBConnStrs.Size = new System.Drawing.Size(121, 20);
            this.cboxDBConnStrs.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据库";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 642);
            this.Controls.Add(this.panel2);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSelectModelDLLFile;
        private System.Windows.Forms.TextBox txtModelDLLFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox cboxDBConnStrs;
        private System.Windows.Forms.Label label1;
    }
}