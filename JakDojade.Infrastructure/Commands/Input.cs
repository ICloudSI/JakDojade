using System.Collections.Generic;
using JakDojade.Core.Domain;

namespace JakDojade.Infrastructure.Commands
{
    public class Input
    {
        public bool Directed {get; set;}
        public Graph Graph {get; set;}
        public List<Link> Links {get;set;}
        public bool Multigraph {get; set;}
        public List <Node> Nodes { get; set;}
    }
}