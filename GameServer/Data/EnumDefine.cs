
using System.Collections;

/// <summary>
/// 语言
/// </summary>
public enum Language
{
    CN,
    EN
}

#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    LogOn,
    SelectRole,
    WorldMap,
    GameLevel,
}
#endregion

/// <summary>
/// 消息类型
/// </summary>
public enum MessageViewType
{
    Ok,
    OkAndCancel
}

#region WindowUIType 窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
    /// <summary>
    /// 进入区服
    /// </summary>
    GameServerEnter,
    /// <summary>
    /// 区服选择
    /// </summary>
    GameServerSelect,
    /// <summary>
    /// 角色信息
    /// </summary>
    RoleInfo,
    /// <summary>
    /// 剧情关卡地图
    /// </summary>
    GameLevelMap,
    /// <summary>
    /// 剧情关卡详情
    /// </summary>
    GameLevelDetail,
}
#endregion

#region WindowUIContainerType UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum WindowUIContainerType
{
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
    /// <summary>
    /// 居中
    /// </summary>
    Center
}
#endregion

#region WindowShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2
}
#endregion

/// <summary>
/// 角色状态
/// </summary>
public enum RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 待机
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 跑了
    /// </summary>
    Run = 2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack = 3,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt = 4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 5,
    /// <summary>
    /// 选择
    /// </summary>
    Select = 11
}

/// <summary>
/// 角色动画状态
/// </summary>
public enum RoleAnimatorState
{
    Idle_Normal = 1,
    Idle_Fight = 2,
    Run = 3,
    Hurt = 4,
    Die = 5,
    Select = 6,
    XiuXian = 7,
    PhyAttack1 = 11,
    PhyAttack2 = 12,
    PhyAttack3 = 13,
    Skill1 = 14,
    Skill2 = 15,
    Skill3 = 16,
    Skill4 = 17,
    Skill5 = 18,
    Skill6 = 19,
}

/// <summary>
/// 角色待机状态
/// </summary>
public enum RoleIdleState
{
    IdleNormal,
    IdleFight
}

public enum ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToPhyAttack,
    ToSkill,
    ToSelect,
    ToXiuXian,
    CurrState
}

/// <summary>
/// 角色攻击类型
/// </summary>
public enum RoleAttackType
{
    /// <summary>
    /// 物理攻击
    /// </summary>
    PhyAttack,
    /// <summary>
    /// 技能攻击
    /// </summary>
    SkillAttack
}

/// <summary>
/// 游戏关卡难度等级
/// </summary>
public enum GameLevelGrade
{
    /// <summary>
    /// 普通
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 困难
    /// </summary>
    Hard = 1,
    /// <summary>
    /// 地狱
    /// </summary>
    Hell = 2
}

/// <summary>
/// 物品类型
/// </summary>
public enum GoodsType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equip = 0,
    /// <summary>
    /// 道具
    /// </summary>
    Item = 1,
    /// <summary>
    /// 材料
    /// </summary>
    Material = 2
}

#region ChangeType 虚拟物品更改方式
/// <summary>
/// 虚拟物品更改方式
/// </summary>
public enum ChangeType
{
    Add = 0,
    Reduce = 1
}
#endregion

#region MoneyAddType 元宝增加方式
/// <summary>
/// 元宝增加方式
/// </summary>
public enum MoneyAddType
{
    None = 0,
    /// <summary>
    /// 充值
    /// </summary>
    Recharge = 1,
    /// <summary>
    /// 使用元宝票
    /// </summary>
    UseMoneyTicket = 2,
    /// <summary>
    /// 系统奖励
    /// </summary>
    SysRewards = 3,
    /// <summary>
    /// GM奖励或补偿
    /// </summary>
    GMRewards = 4,
    /// <summary>
    /// 道具使用
    /// </summary>
    ItemUsed = 5
}
#endregion

#region MoneyReduceType 元宝减少方式
/// <summary>
/// 元宝减少方式
/// </summary>
public enum MoneyReduceType
{
    None = 0,
    /// <summary>
    /// 购买商城物品
    /// </summary>
    BuyShopProduct = 1,
    /// <summary>
    /// 兑换成元宝票
    /// </summary>
    ChangeToMoneyTicket = 2,
    /// <summary>
    /// 原地复活
    /// </summary>
    Resurgence = 3
}
#endregion

#region GoldAddType 金币增加方式
/// <summary>
/// 金币增加方式
/// </summary>
public enum GoldAddType
{
    /// <summary>
    /// 元宝兑换
    /// </summary>
    MoneyChange = 1,
    /// <summary>
    /// 物品出售
    /// </summary>
    GoodsSell = 2,
    /// <summary>
    /// 系统奖励
    /// </summary>
    SysRewards = 3,
    /// <summary>
    /// GM奖励或补偿
    /// </summary>
    GMRewards = 4,
    /// <summary>
    /// 道具使用
    /// </summary>
    ItemUsed = 5
}
#endregion

/// <summary>
/// 金币减少方式
/// </summary>
public enum GoldReduceType
{
    None = 0,
    /// <summary>
    /// 购买物品
    /// </summary>
    BubGoods = 1,

}


/// <summary>
/// 物品入库类型
/// </summary>
public enum GoodsInType
{
    /// <summary>
    /// 任务奖励
    /// </summary>
    TaskRewards = 0,
    /// <summary>
    /// 掉落
    /// </summary>
    DropOut = 1,
    /// <summary>
    /// NPC购买
    /// </summary>
    NpcBuy = 2,
    /// <summary>
    /// 物品制造
    /// </summary>
    Make = 3,
    /// <summary>
    /// 商城购买
    /// </summary>
    ShopBuy = 4,
    /// <summary>
    /// 交易获得
    /// </summary>
    PlayerTrading = 5,
    /// <summary>
    /// 道具使用增加
    /// </summary>
    ItemUsed = 6
}

/// <summary>
/// 物品出库类型
/// </summary>
public enum GoodsOutType
{
    /// <summary>
    /// 丢弃
    /// </summary>
    Throw = 0,
    /// <summary>
    /// 卖给NPC
    /// </summary>
    SellToNpc = 1,
    /// <summary>
    /// 卖给玩家
    /// </summary>
    PlayerTrading = 2,
    /// <summary>
    /// 道具使用
    /// </summary>
    ItemUse = 3,
    /// <summary>
    /// 材料使用
    /// </summary>
    MaterialUse = 4
}

/// <summary>
/// 物品库转换类型
/// </summary>
public enum GoodsChangeType
{
    /// <summary>
    ///  穿上
    /// </summary>
    PutOn = 0,
    /// <summary>
    /// 脱下
    /// </summary>
    TakeOff = 1,
    /// <summary>
    /// 镶嵌
    /// </summary>
    Inlay = 2,
    /// <summary>
    /// 摘除
    /// </summary>
    Remove = 3
}

/// <summary>
/// 背包项改变枚举
/// </summary>
public enum BackpackItemChangeType
{
    /// <summary>
    /// 增加
    /// </summary>
    Add = 0,
    /// <summary>
    /// 修改
    /// </summary>
    Update = 1,
    /// <summary>
    /// 删除
    /// </summary>
    Delete = 2
}

/// <summary>
/// 道具类型
/// </summary>
public enum ItemType
{
    /// <summary>
    /// 元宝
    /// </summary>
    Money = 1,
    /// <summary>
    /// 金币
    /// </summary>
    Gold = 2,
    /// <summary>
    /// 经验
    /// </summary>
    Exp = 3,
    /// <summary>
    /// 体力
    /// </summary>
    Power = 4,
    /// <summary>
    /// 宝箱
    /// </summary>
    TreasureChests = 5,
    /// <summary>
    /// 复活药
    /// </summary>
    Reactivators = 6,
    /// <summary>
    /// 回血药
    /// </summary>
    Medigel = 7
}

/// <summary>
/// 道具使用后获得物类型
/// </summary>
public enum ItemUsedAcquisitionType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equip = 0,
    /// <summary>
    /// 道具
    /// </summary>
    Item = 1,
    /// <summary>
    /// 材料
    /// </summary>
    Material = 2,
    /// <summary>
    /// 元宝
    /// </summary>
    Money = 100,
    /// <summary>
    /// 金币
    /// </summary>
    Gold = 101,
    /// <summary>
    /// 经验
    /// </summary>
    Exp = 200,
    /// <summary>
    /// 体力
    /// </summary>
    Power = 201,
    /// <summary>
    /// 生命
    /// </summary>
    HP = 202
}