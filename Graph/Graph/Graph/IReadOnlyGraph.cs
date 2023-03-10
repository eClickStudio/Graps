namespace Graphs.DoubleConnected
{
    public interface IReadOnlyGraph<T>
    {
        IReadOnlyList<T> this[T vertex]
        {
            get;
        }

        IReadOnlyList<T> Vertices { get; }

        int VertexCount { get; }
        int EdgeCount { get; }
        int FaceCount { get; }

        bool isEmpty { get; }

        /// <summary>
        /// Checks if there is a vertex in the graph.
        /// </summary>
        /// <param name="vertex">A vertex.</param>
        /// <returns>Is there a vertex in the graph?</returns>
        bool HasVertex(in T vertex);

        /// <summary>
        /// Checks if there is an edge in the graph.
        /// </summary>
        /// <param name="firstVertex">An edge vertex.</param>
        /// <param name="secondVertex">An edge vertex.</param>
        /// <returns>Is there edge in the graph?</returns>
        bool HasEdge(in T firstVertex, in T secondVertex);

        /// <summary>
        /// Checks if there is a face in the graph.
        /// </summary>
        /// <param name="vertices">Face vertices</param>
        /// <returns>Is there a face in the graph?</returns>
        bool HasFace(in IReadOnlyList<T> vertices);

        /// <summary>
        /// Checks if there is a face in the graph.
        /// </summary>
        /// <param name="vertices">Face vertices</param>
        /// <returns>Is there a face in the graph?</returns>
        bool HasFace(params T[] vertices);

        /// <summary>
        /// Checks if there is a subgraph in this graph.
        /// </summary>
        /// <param name="subgraph">A subgraph.</param>
        /// <returns>Is there a subgraph in the graph.</returns>
        bool HasSubgraph(in IReadOnlyGraph<T> subgraph);

        /// <summary>
        /// Prints graph to the console.
        /// </summary>
        void Print();

        string ToString();

        bool IsEqual(IReadOnlyGraph<T> graph);
    }
}