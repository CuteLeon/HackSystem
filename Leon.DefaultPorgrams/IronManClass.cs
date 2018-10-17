using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class IronManClass : ProgramTemplateClass
    {
        public IronManClass()
        {
            Name = "钢铁侠";
            Description = "钢铁侠 [via leon]";
            Icon = DefaultProgramResource.IronManIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.IronMan
                );
        }
    }
}
