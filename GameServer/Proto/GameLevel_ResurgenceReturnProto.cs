
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回复活消息
/// </summary>
public struct GameLevel_ResurgenceReturnProto : IProto
{
    public ushort ProtoCode { get { return 12008; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
                ms.WriteInt(MsgCode);
            }
            return ms.ToArray();
        }
    }

    public static GameLevel_ResurgenceReturnProto GetProto(byte[] buffer)
    {
        GameLevel_ResurgenceReturnProto proto = new GameLevel_ResurgenceReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
                proto.MsgCode = ms.ReadInt();
            }
        }
        return proto;
    }
}