
using System.Collections;

/// <summary>
/// RechargeShop实体
/// </summary>
public partial class RechargeShopEntity : AbstractEntity
{
    /// <summary>
    /// 商品类型0=周卡 1=月卡 2=促销礼包 3=普通充值商品
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 充值商品名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 促销描述
    /// </summary>
    public string SalesDesc { get; set; }

    /// <summary>
    /// 产品描述(以服务器为准)
    /// </summary>
    public string ProductDesc { get; set; }

    /// <summary>
    /// 售价
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// 充值后获得虚拟货币
    /// </summary>
    public int Virtual { get; set; }

    /// <summary>
    /// 充值商品图标
    /// </summary>
    public string Icon { get; set; }

}
