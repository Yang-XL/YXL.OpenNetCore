using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Utility.Extensions;
using IService;
using LoggerExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PermissionSystem.Models;

namespace Service.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<User>
        , IUserPhoneNumberStore<User>,
        IUserEmailStore<User>
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private bool _disposed;

        public ApplicationUserStore(IUserService userService, ILogger<ApplicationUserStore> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Email = email;
            return TaskCache.CompletedTask;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.EmailConfirmed = confirmed;
            return TaskCache.CompletedTask;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            normalizedEmail = normalizedEmail.ToUpper();
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var user = await _userService.SingleAsync(a => a.NormalizedEmail == normalizedEmail, cancellationToken);
            return user;
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.NormalizedEmail = normalizedEmail;
            return TaskCache.CompletedTask;
        }

        /// <summary>
        ///     Dispose the store
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.ID.ToString());
        }

        public Task<string> GetUserNameAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.ID.ToString());
        }

        public Task SetUserNameAsync(User user, string userName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Name = userName;
            return TaskCache.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.LoginName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.LoginName = normalizedName;
            return TaskCache.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user = await _userService.InsertAsync(user, cancellationToken: cancellationToken);
            if (user == null) return IdentityResult.Failed();
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.Debug(JsonConvert.SerializeObject(user), "更新用户");
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var result = await _userService.UpdateAsync(user, cancellationToken: cancellationToken);
            return result == 1 ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            _logger.Debug(JsonConvert.SerializeObject(user), "删除用户");
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            var result = await _userService.DeleteAsync(user.ID, cancellationToken: cancellationToken);
            return result > 0 ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<User> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!Guid.TryParse(userId, out Guid id))
                throw new ArgumentException(nameof(userId));
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await _userService.SingleAsync(id, cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            var user = await _userService.SingleAsync(a => a.LoginName == normalizedUserName, cancellationToken);
            return user;
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.Password = passwordHash;
            return TaskCache.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.Password != null);
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(GetType().Name);
        }

        #region IUserPhoneNumberStore

        public Task SetPhoneNumberAsync(User user, string phoneNumber, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.MobilePhone = phoneNumber;
            return TaskCache.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.MobilePhone);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return Task.FromResult(user.MobilePhoneConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            user.MobilePhoneConfirmed = confirmed;
            return TaskCache.CompletedTask;
        }

        #endregion
    }
}