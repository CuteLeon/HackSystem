using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace LoginTemplate
{
    static class HeadPortraitController
    {
        /// <summary>
        /// 获取圆形头像
        /// </summary>
        /// <param name="HeadPortrait">原头像</param>
        /// <returns>圆形头像</returns>
        public static Bitmap GetCircularHeadPortrait(Bitmap HeadPortrait)
        {
            if (HeadPortrait == null) return null;
            Bitmap HPBuffer = HeadPortrait.Clone() as Bitmap, CircularHeadPortrait = new Bitmap(HeadPortrait.Width, HeadPortrait.Height);
            using (Graphics HPGraphics = Graphics.FromImage(HPBuffer))
            {
                using (GraphicsPath HPPath = new GraphicsPath())
                {
                    HPPath.AddRectangle(new Rectangle(0, 0, HeadPortrait.Width, HeadPortrait.Height));
                    HPPath.AddEllipse(0, 0, HeadPortrait.Width, HeadPortrait.Height);
                    HPGraphics.FillPath(Brushes.Black, HPPath);
                }
            }
            HPBuffer.MakeTransparent(Color.Black);

            using (Graphics HPGraphics = Graphics.FromImage(CircularHeadPortrait))
            {
                HPGraphics.FillEllipse(Brushes.Black, 0, 0, CircularHeadPortrait.Width, CircularHeadPortrait.Height);
                HPGraphics.DrawImageUnscaled(HPBuffer, 0, 0);
            }
            return CircularHeadPortrait;
        }

        /*
        Dim CircularBitmap As Bitmap = New Bitmap(BitmapSize.Width, BitmapSize.Height)
        Dim CircularGraphics As Graphics = Graphics.FromImage(InitialBitmap)
        Dim CircularGraphicsPath As Drawing2D.GraphicsPath = New Drawing2D.GraphicsPath
        '创建圆形外围路径，填充颜色后将该颜色置为透明
        CircularGraphicsPath.AddRectangle(New RectangleF(0, 0, BitmapSize.Width, BitmapSize.Height))
        CircularGraphicsPath.AddEllipse(0, 0, BitmapSize.Width, BitmapSize.Height)
        CircularGraphics.FillPath(Brushes.Black, CircularGraphicsPath)
        CircularGraphicsPath.Dispose()
        InitialBitmap.MakeTransparent(Color.Black)
        '再用被透明的颜色绘制一个圆形当做背景，并覆盖处理后的圆形图像，防止中心圆形图像被透明
        CircularGraphics = Graphics.FromImage(CircularBitmap)
        CircularGraphics.FillEllipse(Brushes.Black, New Rectangle(0, 0, CircularBitmap.Width, CircularBitmap.Height))
        CircularGraphics.DrawImage(InitialBitmap, New Rectangle(0, 0, InitialBitmap.Width, InitialBitmap.Height))
        CircularGraphics.Dispose()
         */
    }
}
