using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Models;
using DigitalFirmaClone.Models.APIClasses;
using DigitalFirmaClone.Models.ModelClasses;
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
        public SignatureController(IWebHostEnvironment hostingEnvironment, IConfiguration _configuration, ISignatureManager _SignatureManager)
        {
            _hostingEnvironment = hostingEnvironment;
            configuration = _configuration;
            SignatureManager = _SignatureManager;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.ParentCode = "Documents";
            ViewBag.Title = "Everybody";
            ViewBag.SubTitle = "List";

            APIResponse<List<GetSignature>> reservationList = new APIResponse<List<GetSignature>>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c3d-192b-817c-4aa4bbb30009:d2a9e08e2cdbf21bc9d0d118b064ecc808f4f83a1cd58495875a59dfdeb29317");
                using (var response = await httpClient.GetAsync("http://api.digitafirma.com/v1/documents"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<APIResponse<List<GetSignature>>>(apiResponse);
                }
            }
            reservationList = new APIResponse<List<GetSignature>>();
            reservationList.data = new List<GetSignature>();
            return View(reservationList.data);
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
                //var IdSearch = GetValues["columns[1][search][value]"];
                //var AllowanceNameSearch = GetValues["columns[2][search][value]"];


                //Paging Size (10,20,50,100)    
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                // Getting all Customer data    
                //var customerData = new List<Signature>();
                
                    var customerData = SignatureManager.GetAllSignatures();

                
                //Search    
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(x => x.Id.ToString() == searchValue).ToList();
                }
                //if (!string.IsNullOrEmpty(IdSearch))
                //{
                //    customerData = customerData.Where(x => x.Id.ToString() == IdSearch).ToList();
                //}

                //if (!string.IsNullOrEmpty(AllowanceNameSearch))
                //{
                //    customerData = customerData.Where(x => x.AllowanceName.ToUpper().Contains(AllowanceNameSearch.ToUpper())).ToList();
                //}

                //total number of rows count     
                recordsTotal = customerData.Count();
                //Paging     
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data    
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<IActionResult> Signed()
        {
            GetSignature getSignature = new GetSignature();
            ViewBag.ParentCode = "Documents";
            ViewBag.Title = "Everybody";
            ViewBag.SubTitle = "List";

            APIResponse<List<GetSignature>> reservationList = new APIResponse<List<GetSignature>>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c3d-192b-817c-4aa4bbb30009:d2a9e08e2cdbf21bc9d0d118b064ecc808f4f83a1cd58495875a59dfdeb29317");
                using (var response = await httpClient.GetAsync("http://api.digitafirma.com/v1/documents?sign_status=1"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<APIResponse<List<GetSignature>>>(apiResponse);
                }
            }
            reservationList = new APIResponse<List<GetSignature>>();
            reservationList.data = new List<GetSignature>();
            return View(reservationList.data);
        }



        public async Task<JsonResult> UploadElement(IFormFile file)
        {
            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");


            if (file.Length > 0)
            {
                string filePath = Path.Combine(uploads, User.Identity.Name + "-" + file.FileName );
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


        public IActionResult CreateNew()
        {
            ViewBag.ParentCode = "Documents";
            ViewBag.Title = "Everybody";
            ViewBag.SubTitle = "List";

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CreateNew(Signature Object)
        {
            var PayPalAPI = new PayPalAPI(configuration);
            string url = await PayPalAPI.getRedirectUrlToPayPal(Object.Total, "MXN");

            var Id = url.Split("token=").Count() > 0 ? url.Split("token=")[1] : "";

            string uploads = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");
            string filePath = Path.Combine(uploads, User.Identity.Name + "-" + Object.FileName);
            var array = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(array, 0, array.Length);

            Object.DocumentString = base64String;
            Object.FileName = Object.FileName;
            Object.Auth = "7f000001-7c0b-17f1-817c-0f676f830004:9de039f9ccab4acb64a1f57cfc5c7ad6f27824e763071aa6523ceb49dcec0b8f";
            Object.PaypalId = Id;
            Object.CompanyId = "7f000001-7c0b-17f1-817c-0f5812fd0003";
            Object.SignOrdered = "ACTIVE";
            Object.ModeLogo = "FULL_BRANDING";
            Object.RememberAt = "string";
            Object.RememberEvery = 0;
            Object.SignMode = "CLASSIC";
            Object.SignPosition = "UPPER_RIGHT";

            var IsExist = SignatureManager.AddSignature(Object);
            if (IsExist.Id > 0)
            {
                return Json(new { result = "Redirect", url = url });
            }
            else
            {
                return Json(new { });
            }

        }



        [HttpPost]
        [Route("success")]
        public async Task<IActionResult> Success([FromQuery(Name = "paymentId")] string paymentId, [FromQuery(Name = "payerID")] string payerID)
        {
            var PayPalAPI = new PayPalAPI(configuration);
            PayPalPaymentExecutedResponse result = await PayPalAPI.executedPayment(paymentId, payerID);

            CompanyData receivedReservation = new CompanyData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c0b-17f1-817c-0f676f830004:9de039f9ccab4acb64a1f57cfc5c7ad6f27824e763071aa6523ceb49dcec0b8f");

               
                var Object = SignatureManager.GetSignatureById(new Signature() { PaypalId = paymentId });

                CreateSignature createCompany = new CreateSignature();
                createCompany.document = Object.DocumentString;
                createCompany.tries = 0;
                foreach (var item in Object.SignersList)
                {
                    if (!string.IsNullOrEmpty(item.SignatureEmail == "undefined" ? "" : item.SignatureEmail))
                    {
                        signatures signatures = new signatures();
                        signatures.name = item.SignatureName;
                        signatures.email = item.SignatureEmail;
                        signatures.rfc = "";
                        createCompany.signatures.Add(signatures);
                    }
                }

                foreach (var item in Object.ViewersList)
                {
                    if (!string.IsNullOrEmpty(item.ViewerEmail == "undefined" ? "" : item.ViewerEmail))
                    {
                        spectators spectators = new spectators();
                        spectators.name = item.ViewerName;
                        spectators.email = item.ViewerEmail;
                        createCompany.spectators.Add(spectators);
                    }
                }

                createCompany.companyId = Object.CompanyId;
                createCompany.signOrdered = Object.SignOrdered;
                createCompany.modeLogo = Object.ModeLogo;
                createCompany.rememberAt = Object.RememberAt;
                createCompany.rememberEvery = (int)Object.RememberEvery;
                createCompany.signMode = Object.SignMode;
                createCompany.signPosition = Object.SignPosition;


                StringContent content = new StringContent(JsonConvert.SerializeObject(createCompany), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://api.digitafirma.com/v1/documents", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        receivedReservation = JsonConvert.DeserializeObject<CompanyData>(apiResponse);
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                }
            }



            return View("success");
        }


        public IActionResult PDFValidator()
        {
            ViewBag.ParentCode = "";
            ViewBag.Title = "PDF Validator";
            ViewBag.SubTitle = "";

            return View();
        }


        public async Task<IActionResult> Companies()
        {
            APIResponse<List<CompanyData>> reservationList = new APIResponse<List<CompanyData>>();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c29-16ef-817c-35be2d830017:b48222c13b3a9c6858f8598293f3caf5ec09a90b1f8e62c012c0042da019ea44");
                using (var response = await httpClient.GetAsync("http://api.digitafirma.com/v1/companies"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<APIResponse<List<CompanyData>>>(apiResponse);
                }
            }
            return View(reservationList);
        }

        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(Company file)
        {

            CompanyData receivedReservation = new CompanyData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c3d-192b-817c-4aa4bbb30009:d2a9e08e2cdbf21bc9d0d118b064ecc808f4f83a1cd58495875a59dfdeb29317");

                CreateCompany createCompany = new CreateCompany();
                using (var fileStream = file.Image.OpenReadStream())
                {
                    byte[] array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                    createCompany.logo = array;
                }
                createCompany.name = "sameer 1";
                StringContent content = new StringContent(JsonConvert.SerializeObject(createCompany), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://api.digitafirma.com/v1/companies", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        receivedReservation = JsonConvert.DeserializeObject<CompanyData>(apiResponse);
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        ViewBag.Result = apiResponse;
                        return View();
                    }
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCompany(Company file)
        {

            CompanyData receivedReservation = new CompanyData();
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c29-16ef-817c-35be2d830017:b48222c13b3a9c6858f8598293f3caf5ec09a90b1f8e62c012c0042da019ea44");

                CreateCompany createCompany = new CreateCompany();
                using (var fileStream = file.Image.OpenReadStream())
                {
                    byte[] array = new byte[fileStream.Length];
                    fileStream.Read(array, 0, array.Length);
                    createCompany.logo = array;
                }
                createCompany.name = "sameer 1";
                StringContent content = new StringContent(JsonConvert.SerializeObject(createCompany), Encoding.UTF8, "application/json");

                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://api.digitafirma.com/v1/companies/" + "7f000001-7c29-16ef-817c-350a5c600013"),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"name\", \"value\": \"" + createCompany.name + "\"},{ \"op\": \"replace\", \"path\": \"logo\", \"value\": \"" + createCompany.logo + "\"}]", Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
                int assa = 1;
            }
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> AddFile(Company file)
        //{
        //    string apiResponse = "";

        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.DefaultRequestHeaders.Add("X-Api-Key", "7f000001-7c29-16ef-817c-35be2d830017:b48222c13b3a9c6858f8598293f3caf5ec09a90b1f8e62c012c0042da019ea44");
        //        var form = new MultipartFormDataContent();
        //        //using (var memoryStream = new MemoryStream())
        //        //{
        //        //    await file.Image.CopyToAsync(memoryStream);
        //        //    var ImageByteArray = memoryStream.ToArray();
        //        //}
        //        using (var fileStream = file.Image.OpenReadStream())
        //        {
        //            form.Add(new StreamContent(fileStream), "file", file.Image.FileName);
        //            HttpContent httpContent = new HttpContent();

        //            using (var response = await httpClient.PostAsync("https://localhost:44324/api/Reservation/UploadFile", form))
        //            {
        //                response.EnsureSuccessStatusCode();
        //                apiResponse = await response.Content.ReadAsStringAsync();
        //            }
        //        }
        //    }
        //    return View((object)apiResponse);
        //}


        [HttpPost]
        public async Task<IActionResult> UpdateReservation(Object reservation)
        {
            Object receivedReservation = new Object();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(1.ToString()), "Id");
                content.Add(new StringContent("Name"), "Name");
                content.Add(new StringContent("StartLoc"), "StartLocation");
                content.Add(new StringContent("EndLoc"), "EndLocation");

                using (var response = await httpClient.PutAsync("https://localhost:44324/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedReservation = JsonConvert.DeserializeObject<Object>(apiResponse);
                }
            }
            return View(receivedReservation);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateReservationPatch(int id, Object reservation)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://localhost:44324/api/Reservation/" + id),
                    Method = new HttpMethod("Patch"),
                    Content = new StringContent("[{ \"op\": \"replace\", \"path\": \"Name\", \"value\": \"" + "Name" + "\"},{ \"op\": \"replace\", \"path\": \"StartLocation\", \"value\": \"" + "StartLoc" + "\"}]", Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(request);
            }
            return RedirectToAction("Index");
        }


    }
}
