using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class AirDefenceClass : ProgramTemplateClass
    {
        public AirDefenceClass()
        {
            this.Name = "防空系统";
            this.Description = "防空系统 [via leon]";
            this.Icon = DefaultProgramResource.AirDefenceIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.AirDefence
                );
        }
    }
}
