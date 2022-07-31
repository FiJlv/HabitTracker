using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker.Identity
{
    public static class Configuration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("HabitTrackerWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
           new List<IdentityResource>
           {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new List<ApiResource>
          {
                new ApiResource("HabitTrackerWebAPI", "Web API", new []
                { JwtClaimTypes.Name })
                {
                    Scopes ={ "HabitTrackerWebAPI" }
                }
          };

        public static IEnumerable<Client> Clients =>
         new List<Client>
         {
                new Client
                {
                    ClientId = "habittracker-web-app",
                    ClientName =  "HabitTracker Web",
                     AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://localhost:3000/signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:3000" 
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:3000/signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "HabitTrackerWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
         };
    }
}
