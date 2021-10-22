using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Models;
using DigitalFirmaClone.Models.Dao;
using DigitalFirmaClone.Models.ModelClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Controllers
{
    public class CertificateController : Controller
    {

        public IConfiguration configuration { get; }
        public ISignatureManager SignatureManager { get; }
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IEmailService _emailService;


        private ApiClient _apiClient;
        private Certificates _cer;

        public CertificateController(IWebHostEnvironment hostingEnvironment, IConfiguration _configuration, ISignatureManager _SignatureManager, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            configuration = _configuration;
            SignatureManager = _SignatureManager;
            _emailService = emailService;

            _apiClient = new ApiClient(_configuration.GetSection("Mifiel").GetValue<string>("clientId"), _configuration.GetSection("Mifiel").GetValue<string>("secret"));
            _cer = new Certificates(_apiClient);
            _apiClient.Url = _configuration.GetSection("Mifiel").GetValue<string>("url");
        }

        public async Task<JsonResult> CertificateUploadElement(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, User.Identity.Name + "-Certificate-" + file.FileName);
                if (!System.IO.File.Exists(filePath))
                {
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

            }

            return new JsonResult(new { name = "sameer" });
        }
        public IActionResult CreateCertificate()
        {
            ViewBag.ParentCode = "";
            ViewBag.Title = "Sign-Document";
            ViewBag.SubTitle = "";

            return View();
        }
        [HttpPost]
        public IActionResult CreateCertificate(Certificate certificate)
        {
            SaveDocument(certificate);
            return View();
        }

        private void SaveDocument(Certificate certificate)
        {
            var document = new DigitalFirmaClone.Models.Objects.Certificate()
            {
                File = _hostingEnvironment.ContentRootPath + "/uploads/" + User.Identity.Name + "-Certificate-" + certificate.CertificateFileName,
            };
            document = _cer.Save(document);
        }

        [Authorize]
        public IActionResult SignDocument(string id)
        {
            var UserId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value ?? "0");

            var IsAuthenticated = SignatureManager.IsWidgetAuthenticated(UserId, id);
            if(IsAuthenticated)
            {
                ViewBag.WidgetId = id;
                return View();
            }
            else
            {
                return Redirect("Authentication");
            }
        }

        public IActionResult Authentication()
        {
            return View();
        }
    }
}
