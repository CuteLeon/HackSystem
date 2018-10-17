using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
{
    public class SerpentineStartUpClass : StartUpTemplateClass
    {
        public SerpentineStartUpClass()
        {
            this.Name = "蜿蜒";
            this.Description = "蜿蜒启动画面 - Leon";
            this.Preview = StartUpResource.SerpentinePreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.Serpentine, Color.FromArgb(255, 14, 17, 31)) { ParentStartUp = this, ForeColor = Color.DeepSkyBlue };
        }
    }
}
