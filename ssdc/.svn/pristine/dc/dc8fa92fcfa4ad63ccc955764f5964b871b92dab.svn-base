using System;
using System.Collections.Generic;
using System.Text;

namespace SSDC_RegisterMachine
{
    public class RegisterMachine
    {
        public static string GetRegisterCode(string iMachineInfo, DateTime iRegisterDate)
        {
            string machineInfoStr = SSDC.Common.DES.Decrypt(iMachineInfo);
            string[] machineInfoArray = machineInfoStr.Split(new string[] { "---" }, StringSplitOptions.None);

            if (machineInfoArray != null && machineInfoArray.Length == 2)
            {
                string macId = machineInfoArray[0];
                string diskId = machineInfoArray[1];

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

                string dateEncrypt = iRegisterDate.ToString("yyyy-MM-dd HH:mm:ss");
                dateEncrypt = SSDC.Common.DES.Encrypt(dateEncrypt);
                code = string.Format("{0}---{1}", code, dateEncrypt);

                code = SSDC.Common.DES.Encrypt(code);

                return code;
            }

            return string.Empty;
        }
    }
}
