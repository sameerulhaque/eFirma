﻿using DigitalFirmaClone.Models.Objects;
using DigitalFirmaClone.Models.Utils;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace DigitalFirmaClone.Models.Dao
{
    public class Certificates : BaseObjectDAO<Certificate>
    {
        private string _certificatesPath = "keys";

        public Certificates(ApiClient apiClient) : base(apiClient) { }

        public override void Delete(string id)
        {
            ApiClient.Delete(_certificatesPath + "/" + id);
        }

        public override Certificate Find(string id)
        {
            HttpContent httpResponse = ApiClient.Get(_certificatesPath + "/" + id);
            string response = httpResponse.ReadAsStringAsync().Result;
            return MifielUtils.ConvertJsonToObject<Certificate>(response);
        }

        public override List<Certificate> FindAll()
        {
            HttpContent httpResponse = ApiClient.Get(_certificatesPath);
            string response = httpResponse.ReadAsStringAsync().Result;
            return MifielUtils.ConvertJsonToObject<List<Certificate>>(response);
        }

        public override Certificate Save(Certificate certificate)
        {
            if (string.IsNullOrEmpty(certificate.Id))
            {
                HttpContent httpContent = BuildHttpBody(certificate);
                HttpContent httpResponse = ApiClient.Post(_certificatesPath, httpContent);
                string response = httpResponse.ReadAsStringAsync().Result;
                return MifielUtils.ConvertJsonToObject<Certificate>(response);
            }
            else
            {
                string json = MifielUtils.ConvertObjectToJson(certificate);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpContent httpResponse = ApiClient.Put(_certificatesPath, httpContent);
                string response = httpResponse.ReadAsStringAsync().Result;
                return MifielUtils.ConvertJsonToObject<Certificate>(response);
            }            
        }

        private HttpContent BuildHttpBody(Certificate certificate)
        {
            string certificatePath = certificate.File;

            MultipartFormDataContent multipartContent = new MultipartFormDataContent();
            ByteArrayContent pdfContent = new ByteArrayContent(File.ReadAllBytes(certificatePath));
            pdfContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pkix-cert");

            multipartContent.Add(pdfContent, "file", Path.GetFileName(certificatePath));
            return multipartContent;
        }
    }
}
