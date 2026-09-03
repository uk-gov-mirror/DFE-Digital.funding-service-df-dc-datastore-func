using DC_Datastore_Function.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC_Datastore_Function.Service
{
    public  interface ICdbConnectivity
    {       
        Task<DC_Datastore> GetData(string id);

        Task<DC_Datastore> CreateData(string id, DC_Datastore json);

        Task<DC_Datastore> UpdateData(string id, DC_Datastore json);

        Task<DC_Datastore> DeleteData(string id);

    }
}
