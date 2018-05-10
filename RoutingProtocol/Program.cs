using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutingProtocol
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vertex<string>> vertices = new List<Vertex<string>>
            (
                 new Vertex<string>[]
            {
                new Vertex<string>("Router A"), //0
                new Vertex<string>("Router B"), //1
                new Vertex<string>("Router C"), //2
                new Vertex<string>("Router D")  //3
            }
            );

            vertices[0].AddWeightedEdge(vertices[1], 2);
            vertices[0].AddWeightedEdge(vertices[2], 3);

            vertices[3].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            { vertices[0], vertices[1] , vertices[2] }
            ), new List<int>(new int[] { 2, 3, 7 }));

            foreach (WeightedEdge<string> b in vertices[0].Edges) {
                Console.WriteLine(b.ToString());
            }

            Console.WriteLine(); 
            foreach (WeightedEdge<string> b in vertices[1].Edges)
            {
                Console.WriteLine(b.ToString());
            }
            Console.WriteLine();

            vertices[0].RemoveWeightedEdge(vertices[1]);


            foreach (WeightedEdge<string> b in vertices[3].Edges)
            {
                Console.WriteLine(b.ToString());
            }



















            /*List<Vertex<string>> vertices = new List<Vertex<string>>
            (
                new Vertex<string>[]
                    {
                new Vertex<string>("Router A"), //0
                new Vertex<string>("Router B"), //1
                new Vertex<string>("Router C"), //2
                new Vertex<string>("Router D"), //3
                new Vertex<string>("Router E"), //4
                new Vertex<string>("Router F"), //5
                new Vertex<string>("Router G"), //6
                new Vertex<string>("Router H"), //7
                new Vertex<string>("Router I"), //8
                new Vertex<string>("Router J"), //9
                new Vertex<string>("Router K"), //10
                new Vertex<string>("Router L")  //11
                    }
            );
            */
            /*vertices[0].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[2] , vertices[3], vertices[4], vertices[5]
            }
            ));

            vertices[1].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[2], vertices[3], vertices[4]
            }));

            vertices[2].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[3], vertices[7], vertices[8]
            }));

            vertices[3].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[4]
            }));

            vertices[5].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[6], vertices[7]
            }));
            vertices[7].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }));
            vertices[8].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[9], vertices[10]
            }));
            vertices[9].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }));
            vertices[10].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[11]
            }));*/

            //vertices[?].AddEdge(vertices[?]);
            //vertices[?].RemoveEdge(vertices[?]);

            /*
            UndirectedGenericGraph<string> testGraph = new UndirectedGenericGraph<string>(vertices);

            Console.WriteLine(vertices[0].ToString());

            // Check to see that all neighbors are properly set up
            foreach (Vertex<string> vertex in vertices)
            {
                Console.WriteLine(vertex.ToString());
            }

            // Test searching algorithms
            testGraph.DepthFirstSearch(vertices[0]);
            Console.WriteLine();
            testGraph.BreadthFirstSearch(vertices[0]);
            Console.WriteLine();
            testGraph.Reach(vertices[0], vertices[5]);
            */

        }
    }
}
