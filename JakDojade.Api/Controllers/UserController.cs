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


        /// <summary>
        /// Register new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /RegisterCommand
        ///     {
        ///        "Email": "test@test.com",
        ///        "Username": "Dziki jamnik",
        ///        "Password": "secret",
        ///        "Role": "user"
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Returns new user</returns>
        /// <response code="201">Created new user</response>    
        /// <response code="400">Return message with error.</response>

        [HttpPost("Register")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post([FromBody] RegisterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Username, command.Password,command.Role);

            return Created("/account", null);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /LoginCommand
        ///     {
        ///        "Email": "test@test.com",
        ///        "Password": "secret123",
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Login user and return JWT Token</returns>
        /// <response code="200">Login user</response>    
        /// <response code="500">Return message with error.</response>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenDto),200)]
        public async Task<IActionResult> Post([FromBody]LoginCommand command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));


    }
}
