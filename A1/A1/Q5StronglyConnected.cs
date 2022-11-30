using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCommon;

namespace A12
{
    public class Q5StronglyConnected: Processor
    {
        public Q5StronglyConnected(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long Solve(long nodeCount, long[][] edges)
        {
            long result=0;
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
            while (index<post.Count)
            {
                if (visited[post[(int)index]]==0)
                {
                    visited[post[(int)index]]=1;
                    explore(adj,visited,post[(int)index]);
                    result++;
                }
                index+=1;
            }
            return result;
        }
        public List<long>[] makeAdj(long[][] edges,long nodeCount,bool reverse)
        {
            
            List<long>[] result =new List<long>[nodeCount];
            for(var i=0;i<nodeCount;i++)
            {
                result[i] = new List<long>();
            }
            foreach(var x in edges)
            {
                if (!reverse)
                    result[x[0]-1].Add(x[1]-1);
                else
                    result[x[1]-1].Add(x[0]-1);
            }
            return result;
        }
        public void explore(List<long>[] adj,long[] visited,long index)
        {
            for (int i=0;i<adj[index].Count;i++)
            {
                if (visited[adj[index][i]]==0)
                {
                    visited[adj[index][i]]=1;
                    explore(adj,visited,adj[index][i]);
                }
            }
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
