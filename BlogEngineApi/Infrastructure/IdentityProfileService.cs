using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;

namespace BlogEngineApi.Infrastructure
{
    public class IdentityProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claim = new Claim("test", "test");
            context.IssuedClaims.Add(claim);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
    }
}
