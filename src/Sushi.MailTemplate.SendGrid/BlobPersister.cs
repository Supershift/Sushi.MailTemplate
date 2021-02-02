using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
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

        protected static string ContainerCachePrefix = Guid.NewGuid().ToString();

        public CloudStorageAccount StorageAccount
        {
            get
            {
                return CloudStorageAccount.Parse(ConnectionString);
            }
        }

        public CloudBlobContainer GetContainer(string name)
        {

            // Create the blob client.
            CloudBlobClient blobClient = StorageAccount.CreateCloudBlobClient();
            // Retrieve a reference to a container. 
            var result = blobClient.GetContainerReference(name);

            //did we already call createIfNotExists for this container?
            string cacheKey = ContainerCachePrefix + StorageAccount.Credentials.AccountName + name;

            var cache = System.Runtime.Caching.MemoryCache.Default;
            if (!cache.Contains(cacheKey))
            {
                //create if not exists
                if (result.CreateIfNotExists())
                {
                    result.SetPermissions(new BlobContainerPermissions() { PublicAccess = BlobContainerPublicAccessType.Off });
                }
                cache.Add(cacheKey, true, DateTimeOffset.Now.AddMinutes(1));
            }

            return result;
        }

        public CloudBlockBlob GetBlobReference(string containerName, string blobName)
        {
            var container = this.GetContainer(containerName);
            var blockBlob = container.GetBlockBlobReference(blobName);
            return blockBlob;
        }

        public CloudBlockBlob GetBlobReference(string url)
        {
            var blockBlob = new CloudBlockBlob(new Uri(url), StorageAccount.Credentials);
            return blockBlob;
        }

        public async Task<bool> CheckIfExistsAsync(string container, string blobName)
        {
            var blockBlob = GetContainer(container).GetBlockBlobReference(blobName);

            var result = await blockBlob.ExistsAsync();

            return result;
        }

        public async Task<CloudBlockBlob> SaveAsync(string data, string container, string blobName, string contentType = null, Encoding encoding = null)
        {
            var blockBlob = GetContainer(container).GetBlockBlobReference(blobName);
            if (contentType != null)
                blockBlob.Properties.ContentType = contentType;
            await blockBlob.UploadTextAsync(data, encoding, null, null, null);

            return blockBlob;
        }

        public CloudBlockBlob Save(string data, string container, string blobName, string contentType = null)
        {
            var blockBlob = GetContainer(container).GetBlockBlobReference(blobName);
            if (contentType != null)
                blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadText(data);

            return blockBlob;
        }

        public CloudBlockBlob Save(byte[] data, string container, string blobName, string contentType = null)
        {
            var blockBlob = GetContainer(container).GetBlockBlobReference(blobName);
            if (contentType != null)
                blockBlob.Properties.ContentType = contentType;
            blockBlob.UploadFromByteArray(data, 0, data.Length);

            return blockBlob;
        }

        public async Task<int> DeleteAllBlobsByDateAsync(string containerName, DateTime olderThanDate)
        {
            var container = GetContainer(containerName);
            BlobContinuationToken token = null;
            int deletedCount = 0;
            do
            {
                var listResult = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.Metadata, null, token, new BlobRequestOptions(), new OperationContext());
                token = listResult.ContinuationToken;
                foreach (var blob in listResult.Results)
                {
                    if (blob is CloudBlob)
                    {
                        CloudBlob blobItem = (CloudBlob)blob;
                        if (blobItem.Properties.LastModified < olderThanDate)
                        {
                            await blobItem.DeleteAsync();
                            deletedCount++;
                        }
                    }
                }
            }
            while (token != null);
            return deletedCount;
        }
    }
}
