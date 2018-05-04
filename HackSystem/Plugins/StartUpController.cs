using System;
using StartUpTemplate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HackSystem
{
    class StartUpController
    {
        /// <summary>
        /// 启动窗口列表
        /// </summary>
        public List<StartUpTemplateClass> StartUpList = new List<StartUpTemplateClass>();

        public static void GetStartUpPlugin(string StartUpDirectory)
        {
            UnityModule.DebugPrint("扫描 StartUp 插件 : {0}", StartUpDirectory);

            foreach (string StartUpPath in Directory.GetFiles(StartUpDirectory).Where(x=>x.EndsWith(".dll")))
            {
                UnityModule.DebugPrint("发现 StartUp 插件 dll 文件 : {0}", StartUpPath);
            }
        }

    }
}
