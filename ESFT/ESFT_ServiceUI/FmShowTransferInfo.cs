using System;
using System.Drawing;
using System.Windows.Forms;

namespace ESFT_ServiceUI
{
    public partial class FmShowTransferInfo : Form
    {
        public FmShowTransferInfo()
        {
            InitializeComponent();
        }

        private void FmShowTransferInfo_Load(object sender, EventArgs e)
        {
            this.timer1.Stop();
            this.timer2.Stop();
        }

        string mServerIP = "";
        int mServerPort = 8000;

        private void button1_Click(object sender, EventArgs e)
        {
            this.GetServerIpAndPort();
            GetBaseInfo(mServerIP, mServerPort);
        }

        void GetServerIpAndPort()
        {
            this.mServerIP = this.txtServerIP.Text.Trim();
            if (int.TryParse(this.txtServerPort.Text.Trim(), out this.mServerPort))
            {
            }
        }

        /// <summary>
        /// 获取可用连接数等信息
        /// </summary>
        void GetBaseInfo(string iServerIP, int iPort)
        {
            ESFT.Server.ServiceGetBaseInfo getBaseInfo = new ESFT.Server.ServiceGetBaseInfo(iServerIP, iPort);

            int iAvailableSocketNum = -1;
            int iUsedNum = -1;
            int iWriteThreadNum = -1;
            if (!getBaseInfo.GetServiceBaseInfo(ref iAvailableSocketNum, ref iUsedNum, ref iWriteThreadNum))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取可用连接数失败！");
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取可用连接数成功!");
            }

            this.lbAvailableSocketNum.Text = Convert.ToString(iAvailableSocketNum + 1);
            this.lbUsedNum.Text = Convert.ToString(iUsedNum - 1);
            this.lbWriteThreadNum.Text = iWriteThreadNum.ToString();
        }

        void GetTransferInfo(string iServerIP, int iPort)
        {
            ESFT.Server.ServiceGetTaskInfo getTaskInfo = new ESFT.Server.ServiceGetTaskInfo(iServerIP, iPort);

            string taskInfo = "";
            if (getTaskInfo.GetServiceTaskInfo(ref taskInfo))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取任务信息成功!");
                AddOutPutText(taskInfo);

                BindGrid(taskInfo);
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取任务信息失败!");
            }
        }

        void GetChildServerInfo(string iServerIP, int iPort)
        {
            ESFT.Server.Service.ServiceGetChildServerInfo childServerInfo = new ESFT.Server.Service.ServiceGetChildServerInfo(iServerIP, iPort);
            string childInfo = "";
            if (childServerInfo.GetChildServerInfo(ref childInfo))
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取子服务器信息成功!");
                AddOutPutText(childInfo);

                BindChildServerInfoGrid(childInfo);
            }
            else
            {
                AddOutPutText("-----------------------------------------------------------------");
                AddOutPutText("获取子服务器信息失败!");
            }
        }

        void AddOutPutText(string iText)
        {
            this.txtOutput.SelectionStart = 0;
            this.txtOutput.SelectionLength = 0;

            this.txtOutput.SelectionColor = Color.Red;
            this.txtOutput.SelectedText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss   :   ");

            this.txtOutput.SelectionColor = Color.Black;
            this.txtOutput.SelectedText = iText + "\r\n";
        }

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
                        SetTransferInfoGridRow(info);
                    }
                }
            }
        }

        void BindChildServerInfoGrid(string iChildInfoStr)
        {
            if (iChildInfoStr != null && iChildInfoStr != string.Empty && iChildInfoStr != "" && iChildInfoStr.Length > 0)
            {
                string[] childInfo = iChildInfoStr.Split(';');

                if (childInfo != null && childInfo.Length > 0)
                {
                    for (int i = 0; i < childInfo.Length; i++)
                    {
                        string[] childServer = childInfo[i].Split(',');
                        SetChildInfoGridRow(childServer);
                    }
                }
            }
        }

        void SetChildInfoGridRow(string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length > 0)
            {
                for (int i = 0; i < this.dataGridView2.Columns.Count; i++)
                {
                    string key = iRowValues[0];
                    int rowIndex = RowExistForChildInfo(key);
                    if (rowIndex != -1)
                    {
                        UpdateRowForChildServerInfo(rowIndex, iRowValues);
                    }
                    else
                    {
                        AddRowForChildServerInfo(iRowValues);
                    }
                }
            }
        }

        int RowExistForChildInfo(string iKey)
        {
            for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
            {
                if (this.dataGridView2.Rows[i].Cells["key2"] != null
                    && this.dataGridView2.Rows[i].Cells["key2"].Value != null
                    && this.dataGridView2.Rows[i].Cells["key2"].Value.ToString() == iKey)
                {
                    return i;
                }
            }
            return -1;
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

        void UpdateRowForTransferInfo(int iRowIndex, string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length == 11)
            {
                this.dataGridView1.Rows[iRowIndex].Cells[1].Value = iRowValues[1];
                this.dataGridView1.Rows[iRowIndex].Cells[2].Value = iRowValues[2];
                this.dataGridView1.Rows[iRowIndex].Cells[3].Value = iRowValues[3];
                this.dataGridView1.Rows[iRowIndex].Cells[4].Value = iRowValues[4];
                this.dataGridView1.Rows[iRowIndex].Cells[5].Value = iRowValues[5];
                this.dataGridView1.Rows[iRowIndex].Cells[6].Value = iRowValues[6];
                this.dataGridView1.Rows[iRowIndex].Cells[7].Value = iRowValues[7];
                this.dataGridView1.Rows[iRowIndex].Cells[8].Value = iRowValues[8];
                this.dataGridView1.Rows[iRowIndex].Cells[9].Value = iRowValues[9];
                this.dataGridView1.Rows[iRowIndex].Cells[10].Value = iRowValues[10];
            }
        }

        void UpdateRowForChildServerInfo(int iRowIndex, string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length >= 5)
            {
                this.dataGridView2.Rows[iRowIndex].Cells[1].Value = iRowValues[1];
                this.dataGridView2.Rows[iRowIndex].Cells[2].Value = iRowValues[2];
                this.dataGridView2.Rows[iRowIndex].Cells[3].Value = iRowValues[3];
                this.dataGridView2.Rows[iRowIndex].Cells[4].Value = iRowValues[4];
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
                this.dataGridView1.Rows[iRowIndex].Cells[3].Value = iRowValues[3];
                this.dataGridView1.Rows[iRowIndex].Cells[4].Value = iRowValues[4];
                this.dataGridView1.Rows[iRowIndex].Cells[5].Value = iRowValues[5];
                this.dataGridView1.Rows[iRowIndex].Cells[6].Value = iRowValues[6];
                this.dataGridView1.Rows[iRowIndex].Cells[7].Value = iRowValues[7];
                this.dataGridView1.Rows[iRowIndex].Cells[8].Value = iRowValues[8];
                this.dataGridView1.Rows[iRowIndex].Cells[9].Value = iRowValues[9];
                this.dataGridView1.Rows[iRowIndex].Cells[10].Value = iRowValues[10];
            }
        }

        void AddRowForChildServerInfo(string[] iRowValues)
        {
            if (iRowValues != null && iRowValues.Length >= 5)
            {
                this.dataGridView2.Rows.Add();

                int iRowIndex = this.dataGridView2.Rows.Count - 1;

                this.dataGridView2.Rows[iRowIndex].Cells[0].Value = iRowValues[0];
                this.dataGridView2.Rows[iRowIndex].Cells[1].Value = iRowValues[1];
                this.dataGridView2.Rows[iRowIndex].Cells[2].Value = iRowValues[2];
                this.dataGridView2.Rows[iRowIndex].Cells[3].Value = iRowValues[3];
                this.dataGridView2.Rows[iRowIndex].Cells[4].Value = iRowValues[4];
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.GetServerIpAndPort();
            this.timer1.Stop();
            this.GetBaseInfo(this.mServerIP, this.mServerPort);
            if (this.checkBox1.Checked == true)
            {
                this.timer1.Start();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.timer1.Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.GetServerIpAndPort();
            GetTransferInfo(this.mServerIP, this.mServerPort);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            this.GetServerIpAndPort();
            GetChildServerInfo(this.mServerIP, this.mServerPort);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Stop();
            this.GetServerIpAndPort();
            GetTransferInfo(this.mServerIP, this.mServerPort);
            if (this.checkBox2.Checked == true)
            {
                this.timer2.Start();
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox2.Checked == true)
            {
                this.timer2.Start();
            }
        }

        private void pMiddle_MouseMove(object sender, MouseEventArgs e)
        {

        }

    }
}
