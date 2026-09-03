using System;
using DC_Datastore_Function.Service;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly:FunctionsStartup(typeof(DC_Datastore_Function.Startup))]
namespace DC_Datastore_Function
{
    public class Startup : FunctionsStartup
    {
        public Startup()
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ICdbConnectivity, CdbConnectivity>();
        }
    }
}

