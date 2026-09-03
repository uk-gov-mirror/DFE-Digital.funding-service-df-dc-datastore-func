using DC_Datastore_Function.Model;
using DC_Datastore_Function.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DC_Datastore_Function
{
    public class DC_Datastore_Function
    {
        private ICdbConnectivity _objCdb;
        private readonly ILogger<DC_Datastore> log;

        public DC_Datastore_Function(ICdbConnectivity cdbConnectivity, ILogger<DC_Datastore> _log)
        {
            _objCdb = cdbConnectivity;
            log = _log;
        }

        [Function("dc-datastore")]
        public async Task Run([ServiceBusTrigger("%SBTopicQName%", Connection = "ServiceBusConnection")] string myQueueItem)
        {
            try
            {
                log.LogInformation($"Data received from SB: \r\n{myQueueItem}");
                var inputData = JsonConvert.DeserializeObject<DocumentData>(myQueueItem);
                var arr = inputData.FileName.Split('/');
                var document = new DocData
                {
                    fileName = arr[arr.Length - 1],
                    filePath = inputData.FileName,
                    fileStatus = inputData.FileStatus,
                    sourceSystem = inputData.SourceSystem,
                    scanStatus = "Okay",
                    fileId = inputData.Id.ToString()
                };

                log.LogInformation($"Inserting data to SQL db.");
                await PostDCData(document, log);
                log.LogInformation("Data inserted in to SQL db successfully");
            }
            catch (Exception ex)
            {
                log.LogError($"{ex.Message + ex.InnerException.Message + ex.InnerException.StackTrace}");
            }
        }
        private async Task<bool> PostDCData(DocData DCData, ILogger log)
        {
            try
            {

                log.LogInformation("Entered Post CD Data");
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", Environment.GetEnvironmentVariable("DFSQLAPIKEY"));
                string Url = $"{Environment.GetEnvironmentVariable("DFSQLAPIURL")}/api/PostDocumentCaptureData";
                var key = Environment.GetEnvironmentVariable("DFSQLAPIKEY");
                var json = JsonConvert.SerializeObject(DCData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                log.LogInformation($"Content: {content}");
                log.LogInformation($"JSON: {json}");
                var response = await client.PostAsync(Url, content);

                log.LogInformation($"response: {response}");
                response.EnsureSuccessStatusCode();


                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}