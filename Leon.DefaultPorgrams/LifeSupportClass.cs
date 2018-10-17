using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class LifeSupportClass : ProgramTemplateClass
    {
        public LifeSupportClass()
        {
            Name = "生命维持";
            Description = "生命维持 [via leon]";
            Icon = DefaultProgramResource.LifeSupportIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.LifeSupport
                );
        }
    }
}
