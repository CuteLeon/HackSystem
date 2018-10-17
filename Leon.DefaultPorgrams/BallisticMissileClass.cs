using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class BallisticMissileClass : ProgramTemplateClass
    {
        public BallisticMissileClass()
        {
            this.Name = "洲际导弹";
            this.Description = "洲际导弹 [via leon]";
            this.Icon = DefaultProgramResource.BallisticMissileIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.BallisticMissile
                );
        }
    }
}
