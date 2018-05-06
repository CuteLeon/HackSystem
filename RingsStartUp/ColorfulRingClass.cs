using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class ColorfulRingClass : StartUpTemplateClass
    {
        public ColorfulRingClass()
        {
            Name = "多彩圆环";
            Description = "多彩圆环启动画面 - Leon";
            Preview = RingsStartUpResource.ColorfulRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.ColorfulRing, Color.FromArgb(255,41,41,41)) { ParentStartUp  =this , Padding = new Padding(50, 180, 50, 50), ForeColor= Color.OrangeRed};
        }
    }
}
