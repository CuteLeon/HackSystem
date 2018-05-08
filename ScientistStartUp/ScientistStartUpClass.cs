using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StartUpTemplate;
using System.Windows.Forms;

namespace ScientistStartUp
{
    public class ScientistStartUpClass : StartUpTemplateClass
    {

        public ScientistStartUpClass()
        {
            Name = "科学家";
            Description = "科学家 - Leon";
            Preview = ScientistStartUpResource.ScientistStartUpPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new ScientistStartUpForm() { ParentStartUp = this };
        }

    }
}
