using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class CombatClass : ProgramTemplateClass
    {
        public CombatClass()
        {
            this.Name = "战役";
            this.Description = "战役 [via leon]";
            this.Icon = DefaultProgramResource.CombatIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Combat
                );
        }
    }
}
