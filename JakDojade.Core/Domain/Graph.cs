using System;
using System.Collections.Generic;

namespace JakDojade.Core.Domain
{
    public class Graph
    {
        public List<List<int>> graph;
        public int V = 0;

        public Graph()
        {
            graph = new List<List<int>>();
        }

        public void Add(int source, int target, int distance)
        {
            if(graph.Count<=source || graph.Count <= target)
            {
                while(graph.Count <=source || graph.Count<=target )
                {
                    graph.Add(new List<int>());
                }
            }
            if(graph[source].Count <=target)
            {
                while(graph[source].Count<=target)
                    graph[source].Add(-1);
            }
            if(graph[target].Count <= target)
            {
                while(graph[target].Count<=source)
                    graph[target].Add(-1);
            }

            graph[source][target] = distance;
            graph[target][source] = distance;
        }
       
 
    }
}
