using System;
using UnityEngine;

namespace NetBase
{
    public class Session : ISession
    {

        protected string Ip = "";

        protected int Port = 0;
        private Action<IPacket> ProcessPacket;

        public Action<IPacket> GetProcessPacket()
        {
            return ProcessPacket;
        }

        public void SetProcessPacket(Action<IPacket> value)
        {
            ProcessPacket = value;
        }

        public virtual void Config(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public virtual void Send<TPacket>(TPacket packet) where TPacket: IPacket,new()
        {
            
        }

        public virtual void Start()
        {
            
        }

        public virtual void Update()
        {
            
        }

        /// <summary>
        /// 处理包数据
        /// </summary>
        /// <param name="data"></param>
        protected virtual void ProcessData(byte[] data)
        {
            var lengthBytes = new byte[sizeof(int)];
            var packetIdBytes = new byte[sizeof(int)];
            int offset = lengthBytes.Length + packetIdBytes.Length;
            if (data.Length < offset)
            {
                Debug.LogError("Process packet data length is too little.");
                return;
            }
            var bytes = new byte[data.Length - offset];
            for (int i = 0; i < lengthBytes.Length; ++i)
            {
                lengthBytes[i] = data[i];
            }

            for (int i = 0; i < packetIdBytes.Length; ++i)
            {
                packetIdBytes[i] = data[i+lengthBytes.Length];
            }
            try 
            {
                EnumPacketId packetId = (EnumPacketId)BitConverter.ToInt32(packetIdBytes, 0);
                for (int i = 0; i < bytes.Length; ++i)
                {
                    bytes[i] = data[i+offset];
                }

                var packet = NetTool.GetPacketFromBytes(PacketBindings.GetTypeByPacketId(packetId), bytes);
                GetProcessPacket()(packet);
            }
            catch (Exception ex)
            {
                Debug.LogErrorFormat("Can't get packet data or process packet: {0}", ex);
            }
            
        }
    }
}