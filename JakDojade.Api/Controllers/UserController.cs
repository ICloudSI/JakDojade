using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
        [HttpGet("BusStop")]
        public async Task<IActionResult> GetBusStop()
        {
            using (StreamReader r = new StreamReader("/ Users / kacperdziobczynski / Projects / JakDojade / solvroCity.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                /*foreach (var item in array)
                {
                    Console.WriteLine("{0} {1}", item.temp, item.vcc);
                }*/
                return array;
            }
            
        }

    }
}
