using Microsoft.Extensions.Hosting.WindowsServices;
using MyAPI.Extensions;
using Unity;
using UserApi;

namespace MyApi1
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var options = new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = Directory.GetCurrentDirectory()
            };
            var host = CreateHostBuilder(options).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(WebApplicationOptions applicationOptions)
        {
            var hostBuilder = Host.CreateDefaultBuilder(applicationOptions.Args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(applicationOptions.ContentRootPath);
                    webBuilder.UseKestrel((hostingContext, options) =>
                    {
                        var kestrelSection = hostingContext.Configuration.GetSection("Kestrel");
                        options.Configure(kestrelSection);
                    });
                    webBuilder.UseStartup<Startup>();
                }).UseUnity(new UnityContainer());

            return hostBuilder;
        }
    }
}
