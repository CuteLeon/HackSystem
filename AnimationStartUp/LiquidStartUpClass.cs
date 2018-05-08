using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace AnimationStartUp
{
    public class LiquidStartUpClass : StartUpTemplateClass
    {
        public LiquidStartUpClass()
        {
            Name = "流体";
            Description = "流体启动画面 - Leon";
            Preview = StartUpResource.LiquidPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.Liquid, Color.White) { ParentStartUp = this, ForeColor = Color.Gray };
        }
    }
}
