using System;
using Limit.OfficialSite.Authorization.Roles;
using Limit.OfficialSite.Authorization.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Limit.OfficialSite.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<LimitUser, LimitRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

    


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 移除自动建表时自动加上s的复数形式
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                builder.Entity(entityType.ClrType).ToTable(entityType.ClrType.Name);
            }
        }

    }
}