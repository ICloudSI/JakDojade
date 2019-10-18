using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Infrastructure.Dto;
using JakDojade.Core.Domain;

namespace JakDojade.Infrastructure.Services.User
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(Guid id);
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task Register(Guid id, string email, string username, string password,
            string role = "user");
        Task Login(string email, string password);
    }
}
