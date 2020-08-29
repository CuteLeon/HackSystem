using System;
using System.Drawing;
using System.IO;

namespace HackSystem.Host.Extensions
{
    public static class BitmapExtension
    {
        public static string ToBase64(this Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, bitmap.RawFormat);
                byte[] bytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(bytes, 0, (int)stream.Length);
                stream.Close();
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
