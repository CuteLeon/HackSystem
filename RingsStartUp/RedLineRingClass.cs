using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class RedLineRingClass : StartUpTemplateClass
    {
        public RedLineRingClass()
        {
            Name = "红线圆环";
            Description = "红线圆环启动画面 - Leon";
            Preview = RingsStartUpResource.RedLineRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.RedLineRing, Color.FromArgb(241, 236, 219)) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.FromArgb(255, 135, 36, 56) };
        }
    }
}
