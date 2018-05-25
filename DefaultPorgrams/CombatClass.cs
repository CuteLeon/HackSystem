using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class CombatClass : ProgramTemplateClass
    {
        public CombatClass()
        {
            Name = "战役";
            Description = "战役 [via leon]";
            Icon = DefaultProgramResource.CombatIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Combat
                );
        }
    }
}
