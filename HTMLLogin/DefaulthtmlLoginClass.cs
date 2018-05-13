using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LoginTemplate;
using System.IO;

namespace HTMLLogin
{
    public class DefaultHTMLLoginClass : LoginTemplateClass
    {

        public DefaultHTMLLoginClass()
        {
            Name = "浅绿";
            Description = "浅绿登录画面-Leon";
            Preview = HTMLLoginResource.DefaultHTMLLoginPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLoginForm()
        {
            return new HTMLLoginForm() { ParentLogin = this, HTMLStream = new MemoryStream(Encoding.UTF8.GetBytes(HTMLLoginResource.DefaultHTMLLogin)) };
        }
    }
}
