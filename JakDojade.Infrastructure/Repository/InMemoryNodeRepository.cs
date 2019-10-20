using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;

namespace JakDojade.Infrastructure.Repository
{
    public class InMemoryNodeRepository : INodeRepository
    {
        private static readonly ISet<Node> _nodes = new HashSet<Node>();
        public async Task<Node> GetAsync(int id)
            => await Task.FromResult(_nodes.SingleOrDefault(x => x.Id == id));
        public async Task<IEnumerable<Node>> BrowseAsync()
        {
            var nodes = _nodes.AsEnumerable();
            return await Task.FromResult(nodes);
        }
        public async Task AddAsync(Node node)
        {
            _nodes.Add(node);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Node node)
        {
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Node node)
        {
            _nodes.Remove(node);
            await Task.CompletedTask;
        }



    }
}