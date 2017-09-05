using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Core.Log
{
    public interface ILogSetting
    {
        bool IncludeScopes { get; }

        IChangeToken ChangeToken { get; }

        bool TryGetSwitch(string name, out LogLevel level);

        ILogSetting Reload();
    }
}