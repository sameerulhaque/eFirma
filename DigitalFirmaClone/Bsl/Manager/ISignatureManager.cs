﻿using DigitalFirmaClone.Models.ModelClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Bsl.Manager
{
    public interface ISignatureManager
    {
        public Signature GetSignatureById(Signature signature);
        public List<Signature> GetAllSignatures(int userId);
        public Signature AddSignature(Signature signature);
        public bool IsWidgetAuthenticated(string Email, string WidgetId);
    }
}
