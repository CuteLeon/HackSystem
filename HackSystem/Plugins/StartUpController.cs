using System;
using StartUpTemplate;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace HackSystem
{
    public static class StartUpController
    {

        /// <summary>
        /// 从指定 StartUp 插件文件获取 Name 的插件实例
        /// </summary>
        /// <param name="StartUpPath">插件路径</param>
        /// <param name="StartUpName">插件名称</param>
        /// <returns></returns>
        public static StartUpTemplateClass GetStartUpPlugin(string StartUpPath, string StartUpName)
        {
            LogController.Info("获取 {0} 内的 StartUp 插件，StartUpName : {1}", StartUpPath, StartUpName);

            Assembly StartUpAssembly = AssemblyController<StartUpTemplateClass>.CreateAssembly(StartUpPath);
            if (StartUpAssembly == null) return null;

            return AssemblyController<StartUpTemplateClass>.CreateTypeInstance(StartUpAssembly, StartUpName).FirstOrDefault();
        }

        /// <summary>
        /// 扫描目录内所有 StartUp 插件；
        /// </summary>
        /// <param name="StartUpDirectory">插件目录</param>
        /// <param name="Extension">文件扩展名</param>
        /// <returns></returns>
        public static IEnumerable<StartUpTemplateClass> ScanStartUpPlugins(string StartUpDirectory, string Extension = ".dll")
        {
            LogController.Debug("扫描 StartUp 插件 : {0}", StartUpDirectory);
            if (!Directory.Exists(StartUpDirectory)) yield break;

            foreach (string StartUpPath in Directory.GetFiles(StartUpDirectory).Where(StartUpPath => StartUpPath.ToLower().EndsWith(Extension)))
            {
                LogController.Debug("发现 StartUp 插件 dll 文件 : {0}", StartUpPath);
                Assembly StartUpAssembly = AssemblyController<StartUpTemplateClass>.CreateAssembly(StartUpPath);
                if (StartUpAssembly == null) continue;
                
                foreach (StartUpTemplateClass StartUpInstance in AssemblyController<StartUpTemplateClass>.CreateTypeInstance(StartUpAssembly))
                {
                    LogController.Info("创建 StartUp 实例 : {0} ({1})", StartUpInstance.Name, StartUpInstance.Description);
                    yield return StartUpInstance;
                }
            }
        }

    }
}
