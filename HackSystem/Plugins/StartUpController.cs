using System;
using StartUpTemplate;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackSystem
{
    class StartUpController
    {
        /// <summary>
        /// 启动窗口列表
        /// </summary>
        public List<StartUpTemplateClass> StartUps = new List<StartUpTemplateClass>();

        public static StartUpTemplateClass GetStartUpPlugin()
        {
            return default(StartUpTemplateClass);
        }

    }
}
