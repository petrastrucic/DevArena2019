using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using System;
using System.Globalization;
using System.Text;
using IoTHubTrigger = Microsoft.Azure.WebJobs.EventHubTriggerAttribute;

namespace Sauter.Cloud.Functions.Telemetry.Functions
{
    public static class MonitorTelemetry
    {
        [FunctionName("MonitorTelemetry")]
        public static void Run(
            [IoTHubTrigger("messages/events", Connection = "IoTHubEndpoint")]EventData eventGridEvent,
            [SignalR(HubName = "default")]IAsyncCollector<SignalRMessage> signalRMessages)  // TO DO: HubName ??
        {
            Console.WriteLine($"{ DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) }" +
                $" > MonitorTelemetry: Function started. { eventGridEvent.Body.Array }");

            // The Target is the name of the method to be invoked on the client when receiving the message.
            // The Arguments property is an array of zero or more objects to be passed to the client method. 
            // In our case we just forward the message we received from the IoT device.
            var message = new SignalRMessage
            {
                Target = "iotMessage",
                Arguments = new[] { Encoding.UTF8.GetString(eventGridEvent.Body.Array, 0, eventGridEvent.Body.Array.Length) }
            };

            signalRMessages.AddAsync(message);

            Console.WriteLine($"{ DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture) }" +
                $" > MonitorTelemetry: Function finished. { eventGridEvent.Body.Array }");
        }
    }
}