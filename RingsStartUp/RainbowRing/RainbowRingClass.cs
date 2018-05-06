using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class RainbowRingClass : StartUpTemplateClass
    {
        public RainbowRingClass()
        {
            Name = "彩虹环";
            Description = "彩虹环启动画面 - Leon";
            Preview = RingsStartUpResource.RainbowRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new RainbowRingForm() { ParentStartUp  =this };
        }
    }
}
