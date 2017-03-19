using System.Text;
using ProtoBuf;

namespace NetBase
{
    [ProtoContract]
    public class Packet : IPacket 
    {
        [ProtoMember(1)]
        public int PacketId {get;set;}
    }
}