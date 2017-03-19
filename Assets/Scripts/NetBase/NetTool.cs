using System;
using System.IO;
using ProtoBuf;
using UnityEngine;
namespace NetBase 
{
    public class NetTool 
    {

        /// <summary>
        /// 通过包转换为字节数组
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static byte[] GetBytesFromPacket<TPacket>(TPacket packet) where TPacket: IPacket, new()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize<TPacket>(ms, packet);
                    ms.Position = 0;
                    byte[] result = new byte[ms.Length];
                    ms.Read(result, 0, result.Length);
                    return result;
                }
            }
            catch(Exception ex)
            {
                Debug.LogErrorFormat("GetBytesFromPacket Err:{0}", ex);
            }
            return null;
        }

        /// <summary>
        /// 通过字节数组转化为包
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static TPacket GetPacketFromBytes<TPacket>(byte[] data) where TPacket:IPacket, new()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    ms.Position = 0;
                    return Serializer.Deserialize<TPacket>(ms);
                }
            }
            catch(Exception ex)
            {
                Debug.LogErrorFormat("GetPacketFromBytes Err:{0}", ex);
            }
            return default(TPacket);
        }


        /// <summary>
        /// 通过类型转换数据
        /// </summary>
        /// <param name="t"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IPacket GetPacketFromBytes(Type t, byte[] data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(data, 0, data.Length);
                    ms.Position = 0;
                    return Serializer.Deserialize(t, ms) as IPacket;
                }
            }
            catch(Exception ex)
            {
                Debug.LogErrorFormat("GetPacketFromBytes Err:{0}", ex);
            }
            return default(IPacket);
        }
        
    }
}