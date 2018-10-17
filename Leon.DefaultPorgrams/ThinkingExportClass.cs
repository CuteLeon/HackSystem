using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ThinkingExportClass : ProgramTemplateClass
    {
        public ThinkingExportClass()
        {
            this.Name = "思维导出";
            this.Description = "思维导出 [via leon]";
            this.Icon = DefaultProgramResource.ThinkingExportIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.ThinkingExport
                );
        }
    }
}
