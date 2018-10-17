using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ThreeDMapClass : ProgramTemplateClass
    {
        public ThreeDMapClass()
        {
            Name = "3D地图";
            Description = "3D地图 [via leon]";
            Icon = DefaultProgramResource.ThreeDMapIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.ThreeDMap
                );
        }
    }
}
