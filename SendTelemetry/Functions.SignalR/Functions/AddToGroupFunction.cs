using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Sauter.Cloud.Functions.SignalR.Models;
using Serilog;
using System.Threading.Tasks;

namespace Sauter.Cloud.Functions.SignalR.Functions
{
    public static class AddToGroupFunction
    {
        [FunctionName("addToGroup")]
        public static Task Subscribe(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")]Subscription message,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRGroupAction> signalRGroupActions,
            ExecutionContext context
            )
        {
            LoggingHelper.Configure(context.FunctionAppDirectory);

            Log.Debug("Subscribe: Function started.");

            Log.Information($"Subscribe: New user is being added to group. Message: [UserId: { message.UserId }, GroupName: { message.GroupName }, Action: Add].");
            return signalRGroupActions.AddAsync(
                new SignalRGroupAction
                {
                    UserId = message.UserId,
                    GroupName = message.GroupName,
                    Action = GroupAction.Add
                });
        }

    }
}
