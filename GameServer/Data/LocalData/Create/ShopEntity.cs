
//===================================================

using System.Collections;

/// <summary>
/// Shop实体
/// </summary>
public partial class ShopEntity : AbstractEntity
{
    /// <summary>
    /// 物品分类编号
    /// </summary>
    public int ShopCategoryId { get; set; }

    /// <summary>
    /// 物品类型
    /// </summary>
    public int GoodsType { get; set; }

    /// <summary>
    /// 物品Id
    /// </summary>
    public int GoodsId { get; set; }

    /// <summary>
    /// 原价
    /// </summary>
    public int OldPrice { get; set; }

    /// <summary>
    /// 售价
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 促销状态 0=无 1=打折 2=新品 3=热卖 4=限时
    /// </summary>
    public int SellStatus { get; set; }

}
