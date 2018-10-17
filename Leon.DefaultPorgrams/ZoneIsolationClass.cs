using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ZoneIsolationClass : ProgramTemplateClass
    {
        public ZoneIsolationClass()
        {
            Name = "区域隔离";
            Description = "区域隔离 [via leon]";
            Icon = DefaultProgramResource.ZoneIsolationIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.ZoneIsolation
                );
        }
    }
}
