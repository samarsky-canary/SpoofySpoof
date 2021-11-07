using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    class AlgPrim<TVertex, TEdge> : ISearchAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }

        List<TEdge> MST = new();

        public AlgorithmStep Begin()
        {
            ISet<TEdge> notUsedE = Graph.Edges;
            List<TVertex> usedV = new List<TVertex>();
            ISet<TVertex> notUsedV = Graph.Verticies;

            return Step();
        }

        private AlgorithmStep Step()
        {

            return Step;
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
