using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Algorithm;
using JakDojade.Infrastructure.Commands;
using JakDojade.Infrastructure.Dto;
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
        public async Task<IActionResult> Post([FromBody] RegisterCommand command)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password);

            return Created("/account", null);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody]LoginCommand command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));


    }
}
