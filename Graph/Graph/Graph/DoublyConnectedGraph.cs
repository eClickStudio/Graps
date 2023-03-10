using System.Text;

namespace Graphs.DoubleConnected
{
    public class DoublyConnectedGraph<T> : IGraph<T>
        where T : notnull
    {
        private Dictionary<T, List<T>> _vertices { get; }

        public int VertexCount
        {
            get { return _vertices.Count; }
        }

        public int EdgeCount
        {
            get
            {
                int count = 0;

                foreach (IReadOnlyList<T> connections in _vertices.Values)
                {
                    count += connections.Count;
                }

                return count / 2;
            }
        }

        public int FaceCount
        {
            get
            {
                throw new Exception();
            }
        }

        public bool isEmpty => _vertices.Count == 0;

        public IReadOnlyList<T> Vertices => _vertices.Keys.ToList();

        public IReadOnlyList<T> this[T vertex]
        {
            get
            {
                if (HasVertex(vertex))
                {
                    return _vertices[vertex];
                }

                throw new ArgumentException($"There is no vertex ({vertex}) here.");
            }
        }

        public DoublyConnectedGraph()
        {
            _vertices = new Dictionary<T, List<T>>();
        }

        public DoublyConnectedGraph(IReadOnlyGraph<T> subgraph)
        {
            _vertices = new Dictionary<T, List<T>>();
            AddSubgraph(subgraph);
        }

        #region Vertex
        public bool HasVertex(in T vertex)
        {
            return _vertices.ContainsKey(vertex);
        }

        public void AddVertex(in T vertex)
        {
            if (!HasVertex(vertex))
            {
                _vertices.Add(vertex, new List<T>());
            }
            else
            {
                throw new ArgumentException($"The same vertex ({vertex}) has already been added to graph.");
            }
        }

        public void RemoveVertex(in T vertex)
        {
            if (HasVertex(vertex))
            {
                IReadOnlyList<T> connectedVertices = this[vertex];

                foreach (T connectedVertex in connectedVertices)
                {
                    _vertices[connectedVertex].Remove(vertex);
                }

                _vertices.Remove(vertex);
            }
            else
            {
                throw new ArgumentException($"There is no vertex ({vertex}) here.");
            }
        }
        #endregion

        #region ConnectedVertices
        private void AddConnection(in T vertex, in T connectedVertex)
        {
            if (!HasEdge(vertex, connectedVertex))
            {
                if (!HasVertex(vertex))
                {
                    AddVertex(vertex);
                }

                if (!HasVertex(connectedVertex))
                {
                    AddVertex(connectedVertex);
                }

                _vertices[vertex].Add(connectedVertex);
                _vertices[connectedVertex].Add(vertex);
            }
        }

        private void RemoveConnection(in T vertex, in T connectedVertex)
        {
            _vertices[vertex].Remove(connectedVertex);
            _vertices[connectedVertex].Remove(vertex);
        }
        #endregion

        #region Edge
        public bool HasEdge(in T firstVertex, in T secondVertex)
        {
            if (!firstVertex.Equals(secondVertex))
            {
                return
                    HasVertex(firstVertex) && _vertices[firstVertex].Contains(secondVertex)
                        &&
                    HasVertex(secondVertex) && _vertices[secondVertex].Contains(firstVertex);
            }
            else
            {
                throw new ArgumentException($"You can't remove cycles (A vertex ({firstVertex}) connected to itself) because they don't exist.");
            }
        }

        public void AddEdge(in T firstVertex, in T secondVertex)
        {
            if (!firstVertex.Equals(secondVertex))
            {
                if (!HasEdge(firstVertex, secondVertex))
                {
                    AddConnection(firstVertex, secondVertex);
                    AddConnection(secondVertex, firstVertex);
                }
                else
                {
                    throw new ArgumentException($"The same edge ({firstVertex}; {secondVertex}) has already been added to graph.");
                }
            }
            else
            {
                throw new ArgumentException($"You can't create cycles. (Connect the vertex ({firstVertex}) with itself).");
            }
        }

        public void RemoveEdge(in T firstVertex, in T secondVertex, bool removeVertices = false)
        {
            if (!firstVertex.Equals(secondVertex))
            {
                if (HasEdge(firstVertex, secondVertex))
                {
                    if (removeVertices)
                    {
                        RemoveVertex(firstVertex);
                        RemoveVertex(secondVertex);
                    }
                    else
                    {
                        RemoveConnection(firstVertex, secondVertex);
                    }
                }
                else
                {
                    throw new ArgumentException($"There is no edge ({firstVertex}; {secondVertex}) here.");
                }
            }
            else
            {
                throw new ArgumentException($"You can't remove cycles (A vertex ({firstVertex}) connected to itself) because they don't exist.");
            }
        }
        #endregion

        #region Face
        public bool HasFace(in IReadOnlyList<T> vertices)
        {
            T[] distinctVertices = vertices.Distinct().ToArray();

            if (distinctVertices.Length > 2)
            {
                for (int i = 0; i < distinctVertices.Length; i++)
                {
                    int nearIndex = (i + 1) % distinctVertices.Length;

                    if (!HasEdge(distinctVertices[i], distinctVertices[nearIndex]))
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Not enough distinct vertices for a face to exist.");
            }
        }

        public bool HasFace(params T[] vertices)
        {
            return HasFace(vertices.ToList());
        }

        public void AddFace(in IReadOnlyList<T> vertices)
        {
            T[] distinctVertices = vertices.Distinct().ToArray();

            if (distinctVertices.Length > 2)
            {
                bool didConnectionAdd = false;

                for (int i = 0; i < distinctVertices.Length; i++)
                {
                    int nearIndex = (i + 1) % distinctVertices.Length;

                    if (!HasEdge(distinctVertices[i], distinctVertices[nearIndex]))
                    {
                        didConnectionAdd = true;

                        AddConnection(distinctVertices[i], distinctVertices[nearIndex]);
                    }
                }

                if (!didConnectionAdd)
                {
                    throw new ArgumentException($"The same face has already been added to graph. \n\tYou tried to add face ({string.Join(", ", vertices)}).");
                }
            }
            else
            {
                throw new ArgumentException("Not enough distinct vertices for a face to exist.");
            }
        }

        public void AddFace(params T[] vertices)
        {
            AddFace(vertices.ToList());
        }

        public void RemoveFace(in IReadOnlyList<T> vertices, bool removeVertices = false)
        {
            T[] distinctVertices = vertices.Distinct().ToArray();

            if (distinctVertices.Length > 2)
            {
                if (HasFace(distinctVertices))
                {
                    for (int i = 0; i < distinctVertices.Length; i++)
                    {
                        int nearIndex = (i + 1) % distinctVertices.Length;

                        Console.WriteLine($"{i}; {nearIndex}");

                        if (removeVertices)
                        {
                            if (HasVertex(distinctVertices[i]))
                            {
                                RemoveVertex(distinctVertices[i]);
                            }

                            if (HasVertex(distinctVertices[nearIndex]))
                            {
                                RemoveVertex(distinctVertices[nearIndex]);
                            }
                        }
                        else
                        {
                            if (HasEdge(distinctVertices[i], distinctVertices[nearIndex]))
                            {
                                RemoveConnection(distinctVertices[i], distinctVertices[nearIndex]);
                            }
                        }
                    }
                }
                else
                {
                    throw new ArgumentException($"There is no face here. \n\tYou tried to remove face ({string.Join(", ", vertices)}).");
                }
            }
            else
            {
                throw new ArgumentException("Not enough distinct vertices for a face to exist.");
            }
        }

        public void RemoveFace(bool removeVertices = false, params T[] vertices)
        {
            RemoveFace(vertices.ToList(), removeVertices);
        }
        #endregion

        #region Subgraph
        public bool HasSubgraph(in IReadOnlyGraph<T> subgraph)
        {
            if (subgraph.isEmpty)
            {
                throw new ArgumentException("You can't check because subgraph is empty.");
            }

            if (subgraph == null)
            {
                throw new ArgumentNullException("You can't check because subgraph is null.");
            }

            foreach (T firstVertex in subgraph.Vertices)
            {
                foreach (T secondVertex in subgraph[firstVertex])
                {
                    if (!HasEdge(firstVertex, secondVertex))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void AddSubgraph(in IReadOnlyGraph<T> subgraph)
        {
            if (subgraph.isEmpty)
            {
                throw new ArgumentException("You can't add a subgraph because it is empty.");
            }

            if (subgraph == null)
            {
                throw new ArgumentNullException("You can't add a subgraph because it is null.");
            }

            bool didConnectionAdded = false;

            foreach (T firstVertex in subgraph.Vertices)
            {
                foreach (T secondVertex in subgraph[firstVertex])
                {
                    if (!HasEdge(firstVertex, secondVertex))
                    {
                        didConnectionAdded = true;

                        AddEdge(firstVertex, secondVertex);
                    }
                }
            }

            if (!didConnectionAdded)
            {
                throw new ArgumentException($"This graph already contains this subgraph: \n{subgraph.ToString()}");
            }
        }

        public void RemoveSubgraph(in IReadOnlyGraph<T> subgraph, bool removeVertices = false)
        {
            if (subgraph.isEmpty)
            {
                throw new ArgumentException("You can't remove a subgraph because it is empty.");
            }

            if (subgraph == null)
            {
                throw new ArgumentNullException("You can't remove a subgraph because it is null.");
            }

            foreach (T firstVertex in subgraph.Vertices)
            {
                foreach (T secondVertex in subgraph[firstVertex])
                {
                    if (HasEdge(firstVertex, secondVertex))
                    {
                        RemoveEdge(firstVertex, secondVertex, removeVertices);
                    }
                    else
                    {
                        throw new ArgumentException($"This graph doesn't contain this subgraph: \n{subgraph.ToString()}");
                    }
                }
            }
        }
        #endregion

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Graph:");

            foreach (KeyValuePair<T, List<T>> pair in _vertices)
            {
                stringBuilder.Append($"\t{pair.Key} connections: ");

                foreach (T vertex in pair.Value)
                {
                    stringBuilder.Append($"{vertex}; ");
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public void Print()
        {
            Console.WriteLine(ToString());
        }

        public bool IsEqual(IReadOnlyGraph<T> graph)
        {
            DoublyConnectedGraph<T> compareGraph = new DoublyConnectedGraph<T>(graph);
            compareGraph.RemoveSubgraph(this);

            return compareGraph.VertexCount == 0;
        }

        public void Clear()
        {
            _vertices.Clear();
        }
    }
}