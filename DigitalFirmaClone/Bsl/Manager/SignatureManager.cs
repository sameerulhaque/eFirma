using DigitalFirmaClone.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigitalFirmaClone.Bsl.Model;
using DigitalFirmaClone.Models.ModelClasses;
using DigitalFirmaClone.Models.APIClasses;
using Microsoft.EntityFrameworkCore;

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

                var sig_signature_IsExist = dbContext.sig_signature
                    .Where(x => x.signature_id == signature.Id)
                    .Include(x => x.sig_signature_details)
                    .Include(x => x.sig_spectators_details)
                    .FirstOrDefault();

                Signature.Id = sig_signature_IsExist.signature_id;
                Signature.DocumentName = sig_signature_IsExist.document_name;

                foreach (var item in sig_signature_IsExist.sig_signature_details)
                {
                    Signers signatures = new Signers();
                    signatures.SignatureName = item.name;
                    signatures.SignatureEmail = item.email;
                    signatures.RFC = "";
                    signatures.WidgetId = item.widget_id;
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

        public List<Signature> GetAllSignatures(int userId)
        {
            try
            {
                List<Signature> SignatureList = new List<Signature>();

                var sig_signature = dbContext.sig_signature.Where(x => x.user_id == userId).ToList();

                foreach (var item in sig_signature)
                {
                    Signature Signature = new Signature();

                    Signature.Id = item.signature_id;
                    Signature.DocumentName = item.document_name;
                    Signature.SignStatus = "UnSigned";
                    Signature.MifielId = item.mifiel_id;
                    Signature.CreateDateString = DateTime.Now.ToString("dd/MM/yyyy");

                    foreach (var x in item.sig_signature_details)
                    {
                        Signers signatures = new Signers();
                        signatures.SignatureName = x.name;
                        signatures.SignatureEmail = x.email;
                        signatures.RFC = "";
                        signatures.WidgetId = x.widget_id;
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

                Signature.document_name = signature.FileName ?? "";
                Signature.mifiel_id = signature.MifielId;
                Signature.user_id = signature.UserId;

                dbContext.sig_signature.Add(Signature);
                dbContext.SaveChanges();

                Signature Signaturess = new Signature();
                Signaturess.Id = Signature.signature_id;

                foreach (var item in signature.SignersList)
                {
                    if (!string.IsNullOrEmpty(item.SignatureEmail == "undefined" ? "" : item.SignatureEmail))
                    {
                        sig_signature_details signatures = new sig_signature_details();
                        signatures.name = item.SignatureName;
                        signatures.email = item.SignatureEmail;
                        signatures.rfc = "";
                        signatures.signature_id = Signaturess.Id;
                        signatures.widget_id = item.WidgetId;
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
                        spectators.signature_id = Signaturess.Id;
                        Signature.sig_spectators_details.Add(spectators);
                    }
                }

                dbContext.SaveChanges();
                Signaturess.Id = Signature.signature_id;
                return Signaturess;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool IsWidgetAuthenticated(string Email, string WidgetId)
        {
            var IsAuthenticated = dbContext.sig_signature_details.Where(x => x.widget_id == WidgetId && x.email == Email).FirstOrDefault();
            if(IsAuthenticated != null)
            {
                return true;
            }
            return false;
        }
    }
}
