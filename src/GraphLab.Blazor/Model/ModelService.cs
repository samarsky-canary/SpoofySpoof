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
        }

        public void Generate(int vertexCount, bool orientir, int min, int max)
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
                    if (from != to && faker.Random.Double() < 0.3)
                    {
                        var edge = Graph.CreateEdge(from, to);
                        int p = faker.Random.Int(min: min, max: max);
                        edge.Price = p;

                        if(!orientir)
                        {
                            var edge2 = Graph.CreateEdge(to, from);
                            edge2.Price = p;
                        }
                    }

                }
            }
        }
    }
}
