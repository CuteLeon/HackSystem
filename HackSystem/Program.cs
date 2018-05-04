using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using StartUpTemplate;

namespace HackSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //初始化目录
            CheckDirectory();
            //补齐默认配置
            ConfigController.LoadDefaultConfig();

            StartUpTemplateClass.UserName = ConfigController.GetConfig("UserName");
            StartUpTemplateClass.Password = ConfigController.GetConfig("Password");
            StartUpTemplateClass.HeadPortrait = null;// ConfigController.GetConfig("HeadPortrait");

            foreach (StartUpTemplateClass ins in AssemblyController<StartUpTemplateClass>.CreatePluginInstance(@"D:\CSharp\HackSystem\Debug\StartUps\DefaultStartUp.dll"))
            {
                MessageBox.Show(ins.Name + "\n" + ins.Description);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartUpForm());
        }

        /// <summary>
        /// 初始化目录
        /// </summary>
        private static void CheckDirectory()
        {
            //TODO : 程序需要的目录在这里初始化
            try
            {
                if (!Directory.Exists(UnityModule.StartUpDirectory))
                    Directory.CreateDirectory(UnityModule.StartUpDirectory);
            }catch(Exception ex)
            {
                UnityModule.DebugPrint("创建工作目录遇到异常：{0}", ex.Message);
            }
        }
    }
}
