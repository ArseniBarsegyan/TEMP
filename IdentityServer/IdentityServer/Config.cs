using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        //Defining the InMemory Clients
        public static IEnumerable<Client> GetClients()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ClientId = "Client1",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                }
            };
            return clients;
        }

        //Defining the InMemory API's
        public static IEnumerable<ApiResource> GetApiResources()
        {
            var apiResources = new List<ApiResource>
            {
                new ApiResource("api1", "First API")
            };
            return apiResources;
        }

        //Defining the InMemory User's
        public static List<TestUser> GetUsers()
        {
            List<TestUser> testUsers = new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "ars",
                    Password = "pass"
                }
            };

            return testUsers;
        }
    }
}