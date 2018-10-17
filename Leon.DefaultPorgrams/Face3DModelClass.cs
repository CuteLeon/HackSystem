using System.Windows.Forms;

using HackSystem.ProgramTemplate;

namespace Leon.DefaultPorgrams
{
    public class Face3DModelClass : ProgramTemplateClass
    {
        public Face3DModelClass()
        {
            Name = "面部模型";
            Description = "面部模型 [via leon]";
            Icon = DefaultProgramResource.Face3DModelIcon;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateProgramForm()
        {
            return new DefaultProgramForm(
                Name,
                Icon,
                DefaultProgramResource.Face3DModel
                );
        }
    }
}
