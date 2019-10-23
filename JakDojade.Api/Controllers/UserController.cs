using System;
using System.IO;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Algorithm;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Services;
using JakDojade.Infrastructure.Services.Node;
using JakDojade.Infrastructure.Services.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JakDojade.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly INodeService _nodeService;
        private readonly IGraphService _graphService;
        public UserController(IUserService userService, INodeService nodeService, IGraphService graphService)
        {
            _userService = userService;
            _nodeService = nodeService;
            _graphService = graphService;
        }


        //   public async Task<IActionResult> Get()
        //        => Json(await _userService.GetAsync());

        [HttpGet("Browse")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromBody] Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password);

            return Created("/account", null);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));


        [HttpGet("BusStop/{source}/{target}")]
        public async Task<IActionResult> GetBusStop(int source, int target)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("/Users/kacperdziobczynski/Projects/JakDojade/solvroCity.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Input array = JsonConvert.DeserializeObject<Input>(line);
                    for(int i=0;i<array.Links.Count;i++)
                    {
                        Link link = new Link {Source = array.Links[i].Source, Target = array.Links[i].Target, Distance =array.Links[i].Distance};
                        await _graphService.AddNewLink(link);
                    }
                    for (int i = 0; i < array.Nodes.Count;i++)
                    {
                        await _nodeService.AddAsync(array.Nodes[i].Id,array.Nodes[i].Stop_name); 
                    }
                    
                   // DijkstraAlgorithm.dijkstra(newGraph.graph,0);
                    return Json(_graphService.GetPath( source,target));

                    //Console.WriteLine(array);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return NoContent();

        }

    }
}
