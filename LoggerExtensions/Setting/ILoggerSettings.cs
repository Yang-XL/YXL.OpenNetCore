using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace LoggerExtensions.Setting
{
    /// <summary>
    /// 日志设置
    /// </summary>
    public interface ILoggerSettings
    {
        /// <summary>
        /// 上下文关联
        /// </summary>
        bool IncludeScopes { get; }
        /// <summary>
        /// 文本日志路径
        /// </summary>
        string FileLogPath { get; }
        /// <summary>
        /// 配置文件变更监控
        /// </summary>
        IChangeToken ChangeToken { get; }
        /// <summary>
        /// 获取日志等级
        /// </summary>
        /// <param name="name">CategoryName</param>
        /// <param name="level">日志登记</param>
        /// <returns>是否允许记录</returns>
        bool TryGetSwitch(string name, out LogLevel level);
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <returns></returns>
        ILoggerSettings Reload();
    }
}