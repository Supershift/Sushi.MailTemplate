using Azure.Storage.Queues;

namespace Sushi.MailTemplate.SendGrid
{
    internal class QueuePersister
    {
        public QueuePersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; protected set; }


        public QueueClient GetQueue(string name)
        {
            return new QueueClient(ConnectionString, name);
        }
    }
}