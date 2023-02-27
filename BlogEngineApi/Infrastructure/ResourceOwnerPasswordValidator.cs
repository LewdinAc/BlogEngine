using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace BlogEngineApi.Infrastructure
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = TestUsers.Users
                .Where(u => u.Username == context.UserName &&
                       u.Password == context.Password)
                .FirstOrDefault();

            if (user == null)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidRequest,
                    "Invalid username and password");

                return Task.CompletedTask;
            }

            context.Result = new GrantValidationResult(
                    subject: user.SubjectId,
                    authenticationMethod: "password",
                    claims: user.Claims);

            return Task.FromResult(context.Result);
        }
    }
}
