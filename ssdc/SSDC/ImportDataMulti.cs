using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportDataMulti : BaseForm
    {
        public ImportDataMulti()
        {
            InitializeComponent();
        }

        public override void InitForm()
        {
            base.InitForm();

            if (this.mShowTable.Columns.Count == 0)
            {
                this.mShowTable.Columns.Add("ID", typeof(int));
                this.mShowTable.Columns.Add("FileName", typeof(string));
                this.mShowTable.Columns.Add("FullFileName", typeof(string));
                this.mShowTable.Columns.Add("Status", typeof(int));
                this.mShowTable.Columns.Add("StatusStr", typeof(string));
                this.mShowTable.Columns.Add("NameCol", typeof(string));
                this.mShowTable.Columns.Add("IdNumberCol", typeof(string));
            }

            this.dgvDataInfo.DataSource = this.mShowTable;
            this.label13.Text = "导入数据到" + this.mTableName;
            this.mColNames.Clear();
        }

        static Semaphore semaphore = new Semaphore(2, 2);
        public string mTableName = string.Empty;
        public int mTableNum = 1;
        List<string> mFileNames = new List<string>();
        List<string> mColNames = new List<string>();
        List<DataTable> mTables = new List<DataTable>();
        DataTable mShowTable = new DataTable();
        int mTotalCount = 0;

        bool mImporting = false;

        /// <summary>
        /// 已完成条数
        /// </summary>
        int completedCount = 0;
        int importRowCountUpdateUI = 3000;

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            this.cBoxName.Items.Clear();
            this.cBoxID.Items.Clear();

            try
            {
                openFileDialog1.Filter = "Excel文件(*.xls)|*.xls|Excel文件(*.xlsx)|*.xlsx";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //LoadData(openFileDialog1.FileName, iGrid, iCBoxName, iCBoxIDNumber, iFileNameTextBox);
                    if (openFileDialog1.FileNames.Count() > 0)
                    {
                        #region 开始加载数据
                        foreach (var fileName in openFileDialog1.FileNames)
                        {
                            //this.LoadFile(fileName);
                            var existFile = (from t in this.mFileNames
                                             where t == fileName
                                             select t).Count() > 0 ? true : false;

                            if (existFile == false)
                            {
                                this.mFileNames.Add(fileName);
                                this.AddFile(fileName);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("文件[{0}]已经加入", fileName));
                            }
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void AddFile(string iFileName)
        {
            int i = this.mShowTable.Rows.Count;

            DataRow dr = this.mShowTable.NewRow();

            dr["ID"] = i + 1;
            dr["FullFileName"] = iFileName;
            dr["FileName"] = iFileName.Substring(iFileName.LastIndexOf('\\') + 1, iFileName.Length - iFileName.LastIndexOf('\\') - 1);
            dr["StatusStr"] = "正在初始化";
            dr["Status"] = 0;
            this.mShowTable.Rows.Add(dr);

            Thread t = new Thread(new ParameterizedThreadStart(LoadFile));
            t.Start(i + 1);
        }

        void LoadFile(object iIndex)
        {
            semaphore.WaitOne();
            int index = (int)iIndex;
            string fileName = this.mShowTable.Rows[index - 1]["FullFileName"].ToString();
            DataSet ds = null;
            try
            {
                 ds = SSDC.Common.ReadExcel.ToDataTable(fileName);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Excel里的表名请不要使用中文名称");
                this.dgvDataInfo.Invoke(new System.Action(delegate()
                {
                    this.dgvDataInfo.Rows.RemoveAt(index - 1);
                }));

                return;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                this.mTables.Add(ds.Tables[0]);
            }

            List<string> colList = new List<string>();

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                colList.Add(ds.Tables[0].Columns[i].ColumnName);
            }

            if (this.mColNames == null || this.mColNames.Count == 0)
            {
                this.mColNames = colList;
            }

            this.mColNames = (from t in this.mColNames
                              join t1 in colList on t equals t1
                              select t).ToList();


            ((DataGridViewComboBoxColumn)(dgvDataInfo.Columns["NameCol"])).DataSource = mColNames.ToArray();
            ((DataGridViewComboBoxColumn)(dgvDataInfo.Columns["IdNumberCol"])).DataSource = mColNames.ToArray();


            this.cBoxID.Invoke(new System.Action(delegate()
            {
                this.cBoxID.Items.Clear();
                foreach (var item in this.mColNames)
                {
                    this.cBoxID.Items.Add(item);
                }
            }));

            this.cBoxName.Invoke(new System.Action(delegate()
            {
                this.cBoxName.Items.Clear();
                foreach (var item in this.mColNames)
                {
                    this.cBoxName.Items.Add(item);
                }
            }));

            this.mShowTable.Rows[index - 1]["StatusStr"] = string.Format("等待导入:0/{0}", ds.Tables[0].Rows.Count);
            this.mShowTable.Rows[index - 1]["Status"] = 1;
            semaphore.Release();
        }



        private void ImportDataMulti_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        private void cBoxName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nameCol = this.cBoxName.Text;

            foreach (DataRow row in this.mShowTable.Rows)
            {
                row["NameCol"] = nameCol;
            }
        }

        private void cBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idCol = this.cBoxID.Text;

            foreach (DataRow row in this.mShowTable.Rows)
            {
                row["IdNumberCol"] = idCol;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (this.mImporting == true)
            {
                MessageBox.Show("正在导入数据，请稍后");
                return;
            }
            for (int i = 0; i < this.mShowTable.Rows.Count; i++)
            {
                string fileFullName = this.mShowTable.Rows[i]["FullFileName"].ToString();
                string nameCol = this.mShowTable.Rows[i]["NameCol"].ToString();
                string idCol = this.mShowTable.Rows[i]["IdNumberCol"].ToString();
                int status = Convert.ToInt32(this.mShowTable.Rows[i]["Status"] == null ? 0 : this.mShowTable.Rows[i]["Status"]);

                if (string.IsNullOrEmpty(nameCol))
                {
                    MessageBox.Show(string.Format("文件【{0}】的姓名列尚未选择", fileFullName));
                    this.dgvDataInfo.Rows[i].Selected = true;
                    return;
                }

                if (string.IsNullOrEmpty(idCol))
                {
                    MessageBox.Show(string.Format("文件【{0}】的身份证列尚未选择", fileFullName));
                    this.dgvDataInfo.Rows[i].Selected = true;
                    return;
                }

                if (status == 0)
                {
                    MessageBox.Show(string.Format("文件【{0}】正在初始化，请稍后导入", fileFullName));
                    this.dgvDataInfo.Rows[i].Selected = true;
                    return;
                }
            }

            mTotalCount = 0;
            Thread th = new Thread(ImportDataToDB);
            th.Start();
        }

        void ImportDataToDB()
        {
            if (this.mImporting == false)
            {
                this.mImporting = true;
                DateTime dtStart = DateTime.Now;
                for (int i = 0; i < this.mShowTable.Rows.Count; i++)
                {
                    string fileFullName = this.mShowTable.Rows[i]["FullFileName"].ToString();
                    string nameCol = this.mShowTable.Rows[i]["NameCol"].ToString();
                    string idCol = this.mShowTable.Rows[i]["IdNumberCol"].ToString();
                    int status = Convert.ToInt32(this.mShowTable.Rows[i]["Status"] == null ? 0 : this.mShowTable.Rows[i]["Status"]);



                    if (status == 1)
                    {
                        DataTable dt = this.mTables[i];

                        //新增导入记录
                        SSDC.DAL.ImportRecord.Insert(dt.Rows.Count, DateTime.Now, System.Guid.NewGuid().ToString(), this.mTableNum);

                        this.completedCount = 0;
                        this.ImportRecord(dt, nameCol, idCol, i);

                        this.mTotalCount += dt.Rows.Count;

                        this.mShowTable.Rows[i]["Status"] = 3;
                        this.mShowTable.Rows[i]["StatusStr"] = string.Format("导入完成：{0}/{0}", dt.Rows.Count);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("文件【{0}】已经导入到数据库，请不要重复导入", fileFullName));
                    }

                }

                SSDC.Common.DirectDbHelperMysql.ExecuteSql("call UpdateDataInfo");

                DateTime dtEnd = DateTime.Now;

                TimeSpan ts = dtEnd - dtStart;

                this.label3.Invoke(new System.Action(delegate()
                {
                    this.label3.Text = string.Format("共使用{0}秒，导入{1}条数据", ts.TotalSeconds.ToString(), mTotalCount);
                }));

                this.mImporting = false;
            }
            else
            {
                MessageBox.Show("正在导入数据，请稍后重试");
                return;
            }
        }

        void ImportRecord(DataTable dt, string iNameCol, string iIdNumberCol, int iShowTableIndex)
        {
            StringBuilder sqlStr = new StringBuilder();

            if (dt != null)
            {
                string insertStr = this.GetInsertStr(dt, iNameCol, iIdNumberCol);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    #region 导入记录到数据库
                    sqlStr.Append(this.GetVauleStr(dt.Rows[i], iNameCol, iIdNumberCol));

                    this.completedCount++;
                    if (this.completedCount % importRowCountUpdateUI == 0)
                    {
                        //提交到数据库
                        sqlStr.Append(";");

                        sqlStr.Insert(0, insertStr + " VALUES");
                        SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr.ToString());


                        sqlStr.Clear();
                    }
                    else
                    {
                        sqlStr.Append(",");
                    }

                    if (this.completedCount % importRowCountUpdateUI == 0)
                    {
                        this.mShowTable.Rows[iShowTableIndex]["StatusStr"] = string.Format("正在导入:{0}/{1}", this.completedCount, dt.Rows.Count);
                    }
                    #endregion
                }

                if (sqlStr.Length > 0)
                {
                    if (sqlStr[sqlStr.Length - 1] == ',')
                    {
                        sqlStr.Remove(sqlStr.Length - 1, 1);
                    }
                    sqlStr.Append(";");

                    sqlStr.Insert(0, insertStr + " VALUES");
                    SSDC.Common.DirectDbHelperMysql.ExecuteSql(sqlStr.ToString());

                    sqlStr.Clear();

                    this.mShowTable.Rows[iShowTableIndex]["StatusStr"] = string.Format("正在导入:{0}/{1}", this.completedCount, dt.Rows.Count);

                }
            }
        }

        string GetInsertStr(DataTable iTable, string iNameColName, string iIdNumberColName)
        {
            if (iTable != null && iTable.Columns.Count > 0)
            {
                string sqlStr = "INSERT INTO ssdc.user_data(NAME,IdNumber,TableNum,";
                int index = 0;
                for (int i = 0; i < iTable.Columns.Count; i++)
                {
                    DataColumn item = iTable.Columns[i];
                    if (item.ColumnName != iNameColName && item.ColumnName != iIdNumberColName)
                    {
                        index++;
                        sqlStr += "col" + index.ToString() + ",";
                    }
                }

                sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
                sqlStr += ") ";

                return sqlStr;
            }

            throw new Exception("导入数据没有相关的列信息，请检查excel或联系管理员");
        }

        string GetVauleStr(DataRow iRow, string iNameColName, string iIdNumberColName)
        {
            string sqlStr = string.Empty;

            if (iRow != null)
            {
                string name = iRow[iNameColName] != null ? iRow[iNameColName].ToString() : "";
                string idNumber = iRow[iIdNumberColName] != null ? iRow[iIdNumberColName].ToString() : "";
                sqlStr += string.Format("('{0}','{1}',{2},", this.RemoveDBkey(name), this.RemoveDBkey(idNumber), this.mTableNum);
                for (int i = 0; i < iRow.Table.Columns.Count; i++)
                {
                    DataColumn item = iRow.Table.Columns[i];
                    if (item.ColumnName != iNameColName && item.ColumnName != iIdNumberColName)
                    {
                        string value = iRow[i] != null ? iRow[i].ToString() : "";

                        sqlStr += "'" + this.RemoveDBkey(value) + "',";
                    }
                }

                sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);

                sqlStr += ")";
            }

            return sqlStr;
        }

        string RemoveDBkey(string iStr)
        {
            if (iStr != null)
            {
                iStr = iStr.Replace("'", "");
                iStr = iStr.Replace("\\", "");
                iStr = iStr.Replace("/", "");
            }

            return iStr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.mShowTable.Rows.Clear();
            this.pChildMain.Visible = false;
        }
    }
}
