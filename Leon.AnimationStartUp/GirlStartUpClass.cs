using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
{
    public class GirlStartUpClass : StartUpTemplateClass
    {
        public GirlStartUpClass()
        {
            this.Name = "女孩";
            this.Description = "女孩启动画面 - Leon";
            this.Preview = StartUpResource.GirlPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.Girl, Color.FromArgb(255, 217, 225, 213)) { ParentStartUp = this, ForeColor = Color.DeepPink };
        }
    }
}
