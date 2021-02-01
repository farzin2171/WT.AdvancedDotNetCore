using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WT.Project.ProtoBufSample.Services;

namespace WT.Project.ProtoBufSample.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        [HttpGet("Json")]
        public async Task<IActionResult> Get()
        {
            
            return Ok(await _settingService.GetSettingsAsync(1000));
        }

        [HttpGet("ProtoBuff")]
        public async Task<IActionResult> GetProtoBuff()
        {
            return Ok(await _settingService.GetProtoBuffSettingsAsync(1000));
        }
    }
}
