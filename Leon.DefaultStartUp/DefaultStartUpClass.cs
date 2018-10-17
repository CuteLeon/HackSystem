using System.Windows.Forms;

using HackSystem.StartUpTemplate;

namespace Leon.DefaultStartUp
{
    public class DefaultStartUpClass : StartUpTemplateClass
    {
        public DefaultStartUpClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠启动画面 - Leon";
            this.Preview = DefaultStartUpResource.DefaultStartUpPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateStartUpForm()
        {
            return new DefaultStartUpForm() { ParentStartUp = this };
        }
    }
}
