using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace AnimationStartUp
{
    public class SerpentineStartUpClass : StartUpTemplateClass
    {
        public SerpentineStartUpClass()
        {
            Name = "蜿蜒";
            Description = "蜿蜒启动画面 - Leon";
            Preview = StartUpResource.SerpentinePreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.Serpentine, Color.FromArgb(255, 14, 17, 31)) { ParentStartUp = this, ForeColor = Color.DeepSkyBlue };
        }
    }
}
