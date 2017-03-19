using System.Text;
using System.Collections.Generic;
using ProtoBuf;

namespace NetBase
{
    [ProtoContract]
    public class ModelPacket : Packet
    {
        [ProtoMember(1)]
        public List<string> Data {get;set;}

        public ModelPacket()
        {
            Data = new List<string>();
        }
    }
}