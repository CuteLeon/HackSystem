using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class AirDefenceClass : ProgramTemplateClass
    {
        public AirDefenceClass()
        {
            Name = "防空系统";
            Description = "防空系统 [via leon]";
            Icon = DefaultProgramResource.AirDefenceIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.AirDefence
                );
        }
    }
}
