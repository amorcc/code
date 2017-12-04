using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ESFT_ServiceUI
{
    public partial class FmMain : Form
    {
        public FmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取所有服务器信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string serverIP = this.txtServerIP.Text.Trim();
            int port = 8000;
            if (!int.TryParse(this.txtServerPort.Text.Trim(), out port))
            {
                MessageBox.Show("服务器IP或者PORT填写错误！");
            }

            this.ClearTreeView();

            this.GetChildServerInfo(serverIP, port);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int port = 0;
            if (int.TryParse(this.lbSelecetServerPort.Text, out port))
            {
                if (!this.GetServerTransferInfo(this.lbSelecetServerName.Text, this.lbSelecetServerIP.Text, port))
                {
                    MessageBox.Show("获取该服务器信息失败");
                }
            }
            else
            {
                MessageBox.Show("端口号错误!");
            }
        }

        /// <summary>
        /// 双击选择IP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode.Text == this.treeView1.Nodes[0].Text)
            {
                // 选中第一层节点
                MessageBox.Show("请双击第三层树节点的IP地址!");
                return;
            }

            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (this.treeView1.SelectedNode.Text == this.treeView1.Nodes[0].Nodes[i].Text)
                {
                    // 选中第二层的节点
                    MessageBox.Show("请双击第三层树节点的IP地址!");
                    return;
                }
            }

            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
            {
                for (int j = 0; j < this.treeView1.Nodes[0].Nodes[i].Nodes.Count; j++)
                {
                    TreeNode n = this.treeView1.Nodes[0].Nodes[i].Nodes[j];
                    if (this.treeView1.SelectedNode.Text == n.Text)
                    {
                        TreeNode secondNode = this.treeView1.Nodes[0].Nodes[i];

                        if (secondNode.Nodes.Count == 6
                            && secondNode.Nodes[1] != null)
                        {
                            string serverName = secondNode.Nodes[0].Text;
                            string portStr = secondNode.Nodes[2].Text;
                            string ipStr = n.Text;
                            int port = 0;
                            if (ipStr != null
                                && ipStr != ""
                                && int.TryParse(portStr, out port)
                                && (this.treeView1.SelectedNode.Index == 1 || this.treeView1.SelectedNode.Index == 3 || this.treeView1.SelectedNode.Index == 4 || this.treeView1.SelectedNode.Index == 5)
                                )
                            {
                                if (!this.GetServerTransferInfo(serverName, ipStr, port))
                                {
                                    MessageBox.Show("获取该服务器信息失败");
                                }
                            }
                            else
                            {
                                MessageBox.Show("请双击第三层正确的IP地址！");
                            }
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <param name="iServerIP"></param>
        /// <param name="iPort"></param>
        void GetChildServerInfo(string iServerIP, int iPort)
        {
            ESFT.Server.Service.ServiceGetChildServerInfo childServerInfo = new ESFT.Server.Service.ServiceGetChildServerInfo(iServerIP, iPort);
            string childInfo = "";
            if (childServerInfo.GetChildServerInfo(ref childInfo))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取子服务器信息成功!");
                AddOutPutText(childInfo);

                //BindChildServerInfoGrid(childInfo);
                BindServerInfoToTree(childInfo);
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取子服务器信息失败!");
            }
        }

        /// <summary>
        /// 获取服务器的传输信息
        /// </summary>
        /// <param name="iServerName"></param>
        /// <param name="iServerIp"></param>
        /// <param name="iPort"></param>
        bool GetServerTransferInfo(string iServerName, string iServerIp, int iPort)
        {
            this.lbSelecetServerName.Text = iServerName;
            this.lbSelecetServerIP.Text = iServerIp;
            this.lbSelecetServerPort.Text = iPort.ToString();

            this.dataGridView1.Rows.Clear();

            if (this.GetBaseInfo(iServerIp, iPort) && this.GetTransferInfo(iServerIp, iPort))
            {
                int finishCount = 0;

                if (this.dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        if (this.dataGridView1.Rows[i].Cells["stateStr"] != null
                                && this.dataGridView1.Rows[i].Cells["stateStr"].Value.ToString() == "传输完成")
                        {
                            finishCount++;
                            if (this.checkBox1.Checked == true)
                            {
                                this.dataGridView1.Rows[i].Visible = false;
                            }
                            else
                            {
                                this.dataGridView1.Rows[i].Visible = true;
                            }
                        }
                    }

                    this.lbTaskCount.Text = this.dataGridView1.Rows.Count.ToString();
                    this.lbFinishCount.Text = finishCount.ToString();
                    this.lbUnfinishCount.Text = Convert.ToString(this.dataGridView1.Rows.Count - finishCount);
                }
                else
                {
                    this.lbTaskCount.Text = "0";
                    this.lbFinishCount.Text = "0";
                    this.lbUnfinishCount.Text = "0";
                }

                return true;
            }
            else
            {
                this.lbTaskCount.Text = this.dataGridView1.Rows.Count.ToString();
                this.lbAvailableSocketNum.Text = "-1";
                this.lbUsedNum.Text = "-1";
                this.lbWriteThreadNum.Text = "-1";
                return false;
            }

        }

        /// <summary>
        /// 获取可用连接数等信息
        /// </summary>
        bool GetBaseInfo(string iServerIP, int iPort)
        {
            ESFT.Server.ServiceGetBaseInfo getBaseInfo = new ESFT.Server.ServiceGetBaseInfo(iServerIP, iPort);

            int iAvailableSocketNum = -1;
            int iUsedNum = -1;
            int iWriteThreadNum = -1;
            if (!getBaseInfo.GetServiceBaseInfo(ref iAvailableSocketNum, ref iUsedNum, ref iWriteThreadNum))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取可用连接数失败！");

                return false;
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取可用连接数成功!");

                this.lbAvailableSocketNum.Text = Convert.ToString(iAvailableSocketNum + 1);
                this.lbUsedNum.Text = Convert.ToString(iUsedNum - 1);
                this.lbWriteThreadNum.Text = iWriteThreadNum.ToString();

                return true;
            }

        }

        /// <summary>
        /// 获取传输信息
        /// </summary>
        /// <param name="iServerIP"></param>
        /// <param name="iPort"></param>
        /// <returns></returns>
        bool GetTransferInfo(string iServerIP, int iPort)
        {
            ESFT.Server.ServiceGetTaskInfo getTaskInfo = new ESFT.Server.ServiceGetTaskInfo(iServerIP, iPort);

            string taskInfo = "";
            if (getTaskInfo.GetServiceTaskInfo(ref taskInfo))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取任务信息成功!");
                AddOutPutText(taskInfo);

                BindGrid(taskInfo);
                return true;
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取任务信息失败!");

                return false;
            }
        }

        void ClearTreeView()
        {
            if (this.treeView1.Nodes[0].Nodes != null)
            {
                for (int i = this.treeView1.Nodes[0].Nodes.Count - 1; i >= 0; i--)
                {
                    this.treeView1.Nodes[0].Nodes[i].Remove();
                }
            }

            this.lbAvailableSocketNum.Text = "-1";
            this.lbUsedNum.Text = "-1";
            this.lbWriteThreadNum.Text = "-1";
            this.lbSelecetServerIP.Text = "无";
            this.lbSelecetServerName.Text = "无";
            this.lbSelecetServerPort.Text = "无";

            this.dataGridView1.Rows.Clear();
        }

        #region 绑定GridView
        void BindGrid(string iTaskInfo)
        {
            //1、key
            //2、传输类型 transferType
            //3、服务器路径 serverPath
            //4、文件大小 fileLenght
            //5、已完成 currentLenght
            //6、接受长度 recevieOrSendLenght
            //7、传输状态 stateStr
            //8、传输时间 transferTimeSecond
            //9、速度 speed
            //10、最后包时间 lastPacketTime
            //11、开始时间 beginTime
            if (iTaskInfo != null && iTaskInfo != string.Empty && iTaskInfo != "" && iTaskInfo.Length > 0)
            {
                string[] task = iTaskInfo.Split(';');
                if (task != null && task.Length > 0)
                {
                    string[,] tasks = new string[task.Length, 11];
                    for (int i = 0; i < task.Length; i++)
                    {
                        string[] info = task[i].Split(',');
                        log4net.LogManager.GetLogger(typeof(FmMain)).InfoFormat("{0}/{1}", i, task.Count());
                        SetTransferInfoGridRow(info);
                    }
                }
            }
        }

        void SetTransferInfoGridRow(string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length > 0)
            {
                for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
                {
                    string key = iRowValues[0];
                    int rowIndex = RowExistForTransferInfo(key);
                    if (rowIndex != -1)
                    {
                        UpdateRowForTransferInfo(rowIndex, iRowValues);
                    }
                    else
                    {
                        AddRowForTransferInfo(iRowValues);
                    }
                }
            }
        }

        int RowExistForTransferInfo(string iKey)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1.Rows[i].Cells["key"] != null
                    && this.dataGridView1.Rows[i].Cells["key"].Value != null
                    && this.dataGridView1.Rows[i].Cells["key"].Value.ToString() == iKey)
                {
                    return i;
                }
            }
            return -1;
        }

        void UpdateRowForTransferInfo(int iRowIndex, string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length == 11)
            {
                this.dataGridView1.Rows[iRowIndex].Cells[1].Value = iRowValues[1];
                this.dataGridView1.Rows[iRowIndex].Cells[2].Value = iRowValues[2];
                this.dataGridView1.Rows[iRowIndex].Cells[3].Value = FormatFileLenght(iRowValues[3]);
                this.dataGridView1.Rows[iRowIndex].Cells[4].Value = FormatFileLenght(iRowValues[4]);
                this.dataGridView1.Rows[iRowIndex].Cells[5].Value = FormatFileLenght(iRowValues[5]);
                this.dataGridView1.Rows[iRowIndex].Cells[6].Value = iRowValues[6];
                this.dataGridView1.Rows[iRowIndex].Cells[7].Value = iRowValues[7];
                this.dataGridView1.Rows[iRowIndex].Cells[8].Value = iRowValues[8];
                this.dataGridView1.Rows[iRowIndex].Cells[9].Value = iRowValues[9];
                this.dataGridView1.Rows[iRowIndex].Cells[10].Value = iRowValues[10];
            }
        }

        void AddRowForTransferInfo(string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length == 11)
            {
                this.dataGridView1.Rows.Add();

                int iRowIndex = this.dataGridView1.Rows.Count - 1;

                this.dataGridView1.Rows[iRowIndex].Cells[0].Value = iRowValues[0];
                this.dataGridView1.Rows[iRowIndex].Cells[1].Value = iRowValues[1];
                this.dataGridView1.Rows[iRowIndex].Cells[2].Value = iRowValues[2];
                this.dataGridView1.Rows[iRowIndex].Cells[3].Value = FormatFileLenght(iRowValues[3]);
                this.dataGridView1.Rows[iRowIndex].Cells[4].Value = FormatFileLenght(iRowValues[4]);
                this.dataGridView1.Rows[iRowIndex].Cells[5].Value = FormatFileLenght(iRowValues[5]);
                this.dataGridView1.Rows[iRowIndex].Cells[6].Value = iRowValues[6];
                this.dataGridView1.Rows[iRowIndex].Cells[7].Value = iRowValues[7];
                this.dataGridView1.Rows[iRowIndex].Cells[8].Value = iRowValues[8];
                this.dataGridView1.Rows[iRowIndex].Cells[9].Value = iRowValues[9];
                this.dataGridView1.Rows[iRowIndex].Cells[10].Value = iRowValues[10];
            }
        }
        #endregion

        #region 获取服务器信息并绑定到TreeView
        /// <summary>
        /// 绑定服务器信息到树
        /// </summary>
        /// <param name="iServerInfo"></param>
        void BindServerInfoToTree(string iServerInfoStr)
        {
            if (iServerInfoStr != null && iServerInfoStr != string.Empty && iServerInfoStr != "" && iServerInfoStr.Length > 0)
            {
                string[] childInfo = iServerInfoStr.Split(';');

                if (childInfo != null && childInfo.Length > 0)
                {
                    for (int i = 0; i < childInfo.Length; i++)
                    {
                        string[] childServer = childInfo[i].Split(',');
                        SetServerInfoToTree(childServer);
                    }
                }
            }

            this.treeView1.Nodes[0].ExpandAll();
        }

        /// <summary>
        /// 设置服务器信息到树
        /// </summary>
        /// <param name="iRowValues"></param>
        void SetServerInfoToTree(string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length > 0)
            {
                string key = iRowValues[0];

                TreeNode oldNode = ExistServerInfo(key);
                if (oldNode == null)
                {
                    // 树中不存在该服务器信息，添加
                    AddServerInfoToTree(iRowValues);
                }
                else
                {
                    UpdateServerInfoToTree(oldNode, iRowValues);
                }
            }
        }

        /// <summary>
        /// 更新服务器信息
        /// </summary>
        /// <param name="iKeyNode"></param>
        /// <param name="iRowValues"></param>
        private void UpdateServerInfoToTree(TreeNode iKeyNode, string[] iRowValues)
        {
            if (iKeyNode.Nodes.Count == 4)
            {
                iKeyNode.Nodes[0].Text = iRowValues[1];
                iKeyNode.Nodes[1].Text = iRowValues[2];
                iKeyNode.Nodes[2].Text = iRowValues[3];
                iKeyNode.Nodes[3].Text = iRowValues[4];
                iKeyNode.Nodes[4].Text = iRowValues[5];
                iKeyNode.Nodes[5].Text = iRowValues[6];
            }
        }

        /// <summary>
        /// 新增一个服务器到树
        /// </summary>
        /// <param name="iRowValues"></param>
        private void AddServerInfoToTree(string[] iRowValues)
        {
            if (iRowValues.Length >= 5)
            {
                TreeNode keyNode = new TreeNode(iRowValues[0]);

                TreeNode nameNode = new TreeNode(iRowValues[1]);
                TreeNode portNode = new TreeNode(iRowValues[2]);
                TreeNode ipNode = new TreeNode(iRowValues[3]);
                TreeNode ip1Node = new TreeNode(iRowValues[4]);
                TreeNode ip2Node = new TreeNode(iRowValues[5]);
                TreeNode ip3Node = new TreeNode(iRowValues[6]);

                keyNode.Nodes.Add(nameNode);
                keyNode.Nodes.Add(portNode);
                keyNode.Nodes.Add(ipNode);
                keyNode.Nodes.Add(ip1Node);
                keyNode.Nodes.Add(ip2Node);
                keyNode.Nodes.Add(ip3Node);

                this.treeView1.Nodes[0].Nodes.Add(keyNode);
            }
        }

        /// <summary>
        /// 树中是否已经存在该服务器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TreeNode ExistServerInfo(string key)
        {
            TreeNode node = null;

            //if(this.treeView1.Nodes[0].
            for (int i = 0; i < this.treeView1.Nodes[0].Nodes.Count; i++)
            {
                if (this.treeView1.Nodes[0].Nodes[i].Text == key)
                {
                    node = this.treeView1.Nodes[0].Nodes[i];
                    break;
                }
            }

            return node;
        }
        #endregion

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="iText"></param>
        void AddOutPutText(string iText)
        {
            this.txtOutput.SelectionStart = 0;
            this.txtOutput.SelectionLength = 0;

            this.txtOutput.SelectionColor = Color.Red;
            this.txtOutput.SelectedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss   :   ");

            this.txtOutput.SelectionColor = Color.Black;
            this.txtOutput.SelectedText = iText + "\r\n";
        }


        private void treeView1_Click(object sender, EventArgs e)
        {

        }

        string FormatFileLenght(string iLenght)
        {
            double lenght = Convert.ToDouble(iLenght) / 1024;   //K
            if (lenght > 1024)
            {
                lenght = lenght / 1024;
                return String.Format("{0:F}M", lenght);//lenght.ToString("{0:F}") + "M";
            }
            else
            {
                return String.Format("{0:F}K", lenght); ;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.GetServerTransferInfo(this.lbSelecetServerName.Text, this.lbSelecetServerIP.Text, int.Parse(this.lbSelecetServerPort.Text));
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox2.Checked;
        }
    }
}
