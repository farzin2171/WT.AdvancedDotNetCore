using Microsoft.Extensions.DependencyInjection;

namespace WT.Project.AdvancedDotNetCore.Infrastructure.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStartupTask<T>(this IServiceCollection services) where T : class, IStartupTask
            => services.AddTransient<IStartupTask, T>();


    }
}
