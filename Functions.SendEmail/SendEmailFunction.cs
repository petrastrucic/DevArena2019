using DevArena2019.SendEmailFunction.Models;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace DevArena2019.SendEmailFunction
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmailFunction")]
        public static async Task Run(
            [ServiceBusTrigger("%ServiceBusEmailQueue%", 
            Connection = "ServiceBusConnectionString")]Message mySbMsg)
        {
            SendEmail sendEmail = JsonConvert.DeserializeObject<SendEmail>(System.Text.Encoding.Default.GetString(mySbMsg.Body));

            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("pstrucic@ekobit.com", "DevArena 2019, powered by SendGrid"),
                Subject = "Hello conferencers from the SendGrid SDK!",
                PlainTextContent = sendEmail.Content,
                HtmlContent = $"<p>{ sendEmail.Content }</p>"
            };
            msg.AddTo(new EmailAddress(sendEmail.Mail, sendEmail.RecipientName));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
