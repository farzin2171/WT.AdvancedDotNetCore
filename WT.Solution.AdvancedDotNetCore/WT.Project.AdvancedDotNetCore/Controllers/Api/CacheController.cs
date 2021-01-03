using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WT.Project.AdvancedDotNetCore.Models;
using WT.Project.AdvancedDotNetCore.Services;

namespace WT.Project.AdvancedDotNetCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CacheController : ControllerBase
    {
        private ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get([FromRoute] string key)
        {
            if (string.IsNullOrEmpty(key))
                return NotFound();

            var value = await _cacheService.GetCacheValueAsync(key);
            return string.IsNullOrEmpty(value) ? (IActionResult)NotFound() : Ok(value); 
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewCacheEntryRequest request)
        {
            await _cacheService.SetCacheValueAsync(request.Key, request.Value);
            return Ok();
        }
    }
}