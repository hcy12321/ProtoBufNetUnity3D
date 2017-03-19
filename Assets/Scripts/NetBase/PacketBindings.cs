using System;
using System.Collections.Generic;

namespace NetBase
{
    /// <summary>
    /// 用于包和类型绑定
    /// </summary>
    public class PacketBindings
    {
        /// <summary>
        /// 存储包Id和类型映射字典
        /// </summary>
        /// <returns></returns>
        protected static Dictionary<EnumPacketId, Type> Dict = new Dictionary<EnumPacketId, Type>();

        /// <summary>
        /// 添加包Id和类型映射
        /// </summary>
        /// <param name="packetId"></param>
        /// <param name="t"></param>
        public static void AddBinding(EnumPacketId packetId, Type t)
        {
            Dict[packetId] = t;
        }
        /// <summary>
        /// 是否包含该packetId数据
        /// </summary>
        /// <param name="packetId"></param>
        /// <returns></returns>
        public static bool Contains(EnumPacketId packetId)
        {
            return Dict.ContainsKey(packetId);
        }

        /// <summary>
        /// 获取该packetId对应包
        /// </summary>
        /// <param name="packetId"></param>
        /// <returns></returns>
        public static Type GetTypeByPacketId(EnumPacketId packetId)
        {
            if (!Contains(packetId))
                return null;
            return Dict[packetId];
        }
    }
}