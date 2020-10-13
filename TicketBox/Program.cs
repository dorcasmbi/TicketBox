using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBox
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                getUserAsync().GetAwaiter().GetResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async static Task getUserAsync()
        {
            var clientId = "";
            var tenantId = "";
            var clientSecret = "";

            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(clientId)
                .WithTenantId(tenantId)
                .WithClientSecret(clientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider);

            var groups = await graphClient.Groups.Request().Select(x => new { x.Id, x.DisplayName, x.CreatedDateTime}).GetAsync();

            foreach (var group in groups)
            {
                Console.WriteLine($"{group.DisplayName}, {group.Id}, {group.CreatedDateTime}");
            }
        }
    }
}
