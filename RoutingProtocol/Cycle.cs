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
            _graph.ClearConnectionRoutes();
            _graph.Routing();

            Thread.Sleep(2000);

            _vertices[0].RemoveEdge(_vertices[1]);
            _vertices[0].RemoveEdge(_vertices[2]);
            _vertices[0].RemoveEdge(_vertices[3]);
            _vertices[0].RemoveEdge(_vertices[4]);
            _vertices[0].RemoveEdge(_vertices[5]);

            while (true)
            {
                _graph.ClearConnectionRoutes();
                Thread.Sleep(2000);
            }

        }
    }
}
