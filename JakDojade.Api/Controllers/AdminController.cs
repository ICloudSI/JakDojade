using System;
using System.IO;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Services;
using JakDojade.Infrastructure.Services.Node;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JakDojade.Api.Controllers
{
    [Route("user/[controller]")]
    public class AdminController :ApiControllerBase
    {
        private readonly INodeService _nodeService;
        private readonly IGraphService _graphService;
        public AdminController( INodeService nodeService, IGraphService graphService)
        {
            _nodeService = nodeService;
            _graphService = graphService;
        }

        [HttpGet("LoadData")]
        public async Task<IActionResult> GetBusStop()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("/Users/kacperdziobczynski/Projects/JakDojade/solvroCity.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    InputCommand array = JsonConvert.DeserializeObject<InputCommand>(line);

                    for(int i=0;i<array.Links.Count;i++)
                    {
                        Link link = new Link {Source = array.Links[i].Source, Target = array.Links[i].Target, Distance =array.Links[i].Distance};
                        await _graphService.AddNewLink(link);
                    }
                    for (int i = 0; i < array.Nodes.Count;i++)
                    {
                        await _nodeService.AddAsync(array.Nodes[i].Id,array.Nodes[i].Stop_name); 
                    }
                    return Ok();
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