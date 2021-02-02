using System;
using System.Configuration;
using System.Threading.Tasks;
using Wim.Module.MailTemplate.SendGrid;

namespace Wim.Module.MailTemplate.Portal
{
    public class Mailer
    {
        public Mailer()
        {
            // hook up the Mailer to the SendPreviewEmailEventHandler.SendPreviewEmail
            Wim.Module.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmail += OnSendPreviewEmail;
            Wim.Module.MailTemplate.Logic.SendPreviewEmailEventHandler.SendPreviewEmailAsync += OnSendPreviewEmailAsync;
        }

        /// <summary>
        /// Send the referenced template to the Azure Queue, so it triggers the Azure Function SendMailUsingSendGrid that will send it.
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="emailTo"></param>
        /// <returns></returns>
        public bool SendMailToQueue(Wim.Module.MailTemplate.Data.MailTemplate mail, string emailTo, Guid? customerGuid)
        {
            var emailStorageAccount = ConfigurationManager.AppSettings["EmailStorageAccount"];
            var emailBlobContainer = ConfigurationManager.AppSettings["EmailBlobContainer"];
            var emailQueueName = ConfigurationManager.AppSettings["EmailQueueName"];
            var sendGridAPIKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];

            var mailer = new Wim.Module.MailTemplate.SendGrid.Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);
            var result = mailer.QueueMail(mail, emailTo, customerGuid);

            return result;
        }

        private void OnSendPreviewEmail(object sender, Wim.Module.MailTemplate.Logic.SendPreviewEmailEventArgs e)
        {
            try
            {
                var emailFrom = e.EmailFrom;
                var emailTo = e.EmailTo;
                var subject = e.Subject;
                var body = e.Body;
                var templateName = e.TemplateName;

                var emailStorageAccount = ConfigurationManager.AppSettings["EmailStorageAccount"];
                var emailBlobContainer = ConfigurationManager.AppSettings["EmailBlobContainer"];
                var emailQueueName = ConfigurationManager.AppSettings["EmailQueueName"];

                var mail = new Wim.Module.MailTemplate.Data.MailTemplate
                {
                    DefaultSenderEmail = emailFrom,
                    DefaultSenderName = $"{e.EmailFromName} - Preview",
                    Subject = subject,
                    Body = body,
                    Identifier = templateName
                };

                var mailer = new Wim.Module.MailTemplate.SendGrid.Mailer(emailStorageAccount, emailBlobContainer, emailQueueName);
                e.IsSuccess = mailer.QueueMail(mail, emailTo, customerGUID: Guid.NewGuid());
            }
            catch (Exception ex)
            {
                Wim.Data.Notification.InsertOne("MailTemplate.SendMail", ex);
                e.IsSuccess = false;
            }
        }
        private async Task OnSendPreviewEmailAsync(object sender, Wim.Module.MailTemplate.Logic.SendPreviewEmailEventArgs e)
        {
            try
            {
                var emailFrom = e.EmailFrom;
                var emailTo = e.EmailTo;
                var subject = e.Subject;
                var body = e.Body;
                var templateName = e.TemplateName;

                var emailStorageAccount = ConfigurationManager.AppSettings["EmailStorageAccount"];
                var emailBlobContainer = ConfigurationManager.AppSettings["EmailBlobContainer"];
                var emailQueueName = ConfigurationManager.AppSettings["EmailQueueName"]; 
                var sendGridAPIKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];


                var mailTemplate = MailTemplate.Fetch(templateName);

                var mailer = new Wim.Module.MailTemplate.SendGrid.Mailer(emailStorageAccount, emailBlobContainer, emailQueueName, sendGridAPIKey);

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
                Wim.Data.Notification.InsertOne("MailTemplate.SendMail", ex);
                e.IsSuccess = false;
            }
        }

    }
}