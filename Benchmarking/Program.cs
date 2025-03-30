
using System;
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using WeightedDirectedGraphs;

namespace Benchmarking
{
    public class Benchmarking
    {
        Graph<Point> graph = new Graph<Point>();

        int x1;
        int y1;
        int x2;
        int y2;
        public Benchmarking()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    graph.AddVertex(new Point(i, j));

                }
            }
            Random random = new Random();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j), 1);
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i, j + 1), 1);
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j + 1), (float)Math.Sqrt(2));
                    graph.AddUndirectedEdge(new Point(i, j), new Point(i + 1, j - 1), (float)Math.Sqrt(2));
                }
            }

            x1 = random.Next(0, 20);
            y1 = random.Next(0, 20);
            x2 = random.Next(0, 20);
            y2 = random.Next(0, 20);
        }

        [Benchmark]
        public List<Vertex<Point>>? Dijkstra() => graph.DijkstraAlgorithm(graph.Search(new Point(x1, y1))!, graph.Search(new Point(x2, y2))!);

        [Benchmark]
        public List<Vertex<Point>>? AStar() => graph.AStarAlgorithm(graph.Search(new Point(x1, y1))!, graph.Search(new Point(x2, y2))!, graph.Euclidean);
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}
