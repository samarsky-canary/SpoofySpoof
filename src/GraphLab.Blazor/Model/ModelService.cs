using Bogus;
using GraphLab.Core;

namespace GraphLab.Blazor.Model
{
    /// <summary>
    /// Модель данных
    /// </summary>
    public class ModelService
    {
        public Graph<City, Road> Graph { get; private set; }

        public ModelService()
        {
            Graph = new Graph<City, Road>();
            Generate(6);
        }

        public void Generate(int vertexCount)
        {
            Graph.Clear();

            var faker = new Faker("ru");

            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = Graph.CreateVertex();
                vertex.Name = faker.Address.City();
            }

            foreach (var from in Graph.Verticies)
            {
                foreach (var to in Graph.Verticies)
                {
                    if(from != to && faker.Random.Double() < 0.5)
                    {
                        var edge = Graph.CreateEdge(from, to);
                        edge.Price = faker.Random.Decimal(min: 100, max: 1000);
                    }    
                }
            }
        }
    }
}
