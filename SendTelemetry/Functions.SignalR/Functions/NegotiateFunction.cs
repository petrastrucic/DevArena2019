using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Serilog;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class NegotiateFunction
    {
        [FunctionName("negotiate")]
        public static SignalRConnectionInfo Run(
            [HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
            [SignalRConnectionInfo(HubName = "default")]SignalRConnectionInfo connectionInfo,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("Negotiate: Function started.");

            Log.Information($"Negotiate: Responding to client's negotiation request and redirecting client to SignalR Service... Url: { connectionInfo.Url }.");
            return connectionInfo;
        }
    }
}
