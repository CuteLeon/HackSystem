using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace RingsStartUp
{
    public class WhiteRingClass : StartUpTemplateClass
    {
        public WhiteRingClass()
        {
            Name = "天使光环";
            Description = "天使光环启动画面 - Leon";
            Preview = RingsStartUpResource.WhiteRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.WhiteRing, Color.Black) { ParentStartUp = this, ForeColor = Color.LightGray };
        }
    }
}
