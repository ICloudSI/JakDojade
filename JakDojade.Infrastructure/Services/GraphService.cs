using System.Threading.Tasks;
using AutoMapper;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;
using JakDojade.Infrastructure.Dto;
using JakDojade.Infrastructure.Algorithm;
using System.Linq;
using System;

namespace JakDojade.Infrastructure.Services
{
    public class GraphService : IGraphService
    {
        private readonly IGraphRepository _graphRepository;
        private readonly IMapper _mapper;
        public GraphService(IGraphRepository graphRepository, IMapper mapper)
        {
            _graphRepository = graphRepository;
            _mapper = mapper;
        }
        public async Task AddNewLinkDirected(Link link)
        {
            Graph graph = await _graphRepository.GetAsync();
            graph.AddDirected(link.Source, link.Target, link.Distance);
            await _graphRepository.UpdateAsync(graph);
        }
        public async Task AddNewLinkUndirected(Link link)
        {
            Graph graph = await _graphRepository.GetAsync();
            graph.AddUndirected(link.Source, link.Target, link.Distance);
            await _graphRepository.UpdateAsync(graph);
        }


        public async Task<Graph> GetAsync()
        {
            return await _graphRepository.GetAsync();
        }

        public async Task<PathBusStops> GetPath(int idSource, int idTarget)
        {
            Graph graph = await _graphRepository.GetAsync();
            if(graph.graph.Count<=Math.Max(idSource,idTarget))
            return null;
            
            
            return DijkstraAlgorithm.dijkstra(graph.graph,idSource,idTarget);
            
        }
    }
}