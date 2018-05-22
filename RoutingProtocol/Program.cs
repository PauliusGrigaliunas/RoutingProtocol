using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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




            vertices[0].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[1], vertices[2] , vertices[3], vertices[4], vertices[5]
            }
            ), new List<int>(new int[] { 2, 1, 1, 1, 1 }));

            vertices[1].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[2], vertices[3], vertices[4]
            }), new List<int>(new int[] { 1, 1, 1, 1, 1 }));

            vertices[2].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[3], vertices[7], vertices[8]
            }), new List<int>(new int[] { 1, 1, 1 }));

            vertices[3].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[4]
            }), new List<int>(new int[] { 1 }));

            vertices[5].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[6], vertices[7]
            }), new List<int>(new int[] { 1, 1 }));

            vertices[7].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }), new List<int>(new int[] { 1 }));

            vertices[8].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[9], vertices[10]
            }), new List<int>(new int[] { 1, 1 }));

            vertices[9].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[10]
            }), new List<int>(new int[] { 1 }));
            vertices[10].AddEdges(new List<Vertex<string>>(new Vertex<string>[]
            {
            vertices[11]
            }), new List<int>(new int[] { 1 }));

            //vertices[5].AddEdge(vertices[8], 1);
            //vertices[2].RemoveEdge(vertices[8]);

            UndirectedGenericGraph<string> Graph = new UndirectedGenericGraph<string>(vertices);
            new Cycle<string>(Graph, vertices).Runner();


            Console.WriteLine(Graph.AdressReach(vertices[0], vertices[5]));

            Thread.Sleep(100);

            vertices[0].RemoveEdge(vertices[5]);

            Thread.Sleep(100);

            Console.WriteLine(Graph.AdressReach(vertices[0], vertices[5]));

            Thread.Sleep(2000);

            Console.WriteLine(Graph.AdressReach(vertices[0], vertices[5]));



        }
    }
}
