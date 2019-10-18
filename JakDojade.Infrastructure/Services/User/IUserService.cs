using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services.User
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(Guid id);
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid id, string email, string username, string password,
            string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);
    }
}
