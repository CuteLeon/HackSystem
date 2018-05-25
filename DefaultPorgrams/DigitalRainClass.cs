using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class DigitalRainClass : ProgramTemplateClass
    {
        public DigitalRainClass()
        {
            Name = "数码雨";
            Description = "数码雨 [via leon]";
            Icon = DefaultProgramResource.DigitalRainIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.DigitalRain
                );
        }
    }
}
