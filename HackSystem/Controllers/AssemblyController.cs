using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HackSystem
{
    public static class AssemblyController<T> where T : class
    {
        /// <summary>
        /// 加载程序集
        /// </summary>
        /// <param name="AssemblyPath">程序集路径</param>
        /// <param name="DynamicLoad">在内存中动态加载</param>
        /// <returns>程序集</returns>
        public static Assembly CreateAssembly(string AssemblyPath, bool DynamicLoad = true)
        {
            UnityModule.DebugPrint("开始{0}加载程序集路径：{1} ...", (DynamicLoad ? "动态" : string.Empty), AssemblyPath);

            Assembly PluginAssembly = null;
            try
            {
                if (!DynamicLoad)
                {
                    // 从链接库文件路径加载
                    PluginAssembly = Assembly.LoadFrom(AssemblyPath);
                }
                else
                {
                    // 把链接库文件读入内存后从内存加载，允许程序在运行时更新链接库
                    using (FileStream AssemblyStream = new FileStream(AssemblyPath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader AssemblyReader = new BinaryReader(AssemblyStream))
                        {
                            byte[] AssemblyBuffer = AssemblyReader.ReadBytes((int)AssemblyStream.Length);
                            PluginAssembly = Assembly.Load(AssemblyBuffer);
                            AssemblyReader.Close();
                        }
                        AssemblyStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("创建程序集遇到异常：{0}", ex.Message);
                return null;
            }

            return PluginAssembly;
        }

        /// <summary>
        /// 从程序集创建指定父类派生的所有子类的实例
        /// </summary>
        /// <param name="PluginAssembly">程序集</param>
        /// <param name="TargetTypeName"></param>
        /// <returns>所有子类的实例</returns>
        public static IEnumerable<T> CreateTypeInstance(Assembly PluginAssembly, string TargetTypeName = "")
        {
            if (PluginAssembly == null) yield break;

            UnityModule.DebugPrint("在程序集 {0} 中创建所有 {1} 派生的子类实例 ...", PluginAssembly.FullName, typeof(T).ToString());
            // 仅加载范式类型派生的子类
            foreach (Type PluginType in PluginAssembly.GetTypes().Where(
                (PluginType => 
                    TargetTypeName == string.Empty ? 
                    PluginType.IsSubclassOf(typeof(T)) : 
                    PluginType.IsSubclassOf(typeof(T)) && PluginType.Name == TargetTypeName)
            ))
            {
                UnityModule.DebugPrint(">>> 可加载的插件类型 : {0}", PluginType.FullName);
                //创建允许加载类型的实例
                T PluginInstance = null;
                try
                {
                    PluginInstance = PluginAssembly.CreateInstance(PluginType.FullName) as T;
                }
                catch (Exception ex)
                {
                    UnityModule.DebugPrint("创建 {0} 类型实例遇到异常 : {1}", PluginType.FullName, ex.Message);
                    continue;
                }
                yield return PluginInstance;
            }
        }

    }
}
