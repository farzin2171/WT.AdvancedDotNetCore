using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using WT.Project.ProtoBufSample.Models;
using WT.Project.ProtoBufSample.Services;
using System.Text.Json;
using ProtoBuf;
using System.IO;

namespace WT.ProjeectBenchmark
{
  
    public class SerializationBenchMark
    {
        private static readonly SettingService settingService = new SettingService();

        [Benchmark]
        public async Task<List<Setting>> JsonSerializable() =>
            JsonSerializer.Deserialize<List<Setting>>(JsonSerializer.Serialize(await settingService.GetSettingsAsync(1000)));

        [Benchmark]
        public async Task<List<SettingProtoBuff>> ProtoBuSerializable() =>
            Serializer.Deserialize<List<SettingProtoBuff>>(ProtoSerilize(await settingService.GetProtoBuffSettingsAsync(1000)));


        private static Stream ProtoSerilize<T>(T record) where T:class
        {
            var stream = new MemoryStream();
            Serializer.Serialize(stream, record);
            return stream;
        }


    }
}
