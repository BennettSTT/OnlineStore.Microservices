using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OnlineStore.Services.Catalog
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseKestrel(options =>
                        {
                            /*
                             
                            options.ConfigureHttpsDefaults(adapterOptions =>
                            {
                                adapterOptions.ServerCertificate = new X509Certificate2("aspnetapp.pfx", "123");

                            });
                    
                            OR
                             
                            options.Listen(IPAddress.Loopback, 5200, listenOptions =>
                            {
                                listenOptions.UseHttps("aspnetapp.pfx", "123");
                            });
                            
                            */
                        })
                        .UseStartup<Startup>();
                });
    }
}