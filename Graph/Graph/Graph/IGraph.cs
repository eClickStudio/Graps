namespace Graphs.DoubleConnected
{
    internal interface IGraph<T> : IReadOnlyGraph<T>
    {
        /// <summary>
        /// Adds a vertex to the graph.
        /// </summary>
        /// <param name="vertex">A vertex.</param>
        void AddVertex(in T vertex);

        /// <summary>
        /// Removes a vertex from the graph.
        /// </summary>
        /// <param name="vertex">A vertex.</param>
        void RemoveVertex(in T vertex);

        /// <summary>
        /// Adds an edge to the graph.
        /// </summary>
        /// <param name="firstVertex">An edge vertex.</param>
        /// <param name="secondVertex">An edge vertex.</param>
        void AddEdge(in T firstVertex, in T secondVertex);

        /// <summary>
        /// Removes an edge from the graph.
        /// </summary>
        /// <param name="firstVertex">An edge vertex.</param>
        /// <param name="secondVertex">An edge vertex.</param>
        /// <param name="removeVertices">Remove edge vertices?</param>
        void RemoveEdge(in T firstVertex, in T secondVertex, bool removeVertices = false);


        /// <summary>
        /// Adds a face to the graph.
        /// </summary>
        /// <param name="vertices">A face vertices.</param>
        void AddFace(in IReadOnlyList<T> vertices);

        /// <summary>
        /// Adds a face to the graph.
        /// </summary>
        /// <param name="vertices">A face vertices.</param>
        void AddFace(params T[] vertices);

        /// <summary>
        /// Removes a face from the graph.
        /// </summary>
        /// <param name="vertices">A face vertices.</param>
        /// <param name="removeVertices">Remove face vertices?</param>
        void RemoveFace(in IReadOnlyList<T> vertices, bool removeVertices = false);

        /// <summary>
        /// Removes a face from the graph.
        /// </summary>
        /// <param name="vertices">A face vertices.</param>
        /// <param name="removeVertices">Remove face vertices?</param>
        void RemoveFace(bool removeVertices = false, params T[] vertices);

        /// <summary>
        /// Adds a subgraph to the graph. 
        /// </summary>
        /// <param name="subgraph">A subgraph.</param>
        void AddSubgraph(in IReadOnlyGraph<T> subgraph);

        /// <summary>
        /// Removes a subgraph from the graph. 
        /// </summary>
        /// <param name="subgraph">A subgraph.</param>
        /// <param name="removeVertices">Remove graph vertices?</param>
        void RemoveSubgraph(in IReadOnlyGraph<T> subgraph, bool removeVertices = false);

        /// <summary>
        /// Clears the graph.
        /// </summary>
        void Clear();
    }
}
