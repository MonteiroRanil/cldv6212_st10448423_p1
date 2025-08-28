using Microsoft.AspNetCore.Mvc;
using ABCretailers.Models;
using ABCretailers.Services;

namespace ABCretailers.Controllers
{
    public class UploadController : Controller
    {
        private readonly IAzureStorageService _storageService;

        public UploadController(IAzureStorageService storageService)
        {
            _storageService = storageService;
        }

         public IActionResult Index()
        {
            return View(new FileUpload());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(FileUpload model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    
                    if(model.ProofOfPayment != null && model.ProofOfPayment.Length > 0)
                    {
                        var fileName = await _storageService.UploadFileAsync(model.ProofOfPayment, "payment-proofs");
                        await _storageService.UploadToFileShareAsync(model.ProofOfPayment, "contracts", "payments");
                        TempData["Success"] = $"File uploaded successfully! File Name: {fileName}";

                        return View(new FileUpload());
                    }
                    else
                    {
                        ModelState.AddModelError("ProofOfPayment", "Please select a file to upload.");
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", $"Error uploading file: {ex.Message}");
                }
            }
            return View(model);
        }
    }
}
