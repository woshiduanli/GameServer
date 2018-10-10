using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Common; 

namespace GameServer.DBModel
{
    class MailModel : Singleton<MailModel>
    {
        public MailModel()
        {
            EventDispatcher.Instance.AddEventListener(888, testcodeevent);
        }

        public void init() {
        }

        private void testcodeevent(Role role, byte[] buffer)
        {
            Console.WriteLine("收到了长度是--------------{0}", buffer.Length);

            using (MMO_MemoryStream stream = new MMO_MemoryStream())
            {
                stream.WriteUTF8String("告诉客户端欢迎登陆" + System.DateTime.Now.ToString());
                TestProto test2 = new TestProto();
                test2.Name = "我回来了第二";
                test2.price = 10.9f;
                test2.Id = 3;
                test2.Type = 4;

                role.Client_Socket. SendMsg(test2.ToArray());
            }

        }
    }
}
