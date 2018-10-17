using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class WaitingClass : ProgramTemplateClass
    {
        public WaitingClass()
        {
            this.Name = "等待";
            this.Description = "等待 [via leon]";
            this.Icon = DefaultProgramResource.WaitingIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Waiting
                );
        }
    }
}
