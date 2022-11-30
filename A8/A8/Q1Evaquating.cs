using System;
using System.Collections.Generic;
using TestCommon;

namespace A8
{
    public class Q1Evaquating : Processor
    {
        public Q1Evaquating(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long>)Solve);

        public virtual long Solve(long nodeCount, long edgeCount, long[][] edges)
        {
            // write your code here
            // throw new NotImplementedException();
            long maxFlow=0;
            Dictionary<long,long>[] adj=makeAdj(nodeCount,edges);
            while(true)
            {
                List<long> path=BFS(nodeCount,adj);
                
                if(path==null)
                {
                    break;
                }
                long currentNode=0;
                long minimum=long.MaxValue;
                for(int i=0;i<path.Count;i++)
                {
                    if(adj[currentNode][path[i]]<minimum)
                    {
                        minimum=adj[currentNode][path[i]];
                    }
                    currentNode=path[i];
                }
                currentNode=0;
                for(int i=0;i<path.Count;i++)
                {
                    adj[currentNode][path[i]]-=minimum;
                    if(adj[currentNode][path[i]]==0)
                    {
                        adj[currentNode].Remove(path[i]);
                    }
                    if(adj[path[i]].ContainsKey(currentNode))
                    {
                        adj[path[i]][currentNode]+=minimum;
                    }
                    else
                    {
                        adj[path[i]][currentNode]=minimum;
                    }
                    currentNode=path[i];
                }
                maxFlow+=minimum;
            }
            return maxFlow;

        }
        public Dictionary<long,long>[] makeAdj(long nodeCount,long[][] edges)
        {
            Dictionary<long,long>[] adj=new Dictionary<long, long>[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                adj[i]=new Dictionary<long, long>();
            }
            for (int i=0;i<edges.Length;i++)
            {
                if(edges[i][0]-1==edges[i][1]-1)
                {
                    continue;
                }
                if(adj[edges[i][0]-1].ContainsKey(edges[i][1]-1))
                {
                    adj[edges[i][0]-1][edges[i][1]-1]+=edges[i][2];
                }
                else
                {
                    adj[edges[i][0]-1][edges[i][1]-1]=edges[i][2];
                }
                
            }
            return adj;
        }
        public List<long> BFS(long nodeCount,Dictionary<long,long>[] adj)
        {
            long[] visited=new long[nodeCount];
            bool isExist=false;
            long[] parents=new long[nodeCount];
            Queue<long> q=new Queue<long>();
            q.Enqueue(0);
            visited[0]=1;
            while(q.Count!=0 && !isExist)
            {
                long currentNode=q.Dequeue();
                // if(currentNode==nodeCount-1)
                // {
                //     isExist=true;
                //     break;
                // }
                foreach(var item in adj[currentNode].Keys)
                {
                    if(item==nodeCount-1)
                    {
                        isExist=true;
                        parents[item]=currentNode;
                        break;
                    }
                    if(visited[item]==0)
                    {
                        visited[item]=1;
                        q.Enqueue(item);
                        parents[item]=currentNode;
                    }
                }
            }
            
            List<long> path=new List<long>();
            if(isExist==false)
            {
                return null;
            }
            long parent=nodeCount-1;
            while(parent!=0)
            {
                path.Add(parent);
                parent=parents[parent];
            }
            path.Reverse();
            return path;
        }
    }
}
