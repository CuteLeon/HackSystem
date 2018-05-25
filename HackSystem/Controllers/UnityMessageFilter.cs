using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HackSystem
{
    class UnityMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            //TODO: 增加全局消息过滤器，用于实现
            return false;
            if (m.Msg >= 513 && m.Msg <= 515)
            {
                return true;
            }
            else return false;
        }
    }
}
