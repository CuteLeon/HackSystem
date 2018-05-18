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
        /// <summary>
        /// 窗口集合 (允许插件打开多个程序窗口)
        /// </summary>
        public FormCollection ProgramForms { get; } = new FormCollection();

        /// <summary>
        /// 图标
        /// </summary>
        public static Icon ProgramIcon = null;
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
                NewProgramForm = default(Form);
            }
            //TODO : 需要测试
            //将新窗口添加至程序窗口集合
            ProgramForms.Add(NewProgramForm);
            //TODO : 需要测试
            //窗口关闭后从集合移除
            NewProgramForm.FormClosed += new FormClosedEventHandler(
                (s, e) => {
                    ProgramForms.Remove(NewProgramForm);
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
