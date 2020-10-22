using System;
using Limit.OfficialSite.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Limit.OfficialSite.Authorization.Users
{
    public class LimitUser : IdentityUser<Guid>, IAggregateRoot<Guid>
    {

    }
}