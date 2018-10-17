using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class AttackDataClass : ProgramTemplateClass
    {
        public AttackDataClass()
        {
            this.Name = "攻击数据";
            this.Description = "攻击数据 [via leon]";
            this.Icon = DefaultProgramResource.AttackDataIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.AttackData
                );
        }
    }
}
