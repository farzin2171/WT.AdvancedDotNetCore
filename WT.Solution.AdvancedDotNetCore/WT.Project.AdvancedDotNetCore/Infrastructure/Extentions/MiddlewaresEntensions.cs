using WT.Project.AdvancedDotNetCore.Infrastructure.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
    public static class MiddlewaresEntensions
    {
        public static IApplicationBuilder UseNameRouting(this IApplicationBuilder app)
        {
            return app.UseMiddleware<NameRoutingMiddleware>();
        }
    }
}
