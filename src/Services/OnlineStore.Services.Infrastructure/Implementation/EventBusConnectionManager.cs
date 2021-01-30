using OnlineStore.Services.Infrastructure.Interfaces;
using RabbitMQ.Client;
using System;

namespace OnlineStore.Services.Infrastructure.Implementation
{
    public class EventBusConnectionManager : IEventBusConnectionManager
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly object sync_root = new object();

        private IConnection _connection;
        private bool _disposed;

        public EventBusConnectionManager(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateChannel()
        {
            if (IsConnected || TryConnect())
            {
                return _connection.CreateModel();
            }

            throw new InvalidOperationException("No event bus connections are available to perform this action.");
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            _connection.Dispose();
        }

        private bool TryConnect()
        {
            lock (sync_root)
            {
                try
                {
                    _connection = _connectionFactory.CreateConnection();

                    return true;
                }
                catch (Exception)
                {
                    // TODO: log error
                    return false;
                }
            }
        }
    }
}