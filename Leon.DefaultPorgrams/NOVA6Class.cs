using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class NOVA6Class : ProgramTemplateClass
    {
        public NOVA6Class()
        {
            this.Name = "NOVA6";
            this.Description = "NOVA6 [via leon]";
            this.Icon = DefaultProgramResource.NOVA6Icon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.NOVA6
                );
        }
    }
}
