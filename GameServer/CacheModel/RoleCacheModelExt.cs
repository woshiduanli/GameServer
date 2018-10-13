using GameServerApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class RoleCacheModel
{
    /// <summary>
    /// 穿上装备
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="goodsId"></param>
    /// <param name="goodsServerId"></param>
    /// <param name="changeList"></param>
    public void EquipPutOn(int roleId, int goodsId, int goodsServerId, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        RoleDBModel.Instance.EquipPutOn(roleId, goodsId, goodsServerId, ref changeList);
    }

    /// <summary>
    /// 脱下装备
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="goodsId"></param>
    /// <param name="goodsServerId"></param>
    /// <param name="changeList"></param>
    public void EquipPutOff(int roleId, int goodsId, int goodsServerId, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        RoleDBModel.Instance.EquipPutOff(roleId, goodsId, goodsServerId, ref changeList);
    }
}