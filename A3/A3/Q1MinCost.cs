using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using TestCommon;

namespace A3
{
    public class Q1MinCost : Processor
    {
        public Q1MinCost(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, long, long>)Solve);


        public long Solve(long nodeCount, long[][] edges, long startNode, long endNode)
        {
            //Write Your Code Here
            List<long>[] cost=new List<long>[nodeCount];
            for (int i=0;i<nodeCount;i++)
            {
                cost[i]=new List<long>();
            }
            List<long>[] adj =MakeAdj(nodeCount,edges,cost);
            return dijkstra(nodeCount,adj,cost,startNode-1,endNode-1);
        }
        public long dijkstra(long n,List<long>[] adj,List<long>[] cost,long startNode,long endNode)
        {
            long[] dist = new long[n];
            for(int j=0 ; j<n;j++)
            {
                dist[j]=int.MaxValue;
            }
            dist[startNode]=0;
            SimplePriorityQueue<long> pq = new SimplePriorityQueue<long>();
            for (int j= 0;j<n;j++)
            {
                pq.Enqueue(j,dist[j]);
            }
            while(pq.Count!=0)
            {
                long currentNode = pq.Dequeue();
                for (int j=0;j<adj[currentNode].Count;j++)
                {
                    if(dist[adj[currentNode][j]]>dist[currentNode] + cost[currentNode][j])
                    {
                        dist[adj[currentNode][j]] =dist[currentNode] + cost[currentNode][j];
                        pq.UpdatePriority(adj[currentNode][j],dist[adj[currentNode][j]]);
                    }
                }
            }
            if(dist[endNode]==int.MaxValue)
            {
                return -1;
            }
            return dist[endNode];
        }
        public List<long>[] MakeAdj(long nodeCount, long[][] edges,List<long>[] cost)
        {
            List<long>[] result = new List<long>[nodeCount];
            for(int i = 0 ; i<nodeCount;i++)
            {
                result[i]=new List<long>();
            }
            foreach(long[] item in edges)
            {
                result[item[0]-1].Add(item[1]-1);
                cost[item[0]-1].Add(item[2]);
                // result[item[1]-1].Add(item[0]-1);
                // cost[item[1]-1].Add(item[2]);
            }
            return result;
        }
    }
}
