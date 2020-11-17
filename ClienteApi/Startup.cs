using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestesHttpClient.ClienteApi.Domain;
using TestesHttpClient.ClienteApi.Infrastructure.Repositories;
using TestesHttpClient.ClienteApi.Infrastructure.SeedWork;

namespace TestesHttpClient.ClienteApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

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
                    Title = "TestesHttpClient - ClienteApi",
                    Description = "API usada para demonstrar o uso de SRP nos testes de unidade com HttpClient.",
                    Contact = new OpenApiContact
                    {
                        Name = "Andr� Okazaki",
                        Email = "andre@desenvolverideias.com"
                    }
                });
            });
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddSingleton<IMongoDbContext, ClienteDbContext>();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestesHttpClient ClienteApi V1");
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
