using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Sauter.Cloud.Functions.SignalR.Models;
using Serilog;
using System.Threading.Tasks;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class SendMessageToClientFunction
    {
        [FunctionName("sendToClient")]
        public static Task SendMessageToClient(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]UserMessage message,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRMessage> signalRMessages,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("SendMessageToClient: Function started.");

            Log.Information($"SendMessageToClient: New SignalR message is being sent to client. Message: [UserId: { message.UserId }, Target: { message.MessageName }].");
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    // the message will only be sent to this user ID
                    UserId = message.UserId,
                    Target = message.MessageName,
                    Arguments = new[] { message.Payload }
                });
        }

    }
}
