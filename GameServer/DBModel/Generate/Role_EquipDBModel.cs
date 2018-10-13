
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
public partial class Role_EquipDBModel : MFAbstractSQLDBModel<Role_EquipEntity>
{
    #region Role_EquipDBModel 私有构造
    /// <summary>
    /// 私有构造
    /// </summary>
    private Role_EquipDBModel()
    {

    }
    #endregion

    #region 单例
    private static object lock_object = new object();
    private static Role_EquipDBModel instance = null;
    public static Role_EquipDBModel Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lock_object)
                {
                    if (instance == null)
                    {
                        instance = new Role_EquipDBModel();
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
        get { return "Role_Equip"; }
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
                _ColumnList = new List<string> { "Id", "Status", "RoleId", "EquipId", "Type", "Quality", "Star", "EnchantLevel", "EnchantCount", "BaseAttr1Type", "BaseAttr1Value", "BaseAttr2Type", "BaseAttr2Value", "HP", "MP", "Attack", "Defense", "Hit", "Dodge", "Cri", "Res", "CreateTime", "UpdateTime", "IsPutOn" };
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
    protected override SqlParameter[] ValueParas(Role_EquipEntity entity)
    {
        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@Id", entity.Id) { DbType = DbType.Int32 },
                new SqlParameter("@Status", entity.Status) { DbType = DbType.Int32 },
                new SqlParameter("@RoleId", entity.RoleId) { DbType = DbType.Int32 },
                new SqlParameter("@EquipId", entity.EquipId) { DbType = DbType.Int32 },
                new SqlParameter("@Type", entity.Type) { DbType = DbType.Byte },
                new SqlParameter("@Quality", entity.Quality) { DbType = DbType.Byte },
                new SqlParameter("@Star", entity.Star) { DbType = DbType.Byte },
                new SqlParameter("@EnchantLevel", entity.EnchantLevel) { DbType = DbType.Int32 },
                new SqlParameter("@EnchantCount", entity.EnchantCount) { DbType = DbType.Int32 },
                new SqlParameter("@BaseAttr1Type", entity.BaseAttr1Type) { DbType = DbType.Byte },
                new SqlParameter("@BaseAttr1Value", entity.BaseAttr1Value) { DbType = DbType.Int32 },
                new SqlParameter("@BaseAttr2Type", entity.BaseAttr2Type) { DbType = DbType.Byte },
                new SqlParameter("@BaseAttr2Value", entity.BaseAttr2Value) { DbType = DbType.Int32 },
                new SqlParameter("@HP", entity.HP) { DbType = DbType.Int32 },
                new SqlParameter("@MP", entity.MP) { DbType = DbType.Int32 },
                new SqlParameter("@Attack", entity.Attack) { DbType = DbType.Int32 },
                new SqlParameter("@Defense", entity.Defense) { DbType = DbType.Int32 },
                new SqlParameter("@Hit", entity.Hit) { DbType = DbType.Int32 },
                new SqlParameter("@Dodge", entity.Dodge) { DbType = DbType.Int32 },
                new SqlParameter("@Cri", entity.Cri) { DbType = DbType.Int32 },
                new SqlParameter("@Res", entity.Res) { DbType = DbType.Int32 },
                new SqlParameter("@CreateTime", entity.CreateTime) { DbType = DbType.DateTime },
                new SqlParameter("@UpdateTime", entity.UpdateTime) { DbType = DbType.DateTime },
                new SqlParameter("@IsPutOn", entity.IsPutOn) { DbType = DbType.Boolean },
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
    protected override Role_EquipEntity GetEntitySelfProperty(IDataReader reader, DataTable table)
    {
        Role_EquipEntity entity = new Role_EquipEntity();
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
                case "equipid":
                    if (!(reader["EquipId"] is DBNull))
                        entity.EquipId = Convert.ToInt32(reader["EquipId"]);
                    break;
                case "type":
                    if (!(reader["Type"] is DBNull))
                        entity.Type = Convert.ToByte(reader["Type"]);
                    break;
                case "quality":
                    if (!(reader["Quality"] is DBNull))
                        entity.Quality = Convert.ToByte(reader["Quality"]);
                    break;
                case "star":
                    if (!(reader["Star"] is DBNull))
                        entity.Star = Convert.ToByte(reader["Star"]);
                    break;
                case "enchantlevel":
                    if (!(reader["EnchantLevel"] is DBNull))
                        entity.EnchantLevel = Convert.ToInt32(reader["EnchantLevel"]);
                    break;
                case "enchantcount":
                    if (!(reader["EnchantCount"] is DBNull))
                        entity.EnchantCount = Convert.ToInt32(reader["EnchantCount"]);
                    break;
                case "baseattr1type":
                    if (!(reader["BaseAttr1Type"] is DBNull))
                        entity.BaseAttr1Type = Convert.ToByte(reader["BaseAttr1Type"]);
                    break;
                case "baseattr1value":
                    if (!(reader["BaseAttr1Value"] is DBNull))
                        entity.BaseAttr1Value = Convert.ToInt32(reader["BaseAttr1Value"]);
                    break;
                case "baseattr2type":
                    if (!(reader["BaseAttr2Type"] is DBNull))
                        entity.BaseAttr2Type = Convert.ToByte(reader["BaseAttr2Type"]);
                    break;
                case "baseattr2value":
                    if (!(reader["BaseAttr2Value"] is DBNull))
                        entity.BaseAttr2Value = Convert.ToInt32(reader["BaseAttr2Value"]);
                    break;
                case "hp":
                    if (!(reader["HP"] is DBNull))
                        entity.HP = Convert.ToInt32(reader["HP"]);
                    break;
                case "mp":
                    if (!(reader["MP"] is DBNull))
                        entity.MP = Convert.ToInt32(reader["MP"]);
                    break;
                case "attack":
                    if (!(reader["Attack"] is DBNull))
                        entity.Attack = Convert.ToInt32(reader["Attack"]);
                    break;
                case "defense":
                    if (!(reader["Defense"] is DBNull))
                        entity.Defense = Convert.ToInt32(reader["Defense"]);
                    break;
                case "hit":
                    if (!(reader["Hit"] is DBNull))
                        entity.Hit = Convert.ToInt32(reader["Hit"]);
                    break;
                case "dodge":
                    if (!(reader["Dodge"] is DBNull))
                        entity.Dodge = Convert.ToInt32(reader["Dodge"]);
                    break;
                case "cri":
                    if (!(reader["Cri"] is DBNull))
                        entity.Cri = Convert.ToInt32(reader["Cri"]);
                    break;
                case "res":
                    if (!(reader["Res"] is DBNull))
                        entity.Res = Convert.ToInt32(reader["Res"]);
                    break;
                case "createtime":
                    if (!(reader["CreateTime"] is DBNull))
                        entity.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                    break;
                case "updatetime":
                    if (!(reader["UpdateTime"] is DBNull))
                        entity.UpdateTime = Convert.ToDateTime(reader["UpdateTime"]);
                    break;
                case "isputon":
                    if (!(reader["IsPutOn"] is DBNull))
                        entity.IsPutOn = Convert.ToBoolean(reader["IsPutOn"]);
                    break;
            }
        }
        return entity;
    }
    #endregion

    #endregion
}
