using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class DigitalRainClass : ProgramTemplateClass
    {
        public DigitalRainClass()
        {
            this.Name = "数码雨";
            this.Description = "数码雨 [via leon]";
            this.Icon = DefaultProgramResource.DigitalRainIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.DigitalRain
                );
        }
    }
}
