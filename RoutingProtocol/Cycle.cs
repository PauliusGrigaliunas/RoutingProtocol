using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    public class Cycle<T>
    {
        List<Vertex<T>> _vertices;
        UndirectedGenericGraph<T> _graph;


        public Cycle(UndirectedGenericGraph<T> graph, List<Vertex<T>> vertices)
        {
            _vertices = vertices;
            _graph = graph;
        }
        public void Runner()
        {
            Thread task = new Thread(() => Loop());
            task.Start();
        }

        public void Loop()
        {
            while (true)
            {
                _graph.RebootConnectionRoutes();
                _graph.AddressMenu(); ;
                Thread.Sleep(2000);
            }

        }
    }
}
