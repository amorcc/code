using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFT.Common
{
    public class RunCommandUtils : IDisposable
    {
        private static List<RunCommandUtils> allUtils = new List<RunCommandUtils>();

        public static List<RunCommandUtils> AllUtils
        {
            get
            {
                return allUtils;
            }
        }

        private Process currentProcess;
        private string processErrorInfo;
        private string processOutputInfo;

        public RunCommandUtils(string Filename, string arguments, string WorkDir, bool RedirectInput)
        {
            allUtils.Add(this);
            processErrorInfo = string.Empty;
            processOutputInfo = string.Empty;
            ProcessStartInfo info = new ProcessStartInfo();
            info.Arguments = arguments;
            info.FileName = Filename;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.RedirectStandardInput = RedirectInput;
            info.UseShellExecute = false;
            info.WorkingDirectory = WorkDir;
            info.CreateNoWindow = true;

            currentProcess = new Process();
            currentProcess.StartInfo = info;
            currentProcess.Start();
            currentProcess.PriorityClass = ProcessPriorityClass.Idle;

            currentProcess.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            currentProcess.BeginOutputReadLine();

            currentProcess.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);
            currentProcess.BeginErrorReadLine();
        }

        public void ClearAllInfo()
        {
            processErrorInfo = string.Empty;
            processOutputInfo = string.Empty;
        }

        public int ExitCode
        {
            get
            {
                return currentProcess.ExitCode;
            }
        }

        public bool IsRunning
        {
            get
            {
                return currentProcess != null && !currentProcess.HasExited;
            }
        }

        private object object1;

        /// <summary>
        /// 备用属性1
        /// </summary>
        public object Object1
        {
            get
            {
                return object1;
            }

            set
            {
                object1 = value;
            }
        }

        private object object2;

        /// <summary>
        /// 备用属性2
        /// </summary>
        public object Object2
        {
            get
            {
                return object2;
            }

            set
            {
                object2 = value;
            }
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                processErrorInfo += e.Data + Environment.NewLine;
                if (ErrorDataReceived != null)
                {
                    ErrorDataReceived(this, e);
                }
            }
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                processOutputInfo += e.Data + Environment.NewLine;
                if (OutputDataReceived != null)
                {
                    OutputDataReceived(this, e);
                }
            }
        }

        public string ErrorInfo
        {
            get
            {
                return processErrorInfo;
            }
        }

        public string OutputInfo
        {
            get
            {
                return processOutputInfo;
            }
        }

        public bool HasErrorData
        {
            get
            {
                return !currentProcess.StandardOutput.EndOfStream;
            }
        }

        public bool HasOutputData
        {
            get
            {
                return !currentProcess.StandardError.EndOfStream;
            }
        }

        public event DataReceivedEventHandler OutputDataReceived;

        public event DataReceivedEventHandler ErrorDataReceived;

        #region IDisposable 成员

        public void Dispose()
        {
            if (IsRunning)
            {
                currentProcess.Kill();
                currentProcess.CancelErrorRead();
                currentProcess.CancelOutputRead();
            }

            if (currentProcess != null)
            {
                currentProcess.Dispose();
            }

            allUtils.Remove(this);
        }

        #endregion

        public StreamWriter StandardInput
        {
            get
            {
                if (currentProcess == null || !currentProcess.StartInfo.RedirectStandardInput || currentProcess.HasExited)
                {
                    return null;
                }

                return currentProcess.StandardInput;
            }
        }

        public void WaitExit()
        {
            if (currentProcess != null)
            {
                currentProcess.WaitForExit();
            }
        }
    }
}

