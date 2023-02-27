using IdentityServer4.Test;
using System.Security.Claims;

namespace BlogEngineApi.Infrastructure
{
    public static class TestUsers
    {
        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "jhon",
                Password = "password",
                Claims = new[]
                {
                    new Claim("userId", "1"),
                    new Claim("rols", "reader")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "bob",
                Password = "password",
                Claims = new[]
                {
                    new Claim("userId", "2"),
                    new Claim("rols", "reader"),
                    new Claim("rols", "writer")
                }
            },
            new TestUser
            {
                SubjectId = "3",
                Username = "alan",
                Password = "password",
                Claims = new []
                {
                    new Claim("userId", "3"),
                    new Claim("rols", "reader"),
                    new Claim("rols", "editor"),
                }
            }
        };

        public static TestUser? GetUserById(string userId) =>
            Users.Find(u => u.SubjectId == userId);
    }
}