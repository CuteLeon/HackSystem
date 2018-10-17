using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class DecryptClass : ProgramTemplateClass
    {
        public DecryptClass()
        {
            this.Name = "解密";
            this.Description = "解密 [via leon]";
            this.Icon = DefaultProgramResource.DecryptIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Decrypt
                );
        }
    }
}
