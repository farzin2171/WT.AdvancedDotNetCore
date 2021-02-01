using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WT.Project.ProtoBufSample.Extentions;
using WT.Project.ProtoBufSample.Models;

namespace WT.Project.ProtoBufSample.Services
{
    public class SettingServiceCashed : ISettingService
    {
        private readonly ISettingService _settingService;
        private readonly ICacheService _cacheService;

        public SettingServiceCashed(ISettingService settingService, ICacheService cacheService)
        {
            _settingService = settingService;
            _cacheService = cacheService;
        }
        public  async Task<IEnumerable<SettingProtoBuff>> GetProtoBuffSettingsAsync(int limit)
        {
            var cahcedValue = await _cacheService.GetCacheValueAsync($"WT_ProtoBuff_{limit}");
            if (!string.IsNullOrEmpty(cahcedValue))
            {
                return cahcedValue.DeserializeFromStringToProtoBuff<IEnumerable<SettingProtoBuff>>();
            }

            var setings =await _settingService.GetProtoBuffSettingsAsync(limit);
            await _cacheService.SetCacheValueAsync($"WT_ProtoBuff_{limit}",setings.SerializeToString_PB<IEnumerable<SettingProtoBuff>>());
            return setings;
        }

        public async Task<IEnumerable<Setting>> GetSettingsAsync(int limit)
        {
            var cahcedValue = await _cacheService.GetCacheValueAsync($"WT_Json_{limit}");
            if (!string.IsNullOrEmpty(cahcedValue))
            {
                return JsonSerializer.Deserialize<IEnumerable<Setting>>(cahcedValue);
            }
            var setings = await _settingService.GetSettingsAsync(limit);
            await _cacheService.SetCacheValueAsync($"WT_Json_{limit}", JsonSerializer.Serialize(setings));
            return setings;
        }

        private static byte[] ProtoSerializer<T>(T record) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, record);
            return stream.ToArray();
        }

      
    }
}
