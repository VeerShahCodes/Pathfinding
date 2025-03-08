using System.Text.Json;

namespace WeightedDirectedGraphs
{
    internal class Program
    {
        public struct jsonEdge
        { 
            public string Start { get; set; }
            public string End { get; set; }
            public int Distance { get; set; }
        }
        static void Main(string[] args)
        {
            string[] verticies = JsonSerializer.Deserialize<string[]>(File.ReadAllText(@"../../../../AirportProblemVerticies.json"));

            jsonEdge[] edges = JsonSerializer.Deserialize<jsonEdge[]>(File.ReadAllText(@"../../../../AirportProblemEdges.json"));
            ;








            Graph<int> graph = new Graph<int>();

            graph.AddVertex(1);
            graph.AddVertex(2);
            graph.AddVertex(3);
            graph.AddVertex(4);
            graph.AddVertex(5);
            graph.AddVertex(6);
            graph.AddVertex(7);
            graph.AddVertex(8);

            graph.AddEdge(1, 4, 1.2f);
            graph.AddEdge(1, 2, 4.3f);
            graph.AddEdge(1, 3, 6.7f);
            graph.AddEdge(2, 5, 21f);
            graph.AddEdge(5, 1, 20f);
            graph.AddEdge(2, 8, 15f);
            graph.AddEdge(1, 8, 4.8f);
            graph.AddEdge(4, 7, 5f);
            graph.AddEdge(3, 6, 2.4f);
            graph.AddEdge(7, 5, 5f);

            List<Vertex<int>>? list = graph.PathFindBreadthFirst(graph.Search(1), graph.Search(5));
            List<Vertex<int>>? list2 = graph.PathFindDepthFirst(graph.Search(1), graph.Search(5));
            List<Vertex<int>>? list3 = graph.DijkstraAlgorithm(graph.Search(1), graph.Search(5));

            ;

        }
    }
}
