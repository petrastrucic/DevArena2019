using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using System;

namespace Sauter.Cloud.Functions.Telemetry.Functions
{
    public static class ProcessTelemetry
    {
        // Reference for how to enable public Azure calls to your local function: 
        // https://docs.microsoft.com/en-us/azure/azure-functions/functions-debug-event-grid-trigger-local#allow-azure-to-call-your-local-function
        [FunctionName("ProcessTelemetry")]
        public static void Run([EventGridTrigger]EventGridEvent eventGridEvent)
        {
            Console.WriteLine($"ProcessTelemetry: Function started. { eventGridEvent.Data }");

            // TO DO: process and store telemetry into persistent store.
        }
    }
}
