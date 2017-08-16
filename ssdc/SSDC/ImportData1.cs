using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportData1 : Form
    {
        public ImportData1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 城镇职工医保
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ImportExcel(this.dataGridView1, this.comboBox1, this.comboBox2, this.textBox1);
        }

        /// <summary>
        /// 城镇职工参保数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.ImportExcel(this.dataGridView2, this.comboBox4, this.comboBox3, this.textBox2);
        }

        /// <summary>
        /// 新农合参合数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.ImportExcel(this.dataGridView3, this.comboBox6, this.comboBox5, this.textBox3);
        }

        /// <summary>
        /// 读取excel数据
        /// </summary>
        /// <param name="iGrid"></param>
        /// <param name="iCBoxName"></param>
        /// <param name="iCBoxIDNumber"></param>
        /// <param name="iFileNameTextBox"></param>
        public void ImportExcel(DataGridView iGrid, ComboBox iCBoxName, ComboBox iCBoxIDNumber, TextBox iFileNameTextBox)
        {
            try
            {
                openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    LoadData(openFileDialog1.FileName, iGrid, iCBoxName, iCBoxIDNumber, iFileNameTextBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LoadData(string iFileFullPath, DataGridView iGrid, ComboBox iCBoxName, ComboBox iCBoxIDNumber, TextBox iFileNameTextBox)
        {

            #region 加载数据
            iFileNameTextBox.Text = iFileFullPath;

            DataSet ds = SSDC.Common.ReadExcel.ToDataTable(iFileNameTextBox.Text);

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                iGrid.DataSource = dt;


                if (dt.Columns != null && dt.Columns.Count > 0)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string colName = dt.Columns[i].ColumnName;
                        iCBoxName.Items.Add(colName);

                        iCBoxIDNumber.Items.Add(colName);
                    }
                }
                else
                {
                    throw new Exception("您所选择的excel文件没有列，请选择正确的文件导入！");
                }

            }
            else
            {
                throw new Exception("请正确选择导入的excel数据文件！");
            }
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                #region  验证数据
                if (!(this.dataGridView1 != null && this.dataGridView1.Rows.Count > 0))
                {
                    throw new Exception("没有城镇居民参保数据");
                }

                if (!(this.dataGridView2 != null && this.dataGridView2.Rows.Count > 0))
                {
                    throw new Exception("没有城镇职工参保数据");
                }

                if (!(this.dataGridView3 != null && this.dataGridView3.Rows.Count > 0))
                {
                    throw new Exception("没有新农合参保数据");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox1.Text))
                {
                    throw new Exception("城镇居民参保数据的姓名列没有指定");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox2.Text))
                {
                    throw new Exception("城镇居民参保数据的身份证列没有指定");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox3.Text))
                {
                    throw new Exception("城镇职工参保数据的身份证列没有指定");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox4.Text))
                {
                    throw new Exception("城镇职工参保数据的姓名列没有指定");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox5.Text))
                {
                    throw new Exception("新农合参保数据的身份证列没有指定");
                }

                if (string.IsNullOrWhiteSpace(this.comboBox6.Text))
                {
                    throw new Exception("新农合参保数据的姓名列没有指定");
                }
                #endregion

                #region 调用导入进度窗体
                ImportProgress1 importProgress = new ImportProgress1();
                importProgress.czjmybTable = (DataTable)this.dataGridView1.DataSource;
                importProgress.czzzybTable = (DataTable)this.dataGridView2.DataSource;
                importProgress.xnhybTable = (DataTable)this.dataGridView3.DataSource;

                importProgress.czjmybNameColName = this.comboBox1.Text;
                importProgress.czjmybIdNumberColName = this.comboBox2.Text;

                importProgress.czzzybNameColName = this.comboBox4.Text;
                importProgress.czzzybIdNumberColName = this.comboBox3.Text;

                importProgress.xnhybNameColName = this.comboBox6.Text;
                importProgress.xnhybIdNumberColName = this.comboBox5.Text;

                importProgress.InitCount();

                importProgress.ShowDialog();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadData(this.textBox1.Text, this.dataGridView1, this.comboBox1, this.comboBox2, this.textBox1);

            LoadData(this.textBox2.Text, this.dataGridView2, this.comboBox4, this.comboBox3, this.textBox2);

            LoadData(this.textBox3.Text, this.dataGridView3, this.comboBox6, this.comboBox5, this.textBox3);

            this.comboBox1.Text = "姓名";
            this.comboBox2.Text = "身份证";
            this.comboBox3.Text = "AAC002";
            this.comboBox4.Text = "AAC003";
            this.comboBox5.Text = "身份证号";
            this.comboBox6.Text = "人员姓名";
        }
    }
}
