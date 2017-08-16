using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SSDC.Common
{
    public class FileHelper
    {
        public FileHelper()
        {

        }

        public static void WrithFile(string iFileFullName, string iFileContent)
        {
            if (File.Exists(iFileFullName))
            {
                File.Delete(iFileFullName);
            }


            byte[] myByte = System.Text.Encoding.UTF8.GetBytes(iFileContent);

            using (FileStream fsWrite = new FileStream(iFileFullName, FileMode.OpenOrCreate))
            {
                fsWrite.Write(myByte, 0, myByte.Length);
            }
        }

        public static string ReadFile(string iFileFullName, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            if (File.Exists(iFileFullName) == false)
            {
                iErrorMsg = string.Format("{0}文件不存在！", iFileFullName);
                return string.Empty;
            }

            using (StreamReader sr = new StreamReader(iFileFullName))
            {
                string msg = sr.ReadLine();
                return msg;
            }
        }
    }
}
