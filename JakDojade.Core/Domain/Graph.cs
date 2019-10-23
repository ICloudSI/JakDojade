using System;
using System.Collections.Generic;

namespace JakDojade.Core.Domain
{
    public class Graph
    {
        public List<List<int>> graph;

        public Graph()
        {
            graph = new List<List<int>>();
        }

        public void Add(int source, int target, int distance)
        {
            if (Math.Max(source, target) >= graph.Count)
            {
                int count = Math.Max(source, target);
                while (graph.Count <= count)
                {
                    graph.Add(new List<int>());
                }
                for (int i = 0; i < graph.Count; i++)
                    while (graph[i].Count <= count)
                    {
                        graph[i].Add(-1);
                    }

            }

            graph[source][target] = distance;
            graph[target][source] = distance;
        }

    }
}
