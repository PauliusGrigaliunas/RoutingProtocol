using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    class WeightedEdge<T>
    {
        int _weight;

        Vertex<T> _start;
        Vertex<T> _end;

        public int Weight { get { return _weight; } }

        public Vertex<T> Start { get { return _start; } }
        public Vertex<T> End { get { return _end; } }

        public WeightedEdge(Vertex<T> start, Vertex<T> end, int weight)
        {
            _start = start;
            _end = end;
            _weight = weight;
            start.AddEdge(end);
        }


        public override string ToString()
        {
            return string.Format("{0}-- {1} -->{2}", _start.Value, _weight, _end.Value);
        }
    }
}
