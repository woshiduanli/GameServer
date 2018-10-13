using GameServerApp.Entity;
using Mmcoy.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 角色背包数据层扩展
/// </summary>
public partial class Role_BackpackDBModel
{
    #region Add 物品加入背包
    /// <summary>
    /// 物品加入背包
    /// </summary>
    /// <param name="roleId">物品编号</param>
    /// <param name="goodsInType">物品入库类型</param>
    /// <param name="entity">物品对象实体</param>
    /// <returns></returns>
    public bool Add(int roleId, GoodsInType goodsInType, Role_BackpackAddItemEntity entity, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        List<Role_BackpackAddItemEntity> lst = new List<Role_BackpackAddItemEntity>();
        lst.Add(entity);

        return Add(roleId, goodsInType, lst, ref changeList);
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
        changeList = new List<Role_BackpackItemChangeEntity>();

        using (SqlConnection conn = new SqlConnection(DBConn.DBGameServer))
        {
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(); //开启事务

            for (int i = 0; i < lst.Count; i++)
            {
                List<Role_BackpackItemChangeEntity> lstChange = null;

                bool retValue = Add(trans, roleId, goodsInType, lst[i].GoodsType, lst[i].GoodsId, lst[i].GoodsCount, ref lstChange);
                if (!retValue)
                {
                    trans.Rollback();
                    return false;
                }
                else
                {
                    changeList.AddRange(lstChange);
                }
            }

            trans.Commit();
            return true;
        }
    }
    #endregion

    #region Add 物品加入背包
    /// <summary>
    /// 物品加入背包
    /// </summary>
    /// <param name="trans">事务</param>
    /// <param name="roleId">角色编号</param>
    /// <param name="goodsInType">物品入库类型</param>
    /// <param name="goodsType">物品类型</param>
    /// <param name="goodsId">物品编号</param>
    /// <param name="goodsCount">本次物品入库的数量</param>
    private bool Add(SqlTransaction trans, int roleId, GoodsInType goodsInType, GoodsType goodsType, int goodsId, int goodsCount, ref List<Role_BackpackItemChangeEntity> changeList)
    {
        Queue<int> goodsServerIdQueue = new Queue<int>(); //服务器端物品编号的队列
        MFReturnValue<object> retValue = null;
        changeList = new List<Role_BackpackItemChangeEntity>();
        try
        {
            int goodsServerId = 0;
            int speed = DateTime.Now.ToString("hhMMssfff").ToInt(); //种子

            for (int i = 0; i < goodsCount; i++)
            {
                //1.加入物品库 装备 道具 材料（根据物品类型 加入对应的物品表 如果是装备 要进行随机属性和随机属性值处理）
                switch (goodsType)
                {
                    case GoodsType.Equip:
                        #region 装备入库
                        Role_EquipEntity equipEntity = new Role_EquipEntity();
                        equipEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                        equipEntity.RoleId = roleId;
                        equipEntity.EquipId = goodsId;

                        //拿到基础装备信息
                        EquipEntity baseEntity = EquipDBModel.Instance.Get(goodsId);

                        //基础信息
                        equipEntity.BaseAttr1Type = (byte)baseEntity.BackAttrOneType;
                        equipEntity.BaseAttr1Value = baseEntity.BackAttrOneValue;
                        equipEntity.BaseAttr2Type = (byte)baseEntity.BackAttrTwoType;
                        equipEntity.BaseAttr2Value = baseEntity.BackAttrTwoValue;

                        //随机属性
                        //1.计算出随机属性的数量 一共是8个属性 我们随机取2-6个 也可以根据装备等级不同 随机数不同
                        int attrCount = new Random(speed).Next(2, 7);
                        speed += 100;

                        //0=HP 1=MP 2=攻击 3=防御 4=命中 5=闪避 6=暴击 7=抗性
                        int[] arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };

                        List<int> lst = new List<int>();
                        lst.AddRange(arr);

                        #region 随机属性
                        for (int j = 0; j < attrCount; j++)
                        {
                            //随机获取一个索引 对他进行随机值
                            //随机算法 要根据具体的项目 结合数值策划提供的算法公式来做 比如根据角色的等级 VIP等级 运气值等等来计算
                            //这里是进行最基础的随机

                            int index = new Random(speed).Next(0, lst.Count);
                            speed += 100;

                            switch (lst[index])
                            {
                                case 0:
                                    equipEntity.HP = new Random(speed).Next((int)(baseEntity.HP * 0.5f), (int)(baseEntity.HP * 1.5f));
                                    speed += 100;
                                    break;
                                case 1:
                                    equipEntity.MP = new Random(speed).Next((int)(baseEntity.MP * 0.5f), (int)(baseEntity.MP * 1.5f));
                                    speed += 100;
                                    break;
                                case 2:
                                    equipEntity.Attack = new Random(speed).Next((int)(baseEntity.Attack * 0.5f), (int)(baseEntity.Attack * 1.5f));
                                    speed += 100;
                                    break;
                                case 3:
                                    equipEntity.Res = new Random(speed).Next((int)(baseEntity.Res * 0.5f), (int)(baseEntity.Res * 1.5f));
                                    speed += 100;
                                    break;
                                case 4:
                                    equipEntity.Hit = new Random(speed).Next((int)(baseEntity.Hit * 0.5f), (int)(baseEntity.Hit * 1.5f));
                                    speed += 100;
                                    break;
                                case 5:
                                    equipEntity.Defense = new Random(speed).Next((int)(baseEntity.Defense * 0.5f), (int)(baseEntity.Defense * 1.5f));
                                    speed += 100;
                                    break;
                                case 6:
                                    equipEntity.Dodge = new Random(speed).Next((int)(baseEntity.Dodge * 0.5f), (int)(baseEntity.Dodge * 1.5f));
                                    speed += 100;
                                    break;
                                case 7:
                                    equipEntity.Cri = new Random(speed).Next((int)(baseEntity.Cri * 0.5f), (int)(baseEntity.Cri * 1.5f));
                                    speed += 100;
                                    break;
                            }

                            //这个索引 也就是属性用过了 就移除
                            lst.RemoveAt(index);
                        }
                        #endregion

                        equipEntity.CreateTime = DateTime.Now;
                        equipEntity.UpdateTime = DateTime.Now;

                        retValue = Role_EquipDBModel.Instance.Create(trans, equipEntity);
                        #endregion
                        break;
                    case GoodsType.Item:
                        #region 道具入库
                        Role_ItemEntity itemEntity = new Role_ItemEntity();
                        itemEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                        itemEntity.RoleId = roleId;
                        itemEntity.ItemId = goodsId;
                        itemEntity.CreateTime = DateTime.Now;
                        itemEntity.UpdateTime = DateTime.Now;
                        retValue = Role_ItemDBModel.Instance.Create(trans, itemEntity);
                        #endregion
                        break;
                    case GoodsType.Material:
                        #region 材料入库
                        Role_MaterialEntity materialEntity = new Role_MaterialEntity();
                        materialEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                        materialEntity.RoleId = roleId;
                        materialEntity.MaterialId = goodsId;
                        materialEntity.CreateTime = DateTime.Now;
                        materialEntity.UpdateTime = DateTime.Now;
                        retValue = Role_MaterialDBModel.Instance.Create(trans, materialEntity);
                        #endregion
                        break;
                }

                if (retValue.HasError)
                {
                    return false;
                }

                goodsServerId = retValue.OutputValues["Id"].ToInt(); //物品的服务器端编号
                goodsServerIdQueue.Enqueue(goodsServerId);

                //2.添加物品入库日志（要先从 步骤1 拿到物品的服务器端编号）
                Log_GoodsInEntity goodsInEntity = new Log_GoodsInEntity();
                goodsInEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                goodsInEntity.RoleId = roleId;
                goodsInEntity.Type = (byte)goodsInType;
                goodsInEntity.GoodsType = (byte)goodsType;
                goodsInEntity.GoodsId = goodsId;
                goodsInEntity.GoodsServerId = goodsServerId;
                goodsInEntity.CreateTime = DateTime.Now;
                goodsInEntity.UpdateTime = DateTime.Now;

                retValue = Log_GoodsInDBModel.Instance.Create(trans, goodsInEntity); //加入入库日志
                if (retValue.HasError)
                {
                    return false;
                }
            }

            //3.加入背包（要进行物品的叠加处理）
            //(1).拿到物品基础表的叠加数量 装备是1 道具材料是20
            //(2).如果叠加数量是1 跳过此步骤 查询库中 该物品叠加数量最低的背包项 比如回血药 有两个背包项 20 5，那就按物品叠加数量正顺序排序 取出第一个5
            //(3).如果叠加数量是1 跳过此步骤 对这个背包项进行填充 5 -> 20
            //(4).进行剩余物品的处理 根据剩余物品的数量 该物品的叠加数量上限 动态创建背包项目 直到剩余物品全部添加完毕


            #region (1).拿到物品基础表的叠加数量 装备是1 道具材料是20
            int goodsOverlayCount = 1; //物品叠加数量
            switch (goodsType)
            {
                case GoodsType.Item:
                    ItemEntity itemEntity = ItemDBModel.Instance.Get(goodsId);
                    if (itemEntity != null)
                    {
                        goodsOverlayCount = itemEntity.maxAmount;
                    }
                    break;
                case GoodsType.Material:
                    MaterialEntity materialEntity = MaterialDBModel.Instance.Get(goodsId);
                    if (materialEntity != null)
                    {
                        goodsOverlayCount = materialEntity.maxAmount;
                    }
                    break;
            }
            #endregion

            #region (2).如果叠加数量是1 跳过此步骤 查询库中 该物品叠加数量最低的背包项 比如回血药 有两个背包项 20 5，那就按物品叠加数量正顺序排序 取出第一个5
            Role_BackpackEntity backpackEntity = null;
            if (goodsOverlayCount > 1)
            {

                MFReturnValue<List<Role_BackpackEntity>> retList = Role_BackpackDBModel.Instance.GetPageListWithTran(condition: string.Format("[RoleId]={0} AND [GoodsType]={1} AND [GoodsId]={2}", roleId, (byte)goodsType, goodsId), orderby: "[GoodsOverlayCount]", isDesc: false, pageSize: 1, pageIndex: 1, trans: trans);
                if (retList.HasError)
                {
                    return false;
                }

                if (retList.Value != null && retList.Value.Count > 0)
                {
                    backpackEntity = retList.Value[0];
                }
            }
            #endregion

            #region (3).如果叠加数量是1 跳过此步骤 如果能查到背包项 对这个背包项进行填充 5 -> 20
            if (goodsOverlayCount > 1)
            {
                if (backpackEntity != null)
                {
                    //需要填充的数量 = 物品的叠加数量 - 当前背包项的叠加数量
                    int needCount = goodsOverlayCount - backpackEntity.GoodsOverlayCount;

                    if (needCount > 0)
                    {
                        int currOverlayCount = 0;
                        if (goodsCount > needCount)
                        {
                            //当前叠加数量 = 物品叠加数量
                            currOverlayCount = goodsOverlayCount;
                            goodsCount -= needCount; //本次添加的物品数量减少
                        }
                        else
                        {
                            //当前叠加数量 = 原来的叠加数量 + 本次添加的物品数量
                            currOverlayCount = backpackEntity.GoodsOverlayCount + goodsCount;
                            goodsCount = 0;
                        }

                        backpackEntity.GoodsOverlayCount = currOverlayCount;
                        Role_BackpackDBModel.Instance.Update(trans, backpackEntity);

                        changeList.Add(new Role_BackpackItemChangeEntity()
                        {
                            BackpackId = backpackEntity.Id.Value,
                            Type = BackpackItemChangeType.Update,
                            GoodsType = (GoodsType)backpackEntity.GoodsType,
                            GoodsId = backpackEntity.GoodsId,
                            GoodsCount = backpackEntity.GoodsOverlayCount,
                            GoodsServerId = backpackEntity.GoodsServerId
                        });

                        Console.WriteLine("背包项修改了==>" + backpackEntity.Id.Value);
                    }
                }
            }
            #endregion

            #region (4).进行剩余物品的处理 根据剩余物品的数量 该物品的叠加数量上限 动态创建背包项目 直到剩余物品全部添加完毕
            while (goodsCount > 0)
            {
                int currOverlayCount = 0;
                if (goodsCount > goodsOverlayCount)
                {
                    currOverlayCount = goodsOverlayCount;
                    goodsCount -= goodsOverlayCount;
                }
                else
                {
                    currOverlayCount = goodsCount;
                    goodsCount = 0;
                }

                Role_BackpackEntity entity = new Role_BackpackEntity();
                entity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
                entity.RoleId = roleId;
                entity.GoodsType = (byte)goodsType;
                entity.GoodsId = goodsId;
                entity.GoodsOverlayCount = currOverlayCount;
                entity.GoodsServerId = goodsServerIdQueue.Dequeue();
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;

                retValue = Role_BackpackDBModel.Instance.Create(trans, entity);
                if (retValue.HasError)
                {
                    return false;
                }
                else
                {
                    entity.Id = retValue.GetOutputValue<int>("Id");
                    Console.WriteLine("背包项添加了==>" + entity.Id.Value);

                    changeList.Add(new Role_BackpackItemChangeEntity()
                    {
                        BackpackId = entity.Id.Value,
                        Type = BackpackItemChangeType.Add,
                        GoodsType = (GoodsType)entity.GoodsType,
                        GoodsId = entity.GoodsId,
                        GoodsCount = entity.GoodsOverlayCount,
                        GoodsServerId = entity.GoodsServerId
                    });
                }
            }

            #endregion


            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("报错了==>" + ex.Message);
            return false;
        }
        finally
        {
            goodsServerIdQueue.Clear();
            goodsServerIdQueue = null;

            retValue = null;
        }
    }
    #endregion

    private MFReturnValue<bool> Reduce(int roleId, int roleBackpackId, int goodsType, int goodsId, int goodsServerId, int count, GoodsOutType outType)
    {
        MFReturnValue<bool> retValue = new MFReturnValue<bool>();

        //1.查询物品的售价 物品的已有数量
        int sellPrice = 0; //售价
        int maxAmount = 0; //叠加数量
        int hasCount = 0; //已有数量
        bool isCanSell = false; //是否可以销售

        StringBuilder sbrSql = new StringBuilder();
        StringBuilder sbrSqlLog = new StringBuilder();

        #region 查询 售价 叠加数量 已有数量
        switch ((GoodsType)goodsType)
        {
            case GoodsType.Equip:
                EquipEntity equipEntity = EquipDBModel.Instance.Get(goodsId);
                if (equipEntity != null)
                {
                    sellPrice = equipEntity.SellMoney;
                    maxAmount = 1;
                }

                sbrSql.AppendFormat("[Status]=1 and [EquipId]={0}", goodsId);
                if (goodsServerId > 0)
                {
                    sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                }

                //已有数量
                hasCount = Role_EquipDBModel.Instance.GetCount(sbrSql.ToString());

                if (hasCount >= count)
                {
                    isCanSell = true;
                }
                break;
            case GoodsType.Item:
                ItemEntity itemEntity = ItemDBModel.Instance.Get(goodsId);
                if (itemEntity != null)
                {
                    sellPrice = itemEntity.SellMoney;
                    maxAmount = itemEntity.maxAmount;
                }

                sbrSql.AppendFormat("[Status]=1 and [ItemId]={0}", goodsId);
                if (goodsServerId > 0 && maxAmount == 1)
                {
                    sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                }

                hasCount = Role_ItemDBModel.Instance.GetCount(sbrSql.ToString());

                if (hasCount >= count)
                {
                    isCanSell = true;
                }

                break;
            case GoodsType.Material:
                MaterialEntity materialEntity = MaterialDBModel.Instance.Get(goodsId);
                if (materialEntity != null)
                {
                    sellPrice = materialEntity.SellMoney;
                    maxAmount = materialEntity.maxAmount;
                }

                sbrSql.AppendFormat("[Status]=1 and [MaterialId]={0}", goodsId);
                if (goodsServerId > 0 && maxAmount == 1)
                {
                    sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                }

                hasCount = Role_MaterialDBModel.Instance.GetCount(sbrSql.ToString());

                if (hasCount >= count)
                {
                    isCanSell = true;
                }

                break;
        }
        #endregion

        //2.核对玩家拥有的物品数量是否满足出售数量
        if (!isCanSell)
        {
            retValue.HasError = true;
            retValue.ReturnCode = -1; //数量不足
            return retValue;
        }

        Log_GoodsInEntity logInEntity;
        Log_GoodsOutEntity logOutEntity;
        int currGoodsServerId = 0;

        int currSellCount = count;

        //3.从对应的物品表中移除
        #region 从对应的物品表中移除
        while (currSellCount > 0)
        {
            sbrSql.Clear();
            sbrSqlLog.Clear();

            //从对应的物品表中删除
            switch ((GoodsType)goodsType)
            {
                case GoodsType.Equip:
                    sbrSql.AppendFormat("[Status]=1 and [EquipId]={0}", goodsId);
                    if (goodsServerId > 0)
                    {
                        sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                    }
                    Role_EquipEntity equipEntity = Role_EquipDBModel.Instance.GetEntity(sbrSql.ToString());
                    Role_EquipDBModel.Instance.Delete(equipEntity.Id); //删除物品
                    currGoodsServerId = equipEntity.Id.Value;
                    break;
                case GoodsType.Item:
                    sbrSql.AppendFormat("[Status]=1 and [ItemId]={0}", goodsId);
                    if (goodsServerId > 0 && maxAmount == 1)
                    {
                        sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                    }
                    Role_ItemEntity itemEntity = Role_ItemDBModel.Instance.GetEntity(sbrSql.ToString());
                    Role_ItemDBModel.Instance.Delete(itemEntity.Id); //删除物品
                    currGoodsServerId = itemEntity.Id.Value;
                    break;
                case GoodsType.Material:
                    sbrSql.AppendFormat("[Status]=1 and [MaterialId]={0}", goodsId);
                    if (goodsServerId > 0 && maxAmount == 1)
                    {
                        sbrSql.AppendFormat(" and [Id]={0}", goodsServerId);
                    }
                    Role_MaterialEntity materialEntity = Role_MaterialDBModel.Instance.GetEntity(sbrSql.ToString());
                    Role_MaterialDBModel.Instance.Delete(materialEntity.Id); //删除物品
                    currGoodsServerId = materialEntity.Id.Value;
                    break;
            }

            sbrSqlLog.AppendFormat("[Status]=1 and [RoleId]={0} and [GoodsType]={1} and [GoodsId]={2} and [GoodsServerId]={3}",
                        roleId, goodsType, goodsId, currGoodsServerId);

            //添加出库日志
            logInEntity = Log_GoodsInDBModel.Instance.GetEntity(sbrSqlLog.ToString()); //获取对应的入库日志

            logOutEntity = new Log_GoodsOutEntity();
            logOutEntity.Status = Mmcoy.Framework.AbstractBase.EnumEntityStatus.Released;
            logOutEntity.RoleId = roleId;
            logOutEntity.Type = (byte)outType;
            logOutEntity.GoodsType = (byte)goodsType;
            logOutEntity.GoodsId = goodsId;
            logOutEntity.GoodsServerId = currGoodsServerId;
            logOutEntity.LogGoodsInId = logInEntity.Id.Value;
            logOutEntity.CreateTime = DateTime.Now;
            logOutEntity.UpdateTime = DateTime.Now;

            Log_GoodsOutDBModel.Instance.Create(logOutEntity);

            currSellCount--;
        }
        #endregion

        //4.修改或者删除对应的背包项
        currSellCount = count;

        //背包项改变列表
        List<Role_BackpackItemChangeEntity> lst = new List<Role_BackpackItemChangeEntity>();

        //roleBackpackId 更新背包项的时候，优先从这个背包项更新（修改或者删除）
        while (currSellCount > 0)
        {
            //roleBackpackId 第一次减的时候，这个背包项是肯定存在的

            //出售50个 叠加是20 当前背包项13个

            //1.先从传递过来的背包项中删除物品数量
            Role_BackpackEntity backpackEntity = null;
            if (roleBackpackId > 1)
            {
                //获取传递过来的背包项
                backpackEntity = this.GetEntity(roleBackpackId);
                roleBackpackId = 0;
            }
            else
            {
                //获取其他背包项
                backpackEntity = this.GetEntity(string.Format("[Status]=1 and [RoleId]={0} and [GoodsType]={1} and [GoodsId]={2}",
                    roleId, goodsType, goodsId));
            }

            if (currSellCount >= backpackEntity.GoodsOverlayCount)
            {
                //如果出售的数量 大于等于当前的背包项数量 则把这个背包项删除
                currSellCount -= backpackEntity.GoodsOverlayCount;

                //删除背包项目
                this.Delete(backpackEntity.Id);

                //添加到背包项改变列表
                lst.Add(new Role_BackpackItemChangeEntity()
                {
                    BackpackId = backpackEntity.Id.Value,
                    Type = BackpackItemChangeType.Delete,
                    GoodsType = (GoodsType)goodsType,
                    GoodsId = goodsId,
                    GoodsServerId = backpackEntity.GoodsServerId,
                    GoodsCount = 0
                });
            }
            else
            {
                //如果出售的数量小于 当前背包项的数量 则从当前背包项中 减去出售的数量
                backpackEntity.GoodsOverlayCount -= currSellCount;
                this.Update(backpackEntity);

                //更新背包项目
                currSellCount = 0;

                //添加到背包项改变列表
                lst.Add(new Role_BackpackItemChangeEntity()
                {
                    BackpackId = backpackEntity.Id.Value,
                    Type = BackpackItemChangeType.Update,
                    GoodsType = (GoodsType)goodsType,
                    GoodsId = goodsId,
                    GoodsServerId = backpackEntity.GoodsServerId,
                    GoodsCount = backpackEntity.GoodsOverlayCount
                });
            }
        }

        retValue.SetOutputValue("BackpackItemChange", lst);
        retValue.SetOutputValue("TotalSellPrice", sellPrice * count); //总售价 = 单价 * 数量

        return retValue;
    }

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
        return Reduce(roleId, roleBackpackId, goodsType, goodsId, goodsServerId, sellCount, GoodsOutType.SellToNpc);
    }

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
        //该物品减少
        //根据道具的类型 增加不同的内容 如宝箱会增加物品 经验书会增加经验
        bool isSuccess = false; //是否使用成功
        int oldValue = -1;
        int currValue = -1;


        //1.读取Excel 拿到道具信息
        ItemEntity item = ItemDBModel.Instance.Get(goodsId);

        List<Role_BackpackItemChangeEntity> totalList = new List<Role_BackpackItemChangeEntity>();

        List<VirtualItemUpdateEntity> virtualItemUpdateList = new List<VirtualItemUpdateEntity>(); //虚拟资产更新列表

        if (!string.IsNullOrEmpty(item.UsedItems))
        {
            string[] arr = item.UsedItems.Split('|');
            for (int i = 0; i < arr.Length; i++)
            {
                string[] arr1 = arr[i].Split('_');
                ItemUsedAcquisitionType aType = (ItemUsedAcquisitionType)int.Parse(arr1[0]); //获得物品类型
                int value = int.Parse(arr1[1]);

                List<Role_BackpackItemChangeEntity> changeList = null;
                RoleEntity roleEntity = RoleCacheModel.Instance.GetEntity(roleId);
                Dictionary<string, object> param = new Dictionary<string, object>();
                param["@Id"] = roleId;

                switch (aType)
                {
                    case ItemUsedAcquisitionType.Equip: //增加装备
                        {
                            this.Add(roleId, GoodsInType.ItemUsed, new Role_BackpackAddItemEntity() { GoodsId = value, GoodsType = GoodsType.Equip, GoodsCount = 1 }
                            , ref changeList);
                        }
                        break;
                    case ItemUsedAcquisitionType.Item: //增加道具
                        {
                            this.Add(roleId, GoodsInType.ItemUsed, new Role_BackpackAddItemEntity() { GoodsId = value, GoodsType = GoodsType.Item, GoodsCount = 1 }
                            , ref changeList);
                        }
                        break;
                    case ItemUsedAcquisitionType.Material: //增加材料
                        {
                            this.Add(roleId, GoodsInType.ItemUsed, new Role_BackpackAddItemEntity() { GoodsId = value, GoodsType = GoodsType.Material, GoodsCount = 1 }
                            , ref changeList);
                        }
                        break;
                    case ItemUsedAcquisitionType.Money: //增加元宝
                        {
                            oldValue = roleEntity.Money;
                            currValue = oldValue + value;

                            param["@Money"] = currValue;
                            RoleCacheModel.Instance.Update("[Money]= @Money", "Id=@Id", param); //更新数据库

                            virtualItemUpdateList.Add(new VirtualItemUpdateEntity()
                            {
                                Type = aType,
                                ChangeType = (byte)(currValue >= oldValue ? 0 : 1),
                                OldValue = oldValue,
                                CurrValue = currValue
                            });
                        }
                        break;
                    case ItemUsedAcquisitionType.Gold: //增加金币
                        {
                            oldValue = roleEntity.Gold;
                            currValue = oldValue + value;

                            param["@Gold"] = currValue;
                            RoleCacheModel.Instance.Update("[Gold]= @Gold", "Id=@Id", param); //更新数据库

                            virtualItemUpdateList.Add(new VirtualItemUpdateEntity()
                            {
                                Type = aType,
                                ChangeType = (byte)(currValue >= oldValue ? 0 : 1),
                                OldValue = oldValue,
                                CurrValue = currValue
                            });
                        }
                        break;
                    case ItemUsedAcquisitionType.Exp: //增加经验
                        {
                            oldValue = roleEntity.Exp;
                            currValue = oldValue + value;

                            param["@Exp"] = currValue;
                            RoleCacheModel.Instance.Update("[Exp]= @Exp", "Id=@Id", param); //更新数据库

                            virtualItemUpdateList.Add(new VirtualItemUpdateEntity()
                            {
                                Type = aType,
                                ChangeType = (byte)(currValue >= oldValue ? 0 : 1),
                                OldValue = oldValue,
                                CurrValue = currValue
                            });
                        }
                        break;
                    case ItemUsedAcquisitionType.Power: //增加体力
                        {
                            //如果超过体力上限 不能增加
                        }
                        break;
                    case ItemUsedAcquisitionType.HP: //增加血量
                        {
                            oldValue = roleEntity.CurrHP;
                            currValue = oldValue + value;

                            virtualItemUpdateList.Add(new VirtualItemUpdateEntity()
                            {
                                Type = aType,
                                ChangeType = (byte)(currValue >= oldValue ? 0 : 1),
                                OldValue = oldValue,
                                CurrValue = currValue
                            });
                        }
                        break;
                }

                if (changeList != null)
                {
                    totalList.AddRange(changeList);
                }
            }
        }


        //该物品减少
        MFReturnValue<bool> retValue = Reduce(roleId, roleBackpackId, 1, goodsId, 0, 1, GoodsOutType.ItemUse);

        //背包项改变列表 这次改变 指的是使用了该道具 该道具消失
        List<Role_BackpackItemChangeEntity> lst = retValue.GetOutputValue<List<Role_BackpackItemChangeEntity>>("BackpackItemChange");


        totalList.AddRange(lst);


        //物品增加 或者 虚拟资产增加
        retValue.SetOutputValue("IsSuccess", isSuccess);
        retValue.SetOutputValue("VirtualItemUpdateList", virtualItemUpdateList);
        retValue.SetOutputValue("BackpackItemChange", totalList);
        return retValue;
    }
}