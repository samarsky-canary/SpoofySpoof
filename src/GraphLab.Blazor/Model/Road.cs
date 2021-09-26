using GraphLab.Core;

namespace GraphLab.Blazor.Model
{
    public class Road : IEdge<City>
    {
        public City From { get; set; }
        public City To { get; set; }
        public decimal Price { get; set; }
    }
}
