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
    }
}
