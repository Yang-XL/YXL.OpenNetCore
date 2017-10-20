using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.FileManager
{
    public class FileManager : IFileManager
    {
        public void CreateDirectory(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IList<FileInfo> Get(string path, bool isRecursive = false)
        {
            var dir = new DirectoryInfo(path);
            if (!dir.Exists) return null;
            if (!isRecursive) dir.GetFiles();
            var result = new List<FileInfo>();
            GetFileRecursive(dir, ref result);
            return result;
        }

        public void Write(string filePath, string content)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                CreateDirectory(fileInfo.Directory.FullName);
                var logContentBytes = Encoding.UTF8.GetBytes(content);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    fs.Seek(0, SeekOrigin.End);
                    fs.Write(logContentBytes, 0, logContentBytes.Length);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task WriteAsync(string filePath, string content,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                CreateDirectory(fileInfo.Directory.FullName);
                var logContentBytes = Encoding.UTF8.GetBytes(content);
                using (var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    fs.Seek(0, SeekOrigin.End);
                    await fs.WriteAsync(logContentBytes, 0, logContentBytes.Length, cancellationToken);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
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