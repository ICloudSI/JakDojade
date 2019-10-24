using System.Threading.Tasks;
using AutoMapper;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;
using JakDojade.Infrastructure.Dto;
using JakDojade.Infrastructure.Algorithm;

namespace JakDojade.Infrastructure.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        public GraphService(IGraphRepository graphRepository, IJwtHandler jwtHandler, IMapper mapper, IEncrypter encrypter)
        {
            _graphRepository = graphRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _encrypter = encrypter;
        }
        public async Task AddNewLink(Link link)
        {
            Graph graph = await _graphRepository.GetAsync();
            graph.Add(link.Source, link.Target, link.Distance);
            await _graphRepository.UpdateAsync(graph);
        }

        public async Task<Graph> GetAsync()
        {
            return await _graphRepository.GetAsync();
        }

        public async Task<PathBusStops> GetPath(int idSource, int idTarget)
        {
            Graph graph = await _graphRepository.GetAsync();
            return DijkstraAlgorithm.dijkstra(graph.graph,idSource,idTarget);
            
        }
    }
}