using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Core.Domain;

namespace JakDojade.Core.Repository
{
    public interface IUserRepository : IRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> BrowseAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
