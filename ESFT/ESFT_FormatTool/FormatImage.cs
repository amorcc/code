using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFT_FormatTool
{
    public class FormatImage
    {
        /// <summary>
        /// 转换图片和生成图片缩略图:如果目标文件已经存在，将重新覆盖目标文件
        /// </summary>
        public static void FormatImageAndThumbnail(string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName)
        {
            try
            {
                // ------------------------------------------------------------------
                // 保存原图
                SaveOriginalImage(iSourceFileFullPath, iTargetFilePath, iTargetFileName);

                //// ------------------------------------------------------------------
                //// 旋转图片
                //int rotateProperty = GetImgRotateProperties(iSourceFileFullPath);
                //if (rotateProperty > 1)
                //{
                //    iSourceFileFullPath = RotatingImage(iSourceFileFullPath, rotateProperty);
                //}

                // ------------------------------------------------------------------
                // 生成指定大图
                MakeNewImage(iSourceFileFullPath, iTargetFilePath, iTargetFileName, 1600, 1200, false);

                // ------------------------------------------------------------------
                // 生成缩略图
                MakeNewImage(iSourceFileFullPath, iTargetFilePath + "\\img", iTargetFileName, 200, 200, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 保存原图片
        /// </summary>
        /// <param name="iSourceFileFullPath"></param>
        static void SaveOriginalImage(string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName)
        {
            if (File.Exists(iSourceFileFullPath))
            {
                // 缩略图保存的目标路径
                string targetThumbnailPath = Path.GetFullPath(iTargetFilePath + "\\original");

                // 缩略图文件名
                string targetThumbnailFileName = iTargetFileName;

                //缩略图保存的文件全路径
                string targetThumbnailFullName = Path.GetFullPath(targetThumbnailPath + "\\" + targetThumbnailFileName);

                if (!File.Exists(targetThumbnailPath))
                {
                    Directory.CreateDirectory(targetThumbnailPath);
                }

                if (File.Exists(targetThumbnailFullName))
                {
                    File.Delete(targetThumbnailFullName);
                }
                File.Copy(iSourceFileFullPath, targetThumbnailFullName);
            }

        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="iSourceFileFullPath"></param>
        /// <param name="iTargetFilePath"></param>
        /// <param name="iNewWidth"></param>
        /// <param name="iNewHeight"></param>
        /// <param name="isCut">是否裁剪成指定大小</param>
        static void MakeNewImage(string iSourceFileFullPath, string iTargetFilePath, string iTargetFileName, int iNewWidth, int iNewHeight, bool isCut)
        {
            Image imgSource = Image.FromFile(iSourceFileFullPath);

            // 新图片保存的目标路径
            string targetThumbnailPath = Path.GetFullPath(iTargetFilePath);

            // 图片文件名
            string targetThumbnailFileName = Path.GetFileNameWithoutExtension(iTargetFileName) + ".jpg";

            // 图片保存的文件全路径
            string targetThumbnailFullName = Path.GetFullPath(targetThumbnailPath + "\\" + targetThumbnailFileName);

            // ---------------------------------------
            // 创建缩略图保存路径
            if (!File.Exists(targetThumbnailPath))
            {
                Directory.CreateDirectory(targetThumbnailPath);
            }

            Image newImage = GetThumbnail(imgSource, iNewWidth, iNewHeight, isCut);

            newImage.Save(targetThumbnailFullName, ImageFormat.Jpeg);

            imgSource.Dispose();

            newImage.Dispose();

            //// ---------------------------------------
            //// 等比例缩放计算新图片大小
            //int sWidth = imgSource.Width; // 原图片宽度
            //int sHeight = imgSource.Height; // 原图片高度
            //int rWidth = 0; // 等比例缩放后的宽度
            //int rHeight = 0; // 等比例缩放后的高度

            //double wScale = (double)sWidth / iNewWidth; // 宽比例
            //double hScale = (double)sHeight / iNewHeight; // 高比例
            //double scale = wScale > hScale ? wScale : hScale;
            //rWidth = (int)Math.Floor(sWidth / scale);
            //rHeight = (int)Math.Floor(sHeight / scale);

            //try
            //{
            //    // ---------------------------------------
            //    // 如果源图片小于新图片要求大小，则直接保存文件
            //    if (sWidth <= iNewWidth && sHeight <= iNewHeight)
            //    {
            //        imgSource.Save(targetThumbnailFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        imgSource.Dispose();
            //        return;
            //    }

            //    // ---------------------------------------
            //    // 开始生成新的图片
            //    Bitmap thumbnail = new Bitmap(rWidth, rHeight);

            //    using (Graphics tGraphic = Graphics.FromImage(thumbnail))
            //    {
            //        tGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
            //        Rectangle rect = new Rectangle(0, 0, rWidth, rHeight);
            //        Rectangle rectSrc = new Rectangle(0, 0, sWidth, sHeight);
            //        tGraphic.DrawImage(imgSource, rect, rectSrc, GraphicsUnit.Pixel);

            //        if (isCut)
            //        {
            //            int xMove = 0; // 向右偏移（裁剪）
            //            int yMove = 0; // 向下偏移（裁剪）
            //            xMove = (rWidth - iNewWidth) / 2;
            //            yMove = (rHeight - iNewHeight) / 2;
            //            Bitmap final_image = new Bitmap(iNewWidth, iNewHeight);
            //            using (Graphics fGraphic = Graphics.FromImage(final_image))
            //            {
            //                fGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
            //                Rectangle rect1 = new Rectangle(0, 0, iNewWidth, iNewHeight);
            //                Rectangle rectSrc1 = new Rectangle(xMove, yMove, iNewWidth, iNewHeight);
            //                fGraphic.DrawImage(thumbnail, rect1, rectSrc1, GraphicsUnit.Pixel);
            //            }

            //            if (thumbnail != null)
            //            {
            //                thumbnail.Dispose();
            //            }


            //            final_image.Save(targetThumbnailFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //            final_image.Dispose();
            //        }
            //        else
            //        {
            //            // 保存新图片
            //            thumbnail.Save(targetThumbnailFullName, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        }
            //    }

            //    if (thumbnail != null)
            //    {
            //        thumbnail.Dispose();
            //        thumbnail = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    imgSource.Dispose();
            //    imgSource = null;
            //}
        }

        //static Image ssss(
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="imgSource">原图片</param>
        /// <param name="newWidth">缩略图宽度</param>
        /// <param name="newHeight">缩略图高度</param>
        /// <param name="isCut">是否裁剪（以中心点）</param>
        /// <returns></returns>
        public static Image GetThumbnail(System.Drawing.Image imgSource, int newWidth, int newHeight, bool isCut)
        {
            int rWidth = 0; // 等比例缩放后的宽度
            int rHeight = 0; // 等比例缩放后的高度
            int sWidth = imgSource.Width; // 原图片宽度
            int sHeight = imgSource.Height; // 原图片高度
            double wScale = (double)sWidth / newWidth; // 宽比例
            double hScale = (double)sHeight / newHeight; // 高比例
            double scale = wScale < hScale ? wScale : hScale;
            rWidth = (int)Math.Floor(sWidth / scale);
            rHeight = (int)Math.Floor(sHeight / scale);
            Bitmap thumbnail;
            try
            {
                // 如果是截取原图，并且原图比例小于所要截取的矩形框，那么保留原图
                if (!isCut && scale <= 1)
                {
                    rWidth = sWidth;
                    rHeight = sHeight;
                }

                thumbnail = new Bitmap(rWidth, rHeight);
                using (Graphics tGraphic = Graphics.FromImage(thumbnail))
                {
                    tGraphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
                    Rectangle rectSrc = new Rectangle(0, 0, sWidth, sHeight);
                    Rectangle rect = new Rectangle(0, 0, rWidth, rHeight);
                    tGraphic.DrawImage(imgSource, rect, rectSrc, GraphicsUnit.Pixel);
                }

                if (!isCut)
                {
                    return thumbnail;
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

                    if (thumbnail != null)
                    {
                        thumbnail.Dispose();
                    }

                    return final_image;
                }
            }
            catch
            {
                return new Bitmap(newWidth, newHeight);
            }
            finally
            {
                if (imgSource != null)
                {
                    imgSource.Dispose();
                }
            }
        }


        /// <summary>
        /// 获取图片文件的旋转属性值
        /// </summary>
        /// <param name="iFileFullName">文件全路径</param>
        /// <returns></returns>
        static int GetImgRotateProperties(string iFileFullName)
        {
            int orien = 1;

            try
            {
                if (File.Exists(iFileFullName))
                {
                    using (Image img = Image.FromFile(iFileFullName))
                    {
                        PropertyItem[] pt = img.PropertyItems;

                        foreach (PropertyItem p in pt)
                        {
                            if (p.Id == 0x0112)
                            {
                                orien = Convert.ToUInt16(p.Value[1] << 8 | p.Value[0]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(FormatImage)).Error(ex.Message, ex);
                orien = 1;
                Console.WriteLine("获取图片文件属性时出错，源文件：" + iFileFullName);
            }

            return orien;
        }

        /// <summary>
        /// 旋转图片,并返回旋转后的图片路径
        /// </summary>
        /// <param name="iFileFullName">原图片路径</param>
        /// <param name="iRotateProperty">旋转属性</param>
        static string RotatingImage(string iFileFullName, int iRotateProperty)
        {
            try
            {
                Bitmap bm = new Bitmap(iFileFullName);

                switch (iRotateProperty)
                {
                    case 2:
                        bm.RotateFlip(RotateFlipType.RotateNoneFlipX);//horizontal flip  
                        break;
                    case 3:
                        bm.RotateFlip(RotateFlipType.Rotate180FlipNone);//right-top  
                        break;
                    case 4:
                        bm.RotateFlip(RotateFlipType.RotateNoneFlipY);//vertical flip  
                        break;
                    case 5:
                        bm.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        bm.RotateFlip(RotateFlipType.Rotate90FlipNone);//right-top  
                        break;
                    case 7:
                        bm.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        bm.RotateFlip(RotateFlipType.Rotate270FlipNone);//left-bottom  
                        break;
                    default:
                        break;
                }

                string tempPath = iFileFullName.Substring(0, iFileFullName.LastIndexOf(".")) + "temp" + iFileFullName.Substring(iFileFullName.LastIndexOf("."));

                bm.Save(tempPath, ImageFormat.Jpeg);
                bm.Dispose();

                return tempPath;
            }
            catch (Exception ex)
            {
                log4net.LogManager.GetLogger(typeof(FormatImage)).Error(ex.Message, ex);
                return iFileFullName;
            }
        }
    }
}