using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Models;
using DigitalFirmaClone.Models.APIClasses;
using DigitalFirmaClone.Models.Dao;
using DigitalFirmaClone.Models.EmailConfigurationModel;
using DigitalFirmaClone.Models.ModelClasses;
using DigitalFirmaClone.Models.Payment;
using DigitalFirmaClone.PayPalHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Controllers
{
    public class SignatureController : Controller
    {
        public IConfiguration configuration { get; }
        public ISignatureManager SignatureManager { get; }
        private readonly IWebHostEnvironment _hostingEnvironment;
        public IEmailService _emailService;

    
        private ApiClient _apiClient;
        private Documents _docs;

        public SignatureController(IWebHostEnvironment hostingEnvironment, IConfiguration _configuration, ISignatureManager _SignatureManager, IEmailService emailService)
        {
            _hostingEnvironment = hostingEnvironment;
            configuration = _configuration;
            SignatureManager = _SignatureManager;
            _emailService = emailService;

            _apiClient = new ApiClient(_configuration.GetSection("Mifiel").GetValue<string>("clientId"), _configuration.GetSection("Mifiel").GetValue<string>("secret"));
            _docs = new Documents(_apiClient);
            _apiClient.Url = _configuration.GetSection("Mifiel").GetValue<string>("url");
        }

        #region Email
       
        [Route("SendEmailWithAttachment")]
        [HttpPost]
        public ActionResult SendEmailWithAttachment(EmailDataWithAttachment emailData)
        {
            _emailService.SendEmailWithAttachment(emailData);
            return View();
        }
        [HttpPost]
        public async Task<CompanyData> SendEmailWithFile(EmailDataWithAttachment file)
        {
            file.EmailBody = "test";
            file.EmailSubject = "test";
            file.EmailToId = "sameer.ulhaq79@gmail.com";
            file.EmailToName = "Areeb";
            file.EmailAttachments = file.EmailAttachments;

            _emailService.SendEmailWithAttachment(file);
            return new CompanyData();
        }

        //public async Task<CompanyData> signDocument(SignDocument file)
        //{
        //    emailData.EmailBody = "test";
        //    emailData.EmailSubject = "test";
        //    emailData.EmailToId = "areebsiddiqui136@gmail.com";
        //    emailData.EmailToName = "Areeb";
        //    _emailService.SendEmailWithAttachment(emailData);


        //    CompanyData receivedReservation = new CompanyData();
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c3d-192b-817c-4aa4bbb30009:d2a9e08e2cdbf21bc9d0d118b064ecc808f4f83a1cd58495875a59dfdeb29317");

        //        string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
        //        string filePaths = Path.Combine(uploads, User.Identity.Name + "-" + file.cer);
        //        var array = System.IO.File.ReadAllBytes(filePaths);
        //        string keybase64String = Convert.ToBase64String(array, 0, array.Length);

        //        string upload = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
        //        string filePath = Path.Combine(upload, User.Identity.Name + "-" + file.key);
        //        var arrays = System.IO.File.ReadAllBytes(filePath);
        //        string cerbase64String = Convert.ToBase64String(arrays, 0, arrays.Length);

        //        SignDocument SignDocument = new SignDocument();
        //        SignDocument.key = keybase64String;
        //        SignDocument.cer = cerbase64String;
        //        SignDocument.password = file.password;
        //        SignDocument.reject = false;
        //        SignDocument.commentary = "";

        //        StringContent content = new StringContent(JsonConvert.SerializeObject(SignDocument), Encoding.UTF8, "application/json");

        //        using (var response = await httpClient.PostAsync("http://api.digitafirma.com/v1/sign/" + SignDocument.SignatureId, content))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();

        //            if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //            {
        //                var APIResponse = JsonConvert.DeserializeObject<APIResponse<List<CompanyData>>>(apiResponse);
        //                return APIResponse.data.LastOrDefault();
        //            }
        //            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        //            {
        //                return new CompanyData();
        //            }
        //        }
        //    }
        //    return new CompanyData();
        //}
        #endregion


        public IActionResult Index()
        {
            ViewBag.ParentCode = "Documents";
            ViewBag.Title = "Everybody";
            ViewBag.SubTitle = "List";

            return View();
        }
        public JsonResult _GetAllSignatures()
        {
            try
            {

                var GetValues = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

                var draw = GetValues["draw"];
                var start = GetValues["start"];
                var length = GetValues["length"];
                var sortColumn = GetValues["columns[" + GetValues["order[0][column]"] + "][name]"];
                var sortColumnDir = GetValues["order[0][dir]"];
                var searchValue = GetValues["search[value]"];
 
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                
                var UserId = User.Claims.FirstOrDefault(x => x.Type == "Id").Value ?? "0";
                var GetAllSignatures = SignatureManager.GetAllSignatures(int.Parse(UserId));

                var customerData = _docs.FindAll().Where(x => GetAllSignatures.Any(y => y.MifielId == x.Id));

                foreach (var item in customerData)
                {
                    item.SignStatus = item.Signed ? "Signed" : "Unsigned";
                    item.SignStatusColor = item.SignStatus == "Signed" ? "success" : "danger";
                    item.CreatedAtString = item.CreatedAt.ToString("dd/MM/yyyy");
                }
  
                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }
       

        public IActionResult Signed()
        {
            ViewBag.ParentCode = "Documents";
            ViewBag.Title = "Everybody";
            ViewBag.SubTitle = "List";
            return View();
        }
        public JsonResult _GetAllSignedSignatures()
        {
            try
            {

                var GetValues = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

                var draw = GetValues["draw"];
                var start = GetValues["start"];
                var length = GetValues["length"];
                var sortColumn = GetValues["columns[" + GetValues["order[0][column]"] + "][name]"];
                var sortColumnDir = GetValues["order[0][dir]"];
                var searchValue = GetValues["search[value]"];

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;


                var UserId = User.Claims.FirstOrDefault(x => x.Type == "Id").Value ?? "0";
                var GetAllSignatures = SignatureManager.GetAllSignatures(int.Parse(UserId));

                var customerData = _docs.FindAll().Where(x => GetAllSignatures.Any(y => y.MifielId == x.Id)).Where(x => x.Signed);

                foreach (var item in customerData)
                {
                    item.SignStatus = item.Signed ? "Signed" : "Unsigned";
                    item.SignStatusColor = item.SignStatus == "Signed" ? "success" : "danger";
                }

                recordsTotal = customerData.Count();
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }


        public IActionResult UploadElement()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UploadElement(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, User.Identity.Name + "-" + file.FileName );
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return new JsonResult(new { name = file.FileName });
            }

            return new JsonResult(new { name = "sameer" });
        }
        public IActionResult CreateNew(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.ParentCode = "Documents";
                ViewBag.Title = "Everybody";
                ViewBag.SubTitle = "List";
                ViewBag.FileName = id;

                return View();
            }
            else
            {
                return RedirectToAction("UploadElement");
            }
        }
        [HttpPost]
        public async Task<JsonResult> CreateNew(Signature Object)
        {
            try
            {
                var userId = int.Parse(User.Claims.FirstOrDefault(x => x.Type == "Id").Value ?? "0");

                Object.PayModel.Amount = 10 * 100;
                var result = await ProcessPayment.PayAsync(Object.PayModel, configuration.GetSection("Stripe").GetValue<string>("secret"));
                if (result.Paid || userId == 3)
                {
                    Object.FileName = _hostingEnvironment.ContentRootPath + "/uploads/" + User.Identity.Name + "-" + Object.FileName;
                    var document = new DigitalFirmaClone.Models.Objects.Document()
                    {
                        File = Object.FileName,
                        ManualClose = false,
                        CallbackUrl = "https://requestb.in/1cuddmz1"
                    };

                    foreach (var item in Object.SignersList)
                    {
                        if (!string.IsNullOrEmpty(item.SignatureEmail == "undefined" ? "" : item.SignatureEmail))
                        {
                            DigitalFirmaClone.Models.Objects.Signature signatures = new DigitalFirmaClone.Models.Objects.Signature();
                            signatures.SignerName = item.SignatureName;
                            signatures.Email = item.SignatureEmail;
                            signatures.TaxId = "";
                            document.Signatures.Add(signatures);
                        }
                    }

                    foreach (var item in Object.ViewersList)
                    {
                        if (!string.IsNullOrEmpty(item.ViewerEmail == "undefined" ? "" : item.ViewerEmail))
                        {
                            DigitalFirmaClone.Models.Objects.Viewer signatures = new DigitalFirmaClone.Models.Objects.Viewer();
                            signatures.Name = item.ViewerName;
                            signatures.Email = item.ViewerEmail;
                            document.Viewers.Add(signatures);
                        }
                    }

                    document.SendMail = false;
                    document.SendInvites = false;
                    document = _docs.Save(document);


                    foreach (var item in document.Signers)
                    {
                        var UserIsExist = Object.SignersList.Where(x => x.SignatureEmail == item.Email).FirstOrDefault();
                        if (UserIsExist != null)
                        {
                            UserIsExist.WidgetId = item.WidgetId;
                        }
                    }
                    Object.MifielId = document.Id;
                    Object.PaypalId = "";
                    Object.CompanyId = "";
                    Object.UserId = userId;
                    var IsExist = SignatureManager.AddSignature(Object);



                    foreach (var item in document.Signers)
                    {
                        EmailData emailData = new EmailData();
                        emailData.EmailBody = "<h2>Hola " + item.Name + "</h2>";
                        emailData.EmailBody += "<p>" + User.Identity.Name + " has requested that you sign the same document as " + document.FileFileName + "</p>";
                        emailData.EmailBody += "<p> has requested that you sign the same document as " + document.FileFileName + "</p>";
                        emailData.EmailBody += "<a href='https://efirma.bivts.com/Certificate/SignDocument/" + item.WidgetId + "'> Click here to sign </a>";
                        emailData.EmailBody += "<p>Greetings</p>";
                        emailData.EmailBody += "<p>Desique Team.</p>";

                        emailData.EmailSubject = "Signature required by " + User.Identity.Name;
                        emailData.EmailToId = item.Email;
                        emailData.EmailToName = item.Name;
                        SendEmail(emailData);
                    }


                    return Json(new { result = "Redirect", url = "/Signature/Success" });
                }
                else
                {
                    return Json(new { result = "Redirect", url = "/Signature/Error" });
                }
            }
            catch(Exception e)
            {
                return Json(new { result = "Redirect", url = "/Signature/Error" });
            }

        }

        public IActionResult Success()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
        private void SendEmail(EmailData emailData)
        {
            _emailService.SendEmail(emailData);
        }













        //[HttpPost]
        //public async Task<JsonResult> CreateNew(Signature Object)
        //{
        //    var PayPalAPI = new PayPalAPI(configuration);
        //    string url = await PayPalAPI.getRedirectUrlToPayPal(Object.Total, "MXN");
        //    var Id = url.Split("token=").Count() > 0 ? url.Split("token=")[1] : "";

        //    Object.FileName = _hostingEnvironment.ContentRootPath + "/uploads/" + User.Identity.Name + "-" + Object.FileName;
        //    Object.PaypalId = Id;
        //    Object.CompanyId = "7f000001-7c0b-17f1-817c-0f5812fd0003";

        //    var IsExist = SignatureManager.AddSignature(Object);
        //    SaveDocument(Id);
        //    if (IsExist.Id > 0)
        //    {
        //        return Json(new { result = "Redirect", url = url });
        //    }
        //    else
        //    {
        //        return Json(new { });
        //    }

        //}
        //[HttpPost]
        //[Route("success")]
        //public async Task<IActionResult> Success([FromQuery(Name = "paymentId")] string paymentId, [FromQuery(Name = "payerID")] string payerID)
        //{
        //    var PayPalAPI = new PayPalAPI(configuration);
        //    PayPalPaymentExecutedResponse result = await PayPalAPI.executedPayment(paymentId, payerID);


        //    return View("success");
        //}
        //private void SaveDocument(string paymentId)
        //{
        //    var Object = SignatureManager.GetSignatureById(new Signature() { PaypalId = paymentId });

        //    var document = new DigitalFirmaClone.Models.Objects.Document()
        //    {
        //        File = Object.DocumentName,
        //        ManualClose = false,
        //        CallbackUrl = "https://requestb.in/1cuddmz1"
        //    };

        //    foreach (var item in Object.SignersList)
        //    {
        //        if (!string.IsNullOrEmpty(item.SignatureEmail == "undefined" ? "" : item.SignatureEmail))
        //        {
        //            DigitalFirmaClone.Models.Objects.Signature signatures = new DigitalFirmaClone.Models.Objects.Signature();
        //            signatures.SignerName = item.SignatureName;
        //            signatures.Email = item.SignatureEmail;
        //            signatures.TaxId = "";
        //            document.Signatures.Add(signatures);
        //        }
        //    }

        //    foreach (var item in Object.ViewersList)
        //    {
        //        if (!string.IsNullOrEmpty(item.ViewerEmail == "undefined" ? "" : item.ViewerEmail))
        //        {
        //            DigitalFirmaClone.Models.Objects.Viewer signatures = new DigitalFirmaClone.Models.Objects.Viewer();
        //            signatures.Name = item.ViewerName;
        //            signatures.Email = item.ViewerEmail;
        //            document.Viewers.Add(signatures);
        //        }
        //    }

        //    document.SendMail = false;
        //    document.SendInvites = false;
        //    document = _docs.Save(document);

        //    foreach (var item in document.Signers)
        //    {
        //        EmailData emailData = new EmailData();
        //        emailData.EmailBody = "<h2>Hola " + item.Name + "</h2>";
        //        emailData.EmailBody += "<p>" + User.Identity.Name + " has requested that you sign the same document as " + document.FileFileName + "</p>";
        //        emailData.EmailBody += "<p> has requested that you sign the same document as " + document.FileFileName + "</p>";
        //        emailData.EmailBody += "<a href='https://localhost:44349/Certificate/SignDocument/" + item.WidgetId + "'> Click here to sign </a>";
        //        emailData.EmailBody += "<p>Greetings</p>";
        //        emailData.EmailBody += "<p>Desique Team.</p>";

        //        emailData.EmailSubject = "Signature required by " + User.Identity.Name;
        //        emailData.EmailToId = item.Email;
        //        emailData.EmailToName = item.Name;
        //        SendEmail(emailData);
        //    }


        //}
        //private void SendEmail(EmailData emailData)
        //{
        //    _emailService.SendEmail(emailData);
        //}










        public async Task<JsonResult> ValidatorUploadElement(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");


            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, User.Identity.Name + "-" + file.FileName);
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
        public IActionResult PDFValidator()
        {
            ViewBag.ParentCode = "";
            ViewBag.Title = "PDF Validator";
            ViewBag.SubTitle = "";

            return View();
        }


        public ActionResult AddCompany()
        {

            return View();
        }

       


    }
}
