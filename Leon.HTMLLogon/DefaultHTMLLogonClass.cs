using System.IO;
using System.Text;
using System.Windows.Forms;

using HackSystem.LogonTemplate;

namespace Leon.HTMLLogon
{
    public class DefaultHTMLLogonClass : LogonTemplateClass
    {

        public DefaultHTMLLogonClass()
        {
            this.Name = "浅绿";
            this.Description = "浅绿登录画面-Leon";
            this.Preview = HTMLLogonResource.DefaultHTMLLogonPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLogonForm()
        {
            return new HTMLLogonForm() { ParentLogon = this, HTMLStream = new MemoryStream(Encoding.UTF8.GetBytes(HTMLLogonResource.DefaultHTMLLogon)) };
        }
    }
}
