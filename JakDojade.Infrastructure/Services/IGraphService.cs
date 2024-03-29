using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services
{
    public interface IGraphService : IService
    {
        Task<Graph> GetAsync();

        Task AddNewLinkDirected(Link link);
        Task AddNewLinkUndirected(Link link);
        Task<PathBusStops> GetPath(int idSource, int idTarget);

    }
}