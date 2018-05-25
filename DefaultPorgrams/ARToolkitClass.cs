using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class ARToolkitClass : ProgramTemplateClass
    {
        public ARToolkitClass()
        {
            Name = "增强现实";
            Description = "增强现实 [via leon]";
            Icon = DefaultProgramResource.ARToolkitIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.ARToolkit
                );
        }
    }
}
