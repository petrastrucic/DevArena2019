using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Sauter.Cloud.Functions.SignalR.Models;
using Serilog;
using System.Threading.Tasks;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class RemoveFromGroupFunction
    {
        [FunctionName("removeFromGroup")]
        public static Task Unsubscribe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]Subscription message,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRGroupAction> signalRGroupActions,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("SendMessageToAll: Function started.");

            Log.Information($"SendMessageToAll: User is being removed from group. Message: [UserId: { message.UserId }, GroupName: { message.GroupName }, Action: Remove].");
            return signalRGroupActions.AddAsync(
                new SignalRGroupAction
                {
                    UserId = message.UserId,
                    GroupName = message.GroupName,
                    Action = GroupAction.Remove
                });
        }

    }
}
