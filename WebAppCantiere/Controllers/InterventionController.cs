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
using WebAppCantiere.Models.InterventionModels;

namespace WebAppCantiere.Controllers
{
    public class InterventionController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IInterventionRepository _interventionRepository;
        private readonly IBuildingSiteRepository _buildingSiteRepository;

        public InterventionController(IConfiguration configuration, IInterventionRepository interventionRepository, IBuildingSiteRepository buildingSiteRepository)
        {
            _configuration = configuration;
            _interventionRepository = interventionRepository;
            _buildingSiteRepository = buildingSiteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Insert()
        {
            var model = new InterventionInsertViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(int id, InterventionInsertViewModel model)
        {
            var storageAccount = CloudStorageAccount.Parse(_configuration["BlobStorageConnectionString"]);

            var blobClient = storageAccount.CreateCloudBlobClient();

            var buildingSiteNow = _buildingSiteRepository.Get(id);

            var container =
                blobClient.GetContainerReference(buildingSiteNow.BuildingSiteLocation.ToLower().Replace(" ", "-"));
            //await container.CreateIfNotExistsAsync();

            var folder = model.Title.ToLower().Replace(" ", "_");

            foreach (var file in model.Photos)
            {
                var idFile = Guid.NewGuid();
                var fileExtension = Path.GetExtension(file.FileName);

                var blobName = $"{folder}/{idFile}{fileExtension}";

                var blobRef = container.GetBlockBlobReference(blobName);

                using (var stream = file.OpenReadStream())
                {
                    await blobRef.UploadFromStreamAsync(stream);
                }
            }

            if (ModelState.IsValid)
            {
                var todo = new Intervention()
                {
                    Title = model.Title,
                    Type = model.Type,
                    Notes = model.Notes,
                    Cost = model.Cost,
                    BuidingSiteId = id,
                    PhotoFolderUri = $"{container.Name}/{folder}"

                };
                _interventionRepository.Insert(todo);

                return RedirectToAction("../BuildingSite/Index");
            }

            ModelState.AddModelError("", "Errore generico");

            return View(model);

        }
    }
}