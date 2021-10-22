using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models.Payment
{
    public class ProcessPayment
    {
        public static async Task<Charge> PayAsync(PayModel payModel, string APIKey)
        {
            try
            {
                StripeConfiguration.ApiKey = APIKey;

                var options = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = payModel.CardNumder,
                        ExpMonth = payModel.Month,
                        ExpYear = payModel.Year,
                        Cvc = payModel.CVC
                    },
                };

                var serviceToken = new TokenService();
                Token stripeToken = await serviceToken.CreateAsync(options);

                var chargeOptions = new ChargeCreateOptions
                {
                    Amount = payModel.Amount,
                    Currency = "mxn",
                    Description = "Stripe Test Payment",
                    Source = stripeToken.Id
                };

                var chargeService = new ChargeService();
                Charge charge = await chargeService.CreateAsync(chargeOptions);
                return charge;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
