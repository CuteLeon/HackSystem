using System;

namespace HackSystem.Web.Toast.Model
{
    public class ToastDetail
    {
        public enum Icons
        {
            HackSystem = 0,
            Information = 1,
            Question = 2,
            Warning = 3,
            Error = 4
        }

        public DateTime CreateTime { get; protected set; } = DateTime.Now;

        /* TODO: 系统内所有的动态 @key 需要为 int 类型
         * 有多个组件均需要动态为内部子组件分配 @jey, 设计方案可以参考计算机网络的补码，将 int 的4个字节分为组件号(1个字节)和子组件号部分(3个字节)
         * 为容器组件分配唯一的组件号，在容器组件内为子组件动态分配子组件号，子组件的key将为 (容器组件号|子组件号)
         * 例如：组件号为 0x01000000，子组件号为 0x00001001，则key为 0x01000000|0x00001001=0x01001001
         */
        public string Id { get; protected set; } = $"toast_{Guid.NewGuid():N}";

        public string Title { get; set; } = "Hack System";

        public string Message { get; set; } = "Hack System Toast Message.";

        public Icons Icon { get; set; }

        public bool AutoHide { get; set; } = true;

        public int HideDelay { get; set; } = 3000;
    }
}
