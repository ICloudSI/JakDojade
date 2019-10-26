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

        /// <summary>
        /// Returns list of stops in Solvro City
        /// </summary>
        /// <remarks>
        /// Returns list of stops in Solvro City
        /// </remarks>
        /// <returns>Returns list of stops in Solvro City</returns>
        /// <response code="200">Returns list of stops in Solvro City</response>

        [HttpGet("Stops")]
        [ProducesResponseType(typeof(IEnumerable<NodeDto>),200)]
        public async Task<IActionResult> GetBusStopAll()
        {
            var nodes = await _nodeService.BrowseAsync();

            return Json(nodes);
        }

        /// <summary>
        /// Returns list of stops in path and total distance.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /PathCommand
        ///     {
        ///        "Source": "Przystanek Zdenerwowany frontend developer",
        ///        "Target": "Przystanek Dziki jamnik"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Returns list of stops in path and total distance.</returns>
        /// <response code="200">Returns list of stops in path and total distance.</response>    
        /// <response code="500">Return message with error.</response>
        [HttpPost]
        [ProducesResponseType(typeof(PathDto),200)]
        public async Task<IActionResult> GetPath([FromBody]PathCommand command)
        {

            var nodes = await _graphService.GetPath(await _nodeService.GetIdAsync(command.Source),
                await _nodeService.GetIdAsync(command.Target));

            PathDto pathDto = new PathDto();
            List<string> listNodeName = new List<string>();
            foreach (var node in nodes.Path)
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