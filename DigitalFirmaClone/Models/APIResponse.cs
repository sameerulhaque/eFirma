using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Models
{
    public class APIResponse<T> where T : class
    {

        public T data { get; set; }
    }

    public class CompanyData
    {
        public string type { get; set; }
        public string id { get; set; }
        public object attributes { get; set; }
        public object links { get; set; }
    }

    public class CreateCompany
    {
        public string name { get; set; }
        public byte[] logo { get; set; }
    }

    public class Company
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }


    


    

  



   

   


   

   

    
}
