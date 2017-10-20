namespace Core.Plugins
{
    /// <summary>
    ///     Represents a mode to load plugins
    /// </summary>
    public enum PluginsState
    {
        /// <summary>
        ///     所有
        /// </summary>
        All = 0,

        /// <summary>
        ///     已加载的
        /// </summary>
        InstalledOnly = 10,

        /// <summary>
        ///     未加载的
        /// </summary>
        NotInstalledOnly = 20
    }
}