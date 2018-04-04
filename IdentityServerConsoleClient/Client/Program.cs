using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            //Discover the identity server
            var identityServer = await DiscoveryClient.GetAsync("http://localhost:49631/");

            if (identityServer.IsError)
            {
                Console.WriteLine(identityServer.Error);
                return;
            }

            //Get the token
            var tokenClient = new TokenClient(identityServer.TokenEndpoint, "Client1", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("ars", "pass", "api1");

            //Call the API
            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:50523/api/values");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            Console.ReadKey();
        }
    }
}
