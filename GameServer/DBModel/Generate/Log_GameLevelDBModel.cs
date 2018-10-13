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
public partial class Log_GameLevelDBModel : MFAbstractSQLDBModel<Log_GameLevelEntity>
{
    #region Log_GameLevelDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Log_GameLevelDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Log_GameLevelDBModel instance = null;
    public static Log_GameLevelDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Log_GameLevelDBModel();
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
        get { return "Log_GameLevel"; }
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
                _ColumnList = new List<string> { "Id", "Status", "RoleId", "GameLevelId", "Grade", "Action", "CreateTime" };
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
    protected override SqlParameter[] ValueParas(Log_GameLevelEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@RoleId", entity.RoleId) { DbType = DbType.Int32 },
                new SqlParameter("@GameLevelId", entity.GameLevelId) { DbType = DbType.Int32 },
                new SqlParameter("@Grade", entity.Grade) { DbType = DbType.Byte },
                new SqlParameter("@Action", entity.Action) { DbType = DbType.Byte },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
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
    protected override Log_GameLevelEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        Log_GameLevelEntity entity = new Log_GameLevelEntity();
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
                case "gamelevelid":
                    if (!(reader["GameLevelId"] is DBNull))
                        entity.GameLevelId = Convert.ToInt32(reader["GameLevelId"]);
                    break;
                case "grade":
                    if (!(reader["Grade"] is DBNull))
                        entity.Grade = Convert.ToByte(reader["Grade"]);
                    break;
                case "action":
                    if (!(reader["Action"] is DBNull))
                        entity.Action = Convert.ToByte(reader["Action"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
