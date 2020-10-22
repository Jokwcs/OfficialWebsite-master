using System;
using Limit.OfficialSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Limit.OfficialSite.Authorization.Roles
{
    public class LimitRole : IdentityRole<Guid>, IAggregateRoot<Guid>
    {

    }
}