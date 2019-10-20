using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Core.Domain;

namespace JakDojade.Core.Repository
{
    public interface INodeRepository : IRepository
    {
        Task<Node> GetAsync(int id);
        Task<IEnumerable<Node>> BrowseAsync();
        Task AddAsync(Node user);
        Task UpdateAsync(Node user);
        Task DeleteAsync(Node user);
    }
}