using System.Threading.Tasks;
using JakDojade.Infrastructure.Services.Node;
using Microsoft.AspNetCore.Mvc;

namespace JakDojade.Api.Controllers
{
    [Route("[controller]")]
    public class PathController : ApiControllerBase
    {
        private readonly INodeService _nodeService;

        public PathController(INodeService nodeService)
        {
            _nodeService = nodeService;
        }
        [HttpGet("Stops")]
        public async Task<IActionResult> GetBusStopAll()
        {
            var nodes = await _nodeService.BrowseAsync();

            return Json(nodes);
        }
        
    }
}