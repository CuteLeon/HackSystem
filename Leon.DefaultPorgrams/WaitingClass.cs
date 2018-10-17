using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class WaitingClass : ProgramTemplateClass
    {
        public WaitingClass()
        {
            Name = "等待";
            Description = "等待 [via leon]";
            Icon = DefaultProgramResource.WaitingIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Waiting
                );
        }
    }
}
