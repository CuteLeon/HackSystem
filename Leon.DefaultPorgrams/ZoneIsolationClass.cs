using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ZoneIsolationClass : ProgramTemplateClass
    {
        public ZoneIsolationClass()
        {
            this.Name = "区域隔离";
            this.Description = "区域隔离 [via leon]";
            this.Icon = DefaultProgramResource.ZoneIsolationIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.ZoneIsolation
                );
        }
    }
}
