using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StartUpTemplate;

namespace AnimationStartUp
{
    public class PeriodicLatticeStartUpClass : StartUpTemplateClass
    {
        public PeriodicLatticeStartUpClass()
        {
            Name = "点阵";
            Description = "点阵启动画面 - Leon";
            Preview = StartUpResource.PeriodicLatticePreview;
        }

        protected override Form CreateStartUpForm()
        {
            return new CommonAnimationForm(StartUpResource.PeriodicLattice, Color.FromArgb(255, 38, 38, 38)) { ParentStartUp = this, Padding = new Padding(50, 150, 50, 50), ForeColor = Color.FromArgb(220, 220, 210) };
        }
    }
}
