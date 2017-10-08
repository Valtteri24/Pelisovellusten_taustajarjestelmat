using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace API.Middleware
{
    public class AppSettings
    {
        public string apikey { get; set; }
    }
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public AuthenticationMiddleware(RequestDelegate next, IOptions<AppSettings> appsettings)
        {
            _next = next;
            _appSettings = appsettings.Value;
        }
        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers.Keys.Contains("apikey"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("User Key is missing");
                return;
            }
            else
            {
                if (_appSettings.apikey != context.Request.Headers["apikey"])
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync("Invalid User Key");
                    return;
                }
            }
            await _next.Invoke(context);



        }

    }
}