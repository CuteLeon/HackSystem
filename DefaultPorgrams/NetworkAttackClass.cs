using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class NetworkAttackClass : ProgramTemplateClass
    {
        public NetworkAttackClass()
        {
            Name = "网络攻击";
            Description = "网络攻击 [via leon]";
            Icon = DefaultProgramResource.NetworkAttackIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.NetworkAttack
                );
        }
    }
}
