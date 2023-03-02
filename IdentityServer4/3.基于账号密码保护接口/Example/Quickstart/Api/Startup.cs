// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap["sub"] = "userid";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "api1";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //RoleClaimType = "role",
                        //NameClaimType = "unique_name"
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("myPolicy", policy => policy.RequireRole("admin").RequireClaim("client_group", "lingdugroup"));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}