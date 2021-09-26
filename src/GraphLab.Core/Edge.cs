namespace GraphLab.Core
{
    /// <summary>
    /// Обобщенное ребро
    /// </summary>
    /// <typeparam name="TVertex">Тип веришины</typeparam>
    public interface IEdge <TVertex>
        where TVertex : class, IVertex
    {
        TVertex From { get; set; }
        TVertex To { get; set; }
    }
}
