using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace Polykube.Frontend.Controllers
{
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        [HttpGet]
        public async Task<string> Index()
        {
            // Supported types of querying:
            // 1. ?id=   this references a specific identifier inside the source document
            // 2. ?

            // query name params MAP to Postgres indexes hopefully

            var client = new HttpClient();
            var resp = await client.GetAsync("http://goapi.default.kubernetes.local/");
            var content = await resp.Content.ReadAsStringAsync();
            return content;
        }
    }
}
