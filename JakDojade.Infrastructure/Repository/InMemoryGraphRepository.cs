using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JakDojade.Core.Domain;
using JakDojade.Core.Repository;

namespace JakDojade.Infrastructure.Repository
{
    public class InMemoryGraphRepository :  IGraphRepository
    {
        public InMemoryGraphRepository()
        {
        }

        private static readonly Graph _graph = new Graph();

        public async Task<Graph> GetAsync()
            => await Task.FromResult(_graph);

        public async Task UpdateAsync(Graph graph)
            {
                await Task.CompletedTask;
            }

    }
}
