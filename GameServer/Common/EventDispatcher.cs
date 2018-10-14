using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.Common
{
    /// <summary>
    /// 观察者模式
    /// </summary>
    public class EventDispatcher : Singleton<EventDispatcher>
    {
        //委托原型
        public delegate void OnActionHandler(Role role, byte[] buffer);

        //委托字典
        private Map<ushort, List<OnActionHandler>> dic = new Map<ushort, List<OnActionHandler>>();

        public Map<ushort, List<OnActionHandler>> Dic
        {
            get
            {
                return dic;
            }

            set
            {
                dic = value;
            }
        }

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="protoCode"></param>
        /// <param name="handler"></param>
        public void AddEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (!Dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = new List<OnActionHandler>();
                Dic[protoCode] = lstHandler;
            }

            Dic[protoCode].Add(handler);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="protoCode"></param>
        /// <param name="handler"></param>
        //移除监听
        public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (Dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = Dic[protoCode];
                lstHandler.Remove(handler);
                if (lstHandler.Count == 0)
                {
                    Dic.Remove(protoCode);
                }
            }
        }

        /// <summary>
        /// 派发协议
        /// </summary>
        /// <param name="protoCode"></param>
        /// <param name="param"></param>
        public void Dispatch(ushort protoCode, Role role, byte[] buffer)
        {
            if (Dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = Dic[protoCode];
                Console.WriteLine("这是就是{0}了{1}", lstHandler.Count, protoCode);
                if (lstHandler != null && lstHandler.Count > 0)
                {
                    for (int i = 0; i < lstHandler.Count; i++)
                    {
                        if (lstHandler[i] != null)
                        {
                            lstHandler[i](role, buffer);
                        }
                    }
                }
            }
        }
    }
}
