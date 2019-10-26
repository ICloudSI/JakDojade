using System.Threading.Tasks;

namespace JakDojade.Infrastructure.Services
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();    
    }
}