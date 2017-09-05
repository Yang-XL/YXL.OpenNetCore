using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.FileManager
{
    public class FileManager : IFileManager
    {
        public void CreateDirectory(string path)
        {
            if (Directory.Exists(path))return;
            Directory.CreateDirectory(path);
        }

        public void Create(string path, string content)
        {
            throw new NotImplementedException();
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

        private void GetFileRecursive(DirectoryInfo dir, ref List<FileInfo> list)
        {
            foreach (var fileInfo in dir.GetFiles())
            {
                list.Add(fileInfo);
            }
            foreach (var directoryInfo in dir.GetDirectories())
            {
                GetFileRecursive(directoryInfo, ref list);
            }
        }

        public void AppendText(string path, string content)
        {
            try
            {
                using (var fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine(content);
                        sw.Dispose();
                    }
                    fs.Dispose();
                }

            
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        public void Dispose()
        {
           
        }
    }

}
