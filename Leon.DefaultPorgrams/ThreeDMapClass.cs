using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ThreeDMapClass : ProgramTemplateClass
    {
        public ThreeDMapClass()
        {
            this.Name = "3D地图";
            this.Description = "3D地图 [via leon]";
            this.Icon = DefaultProgramResource.ThreeDMapIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.ThreeDMap
                );
        }
    }
}
