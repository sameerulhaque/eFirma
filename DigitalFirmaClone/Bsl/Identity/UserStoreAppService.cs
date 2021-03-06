using DigitalFirmaClone.Bsl.Manager;
using DigitalFirmaClone.Bsl.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DigitalFirmaClone.Bsl.Identity
{
    public class UserStoreAppService : IUserEmailStore<AppUser>, IUserPasswordStore<AppUser>, IUserLoginStore<AppUser>, IDisposable
    {
        private IUserManager _userManager;
        public UserStoreAppService(IUserManager employeeDal)
        {
            _userManager = employeeDal;
        }

        public Task AddLoginAsync(AppUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            //_userManager.CreateAsync(user);
            return null;
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           
        }

        public async Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));
            var employee = _userManager.GetByEmail(normalizedEmail);
            if (employee != null)
            {
                var appUser = new AppUser()
                {
                    Id = employee.Id,
                    PassWord = employee.PassWord,
                    Email = employee.Email,
                    FullName = employee.FullName,
                    Avatar = employee.Avatar
                };
                return appUser;
            }
            return null;
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userId))
                userId = userId.ToLower();
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null) throw new ArgumentNullException(nameof(userId));
            var employee = _userManager.GetById(Convert.ToInt32(userId));
            if (employee != null)
            {
                var appUser = new AppUser()
                {
                    Id = employee.Id,
                    PassWord = employee.PassWord,
                    Email = employee.Email,
                    FullName = employee.FullName,
                    Avatar = employee.Avatar
                };
                return appUser;
            }
            return null;
        }

        public async Task<AppUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(loginProvider) && !string.IsNullOrEmpty(providerKey))
            {
                loginProvider = loginProvider.ToUpper();
                providerKey = providerKey.ToUpper();
            }
            cancellationToken.ThrowIfCancellationRequested();
            if (loginProvider == null) throw new ArgumentNullException(nameof(loginProvider));
            if (providerKey == null) throw new ArgumentNullException(nameof(providerKey));
            var employee = _userManager.SignIn(loginProvider,providerKey);
            if (employee != null)
            {
                var appUser = new AppUser()
                {
                    Id = employee.Id,
                    PassWord = employee.PassWord,
                    Email = employee.Email,
                    FullName = employee.FullName,
                    Avatar = employee.Avatar
                };
                return appUser;
            }
            return null;
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var employee = _userManager.GetByEmail(normalizedUserName.ToLower());
            if (employee != null)
            {
                var appUser = new AppUser()
                {
                    Id = employee.Id,
                    PassWord = employee.PassWord,
                    Email = employee.Email,
                    FullName = employee.FullName,
                    Avatar = employee.Avatar
                };
                return appUser;
            }
            return new AppUser();
            return null;
        }

        public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.PassWord);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(AppUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
