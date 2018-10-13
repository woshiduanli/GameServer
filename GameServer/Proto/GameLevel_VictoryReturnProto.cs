
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回战斗胜利消息
/// </summary>
public struct GameLevel_VictoryReturnProto : IProto
{
    public ushort ProtoCode { get { return 12004; } }

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

    public static GameLevel_VictoryReturnProto GetProto(byte[] buffer)
    {
        GameLevel_VictoryReturnProto proto = new GameLevel_VictoryReturnProto();
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