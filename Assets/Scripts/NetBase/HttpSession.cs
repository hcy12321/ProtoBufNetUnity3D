using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System;

namespace NetBase
{
    /// <summary>
    /// 用于发送Http请求的Session
    /// </summary>
    public class HttpSession :Session
    {
        /// <summary>
        /// 存储待返回的请求
        /// </summary>
        protected List<WWW> PendingReqList = new List<WWW>();

        /// <summary>
        /// Http请求的地址
        /// </summary>
        protected string Url = "";

        public override void Config(string ip, int port)
        {
            base.Config(ip, port);
            Url = string.Format("http://{0}:{1}/", ip, port);
        }

        /// <summary>
        /// 发包
        /// </summary>
        /// <param name="packet"></param>
        public override void Send<TPacket>(TPacket packet)
        {
            var bytes = NetTool.GetBytesFromPacket(packet);
            var data = new byte[bytes.Length + sizeof(int)*2];
            var lengthBytes = BitConverter.GetBytes(bytes.Length);
            var packetIdBytes = BitConverter.GetBytes((int)packet.PacketId);
            lengthBytes.CopyTo(data, 0);
            packetIdBytes.CopyTo(data, sizeof(int));
            bytes.CopyTo(data, sizeof(int) * 2);
            WWW w = new WWW(Url, data);
            PendingReqList.Add(w);
        }
        
        /// <summary>
        /// 每帧执行方法
        /// </summary>
        public override void Update()
        {
            for (int i = PendingReqList.Count - 1; i >= 0; --i) 
            {
                var w = PendingReqList[i];
                if (!w.isDone)
                    continue;
                ProcessData(w.bytes);
                PendingReqList.RemoveAt(i);
            }
        }

        
    }
}