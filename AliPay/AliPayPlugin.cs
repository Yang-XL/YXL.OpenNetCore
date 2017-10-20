using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Plugins;

namespace AliPay
{
    public class AliPayPlugin : IPlugin
    {
        public PluginDescriptor PluginDescriptor { get; set; }

        public void Install()
        {
            throw new NotImplementedException();
        }

        public void Uninstall()
        {
            throw new NotImplementedException();
        }
    }
}
