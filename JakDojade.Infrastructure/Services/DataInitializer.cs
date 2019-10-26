using System;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Infrastructure.Services.User;

namespace JakDojade.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        public DataInitializer(IUserService userService)
        {
            _userService = userService;
        }
        public async Task SeedAsync()
        {
            var users = await _userService.BrowseAsync();
            if(users.Any())
            {
                return;
            }
            await _userService.RegisterAsync(Guid.NewGuid(),"user@test.com", "User1","secret123");

            await _userService.RegisterAsync(Guid.NewGuid(),"admin@test.com", "Admin1","secret123","admin");
        }
    }
}