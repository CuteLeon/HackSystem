using System;
using System.Collections;
using System.Windows.Forms;

namespace HackSystem.ProgramTemplate
{
    /// <summary>
    /// 窗口只读集合
    /// </summary>
    public class FormCollection : ReadOnlyCollectionBase
    {
        /// <summary>
        /// 获取集合内指定名称的对象
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>指定名称的对象</returns>
        public virtual Form this[string name]
        {
            get
            {
                if (name != null)
                {
                    object collectionSyncRoot = FormCollection.CollectionSyncRoot;
                    lock (collectionSyncRoot)
                    {
                        foreach (Form form in base.InnerList)
                        {
                            if (string.Equals(form.Name, name, StringComparison.OrdinalIgnoreCase))
                            {
                                return form;
                            }
                        }
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 获取集合内指定标示的对象
        /// </summary>
        /// <param name="index">标示</param>
        /// <returns>指定标示的对象</returns>
        public virtual Form this[int index]
        {
            get
            {
                Form result = null;
                object collectionSyncRoot = FormCollection.CollectionSyncRoot;
                lock (collectionSyncRoot)
                {
                    result = (Form)base.InnerList[index];
                }
                return result;
            }
        }

        /// <summary>
        /// 添加对象至集合
        /// </summary>
        /// <param name="form"></param>
        internal void Add(Form form)
        {
            object collectionSyncRoot = FormCollection.CollectionSyncRoot;
            lock (collectionSyncRoot)
            {
                base.InnerList.Add(form);
            }
        }

        /// <summary>
        /// 判断集合是否包含目标对象
        /// </summary>
        /// <param name="form">目标</param>
        /// <returns>是否包含</returns>
        internal bool Contains(Form form)
        {
            bool result = false;
            object collectionSyncRoot = FormCollection.CollectionSyncRoot;
            lock (collectionSyncRoot)
            {
                result = base.InnerList.Contains(form);
            }
            return result;
        }

        /// <summary>
        /// 从集合移除目标对象
        /// </summary>
        /// <param name="form">目标对象</param>
        internal void Remove(Form form)
        {
            object collectionSyncRoot = FormCollection.CollectionSyncRoot;
            lock (collectionSyncRoot)
            {
                base.InnerList.Remove(form);
            }
        }

        /// <summary>
        /// 异步锁芯，哈哈哈
        /// </summary>
        internal static object CollectionSyncRoot = new object();
    }
    
}
