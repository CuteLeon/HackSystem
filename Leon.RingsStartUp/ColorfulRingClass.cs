using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.RingsStartUp
{
    public class ColorfulRingClass : StartUpTemplateClass
    {
        public ColorfulRingClass()
        {
            Name = "多彩圆环";
            Description = "多彩圆环启动画面 - Leon";
            Preview = RingsStartUpResource.ColorfulRingPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonRingForm(RingsStartUpResource.ColorfulRing, Color.FromArgb(255,41,41,41)) { ParentStartUp  =this , Padding = new Padding(50, 180, 50, 50), ForeColor= Color.OrangeRed};
        }
    }
}
