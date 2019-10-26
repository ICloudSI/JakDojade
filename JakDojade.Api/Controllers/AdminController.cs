using System;
using System.Collections.Generic;
using System.IO;
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
        [ProducesResponseType(typeof(IEnumerable<UserDto>),200)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
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
        [HttpGet("LoadData")]
        public async Task<IActionResult> GetBusStop()
        {
            try
            {   
                using (StreamReader sr = new StreamReader("solvroCity.json"))
                {
                    String line = sr.ReadToEnd();
                    InputCommand array = JsonConvert.DeserializeObject<InputCommand>(line);

                    for (int i = 0; i < array.Links.Count; i++)
                    {
                        Link link = new Link { Source = array.Links[i].Source, Target = array.Links[i].Target, Distance = array.Links[i].Distance };
                        await _graphService.AddNewLink(link);
                    }
                    for (int i = 0; i < array.Nodes.Count; i++)
                    {
                        await _nodeService.AddAsync(array.Nodes[i].Id, array.Nodes[i].Stop_name);
                    }
                    return Ok();
                }
            }
            catch (FileNotFoundException e)
            {
                throw new Exception("The file could not found: " + e.Message);
            }
            catch (IOException e)
            {
                throw new Exception("The file could not be read: " + e.Message);
            }

            //return BadRequest();

        }
    }
}