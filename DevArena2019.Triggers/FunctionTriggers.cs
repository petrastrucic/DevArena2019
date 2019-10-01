using DevArena2019.SendEmailFunction.Models;
using DevArena2019.Triggers;
using Microsoft.Azure.ServiceBus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace DevArena2019.FunctionTrigger
{
    [TestClass]
    public class IntegrationTests : BaseTriggers
    {
        [TestMethod]
        public async Task Trigger_SendEmail()
        {
            SendEmail sendEmail = new SendEmail()
            {
                Mail = "pstrucic@ekobithr.onmicrosoft.com",
                RecipientName = "Petra Strucic",
                Content = "blab"
            };

            var content = JsonConvert.SerializeObject(sendEmail);

            var queueClient = new QueueClient(ServiceBusConnectionString, EmailQueue);

            var message = new Message(Encoding.UTF8.GetBytes(content));
            await queueClient.SendAsync(message);

            await queueClient.CloseAsync();
        }
    }
}
