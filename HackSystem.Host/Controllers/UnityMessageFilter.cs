using System.Windows.Forms;

namespace HackSystem.Host
{
    class UnityMessageFilter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            //TODO: 增加全局消息过滤器，用于实现
            return false;
            return m.Msg >= 513 && m.Msg <= 515 ? true : false;
        }
    }
}
