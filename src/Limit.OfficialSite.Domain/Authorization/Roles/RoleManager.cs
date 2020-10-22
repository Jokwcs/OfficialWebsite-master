using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Limit.OfficialSite.Authorization.Roles
{
    public class RoleManager : RoleManager<LimitRole>
    {
        public RoleManager(
            IRoleStore<LimitRole> store,
            IEnumerable<IRoleValidator<LimitRole>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<RoleManager<LimitRole>> logger)
            : base(
                store,
                roleValidators,
                keyNormalizer,
                errors,
                logger)
        {

        }
    }
}