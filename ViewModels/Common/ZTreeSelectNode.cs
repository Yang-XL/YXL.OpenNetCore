using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ViewModels.Common
{
    public class ZTreeSelectNode
    {
        /// <summary>
        /// Node Value
        /// </summary>

        [JsonProperty(PropertyName = "id")]
        public string NodeValue { get; set; }
        /// <summary>
        /// Node Name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string NodeText { get; set; }
        /// <summary>
        /// ParentNode
        /// </summary>
        [JsonProperty(PropertyName = "pid")]
        public string ParentNodeValue { get; set; }
        /// <summary>
        ///  Node IsOpen
        /// </summary>
        [JsonProperty(PropertyName = "open")]
        public  bool IsOpen { get; set; }
    }

}