using Azure.Storage.Queues;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.SendGrid
{
    internal class QueuePersister
    {
        public QueuePersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; protected set; }


        public async Task<QueueClient> GetQueueAsync(string name)
        {
            var queue = new QueueClient(ConnectionString, name);
            await queue.CreateIfNotExistsAsync();

            return queue;
        }
    }
}