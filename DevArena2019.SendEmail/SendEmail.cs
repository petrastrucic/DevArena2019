using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;

namespace DevArena2019.SendSms
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmail")]
        public static void Run(
            [ServiceBusTrigger("%ServiceBusTranslationQueue%", 
            Connection = "ServiceBusConnectionString")]Message mySbMsg)
        {

        }
    }
}
