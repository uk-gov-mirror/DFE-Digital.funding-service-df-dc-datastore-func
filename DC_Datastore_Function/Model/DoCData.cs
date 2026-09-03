using System;
using Newtonsoft.Json;

namespace DC_Datastore_Function.Model
{
    public class DocData
    {
        public string? DCDID { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string fileStatus { get; set; }
        public string sourceSystem { get; set; }
        public string scanStatus { get; set; }

        public string fileId { get; set; }
    }
}

