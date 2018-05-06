using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class BallRingClass : StartUpTemplateClass
    {
        public BallRingClass()
        {
            Name = "球形圆环";
            Description = "球形圆环启动画面 - Leon";
            Preview = RingsStartUpResource.BallRingPreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.BallRing, Color.White) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 50, 50, 50) };
        }
    }
}
