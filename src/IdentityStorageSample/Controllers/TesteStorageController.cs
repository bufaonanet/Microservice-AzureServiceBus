using Azure.Identity;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityStorageSample.Controllers
{
    [ApiController]
    [Route("/")]
    public class TesteStorageController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var containerEndPoint = string.Format("https://{0}.blob.core.windows.net/{1}",
                                                   "minhacontadestorage",
                                                   "meu-container");

            BlobContainerClient containerClient =
                new BlobContainerClient(new Uri(containerEndPoint), new DefaultAzureCredential());

            BlobClient blobClient = containerClient.GetBlobClient("gato.jpg");

            var bytes = blobClient.DownloadContent().Value.Content.ToArray();

            return File(bytes, "image/jpg");
        }
    }
}

/