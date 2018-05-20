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
        System.IO.StreamWriter file;

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
            bool overWrite = false;
            while (true)
            {
                _graph.RebootConnectionRoutes();


                using (file = new System.IO.StreamWriter(@"C:\Users\Paulius\Documents\GitHub\RoutingProtocol\RoutingProtocol\Routes.txt", overWrite))
                {
                    file.WriteLine("time: " + DateTime.Now);
                    file.Write(_graph.AddressMenu());
                }
                overWrite = true;
                Thread.Sleep(2000);
            }

        }
    }
}
