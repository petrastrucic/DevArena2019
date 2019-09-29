using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace DevArena2019.SendSms
{
    public static class SendEmailFunction
    {
        [FunctionName("SendEmail")]
        public static void Run(
            [ServiceBusTrigger("%ServiceBusTranslationQueue%", 
            Connection = "ServiceBusConnectionString")]Message mySbMsg)
        {
            var msg = new SendGridMessage();

            string str = System.Text.Encoding.Default.GetString(mySbMsg.Body);

            msg.SetFrom(new EmailAddress("dx@example.com", "SendGrid DX Team"));

            var recipients = new List<EmailAddress>
                {
                    new EmailAddress("pstrucic@ekobithr.onmicrosoft.com", "Petra Strucic")
                };
            msg.AddTos(recipients);

            msg.SetSubject("Testing the SendGrid C# Library");

            msg.AddContent(MimeType.Text, "Hello World plain text!");
            msg.AddContent(MimeType.Html, "<p>Hello World!</p>");
        }
    }
}
