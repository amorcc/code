using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.QrCode;

namespace cc.common.QRCode
{
    public class QRCode
    {
        /// <summary>
        /// 生成二维码信息并上传zol资源站 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool CreateQCcode(string iContent, string iLocalFileName)
        {
            try
            {
                //生成二维码
                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                QrCodeEncodingOptions options = new QrCodeEncodingOptions();
                options.DisableECI = true;
                //设置内容编码
                options.CharacterSet = "UTF-8";
                //设置二维码的宽度和高度
                options.Width = 300;
                options.Height = 300;
                //设置二维码的边距,单位不是固定像素
                options.Margin = 1;
                writer.Options = options;

                using (Bitmap map = writer.Write(iContent))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        map.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                        //如果需要保存到本地，则该地址不为空
                        if (!string.IsNullOrEmpty(iLocalFileName))
                        {
                            using (System.IO.FileStream fs = System.IO.File.Create(System.Web.HttpContext.Current.Server.MapPath(iLocalFileName)))
                            {
                                byte[] bytes = ms.ToArray();
                                fs.Write(bytes, 0, bytes.Length);
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();

                                return true;
                            }
                        }

                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
