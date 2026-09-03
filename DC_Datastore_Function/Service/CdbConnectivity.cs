using System;
using System.Threading.Tasks;
using DC_Datastore_Function.Model;
using Microsoft.Azure.Cosmos;

namespace DC_Datastore_Function.Service
{
    internal class CdbConnectivity:ICdbConnectivity
    {
        private CosmosClient _client;
        private PartitionKey _partitionKey;
        private Container _containerObj;

        private readonly string connectionString = Environment.GetEnvironmentVariable("CosmosDBConnection");
        private readonly string dbName = Environment.GetEnvironmentVariable("CosmosdbName");
        private readonly string containerName = Environment.GetEnvironmentVariable("CosmosdbContainerName");

        public CdbConnectivity()
        {
            _client = new CosmosClient(connectionString);
            _containerObj = _client.GetContainer(dbName, containerName);
        }

        public async Task<DC_Datastore> GetData(string id)
        {
            DC_Datastore result = null;
            try
            {
                if (_client != null && _containerObj != null)
                {
                    _partitionKey = new PartitionKey(id);
                    result = await _containerObj.ReadItemAsync<DC_Datastore>(id, _partitionKey);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<DC_Datastore> CreateData(string id, DC_Datastore item)
        {
            DC_Datastore result = null;
            try
            {
                if (_client != null && _containerObj != null)
                {
                    _partitionKey = new PartitionKey(id);
                    result = await _containerObj.CreateItemAsync<DC_Datastore>(item, _partitionKey);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }

        public async Task<DC_Datastore> UpdateData(string id, DC_Datastore item)
        {
            DC_Datastore result = null;
            try
            {
                if (_client != null && _containerObj != null)
                {
                    result = await _containerObj.ReplaceItemAsync<DC_Datastore>(item, id);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
        public async Task<DC_Datastore> DeleteData(string id)
        {
            DC_Datastore result = null;
            try
            {
                if (_client != null && _containerObj != null)
                {
                    _partitionKey = new PartitionKey(id);
                    result = await _containerObj.DeleteItemAsync<DC_Datastore>(id, _partitionKey);
                }
            }
            catch
            {
                throw;
            }
            return result;
        }


    }
}
