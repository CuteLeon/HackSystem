using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class Face3DModelClass : ProgramTemplateClass
    {
        public Face3DModelClass()
        {
            this.Name = "面部模型";
            this.Description = "面部模型 [via leon]";
            this.Icon = DefaultProgramResource.Face3DModelIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                this.Name,
                this.Icon,
                DefaultProgramResource.Face3DModel
                );
        }
    }
}
