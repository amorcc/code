using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using cc.utility.Data;

namespace cc.model2db
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        List<string> mTableNameList;

        public void Init()
        {
            this.BindDBConnStrs();
            this.txtModelDLLFile.Text = cc.utility.Common.App("modelDllFile");
        }

        /// <summary>
        /// 读取所有数据库连接字符串
        /// </summary>
        public void BindDBConnStrs()
        {
            this.cboxDBConnStrs.Items.Clear();

            List<string> connStrNames = QueryDB.GetDBConnStr();

            if (connStrNames != null)
            {
                foreach (var item in connStrNames)
                {
                    this.cboxDBConnStrs.Items.Add(item);
                }

                this.cboxDBConnStrs.SelectedIndex = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //读取现有数据库的所有表名
            this.mTableNameList = QueryDB.GetTableNameListFromDB(this.cboxDBConnStrs.Text);
            if (!File.Exists(this.txtModelDLLFile.Text))
            {
                MessageBox.Show("请正确选择Model的DLL文件");
            }
            ReadDLL(this.txtModelDLLFile.Text);
        }

        void ReadDLL(string iDllFileName)
        {
            Assembly asm = Assembly.LoadFile(@"E:\cc_space\code\ccms\cc.model\bin\Debug\cc.model.dll");

            foreach (Type t in asm.GetTypes())
            {
                if (t.BaseType == typeof(basemodel.BaseModel))
                {
                    string tbname = (t.GetCustomAttributes(typeof(cc.utility.Data.TableAttribute), false)[0] as cc.utility.Data.TableAttribute).TableName;

                    AppendRichTextBox.Append(this.richTextBox1, string.Format("\n-----------------------------"));
                    AppendRichTextBox.Append(this.richTextBox1, string.Format("开始实体类{0}的处理，表名为{1}", t.ToString(), tbname));

                    //表是否已经在数据库中
                    bool dbExist = (from tt in this.mTableNameList
                                    where tt == tbname
                                    select tt).Count() > 0 ? true : false;

                    if (!string.IsNullOrEmpty(tbname))
                    {
                        List<ColumnData> cols = new List<ColumnData>();

                        //数据库的Model类
                        foreach (var p in t.GetProperties())
                        {
                            ColumnAttribute colAttr = (p.GetCustomAttribute(typeof(ColumnAttribute), false) as ColumnAttribute);
                            string desc = "";
                            bool isPrimary = false;
                            bool isIdentity = false;
                            bool isNotNull = false;
                            int varcharLength = 50;
                            string colName = p.Name;
                            string colDataType = p.PropertyType.ToString();
                            object[] objs = p.GetCustomAttributes(typeof(DescriptionAttribute), false);
                            desc = (objs != null && objs.Length > 0) ? ((DescriptionAttribute)objs[0]).Description : "";

                            if (colAttr != null)
                            {
                                isPrimary = colAttr.IsPrimary;
                                isIdentity = colAttr.IsIdentity;
                                isNotNull = colAttr.IsNotNull;
                                varcharLength = colAttr.VarcharLength;
                            }


                            cols.Add(new ColumnData(colName, isPrimary, isIdentity, isNotNull, colDataType, varcharLength, desc));
                        }

                        if (cols.Count > 0)
                        {
                            //查数据库中的字段
                            List<DBTableColumn> tableColNames = new List<DBTableColumn>();
                            if (dbExist)
                            {
                                tableColNames = QueryDB.GetTableColumnInfo(this.cboxDBConnStrs.Text, tbname);
                            }

                            //写到数据库中
                            string sql = SqlCreate.GetSql(tbname, cols, dbExist, tableColNames);

                            AppendRichTextBox.Append(this.richTextBox1, string.Format("获取实体类{0}的SQL语句，表名为{1}.SQL为：", t.ToString(), tbname));
                            AppendRichTextBox.AppendSQL(this.richTextBox1, string.Format("{0}", sql));

                            string connStr = cc.utility.Common.ConnectionStrings(this.cboxDBConnStrs.Text);
                            SqlHelper.ExecuteNonQuery(connStr, CommandType.Text, sql);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 选择model类的dll文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectModelDLLFile_Click(object sender, EventArgs e)
        {

        }


    }
}
