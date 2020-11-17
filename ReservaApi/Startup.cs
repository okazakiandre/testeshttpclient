using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;
using TestesHttpClient.ReservaApi.Configurations;
using TestesHttpClient.ReservaApi.Domain;
using TestesHttpClient.ReservaApi.Infrastructure.ExternalServices.ClienteApi;
using TestesHttpClient.ReservaApi.Infrastructure.Repositories;
using TestesHttpClient.ReservaApi.Infrastructure.SeedWork;

namespace TestesHttpClient.ReservaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ExternalEdps = new ExternalEndpoints(configuration);
        }

        public IConfiguration Configuration { get; }
        public IExternalEndpoints ExternalEdps { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.AllowTrailingCommas = true;
                });
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "TestesHttpClient - ReservaApi",
                    Description = "API usada para demonstrar o uso de SRP nos testes de unidade com HttpClient.",
                    Contact = new OpenApiContact
                    {
                        Name = "André Okazaki",
                        Email = "andre@desenvolverideias.com"
                    }
                });
            });
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddSingleton<IMongoDbContext, ReservaDbContext>();

            services.AddSingleton(ExternalEdps);
            services.AddHttpClient<IClienteApiClient, ClienteApiClient>(cli => ConfigurarHttpClient(cli, "clienteApi"));
        }

        private void ConfigurarHttpClient(HttpClient cli, string nomeEndpoint)
        {
            var edp = ExternalEdps.GetItem(nomeEndpoint);
            cli.BaseAddress = new Uri(edp.Url);
            cli.Timeout = TimeSpan.FromMilliseconds(edp.Timeout);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestesHttpClient ReservaApi V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
