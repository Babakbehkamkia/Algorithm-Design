using System;
using System.Collections.Generic;
using TestCommon;

namespace A2
{
    public class Q1ShortestPath : Processor
    {
        public Q1ShortestPath(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long,long[][], long, long, long>)Solve);
        
        public long Solve(long NodeCount, long[][] edges, long StartNode,  long EndNode)
        {
            
            long[] parent=new long[NodeCount];
            long[] visited=new long[NodeCount];
            List<long>[] adj =makeAdj(edges,NodeCount);
            Queue<long> q=new Queue<long>();
            StartNode-=1;
            EndNode-=1;
            q.Enqueue(StartNode);
            visited[StartNode]=1;
            parent[StartNode]=-1;
            bool found=false;
            while (q.Count!=0)
            {
                long current=q.Dequeue();
                foreach(long item in adj[current])
                {

                    if(visited[item]==0)
                    {
                        parent[item]=current;
                        visited[item]=1;
                        if (item==EndNode)
                        {
                            found=true;
                            break;
                        }
                        q.Enqueue(item);
                    }
                }
                if (found)
                {
                    break;
                }
                
            }
            if (visited[EndNode]==1)
            {
                long count=0;
                long current=EndNode;
                while(parent[current]!=-1)
                {
                    count+=1;
                    current=parent[current];
                }
                return count;
            }
            else
            {
                return -1;
            }
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
