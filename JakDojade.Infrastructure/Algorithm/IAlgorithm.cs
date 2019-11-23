using System.Collections.Generic;
using JakDojade.Core.Domain;

namespace JakDojade.Infrastructure.Algorithm
{
    public interface IAlgorithm
    {
        PathBusStops GetPatch(List<List<int>> adjacencyMatrix, int startVertex, int targetVertex);
    }
}