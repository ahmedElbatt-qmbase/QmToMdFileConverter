using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace QmToMdFileConverter.Classes
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class ConvertedFileInfo
    {
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; }
    }
}
