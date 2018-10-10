using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Common
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

        /// <summary>
        /// 添加监听
        /// </summary>
        /// <param name="protoCode"></param>
        /// <param name="handler"></param>
        public void AddEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (!dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = new List<OnActionHandler>();
                lstHandler.Add(handler);
                dic[protoCode] = lstHandler;
            }
            dic[protoCode].Add(handler);
        }

        /// <summary>
        /// 移除监听
        /// </summary>
        /// <param name="protoCode"></param>
        /// <param name="handler"></param>
        //移除监听
        public void RemoveEventListener(ushort protoCode, OnActionHandler handler)
        {
            if (dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = dic[protoCode];
                lstHandler.Remove(handler);
                if (lstHandler.Count == 0)
                {
                    dic.Remove(protoCode);
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
            if (dic.ContainsKey(protoCode))
            {
                List<OnActionHandler> lstHandler = dic[protoCode];
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
