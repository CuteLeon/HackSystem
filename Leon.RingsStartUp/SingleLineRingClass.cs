using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class SingleLineRingClass : StartUpTemplateClass
    {
        public SingleLineRingClass()
        {
            this.Name = "单线圆环";
            this.Description = "SingleLineRing圆环启动画面 - Leon";
            this.Preview = RingsStartUpResource.SingleLineRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.SingleLineRing, Color.FromArgb(215, 215, 215)) { ParentStartUp = this, Padding = new Padding(50, 200, 50, 50), ForeColor = Color.FromArgb(255, 42, 42, 42) };
        }
    }
}
