using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileManager
{
    public interface IFileManager
    {

        void CreateDirectory(string path);

        void Create(string path, string content);

        /// <summary>
        /// 获取路径下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="Recursive">是否递归</param>
        /// <returns></returns>
        IList<FileInfo> Get(string path, bool isRecursive = false);

        void AppendText(string path, string content);

        void Dispose();
    }
}
