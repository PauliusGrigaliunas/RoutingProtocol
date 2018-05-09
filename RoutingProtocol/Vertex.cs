using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    class Vertex<T>
    {

        List<Vertex<T>> _neighbors;
        T _value;
        bool _isVisited;

        public List<Vertex<T>> Neighbors { get { return _neighbors; } set { _neighbors = value; } }
        public T Value { get { return _value; } set { _value = value; } }
        public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
        public int NeighborsCount { get { return _neighbors.Count; } }

        public Vertex(T value)
        {
            _value = value;
            _isVisited = false;
            _neighbors = new List<Vertex<T>>();
        }


        public Vertex(T value, List<Vertex<T>> neighbors)
        {
            _value = value;
            _isVisited = false;
            _neighbors = neighbors;
        }

        public void Visit()
        {
            _isVisited = true;
        }
        public void UndoVisit()
        {
            _isVisited = false;
        }

        public void AddEdge(Vertex<T> vertex)
        {
            _neighbors.Add(vertex);
            vertex.AddEdgeBack(this);
        }

        public void AddEdges(List<Vertex<T>> newNeighbors)
        {
            _neighbors.AddRange(newNeighbors);

            foreach (Vertex<T> newNeighbor in newNeighbors) {
                newNeighbor.AddEdgeBack(this);
            }
        }
        private void AddEdgeBack(Vertex<T> vertex)
        {
            _neighbors.Add(vertex);
        }

        public void RemoveEdge(Vertex<T> vertex)
        {
            _neighbors.Remove(vertex);
            vertex.RemoveEdgeBack(this);
        }

        private void RemoveEdgeBack(Vertex<T> vertex)
        {
            _neighbors.Remove(vertex);
        }


        public void Reach(Vertex<T> vertex)
        {


        }

        public override string ToString()
        {

            StringBuilder allNeighbors = new StringBuilder("");
            allNeighbors.Append(_value + ": ");

            foreach (Vertex<T> neighbor in _neighbors)
            {
                allNeighbors.Append(neighbor._value + "  ");
            }

            return allNeighbors.ToString();

        }

    }
}
