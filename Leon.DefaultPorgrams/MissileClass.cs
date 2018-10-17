using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class MissileClass : ProgramTemplateClass
    {
        public MissileClass()
        {
            this.Name = "发射导弹";
            this.Description = "发射导弹 [via leon]";
            this.Icon = DefaultProgramResource.MissileIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Missile
                );
        }
    }
}
