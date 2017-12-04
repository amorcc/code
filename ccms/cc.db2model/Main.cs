using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using cc.utility.Data;

namespace cc.db2model
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.BindDBConnStrs();
            this.ReadConfig();
        }

        List<string> mTableNameList;
        CreateConfig mConfig;

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


        /// <summary>
        /// 查询指定数据库的所有表名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueryTableFromDB_Click(object sender, EventArgs e)
        {
            this.mTableNameList = QueryDB.GetTableNameListFromDB(this.cboxDBConnStrs.Text);
            this.TableNameFilter();
        }

        /// <summary>
        /// 表名过滤
        /// </summary>
        void TableNameFilter()
        {
            this.checkListTableName.Items.Clear();
            if (this.mTableNameList != null)
            {
                foreach (var tn in this.mTableNameList)
                {
                    if (tn.ToLower().IndexOf(this.txtTableNameFilter.Text.Trim().ToLower()) >= 0)
                    {
                        this.checkListTableName.Items.Add(tn);
                    }
                }
            }
        }

        /// <summary>
        /// 表名过滤按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTableNameFilter_Click(object sender, EventArgs e)
        {
            this.TableNameFilter();
        }

        /// <summary>
        /// 表名全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboxTableSelectedAll_CheckedChanged(object sender, EventArgs e)
        {
            bool selected = this.cboxTableSelectedAll.Checked;
            for (int i = 0; i < this.checkListTableName.Items.Count; i++)
            {
                this.checkListTableName.SetItemChecked(i, selected);
            }
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        void ReadConfig()
        {
            this.mConfig = new CreateConfig();
            this.txtModelSavePath.Text = this.mConfig.ModelFileSavePath;
            this.txtDalSavePath.Text = this.mConfig.DalFileSavePath;
            this.txtModelBaseClassName.Text = this.mConfig.ModelBaseClassName;
            this.txtModelNamespace.Text = this.mConfig.ModelNamespace;
            this.txtModelUsing.Text = this.mConfig.ModelUsing;
            this.txtDalBaseClassName.Text = this.mConfig.DalBaseClassName;
            this.txtDalNamespace.Text = this.mConfig.DalNamespace;
            this.txtDalUsing.Text = this.mConfig.DalUsing;
        }

        /// <summary>
        /// 手动选择Model保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectModelSavePath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            dialog.SelectedPath = this.txtModelSavePath.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                this.txtModelSavePath.Text = dialog.SelectedPath;
            }
        }

        private void btnSelectDalSavePath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            dialog.SelectedPath = this.txtDalSavePath.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                this.txtDalSavePath.Text = dialog.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.mConfig.ModelFileSavePath = this.txtModelSavePath.Text;
            this.mConfig.DalFileSavePath = this.txtDalSavePath.Text;
            this.mConfig.ModelBaseClassName = this.txtModelBaseClassName.Text;
            this.mConfig.ModelNamespace = this.txtModelNamespace.Text;
            this.mConfig.ModelUsing = this.txtModelUsing.Text;
            this.mConfig.DalBaseClassName = this.txtDalBaseClassName.Text;
            this.mConfig.DalNamespace = this.txtDalNamespace.Text;
            this.mConfig.DalUsing = this.txtDalUsing.Text;

            List<string> createTables = new List<string>();

            for (int i = 0; i < this.checkListTableName.Items.Count; i++)
            {
                if (this.checkListTableName.GetItemChecked(i))
                {
                    createTables.Add(this.checkListTableName.GetItemText(this.checkListTableName.Items[i]));
                }
            }

            //createTables.Add("Account");
            this.richTextBox1.Focus();
            FileCreate fc = new FileCreate(createTables, this.mConfig, this.richTextBox1, this.cboxDBConnStrs.Text);
            fc.Start();
        }


    }
}
