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
                Content = "Welcome to DevArena! We hope you are having a great time and that you like this mail. " +
                "Yours sincerely - sweet little demo."
            };

            string content = JsonConvert.SerializeObject(sendEmail);
            Message message = new Message(Encoding.UTF8.GetBytes(content));

            QueueClient queueClient = new QueueClient(ServiceBusConnectionString, EmailQueue);
            await queueClient.SendAsync(message);

            await queueClient.CloseAsync();
        }
    }
}
