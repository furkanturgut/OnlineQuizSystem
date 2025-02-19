using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace OnlineQuizSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class testcontroller : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public testcontroller(IDistributedCache cache)
        {
            this._cache = cache;
        }

        [HttpGet("set")]
        public IActionResult SetCache()
        {
            _cache.SetString("TestKey", "Hello Redis", new DistributedCacheEntryOptions
            { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return Ok("Veri Redise Kaydedildi");
        }

        [HttpGet("get")]
        public IActionResult GetCache()
        {
            var key = _cache.GetString("TestKey");
            if (string.IsNullOrEmpty(key))
            {
                return NotFound("Veri Bulunamadi");
            }
            return Ok(key);
        }

        [HttpGet("Delete")]
        public IActionResult DeleteCache()
        {
            _cache.Remove("TestKey");
            return Ok("Veri silindi");
        }
   }
}
