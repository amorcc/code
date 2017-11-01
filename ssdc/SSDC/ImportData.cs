using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportData : BaseForm
    {
        public ImportData()
        {
            InitializeComponent();
        }

        private void ImportData_Load(object sender, EventArgs e)
        {

            this.InitForm();
        }

        public override void InitForm()
        {
            #region 加载datainfo的信息

            //DataGridViewButtonColumn dgvbc = new DataGridViewButtonColumn();
            //dgvbc.Text = "删除";
            //dgvbc.UseColumnTextForButtonValue = true;
            //dgvbc.DisplayIndex = 9;
            //this.dgvDataInfo.Columns.Add(dgvbc);
            this.LoadDataInfo();
            #endregion
        }

        #region 加载datainfo的信息
        void LoadDataInfo()
        {
            SSDC.DAL.datainfo datainfoDal = new DAL.datainfo();

            try
            {
                using (DataSet ds = datainfoDal.GetDataInfo())
                {
                    if (ds != null && ds.Tables != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        //dt.Columns["ID"].ColumnName = "序号";
                        //dt.Columns["DataName"].ColumnName = "数据名称";
                        //dt.Columns["RowCount"].ColumnName = "数据行数";
                        //dt.Columns["ModifyTime"].ColumnName = "最后修改时间";

                        this.dgvDataInfo.DataSource = dt;
                        this.dgvDataInfo.Columns["TableName"].Visible = false;

                        this.cBoxDataName.DataSource = dt;
                        this.cBoxDataName.DisplayMember = "DataName";
                        this.cBoxDataName.ValueMember = "ID";
                    }
                    else
                    {
                        MessageBox.Show("数据出错，请联系管理员！");
                    }
                }
            }
            catch
            {
                System.Environment.Exit(0);
            }
        }
        #endregion

        #region 选择要导入的文件

        private void button3_Click(object sender, EventArgs e)
        {
            this.cBoxName.Items.Clear();
            this.cBoxID.Items.Clear();
            this.ImportExcel(this.dgvExcel, this.cBoxName, this.cBoxID, this.txtFileName);
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

        #endregion

        #region 导入到数据库


        private void btnImport_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                if (MessageBox.Show(string.Format("是否确认先清空{0}表数据后，再导入新数据？", this.cBoxDataName.SelectedText), "确认", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    SSDC.DAL.ImportRecord.DeleteTable(this.cBoxDataName.SelectedIndex + 1);
                }
                else
                {
                    return;
                }
            }

            try
            {
                #region  验证数据
                if (!(this.dgvExcel != null && this.dgvExcel.Rows.Count > 0))
                {
                    throw new Exception("没有数据");
                }


                if (string.IsNullOrEmpty(this.cBoxName.Text))
                {
                    throw new Exception("数据的姓名列没有指定");
                }

                if (string.IsNullOrEmpty(this.cBoxID.Text))
                {
                    throw new Exception("数据的身份证列没有指定");
                }


                #endregion

                #region 调用导入进度窗体
                ImportProgress importProgress = new ImportProgress();
                importProgress.mImportType = this.cBoxDataName.SelectedIndex + 1;
                importProgress.mTable = (DataTable)this.dgvExcel.DataSource;
                importProgress.mColName = this.cBoxName.Text;
                importProgress.mColId = this.cBoxID.Text;
                importProgress.mDatainfoType = Convert.ToInt32(this.cBoxDataName.SelectedValue);

                importProgress.InitCount();

                importProgress.ShowDialog();

                this.InitForm();
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion

        private void dgvDataInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var idCellStr = this.dgvDataInfo.Rows[e.RowIndex].Cells["ID"].Value;
                int id = 0;
                if (idCellStr != null)
                {
                    int.TryParse(idCellStr.ToString(), out id);
                }

                if (MessageBox.Show("确定的要删除数据吗？删除后数据无法恢复！", "警告", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    SSDC.DAL.ImportRecord.DeleteTable(id);
                }
                this.InitForm();
            }
            else if (e.ColumnIndex == 1)
            {
                if (multiImport == null)
                {
                    multiImport = new ImportDataMulti();
                    multiImport.mTableName = this.dgvDataInfo.Rows[e.RowIndex].Cells["DataName"].Value.ToString();
                    multiImport.mTableNum = Convert.ToInt32(this.dgvDataInfo.Rows[e.RowIndex].Cells["ID"].Value);
                    multiImport.InitForm();
                    multiImport.pChildMain.Parent = this.pChildMain;
                    multiImport.pChildMain.BringToFront();
                }
                else
                {
                    multiImport.mTableName = this.dgvDataInfo.Rows[e.RowIndex].Cells["DataName"].Value.ToString();
                    multiImport.mTableNum = Convert.ToInt32(this.dgvDataInfo.Rows[e.RowIndex].Cells["ID"].Value);
                    multiImport.InitForm();
                    multiImport.pChildMain.Visible = true;
                    multiImport.pChildMain.BringToFront();
                }
            }
            else if (e.ColumnIndex == 2)
            {
                ModifyTableName mtnForm = new ModifyTableName();
                mtnForm.mTableName = this.dgvDataInfo.Rows[e.RowIndex].Cells["DataName"].Value.ToString();
                mtnForm.mID = Convert.ToInt32(this.dgvDataInfo.Rows[e.RowIndex].Cells["ID"].Value);
                mtnForm.ShowDialog();
                this.InitForm();
            }
        }

        ImportDataMulti multiImport;

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            this.InitForm();
        }

        private void pChildMain_VisibleChanged(object sender, EventArgs e)
        {
            //if (this.pChildMain.Visible == true)
            //{
            //    this.InitForm();
            //}
        }

    }
}
