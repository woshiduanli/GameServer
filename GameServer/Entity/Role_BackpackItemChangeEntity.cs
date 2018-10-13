using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.Entity
{
    /// <summary>
    /// 背包项改变实体
    /// </summary>
    public class Role_BackpackItemChangeEntity
    {
        /// <summary>
        /// 背包项编号
        /// </summary>
        public int BackpackId { get; set; }

        /// <summary>
        /// 背包项改变类型
        /// </summary>
        public BackpackItemChangeType Type { get; set; }

        /// <summary>
        /// 物品类型
        /// </summary>
        public GoodsType GoodsType { get; set; }

        /// <summary>
        /// 物品编号
        /// </summary>
        public int GoodsId { get; set; }

        /// <summary>
        /// 物品数量
        /// </summary>
        public int GoodsCount { get; set; }

        /// <summary>
        /// 物品服务器端编号
        /// </summary>
        public int GoodsServerId { get; set; }
    }
}
