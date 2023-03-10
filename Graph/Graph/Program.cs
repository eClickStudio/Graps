using Graphs.DoubleConnected;

namespace Graphs
{
    internal class Program
    {
        static void Main()
        {
            DoublyConnectedGraph<int> graph = new DoublyConnectedGraph<int>();

            graph.AddFace(1, 2, 3, 4, 5);
            graph.AddFace(1, 5, 6, 7, 8);
            graph.AddEdge(1, 6);

            graph.RemoveFace(true, 1, 5, 6);

            DoublyConnectedGraph<int> secondGraph = new DoublyConnectedGraph<int>();
            secondGraph.AddFace(2, 3, 13, 12, 11, 10);
            secondGraph.Print();
            secondGraph.Clear();

            graph.AddSubgraph(secondGraph);

            graph.Print();

            Console.ReadLine();
        }
    }
}