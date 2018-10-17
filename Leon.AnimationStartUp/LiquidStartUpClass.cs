using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
{
    public class LiquidStartUpClass : StartUpTemplateClass
    {
        public LiquidStartUpClass()
        {
            this.Name = "流体";
            this.Description = "流体启动画面 - Leon";
            this.Preview = StartUpResource.LiquidPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.Liquid, Color.White) { ParentStartUp = this, ForeColor = Color.Gray };
        }
    }
}
