using DigitalFirmaClone.Bsl.Model;
using DigitalFirmaClone.EF;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Bsl.Manager
{
    public class UserManager : IUserManager
    {
        private readonly ecommerceContext dbContext;
        public UserManager(ecommerceContext context)
        {
            this.dbContext = context;
        }

        public EmployeeModel GetById(int id)
        {
            try
            {
                var ut_user = dbContext.ut_user.Where(x => x.user_id == id).Select(x => new EmployeeModel()
                {
                    Id = x.user_id,
                    PassWord = x.password,
                    Email = x.email,
                    FullName = x.user_name,
                    Company = x.company,
                }).FirstOrDefault();

                return ut_user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeModel GetByEmail(string email)
        {
            try
            {
                var ut_user = dbContext.ut_user.Where(x => x.email.ToUpper() == email.ToUpper()).Select(x => new EmployeeModel()
                {
                    Id = x.user_id,
                    PassWord = x.password,
                    Email = x.email,
                    FullName = x.user_name,
                    Company = x.company,
                }).FirstOrDefault();
                return (ut_user ?? new EmployeeModel()).Id != 0 ? ut_user : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public EmployeeModel SignIn(string email, string password)
        {
            try
            {
                var ut_user = dbContext.ut_user.Where(x => x.email.ToUpper() == email.ToUpper() && x.password.ToUpper() == password.ToUpper()).Select(x => new EmployeeModel()
                {
                    Id = x.user_id,
                    PassWord = x.password,
                    Email = x.email,
                    FullName = x.user_name,
                    Company = x.company,

                }).FirstOrDefault();
                return (ut_user ?? new EmployeeModel()).Id != 0 ? ut_user : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void CreateAsync(AppUser appUser)
        {
            try
            {
                var ut_user = dbContext.ut_user.Where(x => x.email == appUser.Email && (string.IsNullOrEmpty(appUser.AuthenticationType) ? x.remarks != "GOOGLE" : x.remarks == "GOOGLE")).FirstOrDefault();
                if (ut_user == null)
                {

                    ut_user ut_User = new ut_user();
                    ut_User.user_name = appUser.Name ?? "";
                    var hashedProvidedPassword = Utils.Utils.GetMd5x2(appUser.PassWord);
                    ut_User.password = hashedProvidedPassword ?? "";
                    ut_User.show_password = appUser.PassWord ?? "";
                    ut_User.email = appUser.Email ?? "";
                    ut_User.remarks = appUser.AuthenticationType ?? "";
                    ut_User.company = appUser.Company ?? "";
                    ut_User.import_user_id = "";
                    ut_User.mobile = "";
                    dbContext.ut_user.Add(ut_User);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
