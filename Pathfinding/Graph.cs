using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightedDirectedGraphs
{
    public class Graph<T>
    {
        private List<Vertex<T>> vertices;
        public IReadOnlyList<Vertex<T>> Vertices => vertices;
        public IReadOnlyList<Edge<T>> Edges
        {
            get
            {
                List<Edge<T>> edges = new List<Edge<T>>();
                for(int i = 0; i < vertices.Count; i++)
                {
                    for(int j = 0; j < vertices[i].NeighborCount; j++)
                    {
                        edges.Add(vertices[i].Neighbors[j]);

                    }
                }
                return edges;
            }
        }

        public int VertexCount => vertices.Count;

        public Graph()
        {
            vertices = new List<Vertex<T>>();
        }

        private void AddVertex(Vertex<T>? vertex)
        {
            if(vertex != null && vertex.NeighborCount == 0 && vertex.Owner != this)
            {
                vertices.Add(vertex);
                vertex.Owner = this;
            }
        }

        public void AddVertex(T val)
        {
            AddVertex(new Vertex<T>(val));
        }

        private bool RemoveVertex(Vertex<T>? vertex)
        {
            if(vertex == null || vertex.Owner != this) return false;

            for(int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].EndingPoint == vertex)
                {
                    Edges[i].StartingPoint.Neighbors.Remove(Edges[i]);
                }
            }
            vertices.Remove(vertex);
            return true;
        }

        public bool RemoveVertex(T val)
        {
            bool b = RemoveVertex(Search(val));
            return b;
        }

        private void AddEdge(Vertex<T>? a, Vertex<T>? b, float distance)
        {
            if(a != null && b != null && a.Owner == this && b.Owner == this)
            {
                Edge<T> edge = new Edge<T>(a, b, distance);
                if(!a.Neighbors.Contains(edge))
                {
                    a.Neighbors.Add(edge);
                }
            }
        }

        public void AddEdge(T val1, T val2, float distance)
        {
            AddEdge(Search(val1), Search(val2), distance);
        }

        

        private bool RemoveEdge(Vertex<T>? a, Vertex<T>? b)
        {
            if (a == null || b == null) return false;
            bool hasThatEdge = false;
            int index = 0;
            for(int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].StartingPoint == a && Edges[i].EndingPoint == b)
                {
                    hasThatEdge = true;
                    index = i;
                }
            }
            if (hasThatEdge == false) return false;

            a.Neighbors.Remove(Edges[index]);
            return true;
        }

        public bool RemoveEdge(T val1, T val2)
        {
            bool b = RemoveEdge(Search(val1), Search(val2));
            return b;
        }

        public Vertex<T>? Search(T? value)
        {
            if (value == null) return null;
            int z = -1;

            for(int i = 0; i < vertices.Count; i++)
            {
                if (value.Equals(vertices[i].Value))
                {
                    z = i;
                    break;
                }
            }

            if (z == -1) return null;
            else
            {
                return vertices[z];
            }

        }

        public Edge<T>? GetEdge(Vertex<T>? a, Vertex<T>? b)
        {
            if (a == null || b == null) return null;
            bool hasThatEdge = false;
            int index = 0;
            for (int i = 0; i < Edges.Count; i++)
            {
                if (Edges[i].StartingPoint == a && Edges[i].EndingPoint == b)
                {
                    hasThatEdge = true;
                    index = i;
                }
            }
            if (hasThatEdge == false) return null;

            return Edges[index];
        }

        public List<Vertex<T>>? PathFindBreadthFirst(Vertex<T>? start, Vertex<T>? end)
        {
            if (start == null || end == null) return null;
            float pathCost = 0f;

            Dictionary<Vertex<T>, Vertex<T>?> previousVertex = new()
            {
                [start] = null
            };

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            queue.Enqueue(start);
            while(queue.Count > 0)
            {
                Vertex<T>? current = queue.Dequeue();
                if(current.Equals(end))
                {
                    List<Vertex<T>> path = new List<Vertex<T>>();
                    while (current != null)
                    {
                        path.Add(current);
                        var edge = GetEdge(previousVertex[current], current);
                        if (edge is not null)
                        {
                            pathCost += edge.Distance;

                        }
                        current = previousVertex[current];
                    }
                    path.Reverse();
                    Console.WriteLine("Breadth First: " + pathCost + " path cost");
                    return path;
                }

                for (int i = 0; i < current.NeighborCount; i++)
                {
                    if (!previousVertex.ContainsKey(current.Neighbors[i].EndingPoint))
                    {
                        queue.Enqueue(current.Neighbors[i].EndingPoint);
                        previousVertex[current.Neighbors[i].EndingPoint] = current;
                    }
                }
            }
            return null;
        }

        public List<Vertex<T>>? PathFindDepthFirst(Vertex<T>? start, Vertex<T>? end)
        {
            if (start == null || end == null) return null;
            float pathCost = 0f;
            Dictionary<Vertex<T>, Vertex<T>?> previousVertex = new()
            {
                [start] = null
            };

            Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
            stack.Push(start);

            while(stack.Count > 0)
            {
                Vertex<T>? current = stack.Pop();
                if(current.Equals(end))
                {
                    List<Vertex<T>> path = new List<Vertex<T>>();
                    while (current != null)
                    {
                        path.Add(current);
                        var edge = GetEdge(previousVertex[current], current);
                        if (edge != null)
                        {
                            pathCost += edge.Distance;

                        }
                        current = previousVertex[current];
                    }
                    path.Reverse();
                    Console.WriteLine("Depth First: " + pathCost + " path cost");
                    return path;
                }

                for(int i = current.NeighborCount - 1; i > -1; i--)
                {
                    if (!previousVertex.ContainsKey(current.Neighbors[i].EndingPoint))
                    {
                        stack.Push(current.Neighbors[i].EndingPoint);
                        previousVertex[current.Neighbors[i].EndingPoint] = current;
                    }
                } 
            }

            return null;
        }

        public List<Vertex<T>>? DijkstraAlgorithm(Vertex<T> start, Vertex<T> end)
        {
            if (start == null || end == null) return null;

            Dictionary<Vertex<T>, Vertex<T>?> previousVertex = new()
            {
                [start] = null
            };
            HashSet<Vertex<T>> visited = new HashSet<Vertex<T>>();

            Dictionary<Vertex<T>, float> distance = new Dictionary<Vertex<T>, float>();

            PriorityQueue<Vertex<T>, float> queue = new PriorityQueue<Vertex<T>, float>();   

            for (int i = 0; i < vertices.Count; i++)
            {
                distance[vertices[i]] = float.PositiveInfinity;
                previousVertex[vertices[i]] = null;
            }
            distance[start] = 0;
            queue.Enqueue(start, distance[start]);

            while(queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();
                for(int i = 0; i < current.NeighborCount; i++)
                {
                    float tentativeDistance = distance[current] + current.Neighbors[i].Distance;
                    if(tentativeDistance < distance[current.Neighbors[i].EndingPoint])
                    {
                        distance[current.Neighbors[i].EndingPoint] = tentativeDistance;
                        previousVertex[current.Neighbors[i].EndingPoint] = current;
                        visited.Remove(current.Neighbors[i].EndingPoint);
                    } 
                }

                for(int i = 0; i < current.NeighborCount; i++)
                {
                    if (!visited.Contains(current.Neighbors[i].EndingPoint)
                        && !queue.UnorderedItems.AsEnumerable().Contains((current.Neighbors[i].EndingPoint, 2/*gets thrown away by the comparer, soi we don't care what it is*/),
                                                                        new NodeComparer<T>()))
                    {
                        queue.Enqueue(current.Neighbors[i].EndingPoint, distance[current.Neighbors[i].EndingPoint]);
                    }
                }

                visited.Add(current);
            }
            float pathCost = 0f;
            Vertex<T> theCurrentest = end;
            List<Vertex<T>> path = new List<Vertex<T>>();
            while (previousVertex[theCurrentest!] != null)
            {
                path.Add(theCurrentest!);
                pathCost += GetEdge(previousVertex[theCurrentest], theCurrentest)!.Distance;
                theCurrentest = previousVertex[theCurrentest]!;
                
               
            }
            path.Reverse();
            Console.WriteLine("Djikstra Algorithm:" + pathCost);
            return path;
            
        }
    }

    public class NodeComparer<T> : IEqualityComparer<(Vertex<T> Element, float Priority)>
    {
        public bool Equals((Vertex<T> Element, float Priority) x, (Vertex<T> Element, float Priority) y) => x.Element.Equals(y.Element);

        public int GetHashCode([DisallowNull] (Vertex<T> Element, float Priority) obj) => obj.Element.GetHashCode();
    }
}