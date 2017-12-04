using ESFT.Common.SystemInfo;
using ESFT.Server;
using ESFT.Server.FileManage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFT.Server
{
    public partial class FormServer : Form
    {
        public FormServer()
        {
            InitializeComponent();
        }

        ServerListener server;
        int numConnections;
        int receiveBufferSize;

        private void FormServer_Load(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            numConnections = SystemInfo.m_ServerNumConnections;
            receiveBufferSize = SystemInfo.m_ReceiveBufferSize;
            this.lbNumConnections.Text = numConnections.ToString();
            if (this.button1.Text == "开始监听")
            {
                if (this.StartSocket())
                {
                    this.button1.Text = "停止监听";
                }
                else if (sender == null)
                {
                    this.Close();
                }
                else
                {
                    Text = "监听开始失败";
                }
            }
            else
            {
                this.button1.Text = "开始监听";
                server.Dispose();
                server = null;
                GC.Collect();
            }
        }

        bool StartSocket()
        {
            try
            {
                int port = SystemInfo.m_LocalPort;
                if (port <= 1000)
                {
                    port = 8000;
                }
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
                if (server == null)
                {
                    server = new ServerListener(numConnections, receiveBufferSize);
                    server.Init();
                    server.EventAddTransferTask += server_EventAddTransferTask;
                    server.EventTransferTaskProgressChange += server_EventTransferTaskProgressChange;
                    //server.EventReceiveCommandPacket += server_EventReceiveCommandPacket;
                    server.EventReceiveCommandPacket += server_EventReceiveCommandPacket;
                }
                server.Start(iep);
                return true;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(FormServer)).Error(ex.Message, ex);
                return false;
            }
        }

        void server_EventReceiveCommandPacket(object iAsyncUserToken, Message.MsgCommand iMsg)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// 传输任务进度发生改变
        /// </summary>
        /// <param name="iTask"></param>
        void server_EventTransferTaskProgressChange(object iTask)
        {
            if (!this.IsDisposed && this.Created)
            {
                try
                {
                    this.Invoke(new FormServer.InvokeAddRow(SetRowInfo), (FileTransferBase)iTask);
                }
                catch (Exception ex)
                {
                    log4net.LogManager.GetLogger(typeof(FormServer)).Error(ex.Message, ex);
                }
            }
        }

        /// <summary>
        /// 新增了一个传输任务
        /// </summary>
        /// <param name="iTask"></param>
        void server_EventAddTransferTask(object iTask)
        {
            if (!this.IsDisposed && this.Created)
            {
                try
                {
                    this.Invoke(new FormServer.InvokeAddRow(AddRow), (FileTransferBase)iTask);
                }
                catch (Exception ex)
                {
                    log4net.LogManager.GetLogger(typeof(FormServer)).Error(ex.Message, ex);
                }
            }
        }

        public delegate void InvokeAddRow(FileTransferBase iTask);

        private void AddRow(FileTransferBase iTask)
        {
            this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["key"].Value = iTask.Key;
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["IP"].Value = iTask.mUserToken.mClientEndPoint.ToString();
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["FileLenght"].Value = iTask.FileLenght;
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["ServerPath"].Value = iTask.ServerFileFullName;

            //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["CurrentCompleteLenght"].Value = ESFT.CommonFunciton.FormatFileLenghtToStr(iTask.CompletedFileLenght);
            //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["TransferState"].Value = ESFT.CommonFunciton.GetTransferState(iTask.State);
            //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["WriteLenght"].Value = "";
            //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["LastPacketTime"].Value = iTask.LastPacketTime.ToString("MM-dd HH:mm:ss");
            //this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["ServerPath"].Value = iTask.LocalPath;
        }

        private void SetRowInfo(FileTransferBase iTask)
        {
            this.RefrshStatus();
            if (this.dataGridView1.Rows != null)
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells["key"].Value.ToString() == iTask.Key)
                    {
                        this.dataGridView1.Rows[i].Cells["ClientFileFullName"].Value = iTask.ClientFileFullName;
                        this.dataGridView1.Rows[i].Cells["ServerPath"].Value = iTask.ServerFileFullName;
                        this.dataGridView1.Rows[i].Cells["FileLenght"].Value = iTask.FileLenght.ToString("###,###,###,###");
                        this.dataGridView1.Rows[i].Cells["CurrentCompleteLenght"].Value = iTask.CurrentLenght.ToString("###,###,###,###");
                        this.dataGridView1.Rows[i].Cells["RecevieOrSendLenght"].Value = iTask.CurrentReceviceOrSendLenght.ToString("###,###,###,###");
                        this.dataGridView1.Rows[i].Cells["TransferState"].Value = iTask.StateStr;
                        this.dataGridView1.Rows[i].Cells["TransferTime"].Value = Convert.ToDouble(iTask.TransferTimeSecond) + "s";
                        this.dataGridView1.Rows[i].Cells["TSpeed"].Value = Convert.ToInt32(iTask.TransferSpeed / 1024) + "s/K";
                        if (iTask.LastReceivePacketTime.HasValue == true)
                        {
                            this.dataGridView1.Rows[i].Cells["LastPacketTime"].Value = iTask.LastReceivePacketTime.Value.ToString("MM-dd HH:mm:ss");
                        }
                        if (iTask.BeginTime.HasValue == true)
                        {
                            this.dataGridView1.Rows[i].Cells["BeginTime"].Value = iTask.BeginTime.Value.ToString("MM-dd HH:mm:ss");
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Stop();
            RefrshStatus();

            this.timer1.Start();
        }

        private void RefrshStatus()
        {
            this.label8.Text = ServerFileWrite.FileWriteInfo;
            this.label5.Text = DateTime.Now.ToString();
            this.label6.Text = ServerListener.mThisListener.mSocketAsyncPool.Count.ToString() + " / " + ServerListener.mThisListener.mSocketAsyncPool.UsedCount.ToString();
            this.lbWriteThreadNum.Text = ServerFileWrite.FileWriteCount.ToString();
            Application.DoEvents();
        }

        private void FormServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (server != null)
            {
                server.Dispose();
                server = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows != null)
            {
                for (int i = this.dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    if (this.dataGridView1.Rows[i].Cells["TransferState"].Value.ToString() == "传输完成")
                    {
                        this.dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }
        }
    }
}
