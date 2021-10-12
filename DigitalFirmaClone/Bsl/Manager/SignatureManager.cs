using DigitalFirmaClone.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalFirmaClone.Bsl.Model;
using DigitalFirmaClone.Models.ModelClasses;
using DigitalFirmaClone.Models.APIClasses;

namespace DigitalFirmaClone.Bsl.Manager
{
    public class SignatureManager : ISignatureManager
    {
        private readonly ecommerceContext dbContext;
        public SignatureManager(ecommerceContext context)
        {
            this.dbContext = context;
        }


        public Signature GetSignatureById(Signature signature)
        {
            try
            {
                Signature Signature = new Signature();

                var sig_signature_IsExist = dbContext.sig_signature.Where(x => x.paypal_id == signature.PaypalId).FirstOrDefault();

                Signature.Id = sig_signature_IsExist.signature_id;
                Signature.DocumentString = sig_signature_IsExist.document_string;
                Signature.DocumentName = sig_signature_IsExist.document_name;
                Signature.Auth = sig_signature_IsExist.auth;
                Signature.PaypalId = sig_signature_IsExist.paypal_id;
                Signature.CompanyId = sig_signature_IsExist.company_id;
                Signature.SignOrdered = sig_signature_IsExist.sign_ordered;
                Signature.ModeLogo = sig_signature_IsExist.mode_logo;
                Signature.RememberAt = sig_signature_IsExist.remember_at;
                Signature.RememberEvery = sig_signature_IsExist.remember_every;
                Signature.SignMode = sig_signature_IsExist.sign_mode;
                Signature.SignPosition = sig_signature_IsExist.sign_position;
                Signature.Tries = sig_signature_IsExist.tries;

                foreach (var item in sig_signature_IsExist.sig_signature_details)
                {
                    Signers signatures = new Signers();
                    signatures.SignatureName = item.name;
                    signatures.SignatureEmail = item.email;
                    signatures.RFC = "";
                    Signature.SignersList.Add(signatures);
                }

                foreach (var item in sig_signature_IsExist.sig_spectators_details)
                {
                    Viewers spectators = new Viewers();
                    spectators.ViewerName = item.name;
                    spectators.ViewerEmail = item.email;
                    Signature.ViewersList.Add(spectators);
                }

                return Signature;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Signature> GetAllSignatures()
        {
            try
            {
                List<Signature> SignatureList = new List<Signature>();

                var sig_signature = dbContext.sig_signature.ToList();

                foreach (var item in sig_signature)
                {
                    Signature Signature = new Signature();

                    Signature.Id = item.signature_id;
                    Signature.DocumentName = item.document_name;
                    Signature.DocumentString = item.document_string;
                    Signature.Auth = item.auth;
                    Signature.PaypalId = item.paypal_id;
                    Signature.CompanyId = item.company_id;
                    Signature.SignOrdered = item.sign_ordered;
                    Signature.ModeLogo = item.mode_logo;
                    Signature.RememberAt = item.remember_at;
                    Signature.RememberEvery = item.remember_every;
                    Signature.SignMode = item.sign_mode;
                    Signature.SignPosition = item.sign_position;
                    Signature.Tries = item.tries;
                    Signature.SignStatus = "UnSigned";
                    Signature.CreateDateString = DateTime.Now.ToString("dd/MM/yyyy");

                    foreach (var x in item.sig_signature_details)
                    {
                        Signers signatures = new Signers();
                        signatures.SignatureName = x.name;
                        signatures.SignatureEmail = x.email;
                        signatures.RFC = "";
                        Signature.SignersList.Add(signatures);
                    }

                    foreach (var x in item.sig_spectators_details)
                    {
                        Viewers spectators = new Viewers();
                        spectators.ViewerName = x.name;
                        spectators.ViewerEmail = x.email;
                        Signature.ViewersList.Add(spectators);
                    }
                    SignatureList.Add(Signature);
                }

                return SignatureList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Signature AddSignature(Signature signature)
        {
            try
            {
                sig_signature Signature = new sig_signature();

                Signature.document_string = signature.DocumentString ?? "";
                Signature.auth = signature.Auth ?? "";
                Signature.document_name = signature.FileName ?? "";
                Signature.paypal_id = signature.PaypalId ?? "";
                Signature.company_id = signature.CompanyId ?? "";
                Signature.sign_ordered = signature.SignOrdered ?? "";
                Signature.mode_logo = signature.ModeLogo ?? "";
                Signature.remember_at = signature.RememberAt ?? "";
                Signature.remember_every = signature.RememberEvery == 0 ? null : signature.RememberEvery;
                Signature.sign_mode = signature.SignMode ?? "";
                Signature.sign_position = signature.SignPosition ?? "";
                Signature.tries = signature.Tries ?? "";

                foreach (var item in signature.SignersList)
                {
                    if (!string.IsNullOrEmpty(item.SignatureEmail == "undefined" ? "" : item.SignatureEmail))
                    {
                        sig_signature_details signatures = new sig_signature_details();
                        signatures.name = item.SignatureName;
                        signatures.email = item.SignatureEmail;
                        signatures.rfc = "";
                        Signature.sig_signature_details.Add(signatures);
                    }
                }

                foreach (var item in signature.ViewersList)
                {
                    if (!string.IsNullOrEmpty(item.ViewerEmail == "undefined" ? "" : item.ViewerEmail))
                    {
                        sig_spectators_details spectators = new sig_spectators_details();
                        spectators.name = item.ViewerName;
                        spectators.email = item.ViewerEmail;
                        Signature.sig_spectators_details.Add(spectators);
                    }
                }

                dbContext.sig_signature.Add(Signature);
                dbContext.SaveChanges();

                Signature Signaturess = new Signature();
                Signaturess.Id = Signature.signature_id;
                return Signaturess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
