using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cc.utility
{
    public class Utility
    {
        /// <summary>
        /// 转换成base64的字符串 UTF-8编码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToBase64(string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            var re = Convert.ToBase64String(bytes);
            return re;
        }

        /// <summary>
        /// 从base64转换成正常的字符串 UTF-8编码
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static string FromBase64(string base64)
        {
            var re = string.Empty;
            re =  Encoding.UTF8.GetString(Convert.FromBase64String(base64));
            return re;
        }

        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串 </param>
        /// <returns> </returns>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex("^[+]?[0-9]+[.]?[0-9]+$");
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 32位加密
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string ConvertTo32Md5(string ConvertString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取WEB页面的HTML
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebHtml(string url)
        {
            try
            {
                WebClient MyWebClient = new WebClient();
                MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据
                Byte[] pageData = MyWebClient.DownloadData(url);
                return Encoding.UTF8.GetString(pageData);
            }
            catch { return ""; }
        }
    }
}
