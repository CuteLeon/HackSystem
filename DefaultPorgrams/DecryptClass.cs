using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
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
