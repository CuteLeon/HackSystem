using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class NetworkAttackClass : ProgramTemplateClass
    {
        public NetworkAttackClass()
        {
            this.Name = "网络攻击";
            this.Description = "网络攻击 [via leon]";
            this.Icon = DefaultProgramResource.NetworkAttackIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.NetworkAttack
                );
        }
    }
}
