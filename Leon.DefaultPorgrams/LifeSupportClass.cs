using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class LifeSupportClass : ProgramTemplateClass
    {
        public LifeSupportClass()
        {
            this.Name = "生命维持";
            this.Description = "生命维持 [via leon]";
            this.Icon = DefaultProgramResource.LifeSupportIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.LifeSupport
                );
        }
    }
}
