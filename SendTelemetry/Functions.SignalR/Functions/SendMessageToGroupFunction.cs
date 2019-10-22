using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Sauter.Cloud.Functions.SignalR.Models;
using Serilog;
using System.Threading.Tasks;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class SendMessageToGroupFunction
    {
        [FunctionName("sendToGroup")]
        public static Task SendMessageToGroup(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]GroupMessage message,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRMessage> signalRMessages,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("SendMessageToGroup: Function started.");

            Log.Information($"SendMessageToGroup: New SignalR message is being sent to group. Message: [GroupName: { message.GroupName }, Target: { message.MessageName }].");
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    // the message will be sent to the group with this name
                    GroupName = message.GroupName,
                    Target = message.MessageName,
                    Arguments = new[] { message.Payload }
                });
        }

    }
}
