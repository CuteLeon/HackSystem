﻿using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class MissileClass : ProgramTemplateClass
    {
        public MissileClass()
        {
            Name = "发射导弹";
            Description = "发射导弹 [via leon]";
            Icon = DefaultProgramResource.MissileIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Missile
                );
        }
    }
}
