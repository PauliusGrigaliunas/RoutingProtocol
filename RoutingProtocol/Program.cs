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


            //vertices[?].AddEdge(vertices[x]);
            //vertices[?].AddWeightedEdge(vertices[x], y);
            //vertices[?].RemoveEdge(vertices[x]);

            vertices[0].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[2] , vertices[3], vertices[4], vertices[5]
            }
            ), new List<int>(new int[] { 2, 1, 1, 1, 1 }));

            vertices[1].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[2], vertices[3], vertices[4]
            }), new List<int>(new int[] { 1, 1, 1, 1, 1 }));

            vertices[2].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[3], vertices[7], vertices[8]
            }),new List<int>(new int[] { 1, 1, 1 }));

            vertices[3].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[4]
            }),new List<int>(new int[] { 1 }));

            vertices[5].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[6], vertices[7]
            }), new List<int>(new int[] { 1, 1 }));

            vertices[7].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }), new List<int>(new int[] { 1 }));

            vertices[8].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[9], vertices[10]
            }), new List<int>(new int[] { 1, 1 }));

            vertices[9].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }), new List<int>(new int[] { 1 }));
            vertices[10].AddWeightedEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[11]
            }), new List<int>(new int[] { 1 }));



            foreach (Vertex <string> vertex in vertices) {
                Console.WriteLine(vertex.ToString());

            }

            foreach (Vertex<string> vertex in vertices) { 

                foreach (WeightedEdge<string> b in vertex.Edges) {
                Console.WriteLine(b.ToString());
                }
            }
                
            
            
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
            testGraph.Reach(vertices[0], vertices[1]);
        //testGraph.Reach1(vertices[0], vertices[1]);
    }
}
}
