﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("api1", "My First API",new string[]
                {
                    JwtClaimTypes.Role,
                    JwtRegisteredClaimNames.UniqueName
                }){ Scopes={ "api1" } }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("api1", "My First API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("userrole",new string[]{ JwtClaimTypes.Role})
            };

        public static List<TestUser> Users =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "88",
                    Username = "alice",
                    Password = "alice",
                    Claims=new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, "alice"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {
                        "api1",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "userrole"
                    },

                    Claims=new List<ClientClaim>
                    {
                        new ClientClaim("role","lingdurole"),
                        new ClientClaim("group", "lingdugroup")
                    },

                    AlwaysSendClientClaims=true
                }
            };
    }
}