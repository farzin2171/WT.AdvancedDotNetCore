using System.Collections.Generic;
using System.Threading.Tasks;
using WT.Project.ProtoBufSample.Models;

namespace WT.Project.ProtoBufSample.Services
{
    public interface ISettingService
    {
        Task<IEnumerable<Setting>> GetSettingsAsync(int limit);
        Task<IEnumerable<SettingProtoBuff>> GetProtoBuffSettingsAsync(int limit);
    }
}
