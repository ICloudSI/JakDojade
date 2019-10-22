using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;

namespace JakDojade.Infrastructure.Services.Node
{
    public class NodeService : INodeService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        public NodeService(INodeRepository nodeRepository, IJwtHandler jwtHandler, IMapper mapper, IEncrypter encrypter)
        {
            _nodeRepository = nodeRepository;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _encrypter = encrypter;
        }
        public async Task<Core.Domain.Node> GetAsync(int id)
        {
            var node = await _nodeRepository.GetAsync(id);

            if (node == null)
            {
                throw new Exception($"User with id: '{id}' does not exist.");
            }

            return node;
        }
        public async Task<IEnumerable<Core.Domain.Node>> BrowseAsync()
        {
            var nodes = await _nodeRepository.BrowseAsync();

            return nodes;
        }
        public async Task AddAsync(int id, string stop_name)
        {
            var node = await _nodeRepository.GetAsync(id);
            if (node != null)
            {
                //throw new Exception($"Node with id: '{id}' already exist.");
                await Task.CompletedTask;
                return;
            }
            var newNode = new Core.Domain.Node { Id = id, Stop_name = stop_name };
            
            await _nodeRepository.AddAsync(newNode);
        }




    }
}