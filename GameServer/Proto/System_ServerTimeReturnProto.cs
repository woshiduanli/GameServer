//==
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回服务器时间
/// </summary>
public struct System_ServerTimeReturnProto : IProto
{
    public ushort ProtoCode { get { return 14002; } }

    public float LocalTime; //客户端发送的本地时间(毫秒)
    public long ServerTime; //服务器时间(毫秒)

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteFloat(LocalTime);
            ms.WriteLong(ServerTime);
            return ms.ToArray();
        }
    }

    public static System_ServerTimeReturnProto GetProto(byte[] buffer)
    {
        System_ServerTimeReturnProto proto = new System_ServerTimeReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.LocalTime = ms.ReadFloat();
            proto.ServerTime = ms.ReadLong();
        }
        return proto;
    }
}