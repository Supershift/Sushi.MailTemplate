using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Threading.Tasks;
using System.Web;
using SG = SendGrid;

namespace Sushi.MailTemplate.SendGrid
{
    /// <summary>
    /// SendGrid Mailer class to be used in conjunction with Wim.Module.MailTemplate
    /// </summary>
    public class Mailer
    {
        internal string EmailBlobContainer { get; private set; }
        internal string EmailStorageAccount { get; private set; }
        internal string EmailQueueName { get; private set; }
        internal string SendGridAPIKey { get; private set; }

        /// <summary>
        /// Mailer Constructor
        /// </summary>
        /// <param name="emailStorageAccount"></param>
        /// <param name="emailBlobContainer"></param>
        /// <param name="emailQueueName"></param>
        /// <param name="sendGridAPIKey"></param>
        public Mailer(string emailStorageAccount, string emailBlobContainer, string emailQueueName, string sendGridAPIKey)
        {
            EmailStorageAccount = emailStorageAccount;
            EmailBlobContainer = emailBlobContainer;
            EmailQueueName = emailQueueName;
            SendGridAPIKey = sendGridAPIKey;
        }

        /// <summary>
        /// Mailer Constructor
        /// </summary>
        /// <param name="emailStorageAccount"></param>
        /// <param name="emailBlobContainer"></param>
        /// <param name="emailQueueName"></param>
        public Mailer(string emailStorageAccount, string emailBlobContainer, string emailQueueName)
        {
            EmailStorageAccount = emailStorageAccount;
            EmailBlobContainer = emailBlobContainer;
            EmailQueueName = emailQueueName;
        }

        /// <summary>
        /// QueueMail inserts a queue message and saves the Data.MailTemplate mail in blob storage.
        /// </summary>
        /// <param name="mail">Data.MailTemplate with its properties filled</param>
        /// <param name="emailTo">E-mail address to send to</param>
        /// <param name="customerGUID">Optional customer GUID, which can be set to null</param>
        /// <returns></returns>
        public bool QueueMail(Data.MailTemplate mail, string emailTo, Guid? customerGUID)
        {
            // new guid to use as identifier for blob and queue
            var id = Guid.NewGuid();

            (CloudBlockBlob blob, CloudQueue queue, string email) = GetBlobQueueAndEmail(mail, emailTo, id, customerGUID);

            // upload to blob
            blob.UploadText(email);

            // queue message only has id as reference to blob
            var queueMessage = id.ToString();

            // add queue message
            queue.AddMessage(new CloudQueueMessage(queueMessage));

            return true;
        }

        /// <summary>
        /// QueueMailAsync inserts a queue message and saves the Data.MailTemplate mail in blob storage.
        /// </summary>
        /// <param name="mail">Data.MailTemplate with its properties filled</param>
        /// <param name="emailTo">E-mail address to send to</param>
        /// <param name="customerGUID">Optional customer GUID, which can be set to null</param>
        public async Task<bool> QueueMailAsync(Data.MailTemplate mail, string emailTo, Guid? customerGUID)
        {
            // new guid to use as identifier for blob and queue
            var id = Guid.NewGuid();

            (CloudBlockBlob blob, CloudQueue queue, string email) = GetBlobQueueAndEmail(mail, emailTo, id, customerGUID);

            // upload to blob
            await blob.UploadTextAsync(email);

            // queue message only has id as reference to blob
            var queueMessage = id.ToString();

            // add queue message
            await queue.AddMessageAsync(new CloudQueueMessage(queueMessage));

            return true;
        }


        private (CloudBlockBlob blob, CloudQueue queue, string email) GetBlobQueueAndEmail(Data.MailTemplate mail, string emailTo, Guid id, Guid? customerGUID)
        {
            string emailFromName = mail.DefaultSenderName;
            string emailFrom = mail.DefaultSenderEmail;
            string subject = mail.Subject;
            string body = mail.Body;
            string templateName = mail.Identifier;
            string bccs = mail.BCCReceivers;

            // get blob information
            var blobPersister = new BlobPersister(EmailStorageAccount);
            var container = blobPersister.GetContainer(EmailBlobContainer);
            var blob = container.GetBlockBlobReference(id.ToString());

            // create email message to save to blob
            var email = new Email
            {
                ID = id,
                FromName = emailFromName,
                From = emailFrom,
                To = emailTo,
                Subject = subject,
                Body = body,
                Bccs = bccs,
                TemplateName = templateName,
                CustomerGUID = customerGUID
            };
            var jsonEmail = Newtonsoft.Json.JsonConvert.SerializeObject(email);

            // create queue information
            var queuePersister = new QueuePersister(EmailStorageAccount);
            var queue = queuePersister.GetQueue(EmailQueueName);

            return (blob, queue, jsonEmail);
        }

        /// <summary>
        /// Sends the e-mail from the provided email. Does not clean up blob like SendMailFromBlobAsync, you should do this yourself.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> SendMailAsync(Email email)
        {
            if (string.IsNullOrEmpty(SendGridAPIKey))
            {
                throw new ApplicationException("SendGridAPIKey is empty");
            }

            string emailFrom = email.From;
            string emailFromName = HttpUtility.HtmlDecode(email.FromName);
            string emailTo = email.To;
            string subject = email.Subject; // already encoded in apply placeholders
            string body = email.Body; // already encoded in apply placeholders
            string bccs = email.Bccs;
            string templateName = email.TemplateName;
            Guid? customerGuid = email.CustomerGUID;

            var client = new SG.SendGridClient(SendGridAPIKey);

            var message = SG.Helpers.Mail.MailHelper.CreateSingleEmail(
                new SG.Helpers.Mail.EmailAddress(emailFrom, emailFromName),
                new SG.Helpers.Mail.EmailAddress(emailTo),
                subject,
                string.Empty,
                body
                );

            if (!string.IsNullOrWhiteSpace(bccs))
            {
                foreach (var bcc in bccs.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.AddBcc(new SG.Helpers.Mail.EmailAddress(bcc));
                }
            }

            // we are only interested in mail events from mails to customers. If we don't have a customer guid, 
            // then we also do not need to send the message id
            if (customerGuid.HasValue)
            {
                message.CustomArgs = new System.Collections.Generic.Dictionary<string, string>();
                message.CustomArgs.Add("CustomerGUID", customerGuid.ToString());
                message.CustomArgs.Add("MessageGUID", email.ID.ToString());
            }

            var result = await client.SendEmailAsync(message).ConfigureAwait(false);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                // reading response https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md#post-mailsend
                var returnBody = string.Empty;
                if (result.Body != null)
                {
                    returnBody = await result.Body.ReadAsStringAsync();
                }
                throw new ApplicationException($"Sendgrid returned status code {result.StatusCode}, for {returnBody} with headers {result.Headers?.ToString()}");
            }

        }

        /// <summary>
        /// Use SendMailAsync in an Azure Function that is triggered by a queue message. It expects an id to a blob.
        /// </summary>
        /// <param name="id">id of blob</param>
        public async Task<bool> SendMailFromBlobAsync(string id)
        {
            if (string.IsNullOrEmpty(SendGridAPIKey))
            {
                throw new ApplicationException("SendGridAPIKey is empty");
            }

            // get storage account
            var storageAccount = Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(EmailStorageAccount);

            // get blob information
            var blobClient = new Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient(storageAccount.BlobEndpoint, storageAccount.Credentials);
            var container = blobClient.GetContainerReference(EmailBlobContainer);
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(id.ToString());

            // check if the blob exists
            if (await blob.ExistsAsync())
            {
                var text = await blob.DownloadTextAsync();

                var email = Newtonsoft.Json.JsonConvert.DeserializeObject<Email>(text);

                string emailFrom = email.From;
                string emailFromName = HttpUtility.HtmlDecode(email.FromName);
                string emailTo = email.To;
                string subject = email.Subject;
                string body = email.Body;
                string bccs = email.Bccs;
                string templateName = email.TemplateName;
                Guid? customerGuid = email.CustomerGUID;

                var client = new SG.SendGridClient(SendGridAPIKey);

                var message = SG.Helpers.Mail.MailHelper.CreateSingleEmail(
                    new SG.Helpers.Mail.EmailAddress(emailFrom, emailFromName),
                    new SG.Helpers.Mail.EmailAddress(emailTo),
                    subject,
                    string.Empty,
                    body
                    );

                if (!string.IsNullOrWhiteSpace(bccs))
                {
                    foreach (var bcc in bccs.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        message.AddBcc(new SG.Helpers.Mail.EmailAddress(bcc));
                    }
                }

                // we are only interested in mail events from mails to customers. If we don't have a customer guid, 
                // then we also do not need to send the message id
                if (customerGuid.HasValue)
                {
                    message.CustomArgs = new System.Collections.Generic.Dictionary<string, string>();
                    message.CustomArgs.Add("CustomerGUID", customerGuid.ToString());
                    message.CustomArgs.Add("MessageGUID", id);
                }

                var result = await client.SendEmailAsync(message).ConfigureAwait(false);

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    // clean up the blob
                    await blob.DeleteAsync();
                    return true;
                }
                else
                {
                    // reading response https://github.com/sendgrid/sendgrid-csharp/blob/master/USAGE.md#post-mailsend
                    var returnBody = string.Empty;
                    if (result.Body != null)
                    {
                        try
                        {
                            returnBody = await result.Body.ReadAsStringAsync();
                        }
                        catch (Exception)
                        {
                            // leave the returnBody empty
                        }
                    }

                    throw new ApplicationException($"Sendgrid returned status code {result.StatusCode}, for {returnBody} with headers {result.Headers?.ToString()}");
                }
            }
            else
            {
                // blob doesn't exist
                throw new Microsoft.WindowsAzure.Storage.StorageException($"Blob {id} not found");
            }
        }
    }
}
