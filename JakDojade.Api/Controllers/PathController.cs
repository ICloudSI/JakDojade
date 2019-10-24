using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Dto;
using JakDojade.Infrastructure.Services;
using JakDojade.Infrastructure.Services.Node;
using Microsoft.AspNetCore.Mvc;

namespace JakDojade.Api.Controllers
{
    [Route("[controller]")]
    public class PathController : ApiControllerBase
    {
        private readonly INodeService _nodeService;
        private readonly IGraphService _graphService;

        public PathController(INodeService nodeService, IGraphService graphService)
        {
            _nodeService = nodeService;
            _graphService = graphService;
        }
        [HttpGet("Stops")]
        public async Task<IActionResult> GetBusStopAll()
        {
            var nodes = await _nodeService.BrowseAsync();

            return Json(nodes);
        }
        [HttpGet]
        public async Task<IActionResult> GetPath([FromBody]PathCommand command)
        {
            
            var nodes = await _graphService.GetPath(await _nodeService.GetIdAsync(command.Source), 
                await _nodeService.GetIdAsync(command.Target));

            PathDto pathDto = new PathDto();
            List<string> listNodeName = new List<string>();
            foreach(var node in nodes.Path)
            {
                var nameNode = await _nodeService.GetNameAsync(node); 
                listNodeName.Add(nameNode);
            }
            pathDto.Path = listNodeName;
            pathDto.Distance = nodes.Distance;

            return Json(pathDto);

        }

        
    }
}