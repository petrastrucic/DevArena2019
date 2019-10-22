using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Sauter.Cloud.Functions.SignalR.Models;
using Serilog;
using System.Threading.Tasks;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class SendMessageToAllFunction
    {
        [FunctionName("sendToAll")]
        public static Task SendMessageToAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]Message message,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRMessage> signalRMessages,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("SendMessageToAll: Function started.");

            Log.Information($"SendMessageToAll: New SignalR message is being sent to all. Message: [Target: { message.MessageName }].");
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = message.MessageName,
                    Arguments = new[] { message.Payload }
                });
        }
    }
}
