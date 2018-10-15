using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Mmcoy.Framework.AbstractBase;
using GameServerApp;
using GameServerApp.Controller;

namespace GameServer
{
    class Program
    {
        private static string m_serverIP = "192.168.0.101";
        private static int m_point = 1011;
        private static Socket m_ServerSocket;
        static void InitAllController()
        {
            //角色控制器初始化
            RoleController.Instance.Init();

            //世界地图场景管理器初始化
            //WorldMapSceneMgr.Instance.Init();
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("启222动监听{0}成功", GetIpAddress());
            //if ("ER01ZXNIQGFET10" == System.Net.Dns.GetHostName())
            //{
            //    m_serverIP = "192.168.0.102";
            //}
            //else
            //{
            //    m_serverIP = "192.168.2.143";
            //}


            InitAllController(); 
            m_ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 向操作系统申请一个ip和端口， 用来通讯
            m_ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(GetIpAddress()), m_point));
            // test 
            // 设置最多3000个排队请求
            m_ServerSocket.Listen(3000);

            Console.WriteLine("启动监听{0}成功", m_ServerSocket.LocalEndPoint.ToString());

            Thread m_thread = new Thread(ListenClientCallBack);
            m_thread.Start();

            // 测试—代码
            //RoleEntity role = new RoleEntity();

            //role.NickName = "zhangsadddddn";
            //role.Id = 11;
            //role.JobId = 1;
            //role.CreateTime = System.DateTime.Now;
            //role.UpdateTime = System.DateTime.Now;
            //role.Status = EnumEntityStatus.Released;
            //role.AccountId = 11111; 


            //RoleCacheModel.Instance.Create(role);
            Console.ReadLine();
        }

        private static string GetIpAddress()
        {
            string hostName = Dns.GetHostName();   //获取本机名
            IPHostEntry localhost = Dns.GetHostByName(hostName);    //方法已过期，可以获取IPv4的地址
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }

        private static void ListenClientCallBack()
        {
            while (true)
            {
                // 这是是客户端
                Socket socket = m_ServerSocket.Accept();

                Console.WriteLine("客户端{0}连接成功", socket.RemoteEndPoint.ToString());

                Role role = new Role();
                ClientSocket client_socket = new ClientSocket(socket, role);
                // 一个角色就相当于一个客户端
                RoleMgr.Instance.AllRole.Add(role);
            }
        }
    }
}
