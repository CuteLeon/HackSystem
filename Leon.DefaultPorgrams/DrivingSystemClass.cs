using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class DrivingSystemClass : ProgramTemplateClass
    {
        public DrivingSystemClass()
        {
            Name = "驾驶系统";
            Description = "驾驶系统 [via leon]";
            Icon = DefaultProgramResource.DrivingSystemIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.DrivingSystem
                );
        }
    }
}
