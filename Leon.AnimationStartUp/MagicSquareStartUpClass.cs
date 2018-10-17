using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
{
    public class MagicSquareStartUpClass : StartUpTemplateClass
    {
        public MagicSquareStartUpClass()
        {
            this.Name = "魔方";
            this.Description = "魔方启动画面 - Leon";
            this.Preview = StartUpResource.MagicSquarePreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.MagicSquare, Color.Black) { ParentStartUp = this, ForeColor = Color.FromArgb(255, 255, 90, 33) };
        }
    }
}
