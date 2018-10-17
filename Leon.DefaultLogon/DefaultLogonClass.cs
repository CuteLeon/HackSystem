using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace Leon.DefaultLogon
{
    public class DefaultLogonClass : LogonTemplateClass
    {
        public DefaultLogonClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠登录画面 - Leon";
            this.Preview = DefaultLogonResource.DefaultLogonPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLogonForm()
        {
            return new DefaultLogonForm() { ParentLogon = this };
        }
        
    }
}
