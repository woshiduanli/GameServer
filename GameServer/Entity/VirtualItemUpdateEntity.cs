using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp.Entity
{
    /// <summary>
    /// 虚拟物品更新实体
    /// </summary>
    public class VirtualItemUpdateEntity
    {
        public ItemUsedAcquisitionType Type; //更新后获得物品类型
        public int OldValue; //更新前
        public int CurrValue; //更新后当前值
        public byte ChangeType; //更新方式 0=增加 1=减少
    }
}