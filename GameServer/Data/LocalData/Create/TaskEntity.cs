
using System.Collections;

/// <summary>
/// Task实体
/// </summary>
public partial class TaskEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }

}
