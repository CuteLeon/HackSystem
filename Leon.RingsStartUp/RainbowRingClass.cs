using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class RainbowRingClass : StartUpTemplateClass
    {
        public RainbowRingClass()
        {
            Name = "彩虹环";
            Description = "彩虹环启动画面 - Leon";
            Preview = RingsStartUpResource.RainbowRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.RainbowRing, Color.Black) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.Gray };
        }
    }
}
