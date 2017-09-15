// =================================================================== 
// 项目说明
//====================================================================
// YXL @ CopyRight 2006-2010。
// 文件： LogRespository.cs
// 项目名称： 
// 创建时间：2017-05-18
// 负责人：YXL
// ===================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using Core.FileManager;
using Core.Repository.Ef;
using IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using PermissionSystem;
using PermissionSystem.Models;

namespace Service.PermissionSystem
{
    public class LogService : EfRepository<Log>, ILogService
    {
        private readonly IFileManager _fileManager;
        private readonly IHttpContextAccessor _accessor;

        public LogService(PermissionSystemContext context, IFileManager fileManager, IHttpContextAccessor accessor) : base(context)
        {
            _fileManager = fileManager;
            _accessor = accessor;
        }

        public async Task Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter,
            string fileLogPath)
        {
            if (state is Log)
            {
                var entity = state as Log;
                if (string.IsNullOrEmpty(entity.ShortMessage))
                    entity.ShortMessage = categoryName;
                if (string.IsNullOrEmpty(entity.KeyWord))
                    entity.KeyWord = categoryName;
                entity.LogLeve = Convert.ToInt32(logLevel);

                if (_accessor.HttpContext != null)
                {
                    entity.PageUrl = _accessor.HttpContext.Request.GetDisplayUrl();
                    entity.ReferrerUrl = _accessor.HttpContext.Request.Headers["HeaderReferer"].ToString();

                    entity.ReferrerUrl = _accessor.HttpContext.Request.Headers["Referer"].ToString();

                    entity.IpAddress = _accessor.HttpContext.Connection.RemoteIpAddress?.ToString();
                    entity.ServerIpAddress = _accessor.HttpContext.Connection.LocalIpAddress?.ToString();
                }

                await InsertAsync(state as Log);
                SaveChanges();
                return;
            }

             if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
                return;

            message = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}   {logLevel}   {categoryName}   {message}";

            if (exception != null)
                message += Environment.NewLine + exception;
            if (string.IsNullOrEmpty(message))
                return;

            _fileManager.AppendText(Path.Combine(AppContext.BaseDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log"), message);
        }


        public Task Log<TState>(string categoryName, LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, string> formatter)
        {
            return Log(categoryName, logLevel, eventId, state, exception, formatter,
                Path.Combine(AppContext.BaseDirectory, "Logs"));
        }

        public Task Log(string categoryName, LogLevel logLevel, EventId eventId, Log state)
        {
            throw new NotImplementedException();
        }
    }
}