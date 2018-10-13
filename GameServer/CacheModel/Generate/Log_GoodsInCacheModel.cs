﻿
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mmcoy.Framework;
using Mmcoy.Framework.AbstractBase;

/// <summary>
/// CacheModel
/// </summary>
public partial class Log_GoodsInCacheModel : MFAbstractCacheModel
{
    #region Log_GoodsInCacheModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Log_GoodsInCacheModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Log_GoodsInCacheModel instance = null;
    public static Log_GoodsInCacheModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Log_GoodsInCacheModel();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region DBModel 数据模型层单例
    /// <summary>
    /// 数据模型层单例
    /// </summary>
    private Log_GoodsInDBModel DBModel { get { return Log_GoodsInDBModel.Instance; } }
    #endregion

    #region Create 创建
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MFReturnValue<object> Create(Log_GoodsInEntity entity)
    {
        return this.DBModel.Create(entity);
    }
    #endregion

    #region Update 修改
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MFReturnValue<object> Update(Log_GoodsInEntity entity)
    {
        return this.DBModel.Update(entity);
    }

    /// <summary>
    /// 按条件修改指定字段
    /// </summary>
    /// <param name="setStr"></param>
    /// <param name="conditionStr"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public MFReturnValue<object> Update(string setStr, string conditionStr, IDictionary<string, object> parameters)
    {
        return this.DBModel.Update(setStr, conditionStr, parameters);
    }
    #endregion

    #region Delete 根据编号删除
    /// <summary>
    /// 根据编号删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public MFReturnValue<object> Delete(int? id)
    {
        return this.DBModel.Delete(id);
    }

    /// <summary>
    /// 根据多个编号删除
    /// </summary>
    /// <param name="ids"></param>
    /// <returns></returns>
    public MFReturnValue<object> Delete(string ids)
    {
        return this.DBModel.Delete(ids);
    }
    #endregion

    #region GetCount 根据条件查询数量
    /// <summary>
    /// 根据条件查询数量
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public int GetCount(string condition = "")
    {
        return this.DBModel.GetCount(condition);
    }
    #endregion

    #region GetEntity
    /// <summary>
    /// 根据编号查询实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Log_GoodsInEntity GetEntity(int? id)
    {
        return this.DBModel.GetEntity(id);
    }

    /// <summary>
    /// 根据条件查询实体
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="isAutoStatus"></param>
    /// <returns></returns>
    public Log_GoodsInEntity GetEntity(string condition, bool isAutoStatus = true)
    {
        return this.DBModel.GetEntity(condition, isAutoStatus);
    }
    #endregion

    #region GetList 获取列表
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columns"></param>
    /// <param name="condition"></param>
    /// <param name="orderby"></param>
    /// <param name="isDesc"></param>
    /// <param name="isAutoStatus"></param>
    /// <param name="isEfficient"></param>
    /// <returns></returns>
    public List<Log_GoodsInEntity> GetList(string tableName = "", string columns = "*", string condition = "", string orderby = "Id", bool isDesc = true, bool isAutoStatus = true, bool isEfficient = false)
    {
        return this.DBModel.GetList(tableName, columns, condition, orderby, isDesc, isAutoStatus, isEfficient);
    }

    /// <summary>
    /// 获取分页列表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="columns"></param>
    /// <param name="condition"></param>
    /// <param name="orderby"></param>
    /// <param name="isDesc"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <param name="isAutoStatus"></param>
    /// <param name="isEfficient"></param>
    /// <returns></returns>
    public MFReturnValue<List<Log_GoodsInEntity>> GetPageList(string tableName = "", string columns = "*", string condition = "", string orderby = "Id", bool isDesc = true, int? pageSize = 20, int? pageIndex = 1, bool isAutoStatus = true, bool isEfficient = false)
    {
        return this.DBModel.GetPageList(tableName, columns, condition, orderby, isDesc, pageSize, pageIndex, isAutoStatus, isEfficient);
    }
    #endregion
}
