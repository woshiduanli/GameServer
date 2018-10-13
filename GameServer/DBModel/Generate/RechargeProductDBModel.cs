
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
public partial class RechargeProductDBModel : MFAbstractSQLDBModel<RechargeProductEntity>
{
    #region RechargeProductDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private RechargeProductDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static RechargeProductDBModel instance = null;
    public static RechargeProductDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new RechargeProductDBModel();
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
        get { return "RechargeProduct"; }
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
                _ColumnList = new List<string> { "Id", "Status", "ChannelType", "ProductId", "ProductType", "ProductDesc", "Price", "Virtual" };
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
    protected override SqlParameter[] ValueParas(RechargeProductEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Byte },
                new SqlParameter("@ChannelType", entity.ChannelType) { DbType = DbType.Byte },
                new SqlParameter("@ProductId", entity.ProductId) { DbType = DbType.Int32 },
                new SqlParameter("@ProductType", entity.ProductType) { DbType = DbType.Byte },
                new SqlParameter("@ProductDesc", entity.ProductDesc) { DbType = DbType.String },
                new SqlParameter("@Price", entity.Price) { DbType = DbType.Int32 },
                new SqlParameter("@Virtual", entity.Virtual) { DbType = DbType.Int32 },
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
    protected override RechargeProductEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        RechargeProductEntity entity = new RechargeProductEntity();
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
                case "channeltype":
                    if (!(reader["ChannelType"] is DBNull))
                        entity.ChannelType = Convert.ToByte(reader["ChannelType"]);
                    break;
                case "productid":
                    if (!(reader["ProductId"] is DBNull))
                        entity.ProductId = Convert.ToInt32(reader["ProductId"]);
                    break;
                case "producttype":
                    if (!(reader["ProductType"] is DBNull))
                        entity.ProductType = Convert.ToByte(reader["ProductType"]);
                    break;
                case "productdesc":
                    if (!(reader["ProductDesc"] is DBNull))
                        entity.ProductDesc = Convert.ToString(reader["ProductDesc"]);
                    break;
                case "price":
                    if (!(reader["Price"] is DBNull))
                        entity.Price = Convert.ToInt32(reader["Price"]);
                    break;
                case "virtual":
                    if (!(reader["Virtual"] is DBNull))
                        entity.Virtual = Convert.ToInt32(reader["Virtual"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
