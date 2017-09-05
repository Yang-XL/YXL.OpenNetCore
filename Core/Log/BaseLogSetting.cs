using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Core.Log
{
    public class BaseLogSetting : ILogSetting
    {
        public IDictionary<string, LogLevel> Switches { get; set; } = new Dictionary<string, LogLevel>();
        public IChangeToken ChangeToken { get; set; }

        public bool IncludeScopes { get; set; }

        public ILogSetting Reload()
        {
            return this;
        }

        public bool TryGetSwitch(string name, out LogLevel level)
        {
            return Switches.TryGetValue(name, out level);
        }
    }
}