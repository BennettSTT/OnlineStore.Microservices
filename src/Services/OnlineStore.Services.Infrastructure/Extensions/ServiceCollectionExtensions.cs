using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Services.Infrastructure.Implementation;
using OnlineStore.Services.Infrastructure.Interfaces;
using RabbitMQ.Client;
using System;

namespace OnlineStore.Services.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterEventBusServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IEventBusConnectionManager>(provider =>
            {
                var eventBusConfig = configuration.GetSection("EventBus");

                var factory = new ConnectionFactory
                {
                    HostName = Convert.ToString(eventBusConfig["HostName"]),
                    /*
                    UserName = Convert.ToString(eventBusConfig["UserName"]),
                    Password = Convert.ToString(eventBusConfig["Password"]),
                    AutomaticRecoveryEnabled = Convert.ToBoolean(eventBusConfig["AutomaticRecoveryEnabled"]),
                    */
                    DispatchConsumersAsync = Convert.ToBoolean(eventBusConfig["DispatchConsumersAsync"])
                };

                return new EventBusConnectionManager(factory);
            });

            services.AddSingleton<IEventBus>(provider =>
            {
                var connectionManager = provider.GetService<IEventBusConnectionManager>();

                return new EventBus(connectionManager);
            });

            return services;
        }
    }
}