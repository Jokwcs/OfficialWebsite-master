using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;

namespace Limit.OfficialSite.JwtBearer
{
    public class LimitTokenValidator : ISecurityTokenValidator
    {
        public bool CanValidateToken => throw new NotImplementedException();

        public int MaximumTokenSizeInBytes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CanReadToken(string securityToken)
        {
            return true;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            validatedToken = null;
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim("name", "test"));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, securityToken == "test.com" ? "admin" : "user"));
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
    }
}
