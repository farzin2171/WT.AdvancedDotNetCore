using StackExchange.Redis;
using System.Threading.Tasks;

namespace WT.Project.AdvancedDotNetCore.Services
{
    public class RedisCacheService : ICacheService
    {
        private IConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _db;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _db = _connectionMultiplexer.GetDatabase();
        }
        public async Task<string> GetCacheValueAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        public async Task SetCacheValueAsync(string key, string value)
        {
            await _db.StringSetAsync(key, value);
        }
    }
}
