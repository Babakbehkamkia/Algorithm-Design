using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q2BipartiteGraph : Processor
    {
        public Q2BipartiteGraph(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long NodeCount, long[][] edges)
        {
            long[] visited=new long[NodeCount];
            List<long>[] adj =makeAdj(edges,NodeCount);
            Queue<long> q=new Queue<long>();
            long[] order=new long[NodeCount];
            for(int i=0;i<NodeCount;i++)
            {
                order[i]=-1;
            }
            for(int i=0;i<NodeCount;i++)
            {
                if(visited[i]==0)
                {
                    q.Enqueue(i);
                    visited[i]=1;
                    order[i]=0;
                    while(q.Count!=0)
                    {
                        long node=q.Dequeue();
                        foreach(long item in adj[node])
                        {
                            if (visited[item]==1 && order[node]%2==order[item]%2)
                            {
                                return 0;
                            }
                            if (visited[item]==0)
                            {
                                visited[item]=1;
                                order[item]=order[node]+1;
                                q.Enqueue(item);
                            }
                        }
                    }
                }
            }
            return 1;
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
            for(long i=0;i<nodeCount;i++)
            {
                result[i] = new List<long>();
            }
            foreach(long[] x in edges)
            {
                result[x[0]-1].Add(x[1]-1);
                result[x[1]-1].Add(x[0]-1);
            }
            return result;
        }
    }
}
