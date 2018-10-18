using System.Drawing;
using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace Leon.DefaultLogon
{
    public class DefaultLogonClass : LogonTemplateClass
    {
        public override string Name { get; protected set; } = "钢铁侠";

        public override string Description { get; protected set; } = "钢铁侠登录画面 - Leon";

        public override Image Preview { get; protected set; } = DefaultLogonResource.DefaultLogonPreview;

        public DefaultLogonClass()
        {
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLogonForm()
        {
            return new DefaultLogonForm() { ParentLogon = this };
        }

    }
}
