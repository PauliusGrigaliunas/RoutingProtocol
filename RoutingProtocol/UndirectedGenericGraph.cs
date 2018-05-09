using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    class UndirectedGenericGraph<T>
    {

        private List<Vertex<T>> vertices;

        int size;

        public List<Vertex<T>> Vertices { get { return vertices; } }
        public int Size { get { return vertices.Count; } }


        public UndirectedGenericGraph(int initialSize)
        {

            size = initialSize;

            if (size < 0)
            {
                throw new ArgumentException("Number of vertices cannot be negative");
            }

            vertices = new List<Vertex<T>>(initialSize);

        }

        public UndirectedGenericGraph(List<Vertex<T>> initialNodes)
        {
            vertices = initialNodes;
            size = vertices.Count;
        }

        public void AddVertex(Vertex<T> vertex)
        {
            vertices.Add(vertex);
        }

        public void RemoveVertex(Vertex<T> vertex)
        {
            vertices.Remove(vertex);
        }

        public bool HasVertex(Vertex<T> vertex)
        {
            return vertices.Contains(vertex);
        }

        int level = 0;
        public void DepthFirstSearch(Vertex<T> root)
        {
            level++;
            if (!root.IsVisited)
            {
                Console.Write(root.Value + " ");
                root.Visit();

                foreach (Vertex<T> neighbor in root.Neighbors)
                {
                    DepthFirstSearch(neighbor);
                }
            }
            level--;
            if (level == 0) RestoreGraph(root);
        }

        private void RestoreGraph(Vertex<T> root)
        {
            if (root.IsVisited)
            {
                root.UndoVisit();
                foreach (Vertex<T> neighbor in root.Neighbors)
                {
                    RestoreGraph(neighbor);
                }
            }
        }


        public void BreadthFirstSearch(Vertex<T> root)
        {

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();
            Console.Write(root.Value + " ");
            root.Visit();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Vertex<T> current = queue.Dequeue();

                foreach (Vertex<T> neighbor in current.Neighbors)
                {
                    if (!neighbor.IsVisited)
                    {
                        Console.Write(neighbor.Value + " ");
                        neighbor.Visit();
                        queue.Enqueue(neighbor);
                    }
                }
            }

            RestoreGraph(root);
        }

Stack<Vertex<T>> stack = new Stack<Vertex<T>>();
        public void Reach(Vertex<T> root, Vertex<T> vertex)
        {
            level++;

            if (!root.IsVisited)
            {
                
                stack.Push(root);

                root.Visit();

                if (!root.Equals(vertex))
                {
                    foreach (Vertex<T> neighbor in root.Neighbors)
                    {
                        Reach(neighbor, vertex);
                        
                    }
                }
                else
                {
                    foreach (Vertex<T> part in stack.Reverse())
                    {
                        Console.Write( part.Value + " ");
                    }
                }
                stack.Pop();
            }
            level--;
            if (level == 0) RestoreGraph(root);



            /*foreach (Vertex<T> neighbor in root.Neighbors)
            {
                if (neighbor.Equals(vertex)) Console.WriteLine("yes");
            }*/

        }
    }
}
