using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class AgentInfoClass : ProgramTemplateClass
    {
        public AgentInfoClass()
        {
            this.Name = "特工信息";
            this.Description = "特工信息 [via leon]";
            this.Icon = DefaultProgramResource.AgentInfoIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.AgentInfo
                );
        }
    }
}
