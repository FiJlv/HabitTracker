﻿using IdentityModel;
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
                new ApiScope("HabitTrackerWebApi", "Web API")
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
                new ApiResource("HabitTrackerWebApI", "Web API", new []
                { JwtClaimTypes.Name })
                {
                    Scopes ={ "HabitTrackerWebApi" }
                }
          };

        public static IEnumerable<Client> Clients =>
         new List<Client>
         {
                new Client
                {
                    ClientId = "habittracker-web-api",
                    ClientName =  "HabitTracker Web",
                     AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http:/.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "HabitTrackerWebApI"
                    },
                    AllowAccessTokensViaBrowser = true
                }
         };
    }
}
