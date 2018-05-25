using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class AttackDataClass : ProgramTemplateClass
    {
        public AttackDataClass()
        {
            Name = "攻击数据";
            Description = "攻击数据 [via leon]";
            Icon = DefaultProgramResource.AttackDataIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.AttackData
                );
        }
    }
}
