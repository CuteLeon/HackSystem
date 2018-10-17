using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ActionIndicationClass : ProgramTemplateClass
    {
        public ActionIndicationClass()
        {
            Name = "行动指示";
            Description = "行动指示 [via leon]";
            Icon = DefaultProgramResource.ActionIndicationIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.ActionIndication
                );
        }
    }
}
