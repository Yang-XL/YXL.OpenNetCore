using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.FileManager
{
    public interface IFileManager
    {
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        void CreateDirectory(string path);
        /// <summary>
        /// 文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool DirectoryExists(string path);

        /// <summary>
        ///     获取路径下的所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isRecursive">是否递归</param>
        /// <returns></returns>
        IList<FileInfo> Get(string path, bool isRecursive = false);
        /// <summary>
        /// 同步写入文件，路径不存在则创建路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        void Write(string filePath, string content);
        /// <summary>
        /// 异步写入文件，路径不存在则创建路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task WriteAsync(string filePath, string content,CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        bool FileExists(string filePath);
    }
}