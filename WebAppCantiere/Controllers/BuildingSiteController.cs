using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DBRepository;
using DBRepository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using WebAppCantiere.Models.BuildingSiteModels;

namespace WebAppCantiere.Controllers
{
    public class BuildingSiteController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IBuildingSiteRepository _buildingSiteRepository;

        public BuildingSiteController(IConfiguration configuration, IBuildingSiteRepository buildingSiteRepository)
        {
            _configuration = configuration;
            _buildingSiteRepository = buildingSiteRepository;
        }

        public IActionResult Index()
        {
            var buildingSites = _buildingSiteRepository.Get();

            var list = buildingSites.Select(t => new BuildingSiteViewModel(t));

            return View(list);
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var model = new BuildingSiteModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(BuildingSiteModel model)
        {
            if (ModelState.IsValid)
            {
                var storageAccount = CloudStorageAccount.Parse(_configuration["BlobStorageConnectionString"]);

                var blobClient = storageAccount.CreateCloudBlobClient();

                var container =
                    blobClient.GetContainerReference(model.BuildingSiteLocation.ToLower().Replace(" ", "-"));
                await container.CreateIfNotExistsAsync();

                var folder = "BuildigSitePhotos";

                foreach (var file in model.Photos)
                {
                    var id = Guid.NewGuid();
                    var fileExtension = Path.GetExtension(file.FileName);

                    var blobName = $"{folder}/{id}{fileExtension}";

                    var blobRef = container.GetBlockBlobReference(blobName);

                    using (var stream = file.OpenReadStream())
                    {
                        await blobRef.UploadFromStreamAsync(stream);
                    }
                }

                var buildingSite = new BuildingSite
                {
                    CustomerName = model.CustomerName,
                    CutomerEmail = model.CustomerEMail,
                    BuildingSiteLocation = model.BuildingSiteLocation,
                    PhotoFolderUri = $"{container.Name}/{folder}"
                };

                _buildingSiteRepository.Insert(buildingSite);

                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Errore generico");
            return View(model);
        }
    }
}