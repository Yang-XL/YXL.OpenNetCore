using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Plugins.AssemblyLoaders
{
    public class NetCoreAssemblyLoader : AssemblyLoaderBase
    {
        private readonly ILogger _logger;

        public AssemblyLoadContext AssemblyContext = AssemblyLoadContext.Default;

        public NetCoreAssemblyLoader(ILoggerFactory loggerFactoy)
        {
            _logger = loggerFactoy.CreateLogger<NetCoreAssemblyLoader>();
        }

        public override Assembly Load(AssemblyName assemblyName)
        {
            try
            {
                
                return AssemblyContext.LoadFromAssemblyName(assemblyName);
            }
            catch (Exception e)
            {
                _logger.LogError("名称加载程序集错误，原因：{1}", e);
                return null;
            }
        }

        public override Assembly Load(byte[] rawAssembly)
        {
            try
            {

                return AssemblyContext.LoadFromStream(new MemoryStream(rawAssembly));
            }
            catch (Exception e)
            {

                _logger.LogError("根据字节数组加载错误，原因：{1}", e);
                return null;
            }
        }

        public override Assembly LoadFile(string path)
        {
            try
            {
                if (path.ToUpper().EndsWith(".DLL"))
                    return AssemblyContext.LoadFromAssemblyPath(path);
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError("根据路径地址加载程序集错误，地址：{0}，原因：{1}", path, e);
                return null;
            }
        }
    }
}