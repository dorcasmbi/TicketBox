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
            var clientId = "aea0edd8-3011-4c3a-b618-e1613b5d4240";
            var tenantId = "3baceaf5-19f8-44ae-a3d3-9dd826b144d8";
            var clientSecret = "b05rGrW9x.mX.5_3s.bC~04pOe2aCA445x";

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
