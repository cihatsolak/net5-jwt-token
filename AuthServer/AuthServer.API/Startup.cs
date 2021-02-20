using AuthServer.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AuthServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Bu y�ntem �al��ma zaman� taraf�ndan �a�r�l�r. Kapsay�c�ya hizmet eklemek i�in bu y�ntemi kullan�n.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSettingsConfiguration(Configuration);
            services.AddServicesConfiguration(Configuration);
            services.AddAuthenticationConfiguration(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthServer.API", Version = "v1" });
            });
        }

        // Bu y�ntem �al��ma zaman� taraf�ndan �a�r�l�r. HTTP istek ard���k d�zenini yap�land�rmak i�in bu y�ntemi kullan�n.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthServer.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
