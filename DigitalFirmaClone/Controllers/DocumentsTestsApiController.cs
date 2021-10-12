using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DigitalFirmaClone.Models;
using DigitalFirmaClone.Models.Dao;
using DigitalFirmaClone.Models.Objects;
using System.IO;
using NUnit.Framework;

namespace DigitalFirmaClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsTestsApiController : ControllerBase
    {
        const string APP_ID = "be4f488b5c2668b7cbec85a7ff0afb96aec6db43";
        const string APP_SECRET = "gQOzuRAk/pwI9qye2JHTNgfK3cpoiyA4s8bvgWdW6gFQdgPicHuMiaysXrGBZfa7LfpWHCU5UOir+Uajy6dlOA==";
        private static ApiClient _apiClient;
        private static Documents _docs;
        private static string _pdfFilePath;

        private readonly string _currentDirectory = Path.GetFullPath(TestContext.CurrentContext.TestDirectory);

        private bool SetUp()
        {
            try
            {
                _pdfFilePath = Path.Combine(_currentDirectory, "test-pdf.pdf");
                _apiClient = new ApiClient(APP_ID, APP_SECRET);
                _docs = new Documents(_apiClient);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        [Test]
        public IActionResult Documents__WrongUrl__ShouldThrowAnException()
        {
            Assert.Throws<Models.Exceptions.MifielException>(() => _apiClient.Url = "www.google.com");
            return Ok();
        }

        [Test]
        public IActionResult Documents__CorrectUrl__ShouldNotThrowAnException()
        {
            _apiClient.Url = "https://sandbox.mifiel.com";
            return Ok();
        }

        [Test]
        public IActionResult Documents__FindAllDocuments__ShouldReturnAList()
        {
            SetSandboxUrl();
            var allDocuments = _docs.FindAll();
            Assert.IsNotNull(allDocuments);
            return Ok();
        }

        [Test]
        public IActionResult Documents__Close__Should_Success()
        {
            SetSandboxUrl();
            var docId = _docs.FindAll()[0].Id;
            var closeDocument = _docs.Close(docId);
            Assert.IsTrue(closeDocument.Success);
            return Ok();
        }

        [HttpPost("Documents__SaveWithFilePath__ShouldReturnADocument")]
        public IActionResult Documents__SaveWithFilePath__ShouldReturnADocument()
        {
            SetSandboxUrl();
            var document = new Document()
            {
                File = Path.Combine(_currentDirectory, _pdfFilePath),
                ManualClose = false,
                CallbackUrl = "https://requestb.in/1cuddmz1"
            };

            var signatures = new List<Signature>(){
                new Signature(){
                    Email = "juan@mifiel.com",
                    TaxId = "ZAAJ8301061E0",
                    SignerName = "Juan Antonio Zavala Aguilar"
                }
            };

            var viewers = new List<Viewer>() {
                new Viewer(){
                    Name = "Juan Zavala",
                    Email = "ja.zavala.aguilar@gmail.com"
                }
            };

            document.SendMail = true;
            document.SendInvites = true;
            document.Signatures = signatures;
            document.Viewers = viewers;
            document = _docs.Save(document);
            Assert.IsNotNull(document);
            return Ok();
        }

        [Test]
        public IActionResult Documents__AppendPDFBase64InOriginalXml__ShouldGenerateNewXML()
        {
            var pathOriginalXml = Path.Combine(_currentDirectory, "file_hash.xml");
            var pathNewXml = Path.Combine(_currentDirectory, "file_with_hash_and_document.xml");
            Models.Utils.MifielUtils.AppendPDFBase64InOriginalXml(_pdfFilePath, pathOriginalXml, pathNewXml);
            Assert.True(System.IO.File.Exists(pathNewXml));
            return Ok();
        }

        [Test]
        public IActionResult Documents__SaveWithOriginalHashAndFileName__ShouldReturnADocument()
        {
            SetSandboxUrl();
            Document document = new Document()
            {
                OriginalHash = Models.Utils.MifielUtils.GetDocumentHash(_pdfFilePath),
                FileName = "PdfFileName",
                ManualClose = false,
            };

            var signatures = new List<Signature>(){
                new Signature(){
                    Email = "juan@mifiel.com",
                    SignerName = "Juan Antonio Zavala Aguilar"
                }
            };

            var viewers = new List<Viewer>() {
                new Viewer(){
                    Name = "Juan Zavala",
                    Email = "ja.zavala.aguilar@gmail.com"
                }
            };

            document.Signatures = signatures;
            document.Viewers = viewers;

            document = _docs.Save(document);
            Assert.IsNotNull(document);
            return Ok();
        }

        [Test]
        public IActionResult Documents__SaveWithoutRequiredFields__ShouldThrowAnException()
        {
            SetSandboxUrl();
            var document = new Document() { CallbackUrl = "http://www.google.com" };

            Assert.Throws<Models.Exceptions.MifielException>(() => _docs.Save(document));
            Assert.IsNotNull(document);
            return Ok();
        }

        [Test]
        public IActionResult Documents__Find__ShouldReturnADocument()
        {
            SetSandboxUrl();
            Documents__SaveWithOriginalHashAndFileName__ShouldReturnADocument();
            var allDocuments = _docs.FindAll();
            if (allDocuments.Count > 0)
            {
                Document doc1 = _docs.Find(allDocuments[0].Id);
                Assert.IsNotNull(doc1);
            }
            else
            {
                throw new Models.Exceptions.MifielException("No documents found");
            }
            return Ok();
        }

        [Test]
        public IActionResult Documents__Delete__ShouldRemoveADocument()
        {
            SetSandboxUrl();
            Document document = new Document()
            {
                OriginalHash = Models.Utils.MifielUtils.GetDocumentHash(_pdfFilePath),
                FileName = "PdfFileName"
            };

            document = _docs.Save(document);
            _docs.Delete(document.Id);
            return Ok();
        }

        [Test]
        public IActionResult Documents__RequestSignature__ShouldReturnASignatureResponse()
        {
            SetSandboxUrl();
            var document = new Document()
            {
                OriginalHash = Models.Utils.MifielUtils.GetDocumentHash(_pdfFilePath),
                FileName = "PdfFileName"
            };

            document = _docs.Save(document);

            SignatureResponse signatureResponse = _docs.RequestSignature(document.Id,
                                "enrique@test.com", "enrique2@test.com");
            Assert.IsNotNull(signatureResponse);
            return Ok();
        }

        [Test]
        public IActionResult Documents__SaveFile__ShouldSaveFileOnSpecifiedPath()
        {
            SetSandboxUrl();
            Document document = new Document() { File = _pdfFilePath };

            document = _docs.Save(document);

            _docs.SaveFile(document.Id, Path.Combine(_currentDirectory, "pdf_save_test.pdf"));
            return Ok();
        }

        private void SetSandboxUrl()
        {
            SetUp();
            _apiClient.Url = "https://sandbox.mifiel.com";
        }

    }
}
