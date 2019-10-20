using System.Collections.Generic;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
namespace JakDojade.Infrastructure.Services.Node
{
    public interface INodeService:IService
    {
        Task<JakDojade.Core.Domain.Node> GetAsync(int id);
        Task <IEnumerable<JakDojade.Core.Domain.Node>> BrowseAsync();
        Task AddAsync(int id, string stop_name);

        
    }
}