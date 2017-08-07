
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SpeechLuisOwin.Src.Static;
using System;

namespace SpeechLuisOwin.Src.Ext
{
    public static class Auth
    {
        public static void UseAuth(this IApplicationBuilder app)
        {
            app.UseJwtBearerAuthentication(
                new JwtBearerOptions
                {
                    // Audience = Configurations.aad_ClientId,
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    Authority = Configurations.aad_AuthUri,
                    Audience = Configurations.aad_Audience,
                });
        }
    }
}