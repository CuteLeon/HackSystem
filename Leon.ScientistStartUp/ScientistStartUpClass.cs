using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.ScientistStartUp
{
    public class ScientistStartUpClass : StartUpTemplateClass
    {

        public ScientistStartUpClass()
        {
            this.Name = "科学家";
            this.Description = "科学家 - Leon";
            this.Preview = ScientistStartUpResource.ScientistStartUpPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new ScientistStartUpForm() { ParentStartUp = this };
        }

    }
}
