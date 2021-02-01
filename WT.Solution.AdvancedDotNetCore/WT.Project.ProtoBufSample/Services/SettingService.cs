using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WT.Project.ProtoBufSample.Models;
using System.Text.Json;
using ProtoBuf;
using System.IO;
using System.Text;

namespace WT.Project.ProtoBufSample.Services
{
    public  class SettingService:ISettingService
    {
        

        public async Task<IEnumerable<SettingProtoBuff>> GetProtoBuffSettingsAsync(int limit)
        {
           
            var setings = GenerateProtoBufSettings(limit);
            return setings;
        }

        public async Task<IEnumerable<Setting>> GetSettingsAsync(int limit)
        {
            var setings = GenerateSettings(limit);
            return setings;
        }

        private IEnumerable<SettingProtoBuff> GenerateProtoBufSettings(int limit)
        {
            List<SettingProtoBuff> settings = new List<SettingProtoBuff>();
            for (int i = 0; i < limit; i++)
            {
                settings.Add(new SettingProtoBuff
                {
                    Id = Guid.NewGuid(),
                    Name = $"Setting_{i}",
                    Version = i
                });
            }
            return settings;
        }
        private IEnumerable<Setting> GenerateSettings(int limit)
        {
            List<Setting> settings = new List<Setting>();
            for (int i = 0; i < limit; i++)
            {
                settings.Add(new Setting
                {
                    Id = Guid.NewGuid(),
                    Name = $"Setting_{i}",
                    Version = i
                });
            }
            return settings;
        }
    }
}
