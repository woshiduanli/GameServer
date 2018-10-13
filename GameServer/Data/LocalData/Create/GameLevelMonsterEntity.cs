

using System.Collections;

/// <summary>
/// GameLevelMonster实体
/// </summary>
public partial class GameLevelMonsterEntity : AbstractEntity
{
    /// <summary>
    /// 游戏关卡Id
    /// </summary>
    public int GameLevelId { get; set; }

    /// <summary>
    /// 难度等级
    /// </summary>
    public int Grade { get; set; }

    /// <summary>
    /// 区域Id
    /// </summary>
    public int RegionId { get; set; }

    /// <summary>
    /// 精灵Id
    /// </summary>
    public int SpriteId { get; set; }

    /// <summary>
    /// 精灵数量
    /// </summary>
    public int SpriteCount { get; set; }

    /// <summary>
    /// 掉落经验
    /// </summary>
    public int Exp { get; set; }

    /// <summary>
    /// 掉落金币
    /// </summary>
    public int Gold { get; set; }

    /// <summary>
    /// 掉落装备
    /// </summary>
    public string DropEquip { get; set; }

    /// <summary>
    /// 掉落道具
    /// </summary>
    public string DropItem { get; set; }

    /// <summary>
    /// 掉落材料
    /// </summary>
    public string DropMaterial { get; set; }

}
