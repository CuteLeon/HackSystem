using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class ThinkingExportClass : ProgramTemplateClass
    {
        public ThinkingExportClass()
        {
            Name = "思维导出";
            Description = "思维导出 [via leon]";
            Icon = DefaultProgramResource.ThinkingExportIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.ThinkingExport
                );
        }
    }
}
