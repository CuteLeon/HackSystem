using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
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
