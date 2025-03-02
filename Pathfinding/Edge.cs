using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraphs
{
    public class Edge<T>
    {
        public Vertex<T> StartingPoint { get; set; }
        public Vertex<T> EndingPoint { get; set; }
        public float Distance { get; set; }

        public Edge(Vertex<T> startingPoint, Vertex<T> endingPoint, float distance)
        {
            StartingPoint = startingPoint;
            EndingPoint = endingPoint;
            Distance = distance;
        }

        public void SelfDestruct()
        {
            Console.WriteLine("BOOM");
            this.SelfDestruct();
        }
    }
}
