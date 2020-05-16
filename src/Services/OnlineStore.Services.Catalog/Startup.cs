using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineStore.Services.Catalog.EventBusHandlers;
using OnlineStore.Services.Catalog.GrpcServices;
using OnlineStore.Services.Catalog.Models;
using OnlineStore.Services.Infrastructure.Extensions;
using OnlineStore.Services.Infrastructure.Interfaces;

namespace OnlineStore.Services.Catalog
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventBusHandler<CreateUserEvent>, CreateUserHandler>();
            services.AddTransient<IEventBusHandler<UpdateUserEvent>, UpdateUserHandler>();

            services.RegisterEventBusServices(_configuration);

            services.AddControllers();
            services.AddGrpc(options => options.EnableDetailedErrors = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGrpcService<CatalogService>();
            });

            ConfigureEventBus(app);
        }

        private static void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<IEventBusHandler<CreateUserEvent>, CreateUserEvent>();
            eventBus.Subscribe<IEventBusHandler<UpdateUserEvent>, UpdateUserEvent>();
        }
    }
}