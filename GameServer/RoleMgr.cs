using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp
{
    class RoleMgr
    {
        private RoleMgr()
        {

        }
        private static RoleMgr instance;
        private static object lockObj = new object();
        public static RoleMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null) {
                            instance = new RoleMgr(); 
                        }
                    } 
                }
                return instance; 
            }
        }

        internal List<Role> AllRole
        {
            get
            {
                return m_allRole;
            }

            set
            {
                m_allRole = value;
            }
        }

        private List<Role> m_allRole = new List<Role>(); 
    }
}
