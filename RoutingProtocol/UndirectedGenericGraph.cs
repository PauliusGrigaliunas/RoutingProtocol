using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    public struct Chain<T>
    {
        T _value;
        private Vertex<T> _vertex;
        private int _weight;
        private List<Vertex<T>> _path;

        public T Value { get { return _value; } set { _value = value; } }
        public Vertex<T> Vertex { get { return _vertex; } set { _vertex = value; } }
        public int Weight { get { return _weight; } set { _weight = value; } }
        public List<Vertex<T>> Path { get { return _path; } set { _path = value; } }

        public Chain(Vertex<T> vertex, int weight, List<Vertex<T>> path) : this()
        {
            _vertex = vertex;
            _weight = weight;
            _path.AddRange(path);
        }
    }

    class UndirectedGenericGraph<T>
    {

        public List<Chain<T>> chain = new List<Chain<T>>();

        private Dictionary<Vertex<T>, int> memo;
        private Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>> memory;
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

            memory = new Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>>(initialSize);
            foreach (Vertex<T> vertex in vertices) memory.Add(vertex, new Tuple<int, List<Vertex<T>>>(int.MaxValue, new List<Vertex<T>>()));
        }

        public UndirectedGenericGraph(List<Vertex<T>> initialNodes)
        {
            vertices = initialNodes;
            size = vertices.Count;
            memo = new Dictionary<Vertex<T>, int>();
            foreach (Vertex<T> vertex in vertices) memo.Add(vertex, int.MaxValue);

            memory = new Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>>();
            foreach (Vertex<T> vertex in vertices) memory.Add(vertex, new Tuple<int, List<Vertex<T>>>(int.MaxValue, new List<Vertex<T>>()));
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
        /*
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

        }*/



        public List<Vertex<T>> Reach2(Vertex<T> root, Vertex<T> vertex)
        {

            List<Vertex<T>> path = new List<Vertex<T>>();
            int weight = 0;
            memo[root] = 0;
            path.Add(root);


            Queue<KeyValuePair<Vertex<T>, int>> queue = new Queue<KeyValuePair<Vertex<T>, int>>();
            queue.Enqueue(new KeyValuePair<Vertex<T>, int>(root, weight));


            while (queue.Count > 0)
            {

                KeyValuePair<Vertex<T>, int> current = queue.Dequeue();

                foreach (var part in current.Key.Neighbors)
                {

                    if (memo[part.Key] > part.Value + current.Value)
                    {
                        memo[part.Key] = part.Value + current.Value;
                        queue.Enqueue(new KeyValuePair<Vertex<T>, int>(part.Key, part.Value + current.Value));
                    }
                }
            }


            foreach (var m in memo) Console.WriteLine(m.Key.Value + " " + m.Value);
            Console.WriteLine();

            return null;
        }


        public List<Vertex<T>> Reach1(Vertex<T> root, Vertex<T> vertex)
        {

             List<Vertex<T>> path = new  List<Vertex<T>>();
            path.Add(root);


            memory[root] = new Tuple<int, List<Vertex<T>>>(0, path);

            Console.WriteLine(memory[root].Weight);

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
                        pathe.AddRange(memory[current.Key].Value);
                        pathe.Add(part.Key);
                        memory[part.Key].Value = pathe;


                        queue.Enqueue(new KeyValuePair<Vertex<T>, int>(part.Key, part.Value + current.Value));
                    }
                }
            }


            foreach (var m in memory)
            {
                Console.WriteLine(m.Key.Value + " " + m.Value.Weight);
                foreach (var n in m.Value.Value)
                {
                    Console.WriteLine(" ++++" + n.Value + " ");
                }
            }

            Console.WriteLine();

            return null;
        }

    }
}