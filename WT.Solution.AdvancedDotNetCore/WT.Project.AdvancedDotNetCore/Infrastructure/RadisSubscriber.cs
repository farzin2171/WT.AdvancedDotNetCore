using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WT.Project.AdvancedDotNetCore.Infrastructure
{
    public class RadisSubscriber : BackgroundService
    {
        private IConnectionMultiplexer _connectionMultiplexer;

        public RadisSubscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            return subscriber.SubscribeAsync("messages", ((channel, value) => {
                Console.WriteLine($"The message content was:{value}");
            }));
        }
    }
}
