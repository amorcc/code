using System;
using System.Collections.Generic;
using System.Text;

namespace SSDC.Common
{
    public class Register
    {
        public static string GetMachineInfo()
        {
            Computer computer = new Computer();
            string macId = computer.GetMacAddress();
            string diskId = computer.GetDiskID();

            macId = macId.Replace(":", "");
            diskId = diskId.Replace(" ", "");

            macId = macId.ToUpper();
            diskId = diskId.ToUpper();

            //macId = "12345678";
            //diskId = "abcde";
            string result = string.Format("{0}---{1}", macId, diskId);

            result = DES.Encrypt(result);

            return result;
        }

        public static bool RegisterSoft(string iRegCode, out string iErrorMsg)
        {
            iErrorMsg = string.Empty;
            string regCode = DES.Decrypt(iRegCode);
            string[] regCodeArray = regCode.Split(new string[] { "---" }, StringSplitOptions.None);

            if (regCodeArray == null && regCodeArray.Length != 2)
            {
                iErrorMsg = "注册码不正确，请联系管理员！";
                return false;
            }

            string code = regCodeArray[0];
            string regDateStr = regCodeArray[1];

            regDateStr = DES.Decrypt(regDateStr);
            DateTime regDate = DateTime.Now;
            if (DateTime.TryParse(regDateStr, out regDate) == false)
            {
                iErrorMsg = "注册码不正确，请联系管理员！";
                return false;
            }

            string regDateCode = regDate.ToString("yyyy-MM-dd HH:mm:ss");
            regDateCode = DES.Encrypt(regDateCode);
            regDateCode = DES.Encrypt(regDateCode);
            regDateCode = DES.Encrypt(regDateCode);
            regDateCode = DES.Encrypt(regDateCode);

            code = DES.Encrypt(code);
            code = DES.Encrypt(code);
            code = DES.Encrypt(code);
            code = DES.Encrypt(code);
            code = DES.Encrypt(code);

            Common.FileHelper.WrithFile("rc", code);
            Common.FileHelper.WrithFile("rt", regDateCode);
            WriteLastDate();

            return true;
        }

        public static void WriteLastDate()
        {
            string lastDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            lastDate = DES.Encrypt(lastDate);
            lastDate = DES.Encrypt(lastDate);
            lastDate = DES.Encrypt(lastDate);

            Common.FileHelper.WrithFile("lt", lastDate);
        }

        public static bool CheckRegister()
        {
            try
            {
                string errorMsg = string.Empty;
                string code = FileHelper.ReadFile("rc", out errorMsg);
                string regDateStr = FileHelper.ReadFile("rt", out errorMsg);
                string lastDateStr = FileHelper.ReadFile("lt", out errorMsg);

                code = DES.Decrypt(code);
                code = DES.Decrypt(code);
                code = DES.Decrypt(code);
                code = DES.Decrypt(code);
                code = DES.Decrypt(code);

                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);

                string localCode = GetCode();

                if (code != GetCode())
                {
                    return false;
                }

                DateTime regDate = DateTime.Now.AddDays(-10);

                if (DateTime.TryParse(regDateStr, out regDate) == false)
                {
                    return false;
                }

                if (DateTime.Now > regDate)
                {
                    return false;
                }

                if (string.IsNullOrEmpty(lastDateStr))
                {
                    return false;
                }

                lastDateStr = DES.Decrypt(lastDateStr);
                lastDateStr = DES.Decrypt(lastDateStr);
                lastDateStr = DES.Decrypt(lastDateStr);

                DateTime lastDate = DateTime.Now.AddDays(1);

                if (DateTime.TryParse(lastDateStr, out lastDate) == false)
                {
                    return false;
                }

                if (DateTime.Now < lastDate)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DateTime GetRegDate()
        {
            DateTime regDate = new DateTime();
            try
            {
                string errorMsg = string.Empty;
                string regDateStr = FileHelper.ReadFile("rt", out errorMsg);

                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);
                regDateStr = DES.Decrypt(regDateStr);

                if (DateTime.TryParse(regDateStr, out regDate))
                {

                }
            }
            catch
            {

            }

            return regDate;
        }

        static string GetCode()
        {
            Computer computer = new Computer();
            string macId = computer.GetMacAddress();
            string diskId = computer.GetDiskID();

            macId = macId.Replace(":", "");
            diskId = diskId.Replace(" ", "");

            macId = macId.ToUpper();
            diskId = diskId.ToUpper();

            int minLength = macId.Length > diskId.Length ? diskId.Length : macId.Length;

            string code = string.Empty;


            for (int i = 0; i < minLength; i++)
            {
                code += macId[i].ToString() + diskId[i].ToString();
            }

            if (macId.Length > diskId.Length)
            {
                code += macId.Substring(minLength, macId.Length - minLength);
            }
            else
            {
                code += diskId.Substring(minLength, diskId.Length - minLength);
            }

            code = code.ToUpper();

            code = SSDC.Common.MD5Helper.MD5Encrypt(code);

            code = code.ToUpper();

            return code;
        }
    }
}
