using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProgramTemplate;

namespace TenTenGame
{
    public class TenTenGameClass : ProgramTemplateClass
    {
        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            throw new NotImplementedException();
        }
    }
}
