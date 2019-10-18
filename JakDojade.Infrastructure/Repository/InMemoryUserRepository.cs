using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;

namespace JakDojade.Infrastructure.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        public InMemoryUserRepository()
        {
        }

        private static readonly ISet<User> _users = new HashSet<User>
            {
                new User{Id=Guid.NewGuid(),Email="email1@test.com", Username="Janusz", Password="secret123", Role ="user"},
                new User{Id=Guid.NewGuid(),Email="email2@test.com", Username="Andrzej", Password="secret123", Role ="user"}
            };

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));

        public async Task<IEnumerable<User>> BrowseAsync()
            {
                var users = _users.AsEnumerable();
                return await Task.FromResult(users);
            }
        public async Task AddAsync(User user)
            {
                _users.Add(user);
                await Task.CompletedTask;
            }
        public async Task UpdateAsync(User user)
            {
                await Task.CompletedTask;
            }
        public async Task DeleteAsync(User user)
            {
                _users.Remove(user);
                await Task.CompletedTask;
            }

    }
}
