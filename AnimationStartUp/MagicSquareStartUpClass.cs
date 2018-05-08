using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace AnimationStartUp
{
    public class MagicSquareStartUpClass : StartUpTemplateClass
    {
        public MagicSquareStartUpClass()
        {
            Name = "魔方";
            Description = "魔方启动画面 - Leon";
            Preview = StartUpResource.MagicSquarePreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.MagicSquare, Color.Black) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 255, 90, 33) };
        }
    }
}
