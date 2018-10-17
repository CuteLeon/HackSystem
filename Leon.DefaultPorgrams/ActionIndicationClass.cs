using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ActionIndicationClass : ProgramTemplateClass
    {
        public ActionIndicationClass()
        {
            this.Name = "行动指示";
            this.Description = "行动指示 [via leon]";
            this.Icon = DefaultProgramResource.ActionIndicationIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.ActionIndication
                );
        }
    }
}
