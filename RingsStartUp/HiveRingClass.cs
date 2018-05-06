using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class HiveRingClass : StartUpTemplateClass
    {
        public HiveRingClass()
        {
            Name = "蜂巢";
            Description = "蜂巢启动画面 - Leon";
            Preview = RingsStartUpResource.HiveRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.HiveRing, Color.FromArgb(21, 25, 31)) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 133, 155, 178) };
        }
    }
}
