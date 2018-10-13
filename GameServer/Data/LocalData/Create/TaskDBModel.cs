

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Task数据管理
/// </summary>
public partial class TaskDBModel : AbstractDBModel<TaskDBModel, TaskEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Task.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override TaskEntity MakeEntity(GameDataTableParser parse)
    {
        TaskEntity entity = new TaskEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Status = parse.GetFieldValue("Status").ToInt();
        entity.Content = parse.GetFieldValue("Content");
        return entity;
    }
}
