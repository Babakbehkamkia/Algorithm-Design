using System;
using System.Collections.Generic;
using Priority_Queue;
using TestCommon;

namespace E1
{
    public class Q1Betweenness : Processor
    {
        public Q1Betweenness(string testDataName) : base(testDataName)
        {
            //this.ExcludeTestCaseRangeInclusive(2, 50);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long[]>)Solve);


        public long[] Solve(long NodeCount, long[][] edges)
        {
            long[] ans=new long[NodeCount];
            List<long>[] adj=makeAdj(NodeCount,edges);
            
            for(int i=0;i<NodeCount;i++)
            {
                for(int j=0;j<NodeCount;j++)
                {
                    if(i==j)
                    {
                        continue;
                    }
                    List<long> path=BFS(NodeCount,adj,i,j);
                    if(path==null)
                    {
                        continue;
                    }
                    foreach(long item in path)
                    {
                        ans[item]+=1;
                    }
                }
            }
            return ans;
        }
        public List<long> BFS(long nodeCount,List<long>[] adj,long startNode,long endNode)
        {
            Queue<long> q=new Queue<long>();
            long[] cost=new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                cost[i]=int.MaxValue;
            }
            long[] visited=new long[nodeCount];
            List<long> path=new List<long>();
            long[] parent=new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                parent[i]=-1;
            }
            q.Enqueue(startNode);
            while(q.Count!=0)
            {
                long currentNode=q.Dequeue();
                visited[currentNode]=1;
                foreach(long item in adj[currentNode])
                {
                    if (cost[item] == int.MaxValue)
                    {
                        q.Enqueue(item);
                        cost[item] = cost[currentNode] + 1;
                        parent[item] = currentNode ;
                    }
                    if(item==endNode)
                    {
                        return findingPath(parent,startNode,endNode,path);
                    }
                    
                }
            }
            return null;
            
            

        }
        public List<long> findingPath(long[] parent,long startNode,long endNode,List<long> path)
        {
            while(parent[endNode]!=startNode && parent[endNode]!=-1)
            {
                endNode=parent[endNode];
                path.Add(endNode);
            }
            return path;
        }
        public void Prim(long nodeCount,List<long>[] adj,long root,long[] ans)
        {
            long[] parent=new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                parent[i]=-1;
            }
            List<long>[] result=new List<long>[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                result[i]=new List<long>();
            }
            long[] visited=new long[nodeCount];
            SimplePriorityQueue<long> pq = new SimplePriorityQueue<long>();
            for(int i=(int)nodeCount-1;i>-1;i--)
            {
                pq.Enqueue(-(i+1),float.MaxValue);
            }
            pq.UpdatePriority(-(root+1),0);
            visited[root]=1;
            
            while(pq.Count!=0)
            {
                float currentCost=pq.GetPriority(pq.First);
                long currentNode=-(pq.Dequeue()+1);
                visited[currentNode]=1;
                foreach(long item in adj[currentNode])
                {
                    if(visited[item]==0)
                    {
                        if(pq.GetPriority(-(item+1))>currentCost+1)
                        {
                            pq.UpdatePriority(-(item+1),currentCost+1);
                            parent[item]=currentNode;
                        }
                    }
                }
            }
            long[] rank=new long[nodeCount];
            
            
            for(int i=0;i<nodeCount;i++)
            {
                if(parent[i]!=-1)
                    result[parent[i]].Add(i);
            }
            for(int i=0;i<nodeCount;i++)
            {
                if(result[i].Count==0)
                {
                    rank[i]=0;
                    updateRank(nodeCount,rank,parent,i);
                }
            }
            final(root,ans,result,rank);

        }

        private void updateRank(long nodeCount, long[] rank, long[] parent,long node)
        {
            if(parent[node]!=-1)
            {
                rank[parent[node]]+=rank[node]+1;
                updateRank(nodeCount,rank,parent,parent[node]);
            }
        }

        public void final(long root,long[] ans,List<long>[] result,long[] rank)
        {
            Queue<long> q=new Queue<long>();
            q.Enqueue(root);
            while(q.Count!=0)
            {
                long currentNode=q.Dequeue();
                foreach(long item in result[currentNode])
                {
                    ans[item]+=rank[item];
                    q.Enqueue(item);
                }
            }
            
        }
        public List<long>[] makeAdj(long nodeCount,long[][] edges)
        {
            List<long>[] adj=new List<long>[nodeCount];
            // double[] costs=new double[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                adj[i]=new List<long>();
            }
            foreach(long[] item in edges)
            {
                adj[(int)item[0]-1].Add(item[1]-1);
            }
            for(int i=0;i<nodeCount;i++)
            {
                adj[i].Sort();
                adj[i].Reverse();
            }
            return adj;
        }
    }
}
