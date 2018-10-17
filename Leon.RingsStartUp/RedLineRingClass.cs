using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class RedLineRingClass : StartUpTemplateClass
    {
        public RedLineRingClass()
        {
            this.Name = "红线圆环";
            this.Description = "红线圆环启动画面 - Leon";
            this.Preview = RingsStartUpResource.RedLineRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.RedLineRing, Color.FromArgb(241, 236, 219)) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.FromArgb(255, 135, 36, 56) };
        }
    }
}
