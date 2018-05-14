using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    public class Cycle<T>
    {

        UndirectedGenericGraph<T> _graph;


        public Cycle(UndirectedGenericGraph<T> graph)
        {
            _graph = graph;
        }


        

    }
}
