// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ECommerceIdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                 new ApiResource("resource_basket"){Scopes={"basket_fullpermission"}},
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       new IdentityResource{ Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new []{ "role"} }
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                 new ApiScope("basket_fullpermission","Basket API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "identityserver",
                    ClientName = "basket servis",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                       AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.LocalApi.ScopeName,
                            "roles",
                            "basket_fullpermission"
                        },
                },

                // interactive client using code flow + pkce
                new Client
                {
                     ClientName="Asp.Net Core MVC",
                    ClientId="test12",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "api1.read",
                        "CountryAndCity",
                        "Roles"
                    },
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                },
            };

        public static List<TestUser> TestUsers =>
          new List<TestUser>
      {
            new TestUser
            {
                SubjectId = "1",
                Username = "bayram",
                Password = "bayram",
                Claims = new List<Claim>()
                {
                    new Claim("given_name","Bayram"),
                    new Claim("family_name","EREN"),
                    new Claim("country","Turkiye"),
                    new Claim("city","Istanbul"),
                    new Claim("role","admin")
                }
            },
            new TestUser
            {
                SubjectId = "2",
                Username = "test",
                Password = "test",
                Claims = new List<Claim>()
                {
                    new Claim("given_name","Test"),
                    new Claim("family_name","Deneme"),
                    new Claim("country","Turkey"),
                    new Claim("city","Ankara"),
                    new Claim("role","customer")
                }
            }
      };
    }
}