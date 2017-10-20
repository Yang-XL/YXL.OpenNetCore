using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace Core.Plugins
{
    public class PluginViewFileProvider : PhysicalFileProvider
    {
        public IFileInfo GetFileInfo(string subpath)
        {

            throw new NotImplementedException();
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

        public PluginViewFileProvider(string root) : base(root)
        {
        }
        
    }
}
