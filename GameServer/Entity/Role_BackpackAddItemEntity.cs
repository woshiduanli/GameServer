using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 加入背包时候的添加项
/// </summary>
public class Role_BackpackAddItemEntity
{
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
}