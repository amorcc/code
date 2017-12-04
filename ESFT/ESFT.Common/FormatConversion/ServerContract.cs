using ESFT.Common.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace ESFT.Common.FormatConversion
{
    public enum FileType
    {
        image,
        video,
        other
    }

    // 注意: 如果更改此处的类名 "Contract"，也必须更新 App.config 中对 "Contract" 的引用。
    public class ServerContract
    {
        private static string serverRootPath = SystemInfo.SystemInfo.m_ServerRealFilePath;//SystemInfo.m_ServerRealFilePath;//System.Configuration.ConfigurationManager.AppSettings["ServerRootPath"];
        private static string basePath = AppDomain.CurrentDomain.BaseDirectory;

        #region 上传完成后的处理
        public static void UploadCompleted(string iFilePath, string iServerPath, FileType iFileType)
        {
            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "开始文件的截图和格式转换处理");
            try
            {
                iFilePath = System.IO.Path.GetFullPath(iFilePath);
                iFilePath = iFilePath.Replace("\\", "/");

                //iServerPath = System.IO.Path.GetFullPath(iServerPath);
                //iServerPath = iServerPath.Replace("\\", "/");


                // 加密前的文件
                string filePath = iFilePath;

                // 加密后的文件
                string path = serverRootPath + iServerPath;
                path = System.IO.Path.GetFullPath(path);
                path = path.Replace("\\", "/");

                string fullName = Directory.GetParent(path).FullName;
                string fileExtension = path.Substring(path.LastIndexOf(".") + 1, path.Length - path.LastIndexOf(".") - 1);
                string pathNoExtension = path.Substring(0, path.LastIndexOf("."));
                string fileName = pathNoExtension.Substring(pathNoExtension.LastIndexOf("/") + 1);

                if (!Directory.Exists(fullName))
                {
                    Directory.CreateDirectory(fullName);
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                if (iFileType == FileType.image)
                {
                    int rotateProperty = GetImageProperties(filePath);
                    if (rotateProperty > 1)
                    {
                        filePath = Rotating(filePath, rotateProperty);
                    }

                    try
                    {
                        MakeSmallImg(filePath, path, 1280, 960, false);

                        if (System.IO.File.Exists(path))
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "图片截图成功(1280*960)！\r\n源图片路径:" + filePath + "\r\n截图路径:" + path + ",iFilePath = '" + iFilePath + " , iServerPath = " + iServerPath);
                        }
                        else
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "图片截图失败(1280*960)！\r\n源图片路径:" + filePath + "\r\n截图路径:" + path);
                        }

                        // 图片缩略图
                        if (!Directory.Exists(fullName + "/img"))
                        {
                            Directory.CreateDirectory(fullName + "/img");
                        }

                        string thumbImg = fullName + "/img/" + fileName + "." + fileExtension;
                        MakeSmallImg(path, thumbImg, 200, 200, true);

                        if (System.IO.File.Exists(thumbImg))
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "图片截图成功！\r\n源图片路径:" + path + "\r\n截图路径:" + thumbImg);
                        }
                        else
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "图片截图失败！\r\n源图片路径:" + path + "\r\n截图路径:" + thumbImg);
                        }
                    }
                    catch (Exception ex)
                    {
                        log4net.LogManager.GetLogger(typeof(ServerContract)).Error(ex.ToString());
                    }
                    finally
                    {
                        // 原图片
                        if (!Directory.Exists(fullName + "/original"))
                        {
                            Directory.CreateDirectory(fullName + "/original");
                        }

                        string orgFileName = fullName + "/original/" + fileName + "." + fileExtension;
                        if (!System.IO.File.Exists(orgFileName))
                        {
                            File.Copy(filePath, orgFileName);
                        }

                        if (System.IO.File.Exists(orgFileName))
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "原图片保存成功！\r\n源图片路径:" + filePath + "\r\n保存图片路径:" + orgFileName);
                        }
                        else
                        {
                            MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "原图片保存失败！\r\n源图片路径:" + filePath + "\r\n保存图片路径:" + orgFileName);
                        }
                    }
                }
                else
                {
                    File.Copy(filePath, path);
                    if (iFileType == FileType.video)
                    {
                        string[] args = new string[5];
                        args[0] = fullName;
                        args[1] = fileExtension;
                        args[2] = pathNoExtension;
                        args[3] = fileName;
                        args[4] = path;

                        CreateProcess(args);
                    }
                }

                MyLogManage.Debug("Server.ServerContract", "AsyncUserToken_Evnet_ServerWriteFileComplete", "转换结束");
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServerContract)).Error(ex.ToString());
            }
        }

        private static void CreateProcess(string[] iArgs)
        {
            string fullName = iArgs[0];
            string fileExtension = iArgs[1];
            string pathNoExtension = iArgs[2];
            string fileName = iArgs[3];
            string filePath = iArgs[4];

            try
            {
                Process process = new Process();
                ProcessStartInfo info = new ProcessStartInfo(basePath + @"Convert\ESFT_Convert.exe", "\"" + fullName + "\" \"" + fileExtension + "\" \"" + pathNoExtension + "\" \"" + fileName + "\" \"" + filePath + "\"");
                info.UseShellExecute = false;
                info.CreateNoWindow = true;
                process.StartInfo = info;
                process.Start();
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServerContract)).Error(ex.ToString());
            }
        }

        #region 图片自动旋转
        /// <summary>
        /// 获取图片旋转属性
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private static int GetImageProperties(string FileName)
        {
            if (!File.Exists(FileName))
            {
                return 1;
            }

            int rtn = 1;

            System.Drawing.Image img = null;

            try
            {
                img = System.Drawing.Image.FromFile(FileName);
                PropertyItem[] pt = img.PropertyItems;

                foreach (PropertyItem p in pt)
                {
                    if (p.Id == 0x0112)
                    {
                        rtn = Convert.ToUInt16(p.Value[1] << 8 | p.Value[0]);
                    }
                }
            }
            catch
            {
                rtn = 1;
            }
            finally
            {
                if (img != null)
                {
                    img.Dispose();
                }
            }

            return rtn;
        }

        /// <summary>
        /// 旋转图片,并返回旋转后的图片路径
        /// </summary>
        /// <param name="iFilePath">原图片路径</param>
        /// <param name="iRotateProperty">旋转属性</param>
        private static string Rotating(string iFilePath, int iRotateProperty)
        {
            try
            {
                Bitmap bm = new Bitmap(iFilePath);
                switch (iRotateProperty)
                {
                    case 2:
                        bm.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        bm.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        bm.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                    case 5:
                        bm.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        bm.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        bm.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        bm.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                    default:
                        break;
                }

                string tempPath = iFilePath.Substring(0, iFilePath.LastIndexOf(".")) + "temp" + iFilePath.Substring(iFilePath.LastIndexOf("."));
                bm.Save(tempPath);
                bm.Dispose();
                ////if (File.Exists(iFilePath))
                ////{
                ////    File.Delete(iFilePath);
                ////}

                return tempPath;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServerContract)).Error(ex.ToString());
                return iFilePath;
            }
        }
        #endregion

        #region 压缩图片
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="imgSource">原图片</param>
        /// <param name="newWidth">缩略图宽度</param>
        /// <param name="newHeight">缩略图高度</param>
        /// <param name="isCut">是否裁剪（以中心点）</param>
        /// <returns></returns>
        private static void MakeSmallImg(string fromFileUrl, string fileSaveUrl, int newWidth, int newHeight, bool isCut)
        {
            // 从文件取得图片对象，并使用流中嵌入的颜色管理信息
            System.Drawing.Image imgSource = System.Drawing.Image.FromFile(fromFileUrl, true);

            int rWidth = 0; // 等比例缩放后的宽度
            int rHeight = 0; // 等比例缩放后的高度
            int sWidth = imgSource.Width; // 原图片宽度
            int sHeight = imgSource.Height; // 原图片高度
            if (sWidth <= newWidth && sHeight <= newHeight)
            {
                imgSource.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgSource.Dispose();
                return;
            }

            double wScale = (double)sWidth / newWidth; // 宽比例
            double hScale = (double)sHeight / newHeight; // 高比例
            double scale = wScale < hScale ? wScale : hScale;
            rWidth = (int)Math.Floor(sWidth / scale);
            rHeight = (int)Math.Floor(sHeight / scale);
            Bitmap thumbnail = new Bitmap(rWidth, rHeight);
            try
            {
                // 如果是截取原图，并且原图比例小于所要截取的矩形框，那么保留原图
                if (!isCut && scale <= 1)
                {
                    // 保存缩略图
                    imgSource.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imgSource.Dispose();
                }
                else
                {
                    using (Graphics tGraphic = Graphics.FromImage(thumbnail))
                    {
                        tGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                        Rectangle rect = new Rectangle(0, 0, rWidth, rHeight);
                        Rectangle rectSrc = new Rectangle(0, 0, sWidth, sHeight);
                        tGraphic.DrawImage(imgSource, rect, rectSrc, GraphicsUnit.Pixel);
                    }

                    if (!isCut)
                    {
                        // 保存缩略图
                        thumbnail.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                        thumbnail.Dispose();
                    }
                    else
                    {
                        int xMove = 0; // 向右偏移（裁剪）
                        int yMove = 0; // 向下偏移（裁剪）
                        xMove = (rWidth - newWidth) / 2;
                        yMove = (rHeight - newHeight) / 2;
                        Bitmap final_image = new Bitmap(newWidth, newHeight);
                        using (Graphics fGraphic = Graphics.FromImage(final_image))
                        {
                            fGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                            Rectangle rect1 = new Rectangle(0, 0, newWidth, newHeight);
                            Rectangle rectSrc1 = new Rectangle(xMove, yMove, newWidth, newHeight);
                            fGraphic.DrawImage(thumbnail, rect1, rectSrc1, GraphicsUnit.Pixel);
                        }

                        thumbnail.Dispose();

                        final_image.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                        final_image.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(ServerContract)).Error(ex.ToString());

                // 保存缩略图
                imgSource.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                imgSource.Dispose();
            }
            finally
            {
                if (imgSource != null)
                {
                    imgSource.Dispose();
                }
            }
        }
        #endregion

        #endregion
    }
}
