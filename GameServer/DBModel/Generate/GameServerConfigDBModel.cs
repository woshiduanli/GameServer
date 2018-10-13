/// <summary>

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
public partial class GameServerConfigDBModel : MFAbstractSQLDBModel<GameServerConfigEntity>
{
    #region GameServerConfigDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private GameServerConfigDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static GameServerConfigDBModel instance = null;
    public static GameServerConfigDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new GameServerConfigDBModel();
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
        get { return "GameServerConfig"; }
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
                _ColumnList = new List<string> { "Id", "Status", "ConfigCode", "IsOpen", "Param" };
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
    protected override SqlParameter[] ValueParas(GameServerConfigEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@ConfigCode", entity.ConfigCode) { DbType = DbType.String },
                new SqlParameter("@IsOpen", entity.IsOpen) { DbType = DbType.Byte },
                new SqlParameter("@Param", entity.Param) { DbType = DbType.String },
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
    protected override GameServerConfigEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        GameServerConfigEntity entity = new GameServerConfigEntity();
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
                case "configcode":
                    if (!(reader["ConfigCode"] is DBNull))
                        entity.ConfigCode = Convert.ToString(reader["ConfigCode"]);
                    break;
                case "isopen":
                    if (!(reader["IsOpen"] is DBNull))
                        entity.IsOpen = Convert.ToByte(reader["IsOpen"]);
                    break;
                case "param":
                    if (!(reader["Param"] is DBNull))
                        entity.Param = Convert.ToString(reader["Param"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
