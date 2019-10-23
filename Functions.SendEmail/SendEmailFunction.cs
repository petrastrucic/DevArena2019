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
            #region Create SendGrid message
            SendEmail sendEmail = JsonConvert.DeserializeObject<SendEmail>(System.Text.Encoding.Default.GetString(mySbMsg.Body));
            SendGridMessage msg = new SendGridMessage()
            {   
                From = new EmailAddress("pstrucic@ekobit.com", "DevArena 2019, powered by SendGrid"),
                Subject = "Hello conferencers from the SendGrid SDK!",
                PlainTextContent = sendEmail.Content,
                HtmlContent = $"<p>{ sendEmail.Content }</p>"
            };
            msg.AddTo(new EmailAddress(sendEmail.Mail, sendEmail.RecipientName));
            #endregion

            #region Send SendGrid message
            string apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            SendGridClient client = new SendGridClient(apiKey);
            await client.SendEmailAsync(msg);
            #endregion
        }
    }
}
