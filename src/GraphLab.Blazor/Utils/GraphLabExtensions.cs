using GraphLab.Blazor.Model;
using GraphLab.Core;
using GraphLab.Core.Algorithms.Search;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace GraphLab.Blazor.Utils
{
    public static class GraphLabExtensions
    {
        /// <summary>
        /// Первоночальное создание визуального графа с использованием
        /// библиотеки VisJS
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static async Task BindGraph(this IJSRuntime jsRuntime, Graph<City, Road> graph)
        {
            var vertecies = graph.Verticies
                .Select(x => new
                {
                    id = x.Id,
                    label = x.Name,
                    color = "lightblue"
                }).ToArray();

            var edges = graph.Edges
                .Select(x => new
                {
                    from = x.From.Id,
                    to = x.To.Id,
                    arrows = "to",
                    label = x.Price.ToString("N2")
                }).ToArray();

            await jsRuntime.InvokeVoidAsync("bindGraph", vertecies, edges);
        }

        /// <summary>
        /// Обновленние вершины в визуальном графе
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="vertex"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static async Task UpdateVertex(this IJSRuntime jsRuntime, City vertex, SearchState state)
        {
            await jsRuntime.InvokeVoidAsync("updateVertex",
                new
                {
                    id = vertex.Id,
                    label = vertex.Name,
                    color = state switch
                    {
                        SearchState.Current => "pink",
                        SearchState.Visited => "lightgreen",
                        _ => "lightblue"
                    }
                });
        }
    }
}
