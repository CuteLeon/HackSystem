using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using HackSystem.ProgramTemplate;

namespace HackSystem.Host
{
    class ProgramController
    {

        /// <summary>
        /// 从指定 Program 插件文件获取 Name 的插件实例
        /// </summary>
        /// <param name="ProgramPath">插件路径</param>
        /// <param name="ProgramName">插件名称</param>
        /// <returns></returns>
        public static ProgramTemplateClass GetProgramPlugin(string ProgramPath, string ProgramName)
        {
            LogController.Info("获取 {0} 内的 Program 插件，ProgramName : {1}", ProgramPath, ProgramName);

            Assembly ProgramAssembly = AssemblyController<ProgramTemplateClass>.CreateAssembly(ProgramPath);
            if (ProgramAssembly == null) return null;

            return AssemblyController<ProgramTemplateClass>.CreateTypeInstance(ProgramAssembly, ProgramName).FirstOrDefault();
        }

        /// <summary>
        /// 扫描目录内所有 Program 插件；
        /// </summary>
        /// <param name="ProgramDirectory">插件目录</param>
        /// <param name="Extension">文件扩展名</param>
        /// <returns></returns>
        public static IEnumerable<ProgramTemplateClass> ScanProgramPlugins(string ProgramDirectory, string Extension = ".dll")
        {
            LogController.Debug("扫描 Program 插件 : {0}", ProgramDirectory);
            if (!Directory.Exists(ProgramDirectory)) yield break;

            foreach (string ProgramPath in Directory.GetFiles(ProgramDirectory).Where(ProgramPath => ProgramPath.ToLower().EndsWith(Extension)))
            {
                LogController.Debug("发现 Program 插件 dll 文件 : {0}", ProgramPath);
                Assembly ProgramAssembly = AssemblyController<ProgramTemplateClass>.CreateAssembly(ProgramPath);
                if (ProgramAssembly == null) continue;

                foreach (ProgramTemplateClass ProgramInstance in AssemblyController<ProgramTemplateClass>.CreateTypeInstance(ProgramAssembly))
                {
                    LogController.Info("创建 Program 实例 : {0} ({1})", ProgramInstance.Name, ProgramInstance.Description);
                    yield return ProgramInstance;
                }
            }
        }
    }
}
