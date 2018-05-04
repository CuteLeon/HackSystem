using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HackSystem
{
    public static class ConfigController
    {
        private static readonly Configuration UnityConfig = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
        
        /// <summary>
        /// 装填默认配置
        /// </summary>
        public static void LoadDefaultConfig()
        {
            UnityModule.DebugPrint("装载默认配置信息...");

            //TODO : 默认配置写在这里
            Dictionary<string, string> DefaultConfigDictionary = new Dictionary<string, string>();
            DefaultConfigDictionary.Add("StartUpFile", "DefaultStartUp.dll");
            DefaultConfigDictionary.Add("StartUpName", "DefaultStartUpClass");
            DefaultConfigDictionary.Add("UserName", "Leon");
            DefaultConfigDictionary.Add("Password", "123456");
            DefaultConfigDictionary.Add("HeadPortrait", "123456");

            foreach (string Key in DefaultConfigDictionary.Keys)
            {
                if (!UnityConfig.AppSettings.Settings.AllKeys.Contains(Key))
                    UnityConfig.AppSettings.Settings.Add(Key, DefaultConfigDictionary[Key]);
            }

            UnityConfig.Save();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="Key">配置Key</param>
        /// <returns>配置信息</returns>
        public static string GetConfig(string Key)
        {
            UnityModule.DebugPrint("获取配置项Key: {0}", Key);
            try
            {
                KeyValueConfigurationElement ConfigurationElement = UnityConfig.AppSettings.Settings[Key];
                return ConfigurationElement?.Value ?? string.Empty;
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("读取配置失败 : Key = {0}, Message:{1}",Key, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="Key">配置Key</param>
        /// <param name="Value">配置Value</param>
        public static void SetConfig(string Key, string Value)
        {
            UnityModule.DebugPrint("设置配置项Key:{0}, Value:{1}", Key, Value);
            try
            {
                UnityConfig.AppSettings.Settings.Remove(Key);
                UnityConfig.AppSettings.Settings.Add(Key, Value);
                UnityConfig.Save();
            }
            catch (Exception ex)
            {
                UnityModule.DebugPrint("设置配置失败.Key:{0}, Value:{1}, Message:{2}", Key, Value, ex.Message);
            }
        }

    }
}
