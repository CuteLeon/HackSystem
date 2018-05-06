using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class SingleLineRingClass : StartUpTemplateClass
    {
        public SingleLineRingClass()
        {
            Name = "单线圆环";
            Description = "SingleLineRing圆环启动画面 - Leon";
            Preview = RingsStartUpResource.SingleLineRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.SingleLineRing, Color.FromArgb(215, 215, 215)) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.FromArgb(255, 42, 42, 42) };
        }
    }
}
