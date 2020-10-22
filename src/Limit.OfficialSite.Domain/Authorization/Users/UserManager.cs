using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Limit.OfficialSite.Authorization.Users
{
    public class UserManager : UserManager<LimitUser>
    {
        public UserManager(
            IUserStore<LimitUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<LimitUser> passwordHasher,
            IEnumerable<IUserValidator<LimitUser>> userValidators,
            IEnumerable<IPasswordValidator<LimitUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<LimitUser>> logger)
            : base(
                store,
                optionsAccessor,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errors,
                services,
                logger)
        {

        }
    }
}