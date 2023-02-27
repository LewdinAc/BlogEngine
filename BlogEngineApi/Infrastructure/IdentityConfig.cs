using IdentityServer4.Models;

namespace BlogEngineApi.Infrastructure
{
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> {
            new ApiScope("web", "Web scope", new List<string> { "rols", "userId" })
        };

        public static IEnumerable<Client> Clients => new List<Client> {
            new Client {
                ClientId = "webclient",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {
                    new Secret("testsecret".Sha256())
                },
                AllowedScopes = {
                    "web"
                },
            }
        };
    }
}