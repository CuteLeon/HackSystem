using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class SmokeRingClass : StartUpTemplateClass
    {
        public SmokeRingClass()
        {
            Name = "烟圈";
            Description = "烟圈启动画面 - Leon";
            Preview = RingsStartUpResource.SmokeRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.SmokeRing, Color.Black) { ParentStartUp = this, Padding = new Padding(50,120,50,50), ForeColor = Color.LightGray };
        }
    }
}
