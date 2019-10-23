using System;
using System.Collections.Generic;
using System.IO;
using JakDojade.Core.Domain;

namespace JakDojade.Infrastructure.Algorithm
{
    public class DijkstraAlgorithm
    {
        private static readonly int NO_PARENT = -1;

        public static PathBusStops dijkstra(List<List<int>> adjacencyMatrix,
                                            int startVertex, int target)
        {
            int nVertices = adjacencyMatrix.Count;

            int[] shortestDistances = new int[nVertices];
 
            bool[] added = new bool[nVertices];

            for (int vertexIndex = 0; vertexIndex < nVertices;
                                                vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }

            shortestDistances[startVertex] = 0;
 
            int[] parents = new int[nVertices];

            parents[startVertex] = NO_PARENT;
  
            for (int i = 1; i < nVertices; i++)
            {

                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    if (!added[vertexIndex] &&
                        shortestDistances[vertexIndex] <
                        shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                added[nearestVertex] = true;

                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex] [vertexIndex];

                    if (edgeDistance > 0
                        && ((shortestDistance + edgeDistance) <
                            shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance +
                                                        edgeDistance;
                    }
                }
            }

            return printSolution(startVertex, shortestDistances, parents, target);
        }
        private static PathBusStops printSolution(int startVertex,
                                        int[] distances,
                                        int[] parents, int target)
        {
            List<int> path=new List<int>();
            printPath(target,parents, ref path);
            PathBusStops pathBusStops = new PathBusStops{Path = path, Distance = distances[target]};
            return pathBusStops;
        }

        private static void printPath(int currentVertex,
                                    int[] parents, ref List<int> path)
        {

            if (currentVertex == NO_PARENT)
            {
                return;
            }
            printPath(parents[currentVertex], parents, ref path);
            Console.Write(currentVertex + " ");
            path.Add(currentVertex);
        }
    }
}