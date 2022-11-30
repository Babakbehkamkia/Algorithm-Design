using System;
using System.Collections.Generic;
using TestCommon;

namespace A12
{
    public class Q3Acyclic : Processor
    {
        public Q3Acyclic(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            long[] visited=new long[nodeCount];
            long[] visited_inverse=new long[nodeCount];
            List<long> post=new List<long>();
            List<long>[] adj_reverse=makeAdj(edges,nodeCount,true);
            List<long>[] adj=makeAdj(edges,nodeCount,false);
            for (int i=0;i<adj_reverse.Length;i++)
            {
                if (visited_inverse[i]==0)
                {
                    visited_inverse[i]=1;
                    explore_inverse(adj_reverse,visited_inverse,i,post);
                }
            }
            
                
            post.Reverse();
            // post.reverse()
            long index=0;
            bool b=false;
            while (index<post.Count)
            {
                visited[post[(int)index]]=1;
                b=explore(adj,visited,post[(int)index]);
                if (b)
                    break;
                index+=1;
            }
            if (b)
                return 1;
            else
                return 0;
        }
        public List<long>[] makeAdj(long[][] edges,long nodeCount,bool inverse)
        {
            // long[][] result=new long[nodeCount][];
            // // List<long[]> result=new List<long[]>();
            // for (int i=0;i<nodeCount;i++)
            // {
            //     List<long> listToAdd=new List<long>();
            //     for (int j=0;j<edges.Length;j++)
            //     {
            //         if (edges[j][0]-1==i && inverse==false)
            //         {
            //             listToAdd.Add(edges[j][1]-1);
            //         }
            //         if (edges[j][1]-1==i && inverse==true)
            //         {
            //             listToAdd.Add(edges[j][0]-1);
            //         }
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
                if (!inverse)
                    result[x[0]-1].Add(x[1]-1);
                else
                    result[x[1]-1].Add(x[0]-1);
            }
            return result;
        }
        public bool explore(List<long>[] adj,long[] visited,long index)
        {
            for (int i=0;i<adj[index].Count;i++)
            {
                if (visited[adj[index][i]]==0)
                {
                    return true;
                }
            }
            return false;
        }
        public void explore_inverse(List<long>[] adj,long[] visited,long index,List<long> post)
        {
            for (int i=0;i<adj[index].Count;i++)
            {
                if (visited[adj[index][i]]==0)
                {
                    visited[adj[index][i]]=1;
                    
                    explore_inverse(adj,visited,adj[index][i],post);
                }
            }
            post.Add(index);
        }
    }
}