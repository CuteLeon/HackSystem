using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class DrivingSystemClass : ProgramTemplateClass
    {
        public DrivingSystemClass()
        {
            this.Name = "驾驶系统";
            this.Description = "驾驶系统 [via leon]";
            this.Icon = DefaultProgramResource.DrivingSystemIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.DrivingSystem
                );
        }
    }
}
