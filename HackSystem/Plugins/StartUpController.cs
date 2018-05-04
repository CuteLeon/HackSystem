using System;
using StartUpTemplate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace HackSystem
{
    class StartUpController
    {
        /// <summary>
        /// 启动窗口列表
        /// </summary>
        public static List<StartUpTemplateClass> StartUpList = new List<StartUpTemplateClass>();

        public static void GetStartUpPlugin(string StartUpDirectory)
        {
            UnityModule.DebugPrint("扫描 StartUp 插件 : {0}", StartUpDirectory);

            foreach (string StartUpPath in Directory.GetFiles(StartUpDirectory).Where(StartUpPath => StartUpPath.ToLower().EndsWith(".dll")))
            {
                UnityModule.DebugPrint("发现 StartUp 插件 dll 文件 : {0}", StartUpPath);
                Assembly StartUpAssembly = AssemblyController<StartUpTemplateClass>.CreateAssembly(StartUpPath);
                if (StartUpAssembly == null) continue;

                foreach (StartUpTemplateClass StartUpInstance in AssemblyController<StartUpTemplateClass>.CreateTypeInstance(StartUpAssembly))
                {
                    UnityModule.DebugPrint("创建 StartUp 实例 : {0} ({1})", StartUpInstance.Name, StartUpInstance.Description);
                    StartUpList.Add(StartUpInstance);
                }
            }
        }

    }
}
