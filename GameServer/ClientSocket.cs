using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Common;

namespace GameServer
{
    /// <summary>
    ///  和客户端通信的类
    /// </summary>
    public class ClientSocket
    {
        Role role;
        // 接收数据包的缓冲数据流
        private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();
        // 客户端的socket 
        private Socket m_socket;

        // 接受数据用到的线程
        private Thread m_receiveThread;

        // 接受数据包的缓冲区
        private byte[] m_ReceiveBuffer = new byte[2048];


        // 发送消息的队列
        private Queue<byte[]> m_sendQueue = new Queue<byte[]>();
        // 发送消息的委托
        private System.Action m_checkSendQueue;

        // 服务端接收到客户端连接的时候， 创建一个线程， 去接收客户端的消息
        public ClientSocket(Socket socket, Role role)
        {
            GameServer.DBModel.MailModel.Instance.init();
            this.role = role;
            m_socket = socket;
            role.Client_Socket = this;
            m_receiveThread = new Thread(ReceiveMsg);
            m_checkSendQueue = OnCheckSendQueueCallBack;
            m_receiveThread.Start();
        }

        // 接收数据
        private void ReceiveMsg()
        {
            m_socket.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveBack, m_socket);
        }

        // -----------------------     收到消息   --------------------
        private void ReceiveBack(IAsyncResult ar)
        {
            try
            {
                // 接收数据的回调
                int len = m_socket.EndReceive(ar);
                Console.WriteLine("收到的长度:{0}", len);

                if (len > 0)
                {
                    // 已经接收到了数据, 放在缓冲数据流的尾部
                    m_ReceiveMS.Position = m_ReceiveMS.Length;

                    // 把指定长度的字节写入字节流
                    m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                    // 如果长度大于2,至少有个不完整的包过来了, 为什么是2, 因为封装数据包的时候用的是ushort， 他的长度是2 
                    if (m_ReceiveMS.Length > 2)
                    {
                        while (true)
                        {
                            // 数据流指针放在0处 
                            m_ReceiveMS.Position = 0;
                            // 包体的长度， 因为封装的时候， 就把真实数据的长度长度保存在了这个里面
                            int bodyLength = m_ReceiveMS.ReadUShort();

                            // 总包的长度  = 包头长度+包体长度，整个包的长度，就是原来包体的长度，加上， 保存长度的ushort的长度 
                            int allLength = 2 + bodyLength;

                            // 说明至少收到了一个完整的包
                            if (m_ReceiveMS.Length >= allLength)
                            {
                                // 包含了协议id的， 整个协议内容
                                byte[] bufferBody = new byte[bodyLength];

                                // 流的位置转到2
                                m_ReceiveMS.Position = 2;
                                // 把包体读到数组中
                                m_ReceiveMS.Read(bufferBody, 0, bodyLength);
                                // --------------------------------  开始解包  ----------------------------- 
                                // 拿到是否压缩， crc验证码的变量
                                bool isComress = false;
                                ushort oldCrc = 0;
                                byte[] newContent = new byte[bufferBody.Length - 3];
                                using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferBody))
                                {
                                    isComress = ms.ReadBool();
                                    oldCrc = ms.ReadUShort();
                                    ms.Read(newContent, 0, newContent.Length);
                                }

                                // 开始和最新的crc比较
                                ushort newCrc = Crc16.CalculateCrc16(newContent);
                                if (newCrc == oldCrc)
                                {
                                    // 解开异或
                                    newContent = SecurityUtil.Xor(newContent);

                                    // 解开压缩
                                    if (isComress)
                                    {
                                        newContent = ZlibHelper.DeCompressBytes(newContent);
                                    }

                                    ushort protoCode = 0;
                                    byte[] realContent = new byte[newContent.Length];
                                    using (MMO_MemoryStream ms = new MMO_MemoryStream(newContent))
                                    {
                                        protoCode = ms.ReadUShort();
                                        // 读取最终的结果
                                        ms.Read(realContent, 0, realContent.Length);
                                        // ------------------ --------------  结束解包  ----------------------------- 
                                        EventDispatcher.Instance.Dispatch(protoCode, role, realContent);
                                    }
                                    if (protoCode == 888)
                                    {
                                        TestProto test = TestProto.GetProto(realContent);
                                        Console.WriteLine("na----me:{0}", test.Name);
                                        Console.WriteLine("price:{0}", test.price);
                                        Console.WriteLine("id:{0}", test.Id);
                                        Console.WriteLine("type:{0}", test.Type);
                                    }
                                    else
                                    {

                                    }

                                    using (MMO_MemoryStream stream = new MMO_MemoryStream())
                                    {
                                        //stream.WriteUTF8String("告诉客户端欢迎登陆" + System.DateTime.Now.ToString());
                                        TestProto test2 = new TestProto();
                                        test2.Name = "我回来加加加啊了";
                                        test2.price = 10.9f;
                                        test2.Id = 3;
                                        test2.Type = 4;

                                        SendMsg(test2.ToArray());
                                    }
                                }
                                else
                                    break;



                                //// 协议id
                                //ushort protoCode = 0;
                                //// 协议内容
                                //byte[] ProtoContent = new byte[bodyLength - 2];


                                //using (MMO_MemoryStream ms = new MMO_MemoryStream(bufferBody))
                                //{
                                //    protoCode = ms.ReadUShort();
                                //    ms.Read(ProtoContent, 0, ProtoContent.Length);
                                //}
                                //EventDispatcher.Instance.Dispatch(protoCode, role, ProtoContent);



                                // 临时测试



                                //------------- 处理剩余字节数组 -----------------
                                int remainLen = (int)m_ReceiveMS.Length - allLength;
                                // 说明有剩余字节
                                if (remainLen > 0)
                                {
                                    // 把指针放在第一个包的尾部
                                    m_ReceiveMS.Position = allLength;
                                    byte[] remainBuff = new byte[remainLen];

                                    m_ReceiveMS.Read(remainBuff, 0, remainLen);

                                    // 清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);

                                    // 又重新写进流中
                                    m_ReceiveMS.Write(remainBuff, 0, remainBuff.Length);
                                    remainBuff = null;
                                }
                                else
                                {
                                    // 没有剩余字节
                                    // 清空数据流
                                    m_ReceiveMS.Position = 0;
                                    m_ReceiveMS.SetLength(0);
                                    break;
                                }

                            }
                            else
                            {
                                break;
                                // 没有收到完整包
                            }

                        }
                    }

                    // 继续接受包
                    ReceiveMsg();
                }
                else
                {
                    // 说明客户端断开连接
                    Console.WriteLine("客户端{0}断开连接", m_socket.RemoteEndPoint.ToString());
                    RoleMgr.Instance.AllRole.Remove(role);
                }
            }
            catch (Exception)
            {
                // 说明客户端断开连接
                Console.WriteLine("客户端{0}断开连接", m_socket.RemoteEndPoint.ToString());
                RoleMgr.Instance.AllRole.Remove(role);

            }
        }


        //-------------------------    发送消息   ---------------------

        #region 封装数据包
        private const int m_CompressLen = 200;
        // 封装数据包
        byte[] MakeData(byte[] data)
        {
            byte[] retBuffer = null;
            // 1.1 压缩
            bool isCompress = data.Length > m_CompressLen;
            if (isCompress)
            {
                data = ZlibHelper.CompressBytes(data);
            }
            // 2 异或加密
            data = SecurityUtil.Xor(data);
            // 3 加验证
            ushort crc = Crc16.CalculateCrc16(data);

            using (MMO_MemoryStream ms = new MMO_MemoryStream())
            {
                ms.WriteUShort((ushort)(data.Length + 3));
                ms.WriteBool(isCompress);
                ms.WriteUShort(crc);
                ms.Write(data, 0, data.Length);

                retBuffer = ms.ToArray();
            }

            return retBuffer;
        }
        #endregion

        public void SendMsg(byte[] data)
        {
            // 得到包体的数组
            byte[] Senddata = MakeData(data);

            lock (m_sendQueue)
            {
                // 把数据包加入队列
                m_sendQueue.Enqueue(Senddata);

                //启动委托
                if (m_checkSendQueue != null)
                    m_checkSendQueue.BeginInvoke(null, null);
            }
        }

        // 真正发送数据包到服务器
        void Send(byte[] buffer)
        {
            m_socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallBack, m_socket);
        }

        private void SendCallBack(System.IAsyncResult ar)
        {
            m_socket.EndSend(ar);
            OnCheckSendQueueCallBack();
        }

        private void OnCheckSendQueueCallBack()
        {
            lock (m_sendQueue)
            {
                if (m_sendQueue.Count > 0)
                {
                    // 真正发送数据包
                    Send(m_sendQueue.Dequeue());
                }
            }
        }
    }
}
