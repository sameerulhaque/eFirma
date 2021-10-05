﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFirmaClone.PayPalHelper
{
    public class PayPalAPI
    {
        public IConfiguration configuration { get; }


        public PayPalAPI(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public async Task<string> getRedirectUrlToPayPal(double total, string currency)
        {
            try
            {
               return Task.Run(async () =>
               {
                   HttpClient http = GetPaypalHttpClient();
                   PayPalAccessToken accessToken = await GetPaypalAccessTokenAsync(http);
                   PayPalPaymentCreatedResponse createdPayment = await createPayPalPaymentAsync
                   (http, accessToken, total, currency);
                   return createdPayment.links.First(x => x.rel == "approve").href;
               }).Result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, "Failed Login to PayPal");
                return null;
            }
        }


        public async Task<PayPalPaymentExecutedResponse> executedPayment(string paymentId, string payerId)
        {
            try
            {
                HttpClient http = GetPaypalHttpClient();
                PayPalAccessToken accessToken = await GetPaypalAccessTokenAsync(http);
                return await ExecutePayPalPaymentAsync(http, accessToken, paymentId, payerId);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e, "Failed Login to PayPal");
                return null;
            }
        }

        private HttpClient GetPaypalHttpClient()
        {
            string sandbox = configuration["PayPal:urlAPI"];

            var http = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

        public async Task<PayPalAccessToken> GetPaypalAccessTokenAsync(HttpClient http)
        {
          byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(
              $"{configuration["PayPal:clientId"]}:{configuration["PayPal:secret"]}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);
            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);

            return accessToken;
        }


        public async Task<PayPalPaymentCreatedResponse> createPayPalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, double total,  string currency)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v2/checkout/orders");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                intent = "CAPTURE",
                //redirect_urls = new
                //{
                //    return_url = configuration["PayPal:returnUrl"],
                //    cancel_url = configuration["PayPal:cancelUrl"]
                //},
                //payer = new { payer_method = "paypal" },

                purchase_units = JArray.FromObject(new[]
                {
                    new
                    {
                        amount = new
                        {
                            value = total,
                            currency_code = currency
                        }
                    }
                })
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentCreatedResponse payPalPaymentCreated = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(content);

            return payPalPaymentCreated;
        }



        public async Task<PayPalPaymentExecutedResponse> ExecutePayPalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, string paymentId, string payerId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentId}/excecute");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                payer_id = payerId
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment),
                Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentExecutedResponse executedPayment = JsonConvert.DeserializeObject<PayPalPaymentExecutedResponse>(content);

            return executedPayment;
        }

    }
}
