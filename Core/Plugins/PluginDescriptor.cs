using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Core.Plugins
{
    public  class PluginDescriptor : IComparable<PluginDescriptor>
    {
        /// <summary>
        ///     Plugin type
        /// </summary>
        public string PluginFileName { get; set; }

        /// <summary>
        ///     Plugin type
        /// </summary>
        public Type PluginType { get; set; }

        /// <summary>
        ///     Gets or sets the plugin group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        ///     Gets or sets the friendly name
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        ///     Gets or sets the system name
        /// </summary>
        public string SystemName { get; }

        /// <summary>
        ///     Gets or sets the version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     Gets or sets the supported versions of nopCommerce
        /// </summary>
        public IList<string> SupportedVersions { get; set; }

        /// <summary>
        ///     Gets or sets the author
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        ///     Gets or sets the list of store identifiers in which this plugin is available. If empty, then this plugin is
        ///     available in all stores
        /// </summary>
        public IList<int> LimitedToStores { get; set; }

        /// <summary>
        ///     Gets or sets the value indicating whether plugin is installed
        /// </summary>
        public bool Installed { get; set; }

        public IConfiguration ConfigSetting { get; internal set; }

        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);
            return string.Compare(FriendlyName, other.FriendlyName, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null &&
                   SystemName != null &&
                   SystemName.Equals(other.SystemName);
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }
    }
}