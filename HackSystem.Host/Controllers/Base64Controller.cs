﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace HackSystem.Host
{
    /// <summary>
    /// Base64 编码控制器
    /// </summary>
    public static class Base64Controller
    {

        /// <summary>
        /// 转换 Base64 编码为图像
        /// </summary>
        /// <param name="Base64String">Base64</param>
        /// <returns>图像</returns>
        public static Image Base64ToImage(string Base64String)
        {
            LogController.Debug("转换 Base64 编码 (HashCode = 0x{0})为图像 ...", Base64String.GetHashCode().ToString("X"));
            try
            {
                byte[] ImageData = Convert.FromBase64String(Base64String);
                MemoryStream ImageStream = new MemoryStream(ImageData);
                return Image.FromStream(ImageStream);
            }
            catch (Exception ex)
            {
                LogController.Error("转换 Base64 编码为图像时遇到错误：{0}", ex.Message);
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
            LogController.Debug("转换图像(HashCode = 0x{0})为 Base64 编码 ...", ImageObject.GetHashCode().ToString("X"));
            try
            {
                MemoryStream ImageStream = new MemoryStream();
                ImageObject.Save(ImageStream, ImageFormatType);
                return Convert.ToBase64String(ImageStream.GetBuffer());
            }
            catch (Exception ex)
            {
                LogController.Error("转换图像为 Base64 编码时遇到错误：{0}", ex.Message);
                return string.Empty;
            }
        }

    }
}
