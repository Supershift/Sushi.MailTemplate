using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System;
using System.Collections.Generic;
using System.Text;
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

        protected static string QueueCachePrefix = Guid.NewGuid().ToString();
        public CloudQueue GetQueue(string name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            // Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference(name);

            //did we already call createIfNotExists for this container?
            string cacheKey = QueueCachePrefix + storageAccount.Credentials.AccountName + name;

            var cache = System.Runtime.Caching.MemoryCache.Default;
            if (!cache.Contains(cacheKey))
            {
                queue.CreateIfNotExists();

                cache.Add(cacheKey, true, DateTimeOffset.Now.AddMinutes(1));
            }

            return queue;
        }

        public void AddMessage(string data, string queueName)
        {
            var queue = GetQueue(queueName);
            queue.AddMessage(new CloudQueueMessage(data));
        }

        public async Task AddMessageAsync(string data, string queueName)
        {
            var queue = GetQueue(queueName);
            await queue.AddMessageAsync(new CloudQueueMessage(data));
        }

        public async Task AddMessageAsync(byte[] data, string queueName)
        {
            var queue = GetQueue(queueName);
            await queue.AddMessageAsync(new CloudQueueMessage(data));
        }

        public async Task DeleteAsync(CloudQueueMessage message, string queueName)
        {
            var queue = GetQueue(queueName);
            await queue.DeleteMessageAsync(message);
        }
    }
}