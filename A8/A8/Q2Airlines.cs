using System;
using System.Collections.Generic;
using TestCommon;

namespace A8
{
    public class Q2Airlines : Processor
    {
        public Q2Airlines(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, long, long[][], long[]>)Solve);

        public virtual long[] Solve(long flightCount, long crewCount, long[][] info)
        {
            // write your code here
            // throw new NotImplementedException();
            long[] match =new long[flightCount];
            long nodeCount=flightCount+crewCount+2;
            Dictionary<long,long>[] adj=makeAdj(flightCount,crewCount,info);
            while(true)
            {
                List<long> path=BFS(flightCount,nodeCount,adj,match);
                
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
            }
            for(int i=0;i<flightCount;i++)
            {
                if(match[i]==0)
                {
                    match[i]=-1;
                }
            }
            return match;
        }  
        public Dictionary<long,long>[] makeAdj(long flightCount,long crewCount,long[][] edges)
        {
            long nodeCount=flightCount+crewCount;
            Dictionary<long,long>[] adj=new Dictionary<long, long>[nodeCount+2];
            for(int i=0;i<nodeCount+2;i++)
            {
                adj[i]=new Dictionary<long, long>();
            }
            for(int i=1;i<flightCount+1;i++)
            {
                adj[0][i]=1;
            }
            for(int i=(int)flightCount+1;i<nodeCount+1;i++)
            {
                adj[i][nodeCount+1]=1;
            }
            for (int i=0;i<flightCount;i++)
            {
                for(int j=0;j<crewCount;j++)
                {
                    if(edges[i][j]==1)
                    {
                        adj[i+1][flightCount+j+1]=1;
                    }
                }
            }
            return adj;
        }
        public List<long> BFS(long flightCount,long nodeCount,Dictionary<long,long>[] adj,long[] match)
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
            for(int i=0;i<(path.Count-1)/2;i++)
            {
                match[path[2*i]-1]=path[2*i+1]-flightCount;
            }
            
            return path;
        }
    }
}
