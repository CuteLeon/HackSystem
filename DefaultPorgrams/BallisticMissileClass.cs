using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class BallisticMissileClass : ProgramTemplateClass
    {
        public BallisticMissileClass()
        {
            Name = "洲际导弹";
            Description = "洲际导弹 [via leon]";
            Icon = DefaultProgramResource.BallisticMissileIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.BallisticMissile
                );
        }
    }
}
