using System;
using System.Collections.Generic;
using UnityEngine;

namespace NetBase
{
    public class NetManager : MonoBehaviour
    {
        /// <summary>
        /// 存储cmd和包处理方法的字典
        /// </summary>
        /// <returns></returns>
        protected Dictionary<EnumPacketId, Action<IPacket>> ProcessPacketDict = new Dictionary<EnumPacketId, Action<IPacket>>();

        /// <summary>
        /// 存储所有Session
        /// </summary>
        protected List<ISession> SessionList = new List<ISession>();

        /// <summary>
        /// 开始
        /// </summary>
        void Start()
        {
            
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            foreach (var session in SessionList)
            {
                session.Update();
            }
        }

        /// <summary>
        /// 注册cmd和处理方法
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="del"></param>
        public void RegisterProcess(EnumPacketId cmd, Action<IPacket> del)
        {
            ProcessPacketDict[cmd] = del;
        }

        /// <summary>
        /// 处理包的主逻辑
        /// </summary>
        /// <param name="packet"></param>
        private void ProcessPacket(IPacket packet)
        {
            EnumPacketId cmd = (EnumPacketId)packet.PacketId;
            if (!ProcessPacketDict.ContainsKey(cmd))
            {
                Debug.LogWarningFormat("Can't find method to process {0}", cmd);
                return;
            }
            ProcessPacketDict[cmd](packet);
        }

        /// <summary>
        /// 添加Session
        /// </summary>
        /// <param name="session"></param>
        public void AddSession(ISession session)
        {
            session.SetProcessPacket(ProcessPacket);
            SessionList.Add(session);
        }
    }
}