using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    public class Tuple<T, T2>
    {

        public Tuple(T first, T2 second)

        {
            Weight = first;
            Route = second;
        }

        public T Weight { get; set; }
        public T2 Route { get; set; }

    }
}
