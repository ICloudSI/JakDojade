using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Infrastructure.Services.User;
using Microsoft.AspNetCore.Mvc;

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

    }
}
