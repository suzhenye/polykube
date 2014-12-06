using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Polykube.vNextApi.Controllers
{
    [Route("api/[controller]")]
    public class PassthroughController : Controller
    {
        [HttpGet]
        public async Task<string> Index()
        {
            var client = new HttpClient();
            var resp = await client.GetAsync("http://goapi.default.kubernetes.local/");
            var content = await resp.Content.ReadAsStringAsync();
            return content;
        }
    }
}
