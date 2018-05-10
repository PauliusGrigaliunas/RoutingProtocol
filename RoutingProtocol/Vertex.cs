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
        List<WeightedEdge<T>> _edges;
        T _value;
        int _weight;
        bool _isVisited;

        public List<Vertex<T>> Neighbors { get { return _neighbors; } set { _neighbors = value; } }
        public T Value { get { return _value; } set { _value = value; } }
        public int Weight { get { return _weight; } set { _weight = value; } }
        public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
        public int NeighborsCount { get { return _neighbors.Count; } }

        public Vertex(T value, int weight = 0)
        {
            _value = value;
            _weight = weight;
            _isVisited = false;
            _neighbors = new List<Vertex<T>>();
            a = new List<Vertex<T>, int>();
        }


        public Vertex(T value, List<Vertex<T>> neighbors, int weight = 0)
        {
            _value = value;
            _weight = weight;
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
        public void AddWeightedEdge(WeightedEdge<T> edge)
        {
            _edges.Add(edge);
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

    internal class List<T1, T2>
    {

    }
}
