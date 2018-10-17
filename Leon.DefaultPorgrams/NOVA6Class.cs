using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class NOVA6Class : ProgramTemplateClass
    {
        public NOVA6Class()
        {
            Name = "NOVA6";
            Description = "NOVA6 [via leon]";
            Icon = DefaultProgramResource.NOVA6Icon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.NOVA6
                );
        }
    }
}
