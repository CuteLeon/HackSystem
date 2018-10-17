using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class UAVCameraClass : ProgramTemplateClass
    {
        public UAVCameraClass()
        {
            this.Name = "无人机";
            this.Description = "无人机 [via leon]";
            this.Icon = DefaultProgramResource.UAVCameraIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.UAVCamera
                );
        }
    }
}
