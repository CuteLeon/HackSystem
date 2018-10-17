using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.ScientistStartUp
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
