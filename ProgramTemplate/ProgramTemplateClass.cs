using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramTemplate
{
    public abstract class ProgramTemplateClass : IDisposable
    {
        /* TODO: 右键菜单对象(可为空)，系统桌面图标被右击时弹出
         * 固定菜单项：
         *      新建窗口
         *      ————————
         *      关闭所有已打开窗口
         */

        /// <summary>
        /// 窗口集合 (允许插件打开多个程序窗口)
        /// </summary>
        public FormCollection ProgramForms { get; } = new FormCollection();

        private Image _icon = ProgramResource.DefaultProgramIcon;
        /// <summary>
        /// 图标
        /// </summary>
        public Image Icon
        {
            get => _icon;
            set
            {
                if (value == null)
                {
                    _icon = null;
                }
                else
                {
                    _icon = new Bitmap(value, new Size(64, 64));
                }
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; protected set; } = string.Empty;
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; protected set; } = string.Empty;

        /// <summary>
        /// 程序集所在文件名称
        /// </summary>
        public abstract string FileName { get; }

        /// <summary>
        /// 构造程序窗口
        /// </summary>
        /// <returns>程序窗口</returns>
        public virtual Form GetNewProgramForm()
        {
            Form NewProgramForm = CreateProgramForm();
            if (NewProgramForm == null)
            {
                NewProgramForm = new Form();
            }
            //将新窗口添加至程序窗口集合
            ProgramForms.Add(NewProgramForm);
            System.Diagnostics.Debug.Print("增加新窗口：{0}，窗口列表总数：{1}", NewProgramForm.GetHashCode().ToString("X"), ProgramForms.Count);
            //窗口关闭后从集合移除
            NewProgramForm.FormClosed += new FormClosedEventHandler(
                (s, e) => {
                    ProgramForms.Remove(s as Form);
                    System.Diagnostics.Debug.Print("关闭窗口：{0}，窗口列表总数：{1}", (s as Form).GetHashCode().ToString("X"), ProgramForms.Count);
                });

            return NewProgramForm;
        }

        protected abstract Form CreateProgramForm();

        ~ProgramTemplateClass()
        {
            (this as IDisposable).Dispose();
        }

        void IDisposable.Dispose()
        {
            System.Diagnostics.Debug.Print("Dispose Program : {0}", this.Name);
            //TODO : 需要测试
            foreach(Form programForm in ProgramForms)
            {
                programForm.Close();
            }
            GC.SuppressFinalize(this);
        }

    }
}
