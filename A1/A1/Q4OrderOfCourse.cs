using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestCommon;

namespace A12
{
    public class Q4OrderOfCourse: Processor
    {
        public Q4OrderOfCourse(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);

        public long[] Solve(long nodeCount, long[][] edges)
        {
            long[] visited=new long[nodeCount];
            List<long> post=new List<long>();
            List<long>[] adj=makeAdj(edges,nodeCount);
            for (int i=0;i<adj.Length;i++)
            {
                if (visited[i]==0)
                {
                    visited[i]=1;
                    explore(adj,visited,i,post);
                }
            }
            post.Reverse();
            return post.ToArray();
        }
        public void explore(List<long>[] adj,long[] visited,long index,List<long> post)
        {
            for (int i=0;i<adj[index].Count;i++)
            {
                if (visited[adj[index][i]]==0)
                {
                    visited[adj[index][i]]=1;
                    explore(adj,visited,adj[index][i],post);
                }
            }
            post.Add(index+1);
        }
        public List<long>[] makeAdj(long[][] edges,long nodeCount)
        {
            // long[][] result=new long[nodeCount][];
            // // List<long[]> result=new List<long[]>();
            // for (int i=0;i<nodeCount;i++)
            // {
            //     List<long> listToAdd=new List<long>();
            //     for (int j=0;j<edges.Length;j++)
            //     {
            //         if (edges[j][0]-1==i)
            //         {
            //             listToAdd.Add(edges[j][1]-1);
            //         }
            //         // if (edges[j][1]-1==i)
            //         // {
            //         //     listToAdd.Add(edges[j][0]-1);
            //         // }
            //     }
            //     result[i]=listToAdd.ToArray();
            // }
            // return result;
            List<long>[] result =new List<long>[nodeCount];
            for(var i=0;i<nodeCount;i++)
            {
                result[i] = new List<long>();
            }
            foreach(var x in edges)
            {
                result[x[0]-1].Add(x[1]-1);
            }
            return result;
        }

        public override Action<string, string> Verifier { get; set; } = TopSortVerifier;

        public static void TopSortVerifier(string inFileName, string strResult)
        {
            long[] topOrder = strResult.Split(TestTools.IgnoreChars)
                .Select(x => long.Parse(x)).ToArray();

            long count;
            long[][] edges;
            TestTools.ParseGraph(File.ReadAllText(inFileName), out count, out edges);

            // Build an array for looking up the position of each node in topological order
            // for example if topological order is 2 3 4 1, topOrderPositions[2] = 0, 
            // because 2 is first in topological order.
            long[] topOrderPositions = new long[count];
            for (int i = 0; i < topOrder.Length; i++)
                topOrderPositions[topOrder[i] - 1] = i;
            // Top Order nodes is 1 based (not zero based).

            // Make sure all direct depedencies (edges) of the graph are met:
            //   For all directed edges u -> v, u appears before v in the list
            foreach (var edge in edges)
                if (topOrderPositions[edge[0] - 1] >= topOrderPositions[edge[1] - 1])
                    throw new InvalidDataException(
                        $"{Path.GetFileName(inFileName)}: " +
                        $"Edge dependency violoation: {edge[0]}->{edge[1]}");

        }
    }
}




