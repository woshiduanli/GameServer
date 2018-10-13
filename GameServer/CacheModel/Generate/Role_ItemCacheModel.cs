
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
public partial class Role_ItemCacheModel : MFAbstractCacheModel
{
    #region Role_ItemCacheModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Role_ItemCacheModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Role_ItemCacheModel instance = null;
    public static Role_ItemCacheModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Role_ItemCacheModel();
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
    private Role_ItemDBModel DBModel { get { return Role_ItemDBModel.Instance; } }
    #endregion

    #region Create 创建
    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MFReturnValue<object> Create(Role_ItemEntity entity)
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
    public MFReturnValue<object> Update(Role_ItemEntity entity)
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
    public Role_ItemEntity GetEntity(int? id)
    {
        return this.DBModel.GetEntity(id);
    }

    /// <summary>
    /// 根据条件查询实体
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="isAutoStatus"></param>
    /// <returns></returns>
    public Role_ItemEntity GetEntity(string condition, bool isAutoStatus = true)
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
    public List<Role_ItemEntity> GetList(string tableName = "", string columns = "*", string condition = "", string orderby = "Id", bool isDesc = true, bool isAutoStatus = true, bool isEfficient = false)
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
    public MFReturnValue<List<Role_ItemEntity>> GetPageList(string tableName = "", string columns = "*", string condition = "", string orderby = "Id", bool isDesc = true, int? pageSize = 20, int? pageIndex = 1, bool isAutoStatus = true, bool isEfficient = false)
    {
        return this.DBModel.GetPageList(tableName, columns, condition, orderby, isDesc, pageSize, pageIndex, isAutoStatus, isEfficient);
    }
    #endregion
}
