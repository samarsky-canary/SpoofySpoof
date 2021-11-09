using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgFloydWarshell<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        public int[,] lenghtMatr { get; set; }
        public List<TVertex> allVert { get; set; }


        public AlgFloydWarshell(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
            lenghtMatr = new int[Graph.Verticies.Count, Graph.Verticies.Count];
            allVert = new();
            Graph.Verticies.First().Lenght = 0;
        }

        public int[,] Begin()
        {
            for (int i = 0; i < lenghtMatr.Length; i++)
                for (int j = 0; j < lenghtMatr.Length; j++)
                    lenghtMatr[i, j] = (i == j) ? 0 : int.MaxValue;

            foreach (var edge in Graph.Edges)
            {
                int k = allVert.IndexOf(edge.From);
                int m = allVert.IndexOf(edge.To);
                lenghtMatr[k, m] = edge.Price;
            }

            for (int k = 0; k < Graph.Verticies.Count; k++)
                for (int i = 0; i < Graph.Verticies.Count; i++)
                    for (int j = 0; j < Graph.Verticies.Count; j++)
                        if (lenghtMatr[i, j] > lenghtMatr[i, k] + lenghtMatr[k, j])
                            lenghtMatr[i, j] = lenghtMatr[i, k] + lenghtMatr[k, j];

            return lenghtMatr;
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
