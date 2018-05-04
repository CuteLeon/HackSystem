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
    static class AssemblyController<T> where T : class
    {
        /// <summary>  
        /// 根据全名和路径构造对象  
        /// </summary>  
        /// <param name="PluginPath">插件链接库文件路径</param>
        /// <param name="DynamicLoad">在内存中动态热加载</param>
        /// <returns>反射实例</returns>  
        public static IEnumerable<T> CreatePluginInstance(string PluginPath, bool DynamicLoad = true)
        {
            UnityModule.DebugPrint("开始创建 {0} 类型实例 {1}...", typeof(T).ToString(), DynamicLoad ? "(动态模式) " : string.Empty);
            Assembly AssemblyObject = null;

            try
            {
                if (!DynamicLoad)
                {
                    // 从链接库文件路径加载
                    AssemblyObject = Assembly.LoadFrom(PluginPath);
                }
                else
                {
                    // 把链接库文件读入内存后从内存加载，允许程序在运行时更新链接库
                    using (FileStream PluginStream = new FileStream(PluginPath, FileMode.Open, FileAccess.Read))
                    {
                        using (BinaryReader PluginReader = new BinaryReader(PluginStream))
                        {
                            byte[] PluginBuffer = PluginReader.ReadBytes((int)PluginStream.Length);
                            AssemblyObject = Assembly.Load(PluginBuffer);
                            PluginReader.Close();
                        }
                        PluginStream.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("创建程序集遇到异常：{0}", ex.Message);
                throw ex;
            }

            if (AssemblyObject == null) throw new Exception("无法加载链接库文件 : " + PluginPath);

            foreach (Type PluginType in AssemblyObject.GetTypes())
            {
                UnityModule.DebugPrint("发现类型 : {0}", PluginType.FullName);

                // 仅加载范式类型派生的子类
                if (!PluginType.IsSubclassOf(typeof(T))) continue;
                UnityModule.DebugPrint(">>> 可加载的插件类型 : {0}", PluginType.FullName);
                //创建并添加可加载类型的实例
                T PluginInstance = null;
                try
                {
                    PluginInstance = AssemblyObject.CreateInstance(PluginType.FullName) as T;
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
