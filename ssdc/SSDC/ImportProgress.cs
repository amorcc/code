using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SSDC
{
    public partial class ImportProgress : Form
    {
        public ImportProgress()
        {
            InitializeComponent();
        }

        private void ImportProgress_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 城镇居民医保datatable
        /// </summary>
        public DataTable mTable = null;


        public string mColName = string.Empty;

        public string mColId = string.Empty;

        public int mDatainfoType = 0;

        /// <summary>
        /// 数据总条数
        /// </summary>
        int rowCount = 0;

        /// <summary>
        /// 已完成条数
        /// </summary>
        int completedCount = 0;

        /// <summary>
        /// 是否导入中
        /// </summary>
        bool importing = false;

        /// <summary>
        /// 开始导入时间
        /// </summary>
        DateTime beginImportTime = DateTime.Now;

        /// <summary>
        /// 本次导入批次唯一标识
        /// </summary>
        string thisImportBatch = System.Guid.NewGuid().ToString();

        /// <summary>
        /// 1-城镇居民医保,2-城镇职工医保,3-新农合医保
        /// </summary>
        public int mImportType = 0;

        /// <summary>
        /// 导入多少条数据开始更新界面
        /// </summary>
        int importRowCountUpdateUI = 3000;

        public void InitCount()
        {
            #region 获取数据
            rowCount += this.mTable.Rows.Count;
            #endregion

            this.label1.Text = "0/" + rowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.button1.Text == "导入完成")
            {
                this.Close();
            }

            if (importing == false)
            {
                importing = true;
                this.button1.Text = "停止导入";
                Thread t = new Thread(StartImportToDB);
                t.Start();
            }
            else
            {
                this.importing = false;
                this.button1.Text = "开始导入";
            }

        }

        int count = 0;

        public void StartImportToDB()
        {
            DateTime dtStart = DateTime.Now;
            if (this.completedCount == 0)
            {
                //刚开始导入
                this.beginImportTime = DateTime.Now;

                //新增导入记录
                SSDC.DAL.ImportRecord.Insert(this.rowCount, this.beginImportTime, this.thisImportBatch, this.mImportType);

                //更新UI
                this.UpdateUI();
            }

            //导入城镇居民医保
            this.Import(this.mDatainfoType, this.mTable, this.mColName, this.mColId);

            SSDC.DAL.datainfo.SetDataInfoColumnsNum(this.mImportType);

            DateTime dtEnd = DateTime.Now;
            this.CompletedImport();

            TimeSpan ts = dtEnd - dtStart;

            this.label3.Invoke(new System.Action(delegate()
            {
                this.label3.Text = ts.TotalSeconds.ToString();
            }));
        }

        /// <summary>
        /// 导入表数据
        /// </summary>
        /// <param name="iType">1-城镇居民医保,2-城镇职工医保,3-新农合医保</param>
        /// <param name="iDT"></param>
        /// <param name="iNameCol"></param>
        /// <param name="iIdNumberCol"></param>
        public void Import(int iType, DataTable iDT, string iNameCol, string iIdNumberCol)
        {
            this.mImportType = iType;
            string importTitle = string.Empty;

            StringBuilder sqlStr = new StringBuilder();

            if (iDT != null && iDT.Rows.Count > 0)
            {


                #region 开始插入数据
                //生成insert语句前半部分
                string tableName = "";

                if (mDatainfoType >= 1 && mDatainfoType <= 8)
                {
                    tableName = "table" + mDatainfoType;
                }
                else
                {
                    throw new Exception("生成insert语句的类型不明确");
                }

                string insertStr = this.GetInsertStr(iDT, tableName, iNameCol, iIdNumberCol);

                for (int i = 0; i < iDT.Rows.Count; i++)
                {
                    if (this.importing == true)
                    {

                        string valueStr = this.GetVauleStr(iDT.Rows[i], iNameCol, iIdNumberCol);
                        sqlStr.Append(valueStr);

                        this.completedCount++;
                        if (this.completedCount % 3000 == 0)
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
                            this.UpdateUI();
                        }
                    }
                    else
                    {
                        this.CancelImport();
                    }

                #endregion

                }
            }
        }

        string GetInsertStr(DataTable iTable, string iTableName, string iNameColName, string iIdNumberColName)
        {
            if (iTable != null && iTable.Columns.Count > 0)
            {
                string sqlStr = "INSERT INTO ssdc." + iTableName + "(NAME,IdNumber,";
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
                sqlStr += string.Format("('{0}','{1}',", this.RemoveDBkey(name), this.RemoveDBkey(idNumber));
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
                iStr.Replace("'", "''");
                iStr.Replace("\\", "\\\\");
                iStr.Replace("/", "");
            }

            return iStr;
        }

        /// <summary>
        /// 取消导入
        /// </summary>
        public void CancelImport()
        {

        }

        /// <summary>
        /// 导入完成
        /// </summary>
        public void CompletedImport()
        {
            this.button1.Invoke(new System.Action(delegate()
            {
                this.button1.Text = "导入完成";
            }));
            MessageBox.Show("导入完成");
        }

        public void UpdateUI()
        {
            //Thread.Sleep(100);

            int progress = Convert.ToInt32(Convert.ToDouble(this.completedCount) / Convert.ToDouble(this.rowCount) * 100);
            this.progressBar1.Invoke(new System.Action(delegate()
            {
                this.progressBar1.Value = progress;
            }));

            string importTitle = string.Empty;

            importTitle = "正在导入数据……";


            this.label2.Invoke(new System.Action(delegate()
            {
                this.label2.Text = importTitle;
            }));

            this.label1.Invoke(new System.Action(delegate()
            {
                this.label1.Text = this.completedCount.ToString() + "/" + this.rowCount.ToString();
            }));
        }

        private void ImportProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.importing = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
