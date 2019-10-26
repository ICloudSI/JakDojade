using System.Threading.Tasks;
using JakDojade.Infrastructure.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JakDojade.Api.Controllers
{
    [Route("[controller]")]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Get user data
        /// </summary>
        /// <returns>Return user data</returns>
        /// <response code="200">Login user</response>    
        /// <response code="500">Return message with error.</response>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
            => Json(await _userService.GetAsync(UserId));

    }
}