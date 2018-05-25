using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTemplate;
using System.Windows.Forms;

namespace DefaultPorgrams
{
    public class UAVCameraClass : ProgramTemplateClass
    {
        public UAVCameraClass()
        {
            Name = "无人机";
            Description = "无人机 [via leon]";
            Icon = DefaultProgramResource.UAVCameraIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.UAVCamera
                );
        }
    }
}
