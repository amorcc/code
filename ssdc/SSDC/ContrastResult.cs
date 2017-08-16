using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ContrastResult : BaseForm
    {
        public ContrastResult()
        {
            InitializeComponent();
        }


        public override void InitForm()
        {
            this.dataGridView1.DataSource = SSDC.DAL.ContrastResult.GetRepeatInfo();

            checkBox1_CheckedChanged(null, null);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
            {
                return;
            }

            int columnCount = this.dataGridView1.ColumnCount;
            int sumCount = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[columnCount - 1].Value);

            if (e.ColumnIndex > 0 && sumCount > 1)
            {
                int cellCount = Convert.ToInt32(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (cellCount >= 1)
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                    //this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style
                }
            }

            if (e.Value.ToString() == "是")
            {

            }

            DataGridView view = (DataGridView)sender;
            object originalValue = e.Value;

            //if (view.Columns[e.ColumnIndex].DataPropertyName == "sex")
            //    e.Value = ((int)originalValue == 1) ? "男" : "";
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.dataGridView1.CurrentCell = null;
            //CurrencyManager cm = (CurrencyManager)BindingContext[this.dataGridView1.DataSource];
            //cm.SuspendBinding();

            if (this.checkBox1.Checked == true)
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    int columnCount = this.dataGridView1.ColumnCount;
                    int sumCount = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[columnCount - 1].Value);

                    if (sumCount <= 1)
                    {

                        this.dataGridView1.Rows[i].Visible = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    this.dataGridView1.Rows[i].Visible = true;
                }
            }
            //cm.ResumeBinding();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.pHintExport.Show();
            DataTable dt = (DataTable)this.dataGridView1.DataSource;

            DataTable dtExport = dt.Clone();

            if (dtExport != null && dtExport.Columns.Count > 0)
            {
                for (int i = 0; i < dtExport.Columns.Count; i++)
                {
                    dtExport.Columns[i].ColumnName = this.dataGridView1.Columns[i].HeaderText;
                }
            }


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (this.checkBox1.Checked == true)
                {
                    int columnCount = dt.Columns.Count;
                    int sumCount = Convert.ToInt32(dt.Rows[i][columnCount - 1]);

                    if (sumCount > 1)
                    {
                        dtExport.Rows.Add(dt.Rows[i].ItemArray);
                    }
                }
                else
                {
                    dtExport.Rows.Add(dt.Rows[i].ItemArray);
                }
            }


            //SSDC.Common.ReadExcel.ExportExcel(dtExport);
            doExport(dtExport);

            this.pHintExport.Hide();
        }

        private void ContrastResult_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要分析身份证信息吗？该操作可能需要耗费较长时间.", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                AnalysisDataHint hint = new AnalysisDataHint();
                hint.ShowDialog();
            }
        }

        public void doExport(DataTable dt)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Execl files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName == "")
            {
                return;
            }

            Stream myStream;
            myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
            string str = "";

            try
            {
                //写标题
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        str += "\t";
                    }
                    str += dt.Columns[i].ToString().Trim();
                }
                sw.WriteLine(str);

                //写内容
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string tempStr = "";
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        if (k > 0)
                        {
                            tempStr += "\t";
                        }

                        tempStr += dt.Rows[j][k].ToString();
                    }
                    sw.WriteLine(tempStr);
                }

                sw.Close();
                myStream.Close();
            }

            catch (Exception ex)
            {
                sw.Close();
                myStream.Close();
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }
    }
}
