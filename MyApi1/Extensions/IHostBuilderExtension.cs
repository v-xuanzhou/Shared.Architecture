using MyAPI.Common;
using Unity;
using Unity.Microsoft.DependencyInjection;

namespace MyAPI.Extensions
{
    public static class IHostBuilderExtension
    {
        public static IHostBuilder UseUnity(this IHostBuilder hostBuilder, IUnityContainer unityContainer)
        {
            hostBuilder.UseUnityServiceProvider(unityContainer);
            new DependencyResolver(unityContainer);
            return hostBuilder;
        }
    }
}
