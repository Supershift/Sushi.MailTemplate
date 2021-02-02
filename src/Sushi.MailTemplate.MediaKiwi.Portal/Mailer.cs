using System;
using System.Configuration;
using System.Threading.Tasks;
using Sushi.MailTemplate.SendGrid;
using Sushi.Mediakiwi.Data;

namespace Sushi.MailTemplate.MediaKiwi.Portal
{
    public class Mailer
    {
        public Mailer(string emailStorageAccount, string emailBlobContainer, string emailQueueName, string sendGridAPIKey)
        {
            EmailBlobContainer = emailBlobContainer;
            EmailQueueName = emailQueueName;
            EmailStorageAccount = emailStorageAccount;
            SendGridAPIKey = sendGridAPIKey;

            // hook up the Mailer to the SendPreviewEmailEventHandler.SendPreviewEmail
            Sushi.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmail += OnSendPreviewEmail;
            Sushi.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmailAsync += OnSendPreviewEmailAsync;
        }

        public string EmailStorageAccount { get; set; }
        public string EmailBlobContainer { get; set; }
        public string EmailQueueName { get; set; }
        public string SendGridAPIKey { get; set; }

        /// <summary>
        /// Send the referenced template to the Azure Queue, so it triggers the Azure Function SendMailUsingSendGrid that will send it.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        public bool SendMailToQueue(Sushi.MailTemplate.Data.MailTemplate mail, string emailTo, Guid? customerGuid)
        {
            var mailer = new Sushi.MailTemplate.SendGrid.Mailer(EmailStorageAccount, EmailBlobContainer, EmailQueueName, SendGridAPIKey);
            var result = mailer.QueueMail(mail, emailTo, customerGuid);

            return result;
        }

        private void OnSendPreviewEmail(object sender, Sushi.MailTemplate.Logic.SendPreviewEmailEventArgs e)
        {
            try
            {
                var emailFrom = e.EmailFrom;
                var emailTo = e.EmailTo;
                var subject = e.Subject;
                var body = e.Body;
                var templateName = e.TemplateName;

                var mail = new Sushi.MailTemplate.Data.MailTemplate
                {
                    DefaultSenderEmail = emailFrom,
                    DefaultSenderName = $"{e.EmailFromName} - Preview",
                    Subject = subject,
                    Body = body,
                    Identifier = templateName
                };

                var mailer = new Sushi.MailTemplate.SendGrid.Mailer(EmailStorageAccount, EmailBlobContainer, EmailQueueName);
                e.IsSuccess = mailer.QueueMail(mail, emailTo, customerGUID: Guid.NewGuid());
            }
            catch (Exception ex)
            {
                Notification.InsertOne("MailTemplate.SendMail", ex.ToString());
                e.IsSuccess = false;
            }
        }
        private async Task OnSendPreviewEmailAsync(object sender, Sushi.MailTemplate.Logic.SendPreviewEmailEventArgs e)
        {
            try
            {
                var emailFrom = e.EmailFrom;
                var emailTo = e.EmailTo;
                var subject = e.Subject;
                var body = e.Body;
                var templateName = e.TemplateName;

                var mailTemplate = MailTemplate.Fetch(templateName);

                var mailer = new Sushi.MailTemplate.SendGrid.Mailer(EmailStorageAccount, EmailBlobContainer, EmailQueueName, SendGridAPIKey);

                var emailToSend = new Email
                {
                    From = emailFrom,
                    FromName = mailTemplate.DefaultSenderName,
                    TemplateName = templateName,
                    Body = body,
                    Subject = subject,
                    To = emailTo
                };

                e.IsSuccess = await mailer.SendMailAsync(emailToSend);
            }
            catch (Exception ex)
            {
                Notification.InsertOne("MailTemplate.SendMail", ex.ToString());
                e.IsSuccess = false;
            }
        }

    }
}