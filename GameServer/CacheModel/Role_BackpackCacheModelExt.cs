using GameServerApp.Entity;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class Role_BackpackCacheModel
{
    #region Add 物品加入背包
    /// <summary>
    /// 物品加入背包
    /// </summary>
    /// <param name="roleId">物品编号</param>
    /// <param name="goodsInType">物品类型</param>
    /// <param name="entity">物品对象实体</param>
    /// <returns></returns>
    public bool Add(int roleId, GoodsInType goodsInType, Role_BackpackAddItemEntity entity, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        return Role_BackpackDBModel.Instance.Add(roleId, goodsInType, entity, ref changeList);
    }
    #endregion

    #region Add 物品加入背包
    /// <summary>
    /// 物品加入背包
    /// </summary>
    /// <param name="roleId">角色编号</param>
    /// <param name="goodsInType">物品入库类型</param>
    /// <param name="lst">物品列表</param>
    /// <returns></returns>
    public bool Add(int roleId, GoodsInType goodsInType, List<Role_BackpackAddItemEntity> lst, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        return Role_BackpackDBModel.Instance.Add(roleId, goodsInType, lst, ref changeList);
    }
    #endregion

    #region SellToSys 出售物品给系统
    /// <summary>
    /// 出售物品给系统
    /// </summary>
    /// <param name="roleId">角色编号</param>
    /// <param name="roleBackpackId">角色背包项编号</param>
    /// <param name="goodsType">物品类型</param>
    /// <param name="goodsId">物品编号</param>
    /// <param name="goodsServerId">物品服务器端编号</param>
    /// <param name="sellCount">出售数量</param>
    public MFReturnValue<bool> SellToSys(int roleId, int roleBackpackId, int goodsType, int goodsId, int goodsServerId, int sellCount)
    {
        return Role_BackpackDBModel.Instance.SellToSys(roleId, roleBackpackId, goodsType, goodsId, goodsServerId, sellCount);
    }
    #endregion

    /// <summary>
    /// 道具使用
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleBackpackId"></param>
    /// <param name="goodsType"></param>
    /// <param name="goodsId"></param>
    /// <param name="goodsServerId"></param>
    /// <returns></returns>
    public MFReturnValue<bool> UseItem(int roleId, int roleBackpackId, int goodsId)
    {
        return Role_BackpackDBModel.Instance.UseItem(roleId, roleBackpackId, goodsId);
    }
}