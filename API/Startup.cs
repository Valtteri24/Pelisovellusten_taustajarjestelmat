using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using gameapi.mongodb;
using gameapi.Processors;
using gameapi.Repositories;
using API.Middleware;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{System.IO.Directory.GetCurrentDirectory()}.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
           // Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Add our own services, AddSingleton means that we have just one instance of the service in the whole application
            services.AddSingleton<MongoDBClient>();
            services.AddSingleton<PlayersProcessor>();
            services.AddSingleton<ItemsProcessor>();
            services.AddSingleton<IRepository, MongoDbRepository>();
            var appSettings = Configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(appSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMvc();



        }
    }
}
