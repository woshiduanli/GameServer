
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// RechargeShop数据管理
/// </summary>
public partial class RechargeShopDBModel : AbstractDBModel<RechargeShopDBModel, RechargeShopEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "RechargeShop.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override RechargeShopEntity MakeEntity(GameDataTableParser parse)
    {
        RechargeShopEntity entity = new RechargeShopEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.SalesDesc = parse.GetFieldValue("SalesDesc");
        entity.ProductDesc = parse.GetFieldValue("ProductDesc");
        entity.Price = parse.GetFieldValue("Price").ToInt();
        entity.Virtual = parse.GetFieldValue("Virtual").ToInt();
        entity.Icon = parse.GetFieldValue("Icon");
        return entity;
    }
}
