using System.Drawing;
using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.AnimationStartUp
{
    public class PeriodicLatticeStartUpClass : StartUpTemplateClass
    {
        public PeriodicLatticeStartUpClass()
        {
            Name = "点阵";
            Description = "点阵启动画面 - Leon";
            Preview = StartUpResource.PeriodicLatticePreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.PeriodicLattice, Color.FromArgb(255, 38, 38, 38)) { ParentStartUp = this, Padding = new Padding(50, 150, 50, 50), ForeColor = Color.FromArgb(220, 220, 210) };
        }
    }
}
