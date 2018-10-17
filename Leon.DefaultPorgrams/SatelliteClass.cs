using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class SatelliteClass : ProgramTemplateClass
    {
        public SatelliteClass()
        {
            this.Name = "卫星";
            this.Description = "卫星 [via leon]";
            this.Icon = DefaultProgramResource.SatelliteIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Satellite
                );
        }
    }
}
