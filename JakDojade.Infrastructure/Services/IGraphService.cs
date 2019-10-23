using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Infrastructure.Dto;

namespace JakDojade.Infrastructure.Services
{
    public interface IGraphService : IService
    {
        Task<Graph> GetAsync();

        Task AddNewLink(Link link);
        Task<PathDto> GetPath(int idSource, int idTarget);

    }
}