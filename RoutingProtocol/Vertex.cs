using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    public class Vertex<T>
    {
        Dictionary<Vertex<T>, int> _neighbors;
        T _value;
        bool _isVisited;

        
        public Dictionary<Vertex<T>, int> Neighbors { get { return _neighbors; } set { _neighbors = value; } }
        public T Value { get { return _value; } set { _value = value; } }
        public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
        public int NeighborsCount { get { return _neighbors.Count; } }

        public Vertex(T value)
        {
            _value = value;
            _isVisited = false;
            _neighbors = new Dictionary<Vertex<T>, int>();
        }


        public Vertex(T value, List<Vertex<T>> neighbors)
        {
            _value = value;
            _isVisited = false;
            _neighbors = neighbors.ToDictionary(x => x , x => 0);
        }

        public void Visit()
        {
            _isVisited = true;
        }
        public void UndoVisit()
        {
            _isVisited = false;
        }

        public void AddEdge(Vertex<T> edge, int weight = 1)
        {
            _neighbors.Add(edge, weight);
            edge._neighbors.Add(this, weight);
        }

        public void AddEdges(List<Vertex<T>> edges, List<int> weights)
        {
            for (int i = 0; i < edges.Count(); i++)
            {
                _neighbors.Add(edges[i], weights[i]);
                edges[i]._neighbors.Add(this, weights[i]);
            }
        }

        public void RemoveEdge(Vertex<T> vertex)
        {
                Neighbors.Remove(vertex);
            vertex.Neighbors.Remove(this);

        }


        public override string ToString()
        {

            StringBuilder allNeighbors = new StringBuilder("");
            allNeighbors.Append(_value + ": ");

            foreach (var neighbor in _neighbors)
            {
                allNeighbors.Append(neighbor.Key.Value + " (" + neighbor.Value + "), ");
            }

            return allNeighbors.ToString();

        }

    }
}
