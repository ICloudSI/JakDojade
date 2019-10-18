using System;
using System.IO;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Services.User;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JakDojade.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        //   public async Task<IActionResult> Get()
        //        => Json(await _userService.GetAsync());

        [HttpGet("Browse")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] Register command)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password);

            return Created("/account", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));


        [HttpGet("BusStop")]
        public async Task<IActionResult> GetBusStop()
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("/Users/kacperdziobczynski/Projects/JakDojade/solvroCity.json"))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    //JsonSerializer serializer = new JsonSerializer();
                    Input array = JsonConvert.DeserializeObject<Input>(line);
                    Graph newGraph = new Graph();
                    for (int i = 0; i < array.Links.Count; i++)
                    {
                        newGraph.Add(array.Links[i].Source, array.Links[i].Target, array.Links[i].Distance);
                    }


                    //    List<Links> links = array.links;
                    return Json(array, new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented
                    });

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
