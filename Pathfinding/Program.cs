using System.Drawing;
using System.Numerics;
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
            //string[] verticies = JsonSerializer.Deserialize<string[]>(File.ReadAllText(@"../../../../AirportProblemVerticies.json"))!;

            //jsonEdge[] edges = JsonSerializer.Deserialize<jsonEdge[]>(File.ReadAllText(@"../../../../AirportProblemEdges.json"))!;

            //Graph<string> graph = new Graph<string>();

            //for(int i = 0; i < verticies.Length; i++)
            //{
            //    graph.AddVertex(verticies[i]);
            //}

            //for (int i = 0; i < edges.Length; i++)
            //{
            //    graph.AddEdge(edges[i].Start, edges[i].End, edges[i].Distance);
            //}

            //for(int startIndex = 0; startIndex < verticies.Length; startIndex++)
            //{
            //    for(int endIndex = 0; endIndex < verticies.Length; endIndex++)
            //    {
            //        var path = new List<Vertex<string>>();
            //        path = graph.DijkstraAlgorithm(graph.Search(verticies[startIndex]), graph.Search(verticies[endIndex]));
            //        Console.Write($"Path from {verticies[startIndex]} to {verticies[endIndex]}: ");
            //        if (path != null)
            //        {
            //            for (int i = 0; i < path.Count; i++)
            //            {
            //                if (i + 1 < path.Count)
            //                {
            //                    Console.Write($"{path[i].Value} -> ");
            //                }
            //                else
            //                {
            //                    Console.Write($"{path[i].Value} ");
            //                }

            //            }
            //            Console.WriteLine();
            //            Console.WriteLine($"Distance: {graph.GetDistance(path)}");
            //            Console.WriteLine();
            //            Console.WriteLine("--------------------------------------------------");
            //            Console.WriteLine();
            //        }
            //        else
            //        {
            //            Console.WriteLine("No path found.");
            //        }

            //    }
            //}







            //Graph<int> graph = new Graph<int>();

            //graph.AddVertex(1);
            //graph.AddVertex(2);
            //graph.AddVertex(3);
            //graph.AddVertex(4);
            //graph.AddVertex(5);
            //graph.AddVertex(6);
            //graph.AddVertex(7);
            //graph.AddVertex(8);

            //graph.AddEdge(1, 4, 1.2f);
            //graph.AddEdge(1, 2, 4.3f);
            //graph.AddEdge(1, 3, 6.7f);
            //graph.AddEdge(2, 5, 21f);
            //graph.AddEdge(5, 1, 20f);
            //graph.AddEdge(2, 8, 15f);
            //graph.AddEdge(1, 8, 4.8f);
            //graph.AddEdge(4, 7, 5f);
            //graph.AddEdge(3, 6, 2.4f);
            //graph.AddEdge(7, 5, 5f);

            //List<Vertex<int>>? list = graph.PathFindBreadthFirst(graph.Search(1), graph.Search(5));
            //List<Vertex<int>>? list2 = graph.PathFindDepthFirst(graph.Search(1), graph.Search(5));
            //List<Vertex<int>>? list3 = graph.DijkstraAlgorithm(graph.Search(1), graph.Search(5));

            Graph<Point> graph = new Graph<Point>();
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    graph.AddVertex(new Point(i, j));

                }
            }
            Random random = new Random();
            
            for(int i = 0; i < 20; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j), 1);
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i, j + 1), 1);
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j + 1), (float)Math.Sqrt(2));
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j - 1), (float)Math.Sqrt(2));
                }
            }
            
            int x1 = random.Next(0, 20);
            int y1 = random.Next(0, 20);
            int x2 = random.Next(0, 20);
            int y2 = random.Next(0, 20);
            var list5 = graph.DijkstraAlgorithm(graph.Search(new Point(x1, y1))!, graph.Search(new Point(x2,y2))!);
            var list4 = graph.AStarAlgorithm(graph.Search(new Point(x1, y1))!, graph.Search(new Point(x2, y2))!, graph.Euclidean);

            Console.WriteLine("Path Cost: " + graph.GetDistance(list4!));
            ;

            //;

        }
    }
}
