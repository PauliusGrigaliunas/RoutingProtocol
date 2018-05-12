using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    class UndirectedGenericGraph<T>
    {

        private Dictionary<Vertex<T>, int> memo;
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
            memo = new Dictionary<Vertex<T>, int>(initialSize);
            foreach (Vertex<T> vertex in vertices) memo.Add(vertex, int.MaxValue);
        }

        public UndirectedGenericGraph(List<Vertex<T>> initialNodes)
        {
            vertices = initialNodes;
            size = vertices.Count;
            memo = new Dictionary<Vertex<T>, int>();
            foreach (Vertex<T> vertex in vertices) memo.Add(vertex, int.MaxValue);
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

                foreach (var neighbor in root.Neighbors)
                {
                    DepthFirstSearch(neighbor.Key);
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
                foreach (var neighbor in root.Neighbors)
                {
                    RestoreGraph(neighbor.Key);
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

                foreach (var neighbor in current.Neighbors)
                {
                    if (!neighbor.Key.IsVisited)
                    {
                        Console.Write(neighbor.Key + " ");
                        neighbor.Key.Visit();
                        queue.Enqueue(neighbor.Key);
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
                    foreach (var neighbor in root.Neighbors)
                    {
                        Reach(neighbor.Key, vertex);

                    }
                }
                else
                {
                    foreach (Vertex<T> part in stack.Reverse())
                    {
                        Console.Write(part.Value + " ");
                    }
                }
                stack.Pop();
            }
            level--;

            if (level == 0) RestoreGraph(root);

        }

        public void Reach1(Vertex<T> root, Vertex<T> vertex)
        {
            Stack<Vertex<T>> path = new Stack<Vertex<T>>();
            path.Push(vertex);

            if (memo.ContainsKey(root))
            {
                memo[root] = 0;
                if (root == vertex)
                    return;
            }

            Queue<Vertex<T>> queue = new Queue<Vertex<T>>();


            for (int i = 1; i < Size; i++)
            {
                root.Visit();
                queue.Enqueue(root);
                int level = 1;
                for (int j = 1; queue.Count > 0;)
                {
                    Vertex<T> current = queue.Dequeue(); --j;

                    foreach (var neighbor in current.Neighbors)
                    {
                        if (!neighbor.Key.IsVisited)
                        {
                            if (neighbor.Key.Equals(vertex))
                            {
                                Console.WriteLine(level);
                                path.Push(current);
                                vertex = current;
                            }
                            neighbor.Key.Visit();
                            queue.Enqueue(neighbor.Key);
                        }
                    }
                    if (j <= 0) { j = queue.Count; level++; }
                }

                RestoreGraph(root);

            }
            foreach (var part in path)
            {
                Console.WriteLine(part.Value);
            }
            
        }
    }
}