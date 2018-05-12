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
        bool _isVisited;

        public List<Vertex<T>> Neighbors { get { return _neighbors; } set { _neighbors = value; } }
        public List<WeightedEdge<T>> Edges { get { return _edges; } set { _edges = value; } }
        public T Value { get { return _value; } set { _value = value; } }
        public bool IsVisited { get { return _isVisited; } set { _isVisited = value; } }
        public int NeighborsCount { get { return _neighbors.Count; } }

        public Vertex(T value)
        {
            _value = value;
            _isVisited = false;
            _neighbors = new List<Vertex<T>>();
            _edges = new List<WeightedEdge<T>>();
        }


        public Vertex(T value, List<Vertex<T>> neighbors)
        {
            _value = value;
            _isVisited = false;
            _neighbors = neighbors;
            _edges = new List<WeightedEdge<T>>();
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
            RemoveWeightedEdge(vertex);
            _neighbors.Remove(vertex);
            vertex.RemoveEdgeBack(this);
        }
        private void RemoveEdgeBack(Vertex<T> vertex)
        {
            _neighbors.Remove(vertex);
        }

        public void AddWeightedEdge(Vertex<T> edge, int weight = 0)
        {
            AddEdge(edge);
            _edges.Add(new WeightedEdge<T>(this, edge, weight));
            edge.AddWeightedEdgeBack(this, weight);

        }

        public void AddWeightedEdges(List<Vertex<T>> edges, List<int> weights)
        {    
            for(int i = 0; i < edges.Count(); i++) {
                AddEdge(edges[i]);
                if (weights.Count().Equals(i)) weights.Add(0);
                _edges.Add(new WeightedEdge<T>(this, edges[i], weights[i]));
                edges[i].AddWeightedEdgeBack(this, weights[i]);
            }
        }

        private void AddWeightedEdgeBack(Vertex<T> edge, int weight = 0)
        {
            _edges.Add(new WeightedEdge<T>(this, edge, weight));
        }

        private void RemoveWeightedEdge(Vertex<T> vertex)
        {
            foreach(WeightedEdge<T> Edge in Edges)
                if(Edge.End.Equals(vertex)) {
                    Edges.Remove(Edge);
                    break; }
           
            vertex.RemoveWeightedEdgeBack(this);
        }
        private void RemoveWeightedEdgeBack(Vertex<T> vertex)
        {
            foreach (WeightedEdge<T> Edge in Edges)
                if (Edge.End.Equals(vertex))
                {
                    Edges.Remove(Edge);
                    break;
                }
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
