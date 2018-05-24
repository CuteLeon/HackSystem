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
        }

        public override string FileName => throw new NotImplementedException();

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm() { ImageResource = DefaultProgramResource.IronMan };
        }
    }
}
