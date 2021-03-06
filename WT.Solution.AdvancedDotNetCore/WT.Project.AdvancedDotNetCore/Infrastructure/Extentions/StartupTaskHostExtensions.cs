﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WT.Project.AdvancedDotNetCore.Infrastructure.Extentions
{
    public static class StartupTaskHostExtensions
    {
        public static IHost RunTasks(this IHost host)
        {
            var startupTasks = host.Services.GetServices<IStartupTask>();
            foreach (var startupTask in startupTasks)
            {
                startupTask.Execute();
            }

            return host;
        }
    }
}
