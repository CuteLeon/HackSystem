using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class AgentInfoClass : ProgramTemplateClass
    {
        public AgentInfoClass()
        {
            Name = "特工信息";
            Description = "特工信息 [via leon]";
            Icon = DefaultProgramResource.AgentInfoIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.AgentInfo
                );
        }
    }
}
