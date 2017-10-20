// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： ILogRepository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.Threading.Tasks;
using Core.Repository.MongoDB;
using Microsoft.Extensions.Logging;
using Mongo.Models;

namespace IService
{
    public interface ILogService : IMongoRepository<PermissionSystemLogs>
    {
        Task Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter,string fileLogPath);
        Task Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter);
    }
}