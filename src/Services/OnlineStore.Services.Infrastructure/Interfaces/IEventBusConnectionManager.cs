using RabbitMQ.Client;
using System;

namespace OnlineStore.Services.Infrastructure.Interfaces
{
    public interface IEventBusConnectionManager : IDisposable
    {
        bool IsConnected { get; }

        IModel CreateChannel();
    }
}