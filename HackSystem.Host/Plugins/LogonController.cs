using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using HackSystem.LogonTemplate;

namespace HackSystem.Host
{
    class LogonController
    {
        /// <summary>
        /// 从指定 Logon 插件文件获取 Name 的插件实例
        /// </summary>
        /// <param name="LogonPath">插件路径</param>
        /// <param name="LogonName">插件名称</param>
        /// <returns></returns>
        public static LogonTemplateClass GetLogonPlugin(string LogonPath, string LogonName)
        {
            LogController.Info("获取 {0} 内的 Logon 插件，LogonName : {1}", LogonPath, LogonName);

            Assembly LogonAssembly = AssemblyController<LogonTemplateClass>.CreateAssembly(LogonPath);
            if (LogonAssembly == null) return null;

            return AssemblyController<LogonTemplateClass>.CreateTypeInstance(LogonAssembly, LogonName).FirstOrDefault();
        }

        /// <summary>
        /// 扫描目录内所有 Logon 插件；
        /// </summary>
        /// <param name="LogonDirectory">插件目录</param>
        /// <param name="Extension">文件扩展名</param>
        /// <returns></returns>
        public static IEnumerable<LogonTemplateClass> ScanLogonPlugins(string LogonDirectory, string Extension = ".dll")
        {
            LogController.Debug("扫描 Logon 插件 : {0}", LogonDirectory);
            if (!Directory.Exists(LogonDirectory)) yield break;

            foreach (string LogonPath in Directory.GetFiles(LogonDirectory).Where(LogonPath => LogonPath.ToLower().EndsWith(Extension)))
            {
                LogController.Debug("发现 Logon 插件 dll 文件 : {0}", LogonPath);
                Assembly LogonAssembly = AssemblyController<LogonTemplateClass>.CreateAssembly(LogonPath);
                if (LogonAssembly == null) continue;

                foreach (LogonTemplateClass LogonInstance in AssemblyController<LogonTemplateClass>.CreateTypeInstance(LogonAssembly))
                {
                    LogController.Info("创建 Logon 实例 : {0} ({1})", LogonInstance.Name, LogonInstance.Description);
                    yield return LogonInstance;
                }
            }
        }
    }
}
