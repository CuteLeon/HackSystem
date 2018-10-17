using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class DecryptClass : ProgramTemplateClass
    {
        public DecryptClass()
        {
            Name = "解密";
            Description = "解密 [via leon]";
            Icon = DefaultProgramResource.DecryptIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Decrypt
                );
        }
    }
}
