using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class RainbowRingClass : StartUpTemplateClass
    {
        public RainbowRingClass()
        {
            this.Name = "彩虹环";
            this.Description = "彩虹环启动画面 - Leon";
            this.Preview = RingsStartUpResource.RainbowRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.RainbowRing, Color.Black) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.Gray };
        }
    }
}
