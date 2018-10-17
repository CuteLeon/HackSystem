using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class EmptyBallRingClass : StartUpTemplateClass
    {
        public EmptyBallRingClass()
        {
            this.Name = "线条球形圆环";
            this.Description = "线条球形圆环启动画面 - Leon";
            this.Preview = RingsStartUpResource.EmptyBallRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.EmptyBallRing, Color.FromArgb(248, 248, 248)) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.FromArgb(255, 56, 77, 92) };
        }
    }
}
