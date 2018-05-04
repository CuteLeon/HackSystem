using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace HackSystem.Controllers
{
    static class Base64Controller
    {

        /// <summary>
        /// 转换 Base64 编码为图像
        /// </summary>
        /// <param name="Base64String">Base64</param>
        /// <returns>图像</returns>
        public static Image Base64ToBitmap(string Base64String)
        {
            UnityModule.DebugPrint("转换 Base64 编码 (HashCode = {0})为图像 ...", Base64String.GetHashCode());
            try
            {
                byte[] BitmapData = Convert.FromBase64String(Base64String);
                MemoryStream BitmapStream = new MemoryStream(BitmapData);
                return Image.FromStream(BitmapStream);
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("转换 Base64 编码为图像时遇到错误：{0}", ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 转换图像为 Base64 编码 (图像默认按 PNG 格式处理)
        /// </summary>
        /// <param name="ImageObject">图像</param>
        /// <returns>Base64</returns>
        public static string ImageToBase64(Image ImageObject)
        {
            return ImageToBase64(ImageObject, ImageFormat.Png);
        }

        /// <summary>
        /// 转换图像为 Base64 编码
        /// </summary>
        /// <param name="ImageObject">图像</param>
        /// <param name="ImageFormatType">图像格式</param>
        /// <returns>Base64</returns>
        public static string ImageToBase64(Image ImageObject, ImageFormat ImageFormatType)
        {
            UnityModule.DebugPrint("转换图像(HashCode = {0})为 Base64 编码 ...", ImageObject.GetHashCode());
            try
            {
                MemoryStream ImageStream = new MemoryStream();
                ImageObject.Save(ImageStream, ImageFormatType);
                return Convert.ToBase64String(ImageStream.GetBuffer());
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("转换图像为 Base64 编码时遇到错误：{0}", ex.Message);
                return null;
            }
        }

    }
}
