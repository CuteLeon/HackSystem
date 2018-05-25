using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class IronManClass : ProgramTemplateClass
    {
        public IronManClass()
        {
            Name = "钢铁侠";
            Description = "钢铁侠 [via leon]";
            Icon = DefaultProgramResource.IronManIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                "钢铁侠",
                DefaultProgramResource.IronManIcon,
                DefaultProgramResource.IronMan
                );
        }
    }
}
