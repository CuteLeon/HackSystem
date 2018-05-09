using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HackSystem
{
    /// <summary>
    /// 全局模块
    /// </summary>
    public static class UnityModule
    {

        /// <summary>
        /// 启动窗口插件目录
        /// </summary>
        public static readonly string StartUpDirectory = FileController.PathCombine(Environment.CurrentDirectory,"StartUps");
        /// <summary>
        /// 日志文件目录
        /// </summary>
        public static readonly string LogDirectory = FileController.PathCombine(Environment.CurrentDirectory, "Logs");

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprc, IntPtr hrgn, uint flags);
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        public const int HT_CAPTION = 0x2;
        public const int WM_SIZE = 0x5;
        public const int WM_NCCALCSIZE = 0x83;
        public const int WM_PAINT = 0xF;
        public const int WM_NCPAINT = 0x85;
        public const int WM_NCACTIVATE = 0x86;
        public const int WM_NCMOUSEMOVE = 0xA0;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int WM_NCLBUTTONUP = 0xA2;
        public const int WM_NCLBUTTONDBCLK = 0xA3;
        public const int WM_NCRBUTTONDOWN = 0xA4;
        public const int WM_NCRBUTTONUP = 0xA5;
        public const int WM_NCRBUTTONDBCLK = 0xA6;
        public const int WM_NCMOUSEHOVER = 0x2A0;
        public const int WM_NCMOUSELEAVE = 0x2A2;
        //鼠标拖动边框相关常量
        public const int WM_NCHITTEST = 0x0084;
        public const int HT_LEFT = 10;
        public const int HT_RIGHT = 11;
        public const int HT_TOP = 12;
        public const int HT_TOPLEFT = 13;
        public const int HT_TOPRIGHT = 14;
        public const int HT_BOTTOM = 15;
        public const int HT_BOTTOMLEFT = 16;
        public const int HT_BOTTOMRIGHT = 17;

        //REDRAW
        public const int SIZE_MAXIMIZED = 0x2;
        public const uint RDW_INVALIDATE = 0x1;
        public const uint RDW_IUPDATENOW = 0x100;
        public const uint RDW_FRAME = 0x400;

        /// <summary>
        /// 注册以帮助鼠标拖动无边框窗体
        /// </summary>
        public static void MoveFormViaMouse(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage((sender is Form ? (sender as Form).Handle : (sender as Control).FindForm().Handle), WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// 封装的函数以输出调试信息
        /// </summary>
        /// <param name="DebugMessage">调试信息</param>
        public static void DebugPrint(string DebugMessage)
        {
            string Message = string.Format("{0}    {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), DebugMessage);
            Debug.Print(Message);
            LogController.
        }

        /// <summary>
        /// 封装的函数以输出调试信息
        /// </summary>
        /// <param name="DebugMessage">调试信息</param>
        /// <param name="DebugValue">调试信息的值</param>
        public static void DebugPrint(string DebugMessage, params object[] DebugValue)
        {
            DebugPrint(string.Format(DebugMessage, DebugValue));
        }

    }
}
