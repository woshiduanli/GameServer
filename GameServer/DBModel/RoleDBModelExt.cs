using GameServerApp.Entity;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
///  角色数据层扩展
/// </summary>
public partial class RoleDBModel
{
    //100 武器
    //200 护腕
    //300 衣服
    //400 护腿
    //500 鞋
    //600 戒指
    //700 项链
    //800 腰带

    public void EquipPutOn(int roleId, int goodsId, int goodsServerId, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        //1.检查原来的装备槽上是否有装备 有的话先卸下（卸下的装备加入背包更新项 新增）
        //2.把当前装备穿戴到身上（穿戴的装备加入背包更新项目 删除）
        //3.重新计算角色战斗力（发送角色战斗里更新消息）

        MFReturnValue<object> retValue = null;

        //excel表格中的数据
        EquipEntity entity = EquipDBModel.Instance.Get(goodsId);

        //1.检查原来的装备槽上是否有装备 有的话先卸下（卸下的装备加入背包更新项 新增）
        bool isHasPutOffEquip = false;
        int putOffEquipServerId = 0; //脱下的装备服务器端编号

        RoleEntity roleEntity = this.GetEntity(roleId);
        switch (entity.Type)
        {
            case 100:
                if (roleEntity.Equip_Weapon > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Weapon;
                }
                roleEntity.Equip_Weapon = goodsServerId; //设置新装备
                break;
            case 200:
                if (roleEntity.Equip_Cuff > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Cuff;
                }
                roleEntity.Equip_Cuff = goodsServerId; //设置新装备
                break;
            case 300:
                if (roleEntity.Equip_Clothes > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Clothes;
                }
                roleEntity.Equip_Clothes = goodsServerId; //设置新装备
                break;
            case 400:
                if (roleEntity.Equip_Pants > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Pants;
                }
                roleEntity.Equip_Pants = goodsServerId; //设置新装备
                break;
            case 500:
                if (roleEntity.Equip_Shoe > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Shoe;
                }
                roleEntity.Equip_Shoe = goodsServerId; //设置新装备
                break;
            case 600:
                if (roleEntity.Equip_Ring > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Ring;
                }
                roleEntity.Equip_Ring = goodsServerId; //设置新装备
                break;
            case 700:
                if (roleEntity.Equip_Necklace > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Necklace;
                }
                roleEntity.Equip_Necklace = goodsServerId; //设置新装备
                break;
            case 800:
                if (roleEntity.Equip_Belt > 0)
                {
                    isHasPutOffEquip = true;
                    putOffEquipServerId = roleEntity.Equip_Belt;
                }
                roleEntity.Equip_Belt = goodsServerId; //设置新装备
                break;
        }

        if (isHasPutOffEquip)
        {
            //找到当前身上穿的武器 设置状态为脱下
            Role_EquipEntity equipPutOffEntity = Role_EquipDBModel.Instance.GetEntity(putOffEquipServerId);
            equipPutOffEntity.IsPutOn = false;
            Role_EquipDBModel.Instance.Update(equipPutOffEntity);

            //把脱下的武器加入背包
            Role_BackpackEntity backpackEntity = new Role_BackpackEntity();
            backpackEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            backpackEntity.RoleId = roleId;
            backpackEntity.GoodsType = (byte)0;
            backpackEntity.GoodsId = equipPutOffEntity.EquipId;
            backpackEntity.GoodsOverlayCount = 1;
            backpackEntity.GoodsServerId = putOffEquipServerId;
            backpackEntity.CreateTime = DateTime.Now;
            backpackEntity.UpdateTime = DateTime.Now;

            retValue = Role_BackpackDBModel.Instance.Create(backpackEntity);
            backpackEntity.Id = retValue.GetOutputValue<int>("Id");

            changeList.Add(new Role_BackpackItemChangeEntity()
            {
                BackpackId = backpackEntity.Id.Value,
                Type = BackpackItemChangeType.Add,
                GoodsType = GoodsType.Equip,
                GoodsId = equipPutOffEntity.EquipId,
                GoodsCount = 1,
                GoodsServerId = putOffEquipServerId
            });
        }

        //2.把当前装备穿戴到身上（穿戴的装备加入背包更新项目 删除）
        Role_EquipEntity equipPutOnEntity = Role_EquipDBModel.Instance.GetEntity(goodsServerId);
        equipPutOnEntity.IsPutOn = true;
        Role_EquipDBModel.Instance.Update(equipPutOnEntity);

        //查询当前装备的背包项
        Role_BackpackEntity currEquipBackpackEntity = Role_BackpackDBModel.Instance.GetEntity(string.Format(" [RoleId]={0} and [GoodsId]={1} and [GoodsType]=0 and [GoodsServerId]={2}", roleId, goodsId, goodsServerId));
        Role_BackpackDBModel.Instance.Delete(currEquipBackpackEntity.Id); //从背包中移除
        changeList.Add(new Role_BackpackItemChangeEntity()
        {
            BackpackId = currEquipBackpackEntity.Id.Value,
            Type = BackpackItemChangeType.Delete,
            GoodsType = GoodsType.Equip,
            GoodsId = goodsId,
            GoodsCount = 1,
            GoodsServerId = goodsServerId
        });

        //3.重新计算角色战斗力
        UpdateFighting(roleEntity);
    }

    public void EquipPutOff(int roleId, int goodsId, int goodsServerId, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        MFReturnValue<object> retValue = null;

        //1.把当前的装备卸下（卸下的装备加入背包更新项 新增）
        //excel表格中的数据
        EquipEntity entity = EquipDBModel.Instance.Get(goodsId);

        RoleEntity roleEntity = this.GetEntity(roleId);
        switch (entity.Type)
        {
            case 100:
                roleEntity.Equip_Weapon = -1; //
                break;
            case 200:
                roleEntity.Equip_Cuff = -1; //
                break;
            case 300:
                roleEntity.Equip_Clothes = -1; //
                break;
            case 400:
                roleEntity.Equip_Pants = -1; //
                break;
            case 500:
                roleEntity.Equip_Shoe = -1; //
                break;
            case 600:
                roleEntity.Equip_Ring = -1; //
                break;
            case 700:
                roleEntity.Equip_Necklace = -1; //
                break;
            case 800:
                roleEntity.Equip_Belt = -1; //
                break;
        }

        //找到当前身上穿的武器 设置状态为脱下
        Role_EquipEntity equipPutOffEntity = Role_EquipDBModel.Instance.GetEntity(goodsServerId);
        equipPutOffEntity.IsPutOn = false;
        Role_EquipDBModel.Instance.Update(equipPutOffEntity);

        //把脱下的武器加入背包
        Role_BackpackEntity backpackEntity = new Role_BackpackEntity();
        backpackEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
        backpackEntity.RoleId = roleId;
        backpackEntity.GoodsType = (byte)0;
        backpackEntity.GoodsId = goodsId;
        backpackEntity.GoodsOverlayCount = 1;
        backpackEntity.GoodsServerId = goodsServerId;
        backpackEntity.CreateTime = DateTime.Now;
        backpackEntity.UpdateTime = DateTime.Now;

        retValue = Role_BackpackDBModel.Instance.Create(backpackEntity);
        backpackEntity.Id = retValue.GetOutputValue<int>("Id");

        changeList.Add(new Role_BackpackItemChangeEntity()
        {
            BackpackId = backpackEntity.Id.Value,
            Type = BackpackItemChangeType.Add,
            GoodsType = GoodsType.Equip,
            GoodsId = goodsId,
            GoodsCount = 1,
            GoodsServerId = goodsServerId
        });

        //2.重新计算角色战斗力（发送角色战斗里更新消息）
        UpdateFighting(roleEntity);
    }

    /// <summary>
    /// 更新角色战斗力
    /// </summary>
    /// <param name="roleEntity"></param>
    private void UpdateFighting(RoleEntity roleEntity)
    {
        //1.计算角色基础属性 和等级有关
        //给角色战斗相关的属性赋值
        //职业 等级
        JobEntity jobEntity = JobDBModel.Instance.Get(roleEntity.JobId);

        //职业等级数据
        JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(roleEntity.Level);

        roleEntity.MaxHP = jobLevelEntity.HP;
        roleEntity.MaxMP = jobLevelEntity.MP;

        roleEntity.Attack = (int)Math.Round(jobEntity.Attack * jobLevelEntity.Attack * 0.01f);
        roleEntity.Defense = (int)Math.Round(jobEntity.Defense * jobLevelEntity.Defense * 0.01f);
        roleEntity.Hit = (int)Math.Round(jobEntity.Hit * jobLevelEntity.Hit * 0.01f);
        roleEntity.Dodge = (int)Math.Round(jobEntity.Dodge * jobLevelEntity.Dodge * 0.01f);
        roleEntity.Cri = (int)Math.Round(jobEntity.Cri * jobLevelEntity.Cri * 0.01f);
        roleEntity.Res = (int)Math.Round(jobEntity.Res * jobLevelEntity.Res * 0.01f);

        //2.计算角色装备增加的属性
        List<Role_EquipEntity> lst = Role_EquipDBModel.Instance.GetList(condition: string.Format("[RoleId]={0} and [IsPutOn]=1", roleEntity.Id));
        if (lst != null && lst.Count > 0)
        {
            for (int i = 0; i < lst.Count; i++)
            {
                Role_EquipEntity role_EquipEntity = lst[i];

                //0=HP 1=MP 2=攻击 3=防御 4=命中 5=闪避 6=暴击 7=抗性

                #region 基础属性1
                switch (role_EquipEntity.BaseAttr1Type)
                {
                    case 0:
                        roleEntity.MaxHP += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 1:
                        roleEntity.MaxMP += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 2:
                        roleEntity.Attack += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 3:
                        roleEntity.Defense += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 4:
                        roleEntity.Hit += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 5:
                        roleEntity.Dodge += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 6:
                        roleEntity.Cri += role_EquipEntity.BaseAttr1Value;
                        break;
                    case 7:
                        roleEntity.Res += role_EquipEntity.BaseAttr1Value;
                        break;
                }
                #endregion

                #region 基础属性2
                switch (role_EquipEntity.BaseAttr2Type)
                {
                    case 0:
                        roleEntity.MaxHP += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 1:
                        roleEntity.MaxMP += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 2:
                        roleEntity.Attack += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 3:
                        roleEntity.Defense += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 4:
                        roleEntity.Hit += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 5:
                        roleEntity.Dodge += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 6:
                        roleEntity.Cri += role_EquipEntity.BaseAttr2Value;
                        break;
                    case 7:
                        roleEntity.Res += role_EquipEntity.BaseAttr2Value;
                        break;
                }
                #endregion

                roleEntity.Attack += role_EquipEntity.Attack;
                roleEntity.Defense += role_EquipEntity.Defense;
                roleEntity.Hit += role_EquipEntity.Hit;
                roleEntity.Dodge += role_EquipEntity.Dodge;
                roleEntity.Cri += role_EquipEntity.Cri;
                roleEntity.Res += role_EquipEntity.Res;
            }
        }

        //3.计算综合战斗力
        roleEntity.Fighting = roleEntity.Attack * 4 + roleEntity.Defense * 4 + roleEntity.Hit * 2 + roleEntity.Dodge * 2 + roleEntity.Cri + roleEntity.Res;

        RoleDBModel.Instance.Update(roleEntity);
    }
}