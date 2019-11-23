using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Dto;
using JakDojade.Infrastructure.Services;
using JakDojade.Infrastructure.Services.Node;
using JakDojade.Infrastructure.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JakDojade.Api.Controllers
{
    [Route("user/[controller]")]
    [Authorize(Policy = "HasAdminRole")]
    public class AdminController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly INodeService _nodeService;
        private readonly IGraphService _graphService;
        public AdminController(IUserService userService, INodeService nodeService, IGraphService graphService)
        {
            _userService = userService;
            _nodeService = nodeService;
            _graphService = graphService;
        }

        /// <summary>
        /// Returns list of users.
        /// </summary>
        /// <returns>Returns list of users</returns>
        /// <response code="200">Returns list of users</response>    
        /// <response code="500">Return message with error.</response>

        [HttpGet("Browse")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }
        [HttpGet("GetGraph")]
        public async Task<IActionResult> GetGraph()
        {
            var graph = await _graphService.GetAsync();

            return Json(graph);
        }

        /// <summary>
        /// Load data from JSON file solvroCity.json
        /// </summary>
        /// <remarks>
        /// For now, it is loading data from a file solvroCity.json located in the folder JakDojade.Api. <br/>Loads data from links and nodes variables, the rest are not used.
        /// <br/>Sample request:
        ///
        ///     POST /InputCommand
        ///     {
        ///             "directed" : false, 
        ///             "graph" : {}, 
        ///             "links" : [
        ///             {
        ///                 "distance": 6,
        ///                 "source": 0,
        ///                 "target": 2
        ///             },
        ///             {
        ///                 "distance": 8,
        ///                 "source": 0,
        ///                 "target": 13
        ///             }
        ///         ],
        ///             "multigraph" : false,
        ///             "nodes": [ {
        ///                 "id": 0,
        ///                 "stop_name": "Przystanek Zdenerwowany kabanos"
        ///                 },
        ///                 {
        ///                 "id": 1,
        ///                 "stop_name": "Przystanek Zdenerwowany frontend developer"
        ///                 }
        ///         ]
        ///     }
        ///
        ///
        /// </remarks>
        /// 
        /// <returns>Returns list of stops in path and total distance.</returns>
        /// <response code="200">The file has loaded properly</response>  
        /// <response code="500">Return message with error</response>
        [HttpGet("LoadData/{fileName}")]
        public async Task<IActionResult> GetBusStop(string fileName = "solvroCity.json")
        {
            try
            {
                using var sr = new StreamReader(@fileName);
                var line = sr.ReadToEnd();
                var array = JsonConvert.DeserializeObject<InputCommand>(line);

                    
                switch (array.Directed)
                {
                    case false:
                    {
                        foreach (var link in array.Links.Select(t => new Link { Source = t.Source, Target = t.Target, Distance = t.Distance }))
                        {
                            await _graphService.AddNewLinkUndirected(link);
                        }

                        break;
                    }
                    case true:
                    {
                        foreach (var link in array.Links.Select(t => new Link { Source = t.Source, Target = t.Target, Distance = t.Distance }))
                        {
                            await _graphService.AddNewLinkDirected(link);
                        }

                        break;
                    }
                }


                foreach (var t in array.Nodes)
                {
                    await _nodeService.AddAsync(t.Id, t.Stop_name);
                }


                return Ok();
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("The file could not found: " + e.Message);
            }
            catch (IOException e)
            {
                throw new Exception("The file could not be read: " + e.Message);
            }
        }
    }
}