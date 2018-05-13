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
        private Dictionary<Vertex<T>, Tuple<int, List<Vertex<T>>>> memory;
        private List<Vertex<T>> vertices;
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

        private void Search (Vertex<T> root) {

            List<Vertex<T>> path = new List<Vertex<T>>();
            path.Add(root);

            memory[root] = new Tuple<int, List<Vertex<T>>>(0, path);

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

        }
        public List<Vertex<T>> AdressReach(Vertex<T> root, Vertex<T> vertex)
        {
            Search(root);
            answer = memory[vertex].Value;
            return memory[vertex].Value;
        }


        public void AddressTable(Vertex<T> root)
        {
            Search(root);

            foreach (var mem in memory) {
                Console.WriteLine(root.Value + " --- (" + mem.Value.Weight + ") ---> " + mem.Key.Value);
                foreach (var val in mem.Value.Value)
                    Console.Write(val.Value + "  :: ");
                Console.WriteLine("\n");
            }
            
        }

        public override string ToString()
        {
            try
            {
                var last = answer.Count - 1;
                StringBuilder stringBuilder = new StringBuilder("");
                stringBuilder.AppendLine(answer[0].Value + " --- (" + memory[answer[last]].Weight + ") ---> " + answer[last].Value);
                foreach (var ans in answer)
                {
                    stringBuilder.Append(ans.Value + "  ::  ");
                }
                stringBuilder.AppendLine();

                return stringBuilder.ToString();
            }
            catch (Exception) { 
            return "nebuvo bandyta prisijungti! \n";};

        }
    }
}