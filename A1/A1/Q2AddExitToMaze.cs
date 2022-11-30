using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A12
{
    public class Q2AddExitToMaze : Processor
    {
        public Q2AddExitToMaze(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            // bool isFind=false;
            long[] visited=new long[nodeCount];
            long count=0;
            Dictionary<long,List<long>> adj=makeAdj(edges,nodeCount);
            // visited[StartNode-1]=1;
            for (int i=0;i<nodeCount;i++)
            {
                if (visited[i]==0)
                {
                    visited[i]=1;
                    BFS(adj,visited,i);
                    count++;
                }
            }
            return count;
        }
        public Dictionary<long,List<long>> makeAdj(long[][] edges,long nodeCount)
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
            //         if (edges[j][1]-1==i)
            //         {
            //             listToAdd.Add(edges[j][0]-1);
            //         }
            //     }
            //     result[i]=listToAdd.ToArray();
            // }
            // return result;




            // List<long>[] result =new List<long>[nodeCount];
            // for(long i=0;i<nodeCount;i++)
            // {
            //     result[i] = new List<long>();
            // }
            // foreach(long x in edges)
            // {
            //     result[x[0]-1].Add(x[1]-1);
            //     result[x[1]-1].Add(x[0]-1);
            // }
            // return result;



            Dictionary<long,List<long>> result=new Dictionary<long,List<long>>();
            for (int i = 0; i < edges.Length; i++)
            {
                long[] val = new long[2]{edges[i][0]-1,edges[i][1]-1};

                if (!result.ContainsKey(val[0]))
                {
                    result.Add(val[0], new List<long>());
                }
                if (!result.ContainsKey(val[1]))
                {
                    result.Add(val[1], new List<long>());
                }

                result[val[0]].Add(val[1]);
                result[val[1]].Add(val[0]);
            }
            return result;
        }
        // public void explore(Dictionary<long,List<long>> adj,long[] visited,long index)
        // {
        //     for (int i=0;i<adj[index].Count;i++)
        //     {
        //         if (visited[adj[index][i]]==0)
        //         {
        //             visited[adj[index][i]]=1;
        //             explore(adj,visited,adj[index][i]);
        //         }
        //     }
        // }
        public void BFS(Dictionary<long,List<long>> adj,long[] visited,long index)
        {
            Queue<long> q=new Queue<long>();
            q.Enqueue(index);
            while(q.Count!=0)
            {
                long current=q.Dequeue();
                if(adj.ContainsKey(current))
                {
                    foreach(long item in adj[current])
                    {
                        if(visited[item]==0)
                        {
                            visited[item]=1;
                            q.Enqueue(item);
                        }
                    }
                }
            }
        }
    }
}
