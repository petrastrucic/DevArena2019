using Microsoft.Extensions.Configuration;
using Serilog;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public class LoggingHelper
    {
        public static void Configure(string functionDirectory)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(functionDirectory)
                .AddJsonFile("serilog.settings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
