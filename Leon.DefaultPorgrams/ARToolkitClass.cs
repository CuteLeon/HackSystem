using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ARToolkitClass : ProgramTemplateClass
    {
        public ARToolkitClass()
        {
            this.Name = "增强现实";
            this.Description = "增强现实 [via leon]";
            this.Icon = DefaultProgramResource.ARToolkitIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.ARToolkit
                );
        }
    }
}
