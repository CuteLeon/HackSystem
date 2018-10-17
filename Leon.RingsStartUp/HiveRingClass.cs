using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class HiveRingClass : StartUpTemplateClass
    {
        public HiveRingClass()
        {
            Name = "蜂巢";
            Description = "蜂巢启动画面 - Leon";
            Preview = RingsStartUpResource.HiveRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.HiveRing, Color.FromArgb(21, 25, 31)) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 133, 155, 178) };
        }
    }
}
