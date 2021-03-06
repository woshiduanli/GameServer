﻿
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Mmcoy.Framework.AbstractBase;

/// <summary>
/// DBModel
/// </summary>
public partial class Log_ShopBuyProductDBModel : MFAbstractSQLDBModel<Log_ShopBuyProductEntity>
{
    #region Log_ShopBuyProductDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Log_ShopBuyProductDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Log_ShopBuyProductDBModel instance = null;
    public static Log_ShopBuyProductDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Log_ShopBuyProductDBModel();
                    }
                }
            }
            return instance;
        }
    }
    #endregion

    #region 实现基类的属性和方法

    #region ConnectionString 数据库连接字符串
    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    protected override string ConnectionString
    {
        get { return DBConn.DBGameServer; }
    }
    #endregion

    #region TableName 表名
    /// <summary>
    /// 表名
    /// </summary>
    protected override string TableName
    {
        get { return "Log_ShopBuyProduct"; }
    }
    #endregion

    #region ColumnList 列名集合
    private IList<string> _ColumnList;
    /// <summary>
    /// 列名集合
    /// </summary>
    protected override IList<string> ColumnList
    {
        get
        {
            if (_ColumnList == null)
            {
                _ColumnList = new List<string> { "Id", "Status", "RoleId", "ShopId", "GoodsType", "GoodsId", "GoodsCount", "CreateTime", "UpdateTime" };
            }
            return _ColumnList;
        }
    }
    #endregion

    #region ValueParas 转换参数
    /// <summary>
    /// 转换参数
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    protected override SqlParameter[] ValueParas(Log_ShopBuyProductEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Int32 },
                new SqlParameter("@RoleId", entity.RoleId) { DbType = DbType.Int32 },
                new SqlParameter("@ShopId", entity.ShopId) { DbType = DbType.Int32 },
                new SqlParameter("@GoodsType", entity.GoodsType) { DbType = DbType.Byte },
                new SqlParameter("@GoodsId", entity.GoodsId) { DbType = DbType.Int32 },
                new SqlParameter("@GoodsCount", entity.GoodsCount) { DbType = DbType.Int32 },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
                new SqlParameter("@UpdateTime", entity.UpdateTime) { DbType = DbType.DateTime },
                new SqlParameter("@RetMsg", SqlDbType.NVarChar, 255),
                new SqlParameter("@ReturnValue", SqlDbType.Int)
            };
        return parameters;
    }
    #endregion

    #region GetEntitySelfProperty 封装对象
    /// <summary>
    /// 封装对象
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="table"></param>
    /// <returns></returns>
    protected override Log_ShopBuyProductEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        Log_ShopBuyProductEntity entity = new Log_ShopBuyProductEntity();
        foreach (DataRow row in table.Rows)
        {
            var colName = row.Field<string>(0);
            if (reader[colName] is DBNull)
                continue;
            switch (colName.ToLower())
            {
                case "id":
                    if (!(reader["Id"] is DBNull))
                        entity.Id = Convert.ToInt32(reader["Id"]);
                    break;
                case "status":
                    if (!(reader["Status"] is DBNull))
                        entity.Status = (EnumEntityStatus)Convert.ToInt32(reader["Status"]);
                    break;
                case "roleid":
                    if (!(reader["RoleId"] is DBNull))
                        entity.RoleId = Convert.ToInt32(reader["RoleId"]);
                    break;
                case "shopid":
                    if (!(reader["ShopId"] is DBNull))
                        entity.ShopId = Convert.ToInt32(reader["ShopId"]);
                    break;
                case "goodstype":
                    if (!(reader["GoodsType"] is DBNull))
                        entity.GoodsType = Convert.ToByte(reader["GoodsType"]);
                    break;
                case "goodsid":
                    if (!(reader["GoodsId"] is DBNull))
                        entity.GoodsId = Convert.ToInt32(reader["GoodsId"]);
                    break;
                case "goodscount":
                    if (!(reader["GoodsCount"] is DBNull))
                        entity.GoodsCount = Convert.ToInt32(reader["GoodsCount"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
                case "updatetime":
                    if (!(reader["UpdateTime"] is DBNull))
                        entity.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
