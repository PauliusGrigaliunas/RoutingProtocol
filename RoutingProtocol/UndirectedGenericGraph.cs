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
        private Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>> memory;
        private List<Vertex<T>> answer = new List<Vertex<T>>();



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

                foreach (var neighbor in root.Neighbors)
                {
                    DepthFirstSearch(neighbor.Key);
                }
            }
            level--;
            if (level == 0)
            {
                RestoreGraph(root);
                Console.WriteLine();
            }
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
                        Console.Write(neighbor.Key.Value + " ");
                        neighbor.Key.Visit();
                        queue.Enqueue(neighbor.Key);
                    }
                }
            }
            Console.WriteLine();
            RestoreGraph(root);
        }

        public void Search(Vertex<T> root) {

            memory = new Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>>(size);
            foreach (Vertex<T> vertex in vertices)
                memory.Add(vertex, new Tuple<int, List<Vertex<T>>>(int.MaxValue, new List<Vertex<T>>()));

            memory[root] = new Tuple<int, List<Vertex<T>>>(0, new List<Vertex<T>>(new Vertex<T>[] { root }));

            Queue<KeyValuePair<Vertex<T>, int>> queue = new Queue<KeyValuePair<Vertex<T>, int>>();


            queue.Enqueue(new KeyValuePair<Vertex<T>, int>(root, 0));

            while (queue.Count > 0)
            {

                KeyValuePair<Vertex<T>, int> current = queue.Dequeue();

                foreach (var part in current.Key.Neighbors)
                {
                    if (memory[part.Key].Weight > part.Value + current.Value)
                    {
                        memory[part.Key].Weight = part.Value + current.Value;

                        List<Vertex<T>> pathe = new List<Vertex<T>>();
                        pathe.AddRange(memory[current.Key].Route);
                        pathe.Add(part.Key);
                        memory[part.Key].Route = pathe;

                        queue.Enqueue(new KeyValuePair<Vertex<T>, int>(part.Key, part.Value + current.Value));
                    }
                }
                
            }
            root.Connections = memory;
        }
        public void AddressMenu() {
            foreach (var vertex in vertices)
            {
                Search(vertex);
            }
            
            foreach (var vertex in vertices)
            {
                Console.WriteLine("/////////////////////"+ vertex.Value +"/////////////////////////////");
                foreach (var connection in vertex.Connections)
                {
                    Console.WriteLine(vertex.Value + " --- (" + connection.Value.Weight + ") ---> " + connection.Key.Value);

                    foreach (var step in connection.Value.Route)
                        Console.Write(step.Value + " :: ");
                        Console.WriteLine("\n");

                }
            }

        }
        public void AddressTable(Vertex<T> root)
        {
            Search(root);

            Console.WriteLine("/////////////////////" + root.Value + "/////////////////////////////");
            foreach (var connection in root.Connections)
            {
                Console.WriteLine(root.Value + " --- (" + connection.Value.Weight + ") ---> " + connection.Key.Value);

                foreach (var step in connection.Value.Route)
                    Console.Write(step.Value + " :: ");
                Console.WriteLine("\n");

            }

        }

        public List<Vertex<T>> AdressReach(Vertex<T> root, Vertex<T> vertex)
        {
            Search(root);

            foreach (var step in root.Connections[vertex].Route)
                Console.Write(step.Value + " :: ");

            return root.Connections[vertex].Route;
        }

    }
}