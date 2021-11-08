using GraphLab.Blazor.Model;
using GraphLab.Core;
using GraphLab.Core.Algorithms.Search;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using System.Drawing;
using System;
using System.Collections.Generic;

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
        public static async Task BindGraph(this IJSRuntime jsRuntime, Graph<City, Road> graph, bool orientir)
        {
            var vertecies = graph.Verticies
                .Select(x => new
                {
                    id = x.Id,
                    label = x.Name,
                    color = "lightblue"
                }).ToArray();

            if (orientir)
            {
                var edges = graph.Edges
                    .Select(x => new
                    {
                        id = x.Id,
                        from = x.From.Id,
                        to = x.To.Id,
                        arrows = "to",
                        label = x.Price.ToString()
                    }).ToArray();

                await jsRuntime.InvokeVoidAsync("bindGraph", vertecies, edges);
            }
            else
            {
                var edges = graph.Edges
                    .Select(x => new
                    {
                        id = x.Id,
                        from = x.From.Id,
                        to = x.To.Id,
                        line = "to",
                        label = x.Price.ToString()
                    }).ToArray();

                await jsRuntime.InvokeVoidAsync("bindGraph", vertecies, edges);
            }
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

        public static async Task UpdateEdge(this IJSRuntime jsRuntime, List<Road> edges, bool orientir)
        {
            foreach (var ed in edges)
            {
                await jsRuntime.InvokeVoidAsync("updateEdge",
               new
               {
                   id = ed.Id,
                   from = ed.From.Id,
                   to = ed.To.Id,
                   line = "to",
                   label = ed.Price.ToString(),
                   color = "green",
               });
            }
        }



        public static async Task UpdateStrongComp(this IJSRuntime jsRuntime, List<City[]> cities)
        {
            for (int i = 0; i < cities.Count; i++)
            {
                foreach (var c in cities[i])
                {
                    await jsRuntime.InvokeVoidAsync("updateVertex",
                    new
                    {
                        id = c.Id,
                        label = c.Name,
                        color = i switch
                        {
                            0 => "green",
                            1 => "pink",
                            2 => "yellow",
                            3 => "blue",
                            4 => "lightblue",
                            5 => "lightgreen",
                            _ => "red"
                        }
                    }) ;
                }
            }
        }
    }
}
