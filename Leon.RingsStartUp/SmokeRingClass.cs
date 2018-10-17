using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class SmokeRingClass : StartUpTemplateClass
    {
        public SmokeRingClass()
        {
            this.Name = "烟圈";
            this.Description = "烟圈启动画面 - Leon";
            this.Preview = RingsStartUpResource.SmokeRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.SmokeRing, Color.Black) { ParentStartUp = this, Padding = new Padding(50,120,50,50), ForeColor = Color.LightGray };
        }
    }
}
