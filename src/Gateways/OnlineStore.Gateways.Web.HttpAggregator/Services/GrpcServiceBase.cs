using Grpc.Net.Client;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace OnlineStore.Gateways.Web.HttpAggregator.Services
{
    internal abstract class GrpcServiceBase
    {
        private const string Http2UnencryptedSupport = "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport";
        private const string Http2SocketsHttpHandlerSupport = "System.Net.Http.SocketsHttpHandler.Http2Support";
        
        private readonly ILoggerFactory _loggerFactory;

        protected GrpcServiceBase(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        protected async Task<TResponse> CallService<TResponse>(
            [NotNull] string url, 
            [NotNull] Func<GrpcChannel, Task<TResponse>> actionAsync)
        {
            AppContext.SetSwitch(Http2UnencryptedSupport, true);
            AppContext.SetSwitch(Http2SocketsHttpHandlerSupport, true);

            var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { LoggerFactory = _loggerFactory });

            try
            {
                using (channel)
                {
                    return await actionAsync(channel);
                }
            }
            finally
            {
                AppContext.SetSwitch(Http2UnencryptedSupport, false);
                AppContext.SetSwitch(Http2SocketsHttpHandlerSupport, false);
            }
        }

        protected async Task CallService(
            [NotNull] string url, 
            [NotNull] Func<GrpcChannel, Task> func)
        {
            AppContext.SetSwitch(Http2UnencryptedSupport, true);
            AppContext.SetSwitch(Http2SocketsHttpHandlerSupport, true);

            var channel = GrpcChannel.ForAddress(url);

            try
            {
                await func(channel);
            }
            finally
            {
                AppContext.SetSwitch(Http2UnencryptedSupport, false);
                AppContext.SetSwitch(Http2SocketsHttpHandlerSupport, false);
            }
        }
    }
}