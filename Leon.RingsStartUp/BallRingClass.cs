using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class BallRingClass : StartUpTemplateClass
    {
        public BallRingClass()
        {
            this.Name = "球形圆环";
            this.Description = "球形圆环启动画面 - Leon";
            this.Preview = RingsStartUpResource.BallRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.BallRing, Color.White) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 50, 50, 50) };
        }
    }
}
