using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoginTemplate;
using System.Windows.Forms;

namespace DefaultLogin
{
    public class DefaultLoginClass : LoginTemplateClass
    {
        public DefaultLoginClass()
        {
            this.Name = "钢铁侠";
            this.Description = "钢铁侠登录画面 - Leon";
            this.Preview = DefaultLoginResource.DefaultLoginPreview;
        }

        public override string FileName => System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName;

        protected override Form CreateLoginForm()
        {
            return new DefaultLoginForm() { ParentLogin = this };
        }
        
    }
}
