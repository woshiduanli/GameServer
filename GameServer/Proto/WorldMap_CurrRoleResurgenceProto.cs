//===================================================

using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送角色复活消息
/// </summary>
public struct WorldMap_CurrRoleResurgenceProto : IProto
{
    public ushort ProtoCode { get { return 13015; } }

    public int Type; //复活类型

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(Type);
            return ms.ToArray();
        }
    }

    public static WorldMap_CurrRoleResurgenceProto GetProto(byte[] buffer)
    {
        WorldMap_CurrRoleResurgenceProto proto = new WorldMap_CurrRoleResurgenceProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.Type = ms.ReadInt();
        }
        return proto;
    }
}