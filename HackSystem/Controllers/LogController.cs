using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualBasic.Logging;

namespace HackSystem
{
    /// <summary>
    /// 日志控制器
    /// </summary>
    public static class LogController
    {
        
        /// <summary>
        /// 日志类型枚举
        /// </summary>
        public enum LogTypes
        {
            /// <summary>
            /// 调试
            /// </summary>
            DEBUG=0,
            /// <summary>
            /// 信息
            /// </summary>
            INFO=1,
            /// <summary>
            /// 警告
            /// </summary>
            WARN=2,
            /// <summary>
            /// 错误
            /// </summary>
            ERROR=3,
            /// <summary>
            /// 致命
            /// </summary>
            FATAL=4
        }

        /// <summary>
        /// 输出至文件的日志监听器
        /// </summary>
        private static FileLogTraceListener LogListener = null;
        
        /// <summary>
        /// 日志控制器
        /// </summary>
        /// <param name="LogDirectory">日志文件目录</param>
        /// <param name="LogFileName">日志文件名称前缀</param>
        public static void CreateLogListener(string LogDirectory, string LogFileName)
        {
            CloseLogListener(); //仅做尝试

            LogListener = new FileLogTraceListener("UnityLogListener")
            {
                DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.DiscardMessages,
                BaseFileName = LogFileName,
                Location = LogFileLocation.Custom,
                CustomLocation = LogDirectory,
                AutoFlush = true,
                Encoding = Encoding.UTF8,
                LogFileCreationSchedule = LogFileCreationScheduleOption.Daily,
                MaxFileSize = 10 * 1024 * 1024,
                IndentSize = 4,

                // ???
                TraceOutputOptions = TraceOptions.Timestamp
            };

            Debug.Print(LogListener.FullLogFileName);
            LogListener.WriteLine(LogListener.MaxFileSize);
            LogListener.WriteLine("测试信息1");
            LogListener.WriteLine("测试信息2");
            LogListener.WriteLine("测试信息3");
        }

        public static void WriteLine(string LogMessage, LogTypes LogType, params object[] LogValues)
        {
            WriteLine(string.Format(LogMessage, LogValues), LogType);
        }

        public static void WriteLine(string LogMessage, LogTypes LogType)
        {
            LogListener?.WriteLine(string.Format(
                "{0}  [{1}]  {2}",
                DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"),
                LogType.ToString(),
                LogMessage
                ));
        }

        public static void CloseLogListener()
        {
            Debug.Print("关闭日志记录器 ...");
            LogListener?.Close();
            LogListener?.Dispose();
        }

    }
}
