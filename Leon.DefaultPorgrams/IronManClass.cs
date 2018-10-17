using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class IronManClass : ProgramTemplateClass
    {
        public IronManClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠 [via leon]";
            this.Icon = DefaultProgramResource.IronManIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.IronMan
                );
        }
    }
}
