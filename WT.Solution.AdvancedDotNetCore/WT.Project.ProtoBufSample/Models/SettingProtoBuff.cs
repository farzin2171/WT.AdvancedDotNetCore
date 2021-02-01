using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WT.Project.ProtoBufSample.Models
{
    [ProtoContract]
    public class SettingProtoBuff
    {
        [ProtoMember(1)]
        public Guid Id { get; set; }
        [ProtoMember(2)]
        public string Name { get; set; }
        [ProtoMember(3)]
        public int Version { get; set; }
    }
}
