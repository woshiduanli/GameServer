
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回查询背包项消息
/// </summary>
public struct Backpack_SearchReturnProto : IProto
{
    public ushort ProtoCode { get { return 16005; } }

    public int BackpackItemCount; //背包项数量
    public List<BackpackItem> ItemList; //背包项

    /// <summary>
    /// 背包项
    /// </summary>
    public struct BackpackItem
    {
        public int BackpackItemId; //背包项编号
        public byte GoodsType; //物品类型
        public int GoodsId; //物品基础编号
        public int GoodsServerId; //物品服务器端编号
        public int GoodsOverlayCount; //物品叠加数量
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(BackpackItemCount);
            for (int i = 0; i < BackpackItemCount; i++)
            {
                ms.WriteInt(ItemList[i].BackpackItemId);
                ms.WriteByte(ItemList[i].GoodsType);
                ms.WriteInt(ItemList[i].GoodsId);
                ms.WriteInt(ItemList[i].GoodsServerId);
                ms.WriteInt(ItemList[i].GoodsOverlayCount);
            }
            return ms.ToArray();
        }
    }

    public static Backpack_SearchReturnProto GetProto(byte[] buffer)
    {
        Backpack_SearchReturnProto proto = new Backpack_SearchReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.BackpackItemCount = ms.ReadInt();
            proto.ItemList = new List<BackpackItem>();
            for (int i = 0; i < proto.BackpackItemCount; i++)
            {
                BackpackItem _Item = new BackpackItem();
                _Item.BackpackItemId = ms.ReadInt();
                _Item.GoodsType = (byte)ms.ReadByte();
                _Item.GoodsId = ms.ReadInt();
                _Item.GoodsServerId = ms.ReadInt();
                _Item.GoodsOverlayCount = ms.ReadInt();
                proto.ItemList.Add(_Item);
            }
        }
        return proto;
    }
}