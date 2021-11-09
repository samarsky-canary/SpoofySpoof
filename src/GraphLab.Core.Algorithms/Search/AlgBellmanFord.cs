using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgBellmanFord<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        public List<List<int>> lenght { get; set; }
        public List<TVertex> allVert { get; set; }


        public AlgBellmanFord(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
            allVert = new();
            Graph.Verticies.First().Lenght = 0;
        }

        public List<List<int>> Begin()
        {
            lenght = new();
            /* Все вершины */
            allVert = Graph.Verticies.ToList();

            List<int> d = new();
            for (int i = 0; i < allVert.Count; i++)
                d.Add((i == 0) ? 0 : int.MaxValue);

            for (int i = 0; i < allVert.Count - 1; i++)
            {
                foreach (var edge in Graph.Edges)
                {
                    if (d[allVert.IndexOf(edge.From)] < int.MaxValue)
                    {
                        d[allVert.IndexOf(edge.To)] = Math.Min(d[allVert.IndexOf(edge.To)], d[allVert.IndexOf(edge.From)] + edge.Price);
                        edge.To.Lenght = d[allVert.IndexOf(edge.To)];
                    }
                }

                List<int> h = new();
                foreach (var v in d)
                    h.Add(v);

                lenght.Add(h);
            }

            return lenght;
        }

        private void Step(ref List<int> d)
        {
            
        }

        private void MakeCurrent(TVertex vertex)
        {
            var currentVertex = Graph.Verticies.SingleOrDefault(
                x => States[x] == SearchState.Current);

            if (currentVertex != null)
                States[currentVertex] = SearchState.Visited;

            States[vertex] = SearchState.Current;
        }
    }
}
