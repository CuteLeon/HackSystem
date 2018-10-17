using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class GraphicOSClass : ProgramTemplateClass
    {
        public GraphicOSClass()
        {
            this.Name = "示波器";
            this.Description = "示波器 [via leon]";
            this.Icon = DefaultProgramResource.GraphicOSIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.GraphicOS
                );
        }
    }
}
