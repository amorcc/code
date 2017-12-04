using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ESFT.Common
{
    public class FileHash
    {
        /// <summary>
        /// 获取文件MD5值
        /// </summary>
        /// <param name="fileName">文件完整路径</param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fileStream);
                fileStream.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }

                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger("FileHash.cs").Error(ex.ToString());
                return null;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream = null;
                }
            }
        }

        public static string Encry(string str)
        {
            return Encry(Encoding.UTF8.GetBytes(str));
        }

        static string Encry(byte[] bytes)
        {
            string pwd = string.Empty;
            MD5 md5 = MD5.Create();

            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(bytes);

            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X2");
            }

            return pwd;
        }
    }
}
