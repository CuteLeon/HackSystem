using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class GraphicOSClass : ProgramTemplateClass
    {
        public GraphicOSClass()
        {
            Name = "示波器";
            Description = "示波器 [via leon]";
            Icon = DefaultProgramResource.GraphicOSIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.GraphicOS
                );
        }
    }
}
