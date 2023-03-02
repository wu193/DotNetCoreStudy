﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            // discover endpoints from metadata
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                Console.ReadKey();
            }

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "alice",

                Scope = "api1 openid profile userrole"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                Console.ReadKey();
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("https://localhost:6001/test");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                Console.ReadKey();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

            var userInfo = await apiClient.GetUserInfoAsync(new UserInfoRequest
            {
                Token = tokenResponse.AccessToken,
                Address = disco.UserInfoEndpoint,
            });

            if (userInfo.IsError)
            {
                Console.WriteLine(userInfo.Error);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(userInfo.Json);
            }

            Console.ReadKey();
        }
    }
}