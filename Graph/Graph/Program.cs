using Graphs.DoubleConnected;

namespace Graphs
{
    internal class Program
    {
        static void Main()
        {
            DoublyConnectedGraph<int> graph = new DoublyConnectedGraph<int>();

            graph.AddFace(1, 2, 3, 4, 5);
            graph.AddFace(1, 2, 6, 7, 8);
            graph.AddEdge(1, 6);

            graph.Print();
        }
    }
}