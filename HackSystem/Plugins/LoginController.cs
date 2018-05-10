using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using LoginTemplate;

namespace HackSystem
{
    class LoginController
    {
        /// <summary>
        /// 从指定 Login 插件文件获取 Name 的插件实例
        /// </summary>
        /// <param name="LoginPath">插件路径</param>
        /// <param name="LoginName">插件名称</param>
        /// <returns></returns>
        public static LoginTemplateClass GetLoginPlugin(string LoginPath, string LoginName)
        {
            LogController.Info("获取 {0} 内的 Login 插件，LoginName : {1}", LoginPath, LoginName);

            Assembly LoginAssembly = AssemblyController<LoginTemplateClass>.CreateAssembly(LoginPath);
            if (LoginAssembly == null) return null;

            return AssemblyController<LoginTemplateClass>.CreateTypeInstance(LoginAssembly, LoginName).FirstOrDefault();
        }

        /// <summary>
        /// 扫描目录内所有 Login 插件；
        /// </summary>
        /// <param name="LoginDirectory">插件目录</param>
        /// <param name="Extension">文件扩展名</param>
        /// <returns></returns>
        public static IEnumerable<LoginTemplateClass> ScanLoginPlugins(string LoginDirectory, string Extension = ".dll")
        {
            LogController.Debug("扫描 Login 插件 : {0}", LoginDirectory);
            if (!Directory.Exists(LoginDirectory)) yield break;

            foreach (string LoginPath in Directory.GetFiles(LoginDirectory).Where(LoginPath => LoginPath.ToLower().EndsWith(Extension)))
            {
                LogController.Debug("发现 Login 插件 dll 文件 : {0}", LoginPath);
                Assembly LoginAssembly = AssemblyController<LoginTemplateClass>.CreateAssembly(LoginPath);
                if (LoginAssembly == null) continue;

                foreach (LoginTemplateClass LoginInstance in AssemblyController<LoginTemplateClass>.CreateTypeInstance(LoginAssembly))
                {
                    LogController.Info("创建 Login 实例 : {0} ({1})", LoginInstance.Name, LoginInstance.Description);
                    yield return LoginInstance;
                }
            }
        }
    }
}
