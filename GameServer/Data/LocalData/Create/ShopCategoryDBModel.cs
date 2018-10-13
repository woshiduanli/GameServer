

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ShopCategory数据管理
/// </summary>
public partial class ShopCategoryDBModel : AbstractDBModel<ShopCategoryDBModel, ShopCategoryEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "ShopCategory.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ShopCategoryEntity MakeEntity(GameDataTableParser parse)
    {
        ShopCategoryEntity entity = new ShopCategoryEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        return entity;
    }
}
