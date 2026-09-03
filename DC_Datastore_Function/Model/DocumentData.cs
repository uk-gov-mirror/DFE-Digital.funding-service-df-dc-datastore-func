using System;
using Newtonsoft.Json;

namespace DC_Datastore_Function.Model
{
    public class DocumentData
    {
        [JsonProperty("fileId")]
        public Guid Id { get; set; }
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("sourceSystem")]
        public string SourceSystem { get; set; }
        [JsonProperty("fileStatus")]
        public string FileStatus { get; set; }
        [JsonProperty("fileType")]
        public string FileType { get; set; }
    }
}

