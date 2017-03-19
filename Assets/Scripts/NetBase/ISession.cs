

using System;

namespace NetBase
{
    /// <summary>
    /// 抽象的网络访问接口
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// 发送包
        /// </summary>
        /// <param name="packet"></param>
        void Send<TPacket>(TPacket packet) where TPacket: IPacket,new();

        /// <summary>
        /// 得到委托
        /// </summary>
        /// <returns></returns>
        Action<IPacket> GetProcessPacket();
        /// <summary>
        /// 设置委托
        /// </summary>
        /// <param name="value"></param>
        void SetProcessPacket(Action<IPacket> value);

        /// <summary>
        /// 进行一些初始化配置
        /// </summary>
        void Start();

        /// <summary>
        /// 用于更新
        /// </summary>
        void Update();

        /// <summary>
        /// 配置Ip和端口
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        void Config(string ip, int port);

    }
}