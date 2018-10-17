using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class SatelliteClass : ProgramTemplateClass
    {
        public SatelliteClass()
        {
            Name = "卫星";
            Description = "卫星 [via leon]";
            Icon = DefaultProgramResource.SatelliteIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Satellite
                );
        }
    }
}
