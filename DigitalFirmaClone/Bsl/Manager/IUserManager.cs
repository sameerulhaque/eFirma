using DigitalFirmaClone.Bsl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Bsl.Manager
{
   public interface IUserManager
    {
        EmployeeModel GetById(int id);
        EmployeeModel GetByEmail(string email);
        EmployeeModel SignIn(string email, string password);
        void CreateAsync(AppUser AppUser);
    }
}
