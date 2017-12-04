using ESFT.Client;
using System.IO;
using ESFT.Common.TypeDefinitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESFT.Client
{
    public partial class FileClientUpload : Form
    {
        public FileClientUpload()
        {
            InitializeComponent();
            System.Reflection.Assembly a = typeof(System.Net.Sockets.Socket).Assembly;
            System.Diagnostics.FileVersionInfo info = System.Diagnostics.FileVersionInfo.GetVersionInfo(a.Location);
            this.Text = a.GetName().Version.ToString() + " " + a.ImageRuntimeVersion.ToString() + " " + info.FileVersion.ToString();
        }


        private void AddRow(string iServerIp, int iServerPort,
          string iFileFullName)
        {
            this.dataGridView1.Rows.Add();

            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["btn"].Value = "开始";
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["ServerIp"].Value = iServerIp;
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["ServerPort"].Value = iServerPort;
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["FileFullName"].Value = iFileFullName;
            this.dataGridView1.Rows[this.dataGridView1.Rows.Count - 1].Cells["TransferState"].Value = "等待上传";
        }

        public void StartTask(int iRowIndex)
        {
            if (this.dataGridView1.Rows != null
                && this.dataGridView1.Rows.Count > 0
                && iRowIndex >= 0
                && iRowIndex < this.dataGridView1.Rows.Count
                && this.dataGridView1.Rows[iRowIndex].Cells["btn"].Value.ToString() == "开始"
                )
            {
                string serverIp = this.dataGridView1.Rows[iRowIndex].Cells["ServerIp"].Value.ToString();
                int serverPort = Convert.ToInt32(this.dataGridView1.Rows[iRowIndex].Cells["ServerPort"].Value.ToString());
                string fileFullName = this.dataGridView1.Rows[iRowIndex].Cells["FileFullName"].Value.ToString();
                string serverFileName = System.IO.Path.GetFileNameWithoutExtension(fileFullName); //System.Guid.NewGuid().ToString();
                string extension = System.IO.Path.GetExtension(fileFullName);
                string key = StartUploadFile(fileFullName, "\\" + DateTime.Today.ToString("yyyyMMdd"), serverFileName + extension, serverIp, serverPort);

                this.dataGridView1.Rows[iRowIndex].Cells["key"].Value = key;
                this.dataGridView1.Rows[iRowIndex].Cells["btn"].Value = "停止";
                if (this.dataGridView1.Rows[iRowIndex].Cells["TransferState"].Value.ToString() == "暂停")
                {
                    this.dataGridView1.Rows[iRowIndex].Cells["TransferState"].Value = "重新开始";
                }
            }
        }

        string StartUploadFile(string iFileFullName
            , string iServerPath, string iServerFileName
            , string iMasterServerIP, int iMasterPort)
        {
            ClientUploadTask task = new ClientUploadTask();
            task.Evnet_ClientUploadSuccess += task_Evnet_ClientUploadSuccess;
            task.Evnet_ClientUploadError += task_Evnet_ClientUploadError;
            return task.AddUploadTask(iFileFullName, iServerPath, iServerFileName, iMasterServerIP, iMasterPort, null);
        }

        void task_Evnet_ClientUploadError(object sender, ClientUploadErrorArgs e)
        {
            //MessageBox.Show(this, e.ClientFileFullName + "上传失败!     失败原因：" + e.ErrorInfo);
        }

        void task_Evnet_ClientUploadSuccess(object sender, ClientUploadSuccessArgs e)
        {
            //MessageBox.Show(e.ClientFileFullName.ToString() + "上传成功");
        }

        public void StopTask(int iRowIndex)
        {
            if (this.dataGridView1.Rows != null
                    && this.dataGridView1.Rows.Count > 0
                    && iRowIndex >= 0
                    && iRowIndex < this.dataGridView1.Rows.Count
                    && this.dataGridView1.Rows[iRowIndex].Cells["btn"].Value.ToString() == "停止")
            {
                this.dataGridView1.Rows[iRowIndex].Cells["btn"].Value = "开始";
                if (this.dataGridView1.Rows[iRowIndex].Cells["key"].Value != null)
                {
                    string key = this.dataGridView1.Rows[iRowIndex].Cells["key"].Value.ToString();
                    if (ClientUploadTask.m_UploadFileTasks.ContainsKey(key))
                    {
                        ClientUploadFile task = ClientUploadTask.m_UploadFileTasks[key];
                        task.Stop();
                        this.dataGridView1.Rows[iRowIndex].Cells["TransferState"].Value = "暂停";
                    }
                }
            }
        }

        /// <summary>
        /// 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDlg = new OpenFileDialog())
            {
                openDlg.Multiselect = true;
                if (openDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string[] fileNames = openDlg.FileNames;
                    string serverIp = this.textBox2.Text;
                    int port = 8000;
                    if (!int.TryParse(this.textBox3.Text.Trim(), out port))
                    {
                        MessageBox.Show("端口号输入错误");
                        return;
                    }
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        AddRow(serverIp, port, fileNames[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 全部开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows != null
                && this.dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    StartTask(i);
                }
            }
        }

        /// <summary>
        /// 全部停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows != null
                && this.dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    StopTask(i);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "btn")
            {
                if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "开始")
                {
                    StartTask(e.RowIndex);
                }
                else if (dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "停止")
                {
                    StopTask(e.RowIndex);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.dataGridView1.Rows != null && this.dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                {
                    if (this.dataGridView1.Rows[i].Cells["key"].Value != null)
                    {
                        string key = this.dataGridView1.Rows[i].Cells["key"].Value.ToString();
                        if (ClientUploadTask.m_UploadFileTasks.ContainsKey(key))
                        {
                            ClientUploadFile task = ClientUploadTask.m_UploadFileTasks[key];
                            this.dataGridView1.Rows[i].Cells["FileLenght"].Value = task.mFileIO.FileLenght.ToString("###,###,###,###");
                            this.dataGridView1.Rows[i].Cells["CurrentCompleteLenght"].Value = task.mFileIO.CompletedFileLenght.ToString("###,###,###,###");
                            this.dataGridView1.Rows[i].Cells["TSpeed"].Value = Convert.ToInt32(task.mFileIO.Speed / 1024) + "s/K";
                            this.dataGridView1.Rows[i].Cells["TransferTime"].Value = task.mFileIO.TransferTime;

                            //this.dataGridView1.Rows[i].Cells["WriteLenght"].Value = task.m_TransferProgress.CurrentWriteLenghtFormatStr;
                            this.dataGridView1.Rows[i].Cells["LastPacketTime"].Value = task.mFileIO.LastPacketTime.ToString("MM-dd HH:mm:ss");
                            this.dataGridView1.Rows[i].Cells["ServerFuleName"].Value = task.mFileIO.RemoteFilePath + "//" + task.mFileIO.RemoteFileName;
                            this.dataGridView1.Rows[i].Cells["ServerRealFullName"].Value = task.mFileIO.RemoteFilePath + "//" + task.mFileIO.RemoteFileName;


                            this.dataGridView1.Rows[i].Cells["TransferState"].Value = task.mFileIO.State.ToString();
                            this.dataGridView1.Rows[i].Cells["TransferState"].ToolTipText = task.mFileIO.ErrorInfo;
                            switch (task.mFileIO.State)
                            {
                                case Common.TypeDefinitions.TransferState.Error:
                                    this.dataGridView1.Rows[i].Cells["btn"].Value = "开始";
                                    break;
                                case Common.TypeDefinitions.TransferState.Finish:
                                    this.dataGridView1.Rows[i].Cells["btn"].Value = "";
                                    break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            //  下载服务器文件   d:\\d\\1.rmvb
            //  本地存储在  D:\\d\\111\\目录下，并重命名为11.rmvb
            //  下载前，请自己判断本地存储路径下是否由同名文件存在，文件路径是否存在，没有则创建本地文件路径
            ClientDownload download = new ClientDownload(DownloadFilePathType.AbsolutePath, "d:\\d\\1sdf.rmvb",
               "D:\\d\\111\\", "11.rmvb", "192.168.0.66", 8000);
            download.Evnet_ClientDownloadSuccess += download_Evnet_ClientDownloadSuccess;
            download.StartDownload(null);

            //ClientDownload download2 = new ClientDownload(DownloadFilePathType.AbsolutePath, "d:\\d\\1.rmvb",
            //  "D:\\d\\111\\", "12.rmvb", "192.168.0.66", 8000);
            //download2.Evnet_ClientDownloadSuccess += download_Evnet_ClientDownloadSuccess;
            //download2.StartDownload();
        }

        void download_Evnet_ClientDownloadSuccess(ClientDownloadSuccessArgs e)
        {
            MessageBox.Show("下载成功");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FileStream fs1 = new FileStream("d:\\d\\1.rmvb", FileMode.Open, FileAccess.Read);
            FileStream fs2 = new FileStream("d:\\d\\1.rmvb", FileMode.Open, FileAccess.Read);
        }

        private void FileClientUpload_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClientUploadTask.DisposeThread();
        }

    }
}
