using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q1MazeExit : Processor
    {
        public Q1MazeExit(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);

        public long Solve(long nodeCount, long[][] edges, long StartNode, long EndNode)
        {
            bool isFind=false;
            long[] visited=new long[nodeCount];
            visited[StartNode-1]=1;
            long[][] adj=makeAdj(edges,nodeCount);
            explore(adj,visited,StartNode-1,EndNode-1,isFind);
            if (visited[EndNode-1]==0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public long[][] makeAdj(long[][] edges,long nodeCount)
        {
            long[][] result=new long[nodeCount][];
            // List<long[]> result=new List<long[]>();
            for (int i=0;i<nodeCount;i++)
            {
                List<long> listToAdd=new List<long>();
                for (int j=0;j<edges.Length;j++)
                {
                    if (edges[j][0]-1==i)
                    {
                        listToAdd.Add(edges[j][1]-1);
                    }
                    if (edges[j][1]-1==i)
                    {
                        listToAdd.Add(edges[j][0]-1);
                    }
                }
                result[i]=listToAdd.ToArray();
            }
            return result;
        }
        public void explore(long[][] adj,long[] visited,long index,long EndNode,bool isFind)
        {
            if (isFind==false)
            {
                for (int i=0;i<adj[index].Length;i++)
                {
                    if (visited[adj[index][i]]==0)
                    {
                        visited[adj[index][i]]=1;
                        if (adj[index][i]==EndNode)
                        {
                            isFind=true;
                        }
                        explore(adj,visited,adj[index][i],EndNode,isFind);
                    }
                }
            }
        }
    }

}
