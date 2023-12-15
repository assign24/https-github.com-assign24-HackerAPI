using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Web.Business.Intrfaces;
using Web.ViewModels;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private readonly IHackerNewsStories hackerNewsStories;
        private readonly IConfiguration configuration;
        private IMemoryCache memoryCache;
        public HackerNewsController(IHackerNewsStories newsStories,IMemoryCache cache)
        {
            hackerNewsStories  = newsStories;
            memoryCache = cache;
         //   configuration = config;
        }
        /// <summary>
        /// this api method used to get new stories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getnewstories")]
        public async Task<IActionResult> GetShowStoriesItem()
        {
            //var result = await hackerNewsStories.GetShowStoriesItem();
            string key = "cacheddata";
            bool iscached = memoryCache.TryGetValue(key, out List<ShowStoryViewodel> result);
            if (!iscached)
            {
                result = await hackerNewsStories.GetShowStoriesItem();
                var options1 = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                };
                memoryCache.Set(key, result,options1);
            }
            if (result.Any())
            {
                return Ok(result);
            }
            return BadRequest();    
        }
    }
}
