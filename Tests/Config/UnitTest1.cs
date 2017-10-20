using System;
using System.Collections.Generic;
using System.IO;
using Core.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            var build = new ConfigurationBuilder();
            var files = Get(Path.Combine(AppContext.BaseDirectory), true);
            foreach (var file in files) 
            {
                if (file.Extension == ".Json")
                {
                    build.AddJsonFile(file.FullName, true, true);
                }
            }
            IConfiguration _config = build.Build();

            var appconfig = new ServiceCollection()
                .AddOptions()
                .Configure<List<PluginDescriptor>>(_config)
                .BuildServiceProvider()
                .GetService<IOptions<List<PluginDescriptor>>>()
                .Value;
            Assert.AreEqual(appconfig.Count,2);
        }



        public IList<FileInfo> Get(string path, bool isRecursive = false)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists) return null;
            if (!isRecursive) dir.GetFiles();
            List<FileInfo> result = null;
            GetFileRecursive(dir, ref result);
            return result;
        }

        /// <summary>
        ///     文件夹下所有的文件
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="list"></param>
        private void GetFileRecursive(DirectoryInfo dir, ref List<FileInfo> list)
        {
            foreach (var fileInfo in dir.GetFiles())
                list.Add(fileInfo);
            foreach (var directoryInfo in dir.GetDirectories())
                GetFileRecursive(directoryInfo, ref list);
        }
    }
}
