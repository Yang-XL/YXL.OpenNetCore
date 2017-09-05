

namespace Core.Infrastructure
{
    /// <summary>
    /// 计划任务
    /// </summary>
    public interface IStartupTask
    {
        /// <summary>
        /// 执行
        /// </summary>
        void Execute();
        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; }
    }
}
