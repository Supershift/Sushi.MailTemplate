using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.SendGrid
{
    public class SendGridMailerOptions
    {
        public string EmailBlobContainer { get; set; }
        public string EmailStorageAccount { get; set; }
        public string EmailQueueName { get; set; }
        public string SendGridAPIKey { get; set; }
    }
}
