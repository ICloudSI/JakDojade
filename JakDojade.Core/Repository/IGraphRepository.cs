using System.Threading.Tasks;
using JakDojade.Core.Domain;

namespace JakDojade.Core.Repository
{
    public interface IGraphRepository : IRepository
    {
        Task<Graph> GetAsync();
        Task UpdateAsync(Graph graph);
    }
}