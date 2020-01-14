using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NuevoFoundationApiVersion2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace NuevoFoundationApiVersion2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImpactController : ControllerBase
    {
        private const string statsFileName = "impactstats.json";
        private BlobContainerClient blobContainerClient { get; set; }

        [HttpGet]
        public async Task<List<ImpactStats>> GetAsync()
        {
            // Get a reference to a blob
            if (blobContainerClient == null)
            {
                // Create a BlobServiceClient object which will be used to create a container client
                BlobServiceClient blobServiceClient = new BlobServiceClient(AzureStorageConfig.AccountKey);
                blobContainerClient = blobServiceClient.GetBlobContainerClient(AzureStorageConfig.ContainerName);
            }
            var blobClient = blobContainerClient.GetBlobClient(statsFileName);
            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

            using (StreamReader reader = new StreamReader(blobDownloadInfo.Content))
            {
                string text = reader.ReadToEnd();
                List<ImpactStats> stats = JsonSerializer.Deserialize<List<ImpactStats>>(text);

                return stats;
            }
        }
    }
}