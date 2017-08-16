using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace cc.webapi.Controllers
{
    public class UploadFileController : ApiController
    {
        #region 上传文件
        /// <summary>
        /// 通过multipart/form-data方式上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            #region old
            //MessagesDataCodeModel json = new MessagesDataCodeModel(false, "无效参数", 401);

            try
            {
                List<string> filePathList = new List<string>();
                // 是否请求包含multipart/form-data。
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                string root = HttpContext.Current.Server.MapPath("/upload/");
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/upload/")))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/upload/"));
                }

                var provider = new MultipartFormDataStreamProvider(root);

                StringBuilder sb = new StringBuilder(); // Holds the response body

                // 阅读表格数据并返回一个异步任务.
                await Request.Content.ReadAsMultipartAsync(provider);

                // 如何上传文件到文件名.
                foreach (var file in provider.FileData)
                {
                    string orfilename = file.Headers.ContentDisposition.FileName.TrimStart('"').TrimEnd('"');
                    FileInfo fileinfo = new FileInfo(file.LocalFileName);
                    string fileExt = orfilename.Substring(orfilename.LastIndexOf('.'));
                    //定义允许上传的文件扩展名
                    //String fileTypes = SettingConfig.FileTypes;
                    //if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
                    //{
                    //    json.Msg = "图片类型不正确";
                    //    json.Code = 303;
                    //}

                    fileinfo.CopyTo(Path.Combine(root, fileinfo.Name + fileExt), true);
                    string filePath = "/upload/" + fileinfo.Name + fileExt;
                    sb.Append(filePath);

                    filePathList.Add(filePath);
                    fileinfo.Delete();//删除原文件
                }

                var result = cc.common.Utility.MyResponse.ToYou<List<string>>(filePathList);

                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            catch (System.Exception e)
            {
                var result = cc.common.Utility.MyResponse.ShowError<string>(e.ToString());
                return new HttpResponseMessage { Content = new StringContent(JsonConvert.SerializeObject(result), System.Text.Encoding.UTF8, "application/json") };
            }
            #endregion
        }
        #endregion 上传文件

    }
}
