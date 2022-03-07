using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace Sushi.MailTemplate.SendGrid
{
    internal class BlobPersister

    {
        public BlobPersister(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; protected set; }

        public async Task<BlobClient> GetBlockBlobReferenceAsync(string containerName, string blobName)
        {
            var storageAccount = new BlobContainerClient(ConnectionString, containerName);
            await storageAccount.CreateIfNotExistsAsync();

            // Create the blob client.
            var blobClient = storageAccount.GetBlobClient(blobName);
            return blobClient;
        }
    }
}
