using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;
using TestCommon;

namespace A3
{
    public class Q4FriendSuggestion:Processor
    {
        public Q4FriendSuggestion(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long,long[][], long[]>)Solve);

        public long[] Solve(long NodeCount, long EdgeCount, 
                            long[][] edges, long QueriesCount, 
                            long[][] Queries)
        {
            // Write your code here.
            List<long[]>[] forwardAdj=new List<long[]>[NodeCount];
            List<long[]>[] backwardAdj=new List<long[]>[NodeCount];
            for(int i=0;i<NodeCount;i++)
            {
                forwardAdj[i]=new List<long[]>();
                backwardAdj[i]=new List<long[]>();
            }
            foreach(long[] item in edges)
            {
                forwardAdj[item[0]-1].Add(new long[2]{item[1]-1,item[2]});
                backwardAdj[item[1]-1].Add(new long[2]{item[0]-1,item[2]});
            }
            long[] result=new long[QueriesCount];
            for(int i=0;i<QueriesCount;i++)
            {
                result[i]=bidirectionalDijkstra(NodeCount,forwardAdj,backwardAdj,Queries[i][0]-1,Queries[i][1]-1);
            }
            return result;
        }
        // public List<long[]>[] makeAdj(long NodeCount,long[][] edges)
        // {
        //     List<long[]>[] adj=new List<long[]>[NodeCount];
        //     for(int i=0;i<NodeCount;i++)
        //     {
        //         adj[i]=new List<long[]>();
        //     }
        //     foreach(long[] item in edges)
        //     {
        //         if(forward)
        //         {
        //             adj[item[0]-1].Add(new long[2]{item[1]-1,item[2]});
        //         }
        //         else
        //         {
        //             adj[item[1]-1].Add(new long[2]{item[0]-1,item[2]});
        //         }
        //     }
        //     return adj;
        // }
        public long bidirectionalDijkstra(long NodeCount,List<long[]>[] forwardAdj,
                                        List<long[]>[] backwardAdj,long startNode,long endNode)
        {
            if(startNode==endNode)
            {
                return 0;
            }
            SimplePriorityQueue<long> forwardPQ = new SimplePriorityQueue<long>();
            SimplePriorityQueue<long> backwardPQ = new SimplePriorityQueue<long>();
            for(int i=0;i<NodeCount;i++)
            {
                forwardPQ.Enqueue(i,float.MaxValue);
                backwardPQ.Enqueue(i,float.MaxValue);
            }
            forwardPQ.UpdatePriority(startNode,0);
            backwardPQ.UpdatePriority(endNode,0);
            // forwardPQ.Enqueue(startNode,0);
            // backwardPQ.Enqueue(endNode,0);
            long[] forwardVisited=new long[NodeCount];
            long[] backwardVisited=new long[NodeCount];
            // long[] forwardAdded=new long[NodeCount];
            // long[] backwardAdded=new long[NodeCount];
            // forwardVisited[startNode]=1;
            // backwardVisited[endNode]=1;
            // forwardAdded[startNode]=1;
            // backwardAdded[endNode]=1;
            float[] forwardCosts=new float[NodeCount];
            float[] backwardCosts=new float[NodeCount];
            for(int i=0;i<NodeCount;i++)
            {
                forwardCosts[i]=float.MaxValue;
                backwardCosts[i]=float.MaxValue;
            }
            while(true)
            {
                float forwardCost=forwardPQ.GetPriority(forwardPQ.First);
                if (forwardCost == float.MaxValue)
                {
                    return -1;
                }
                float backwardCost=backwardPQ.GetPriority(backwardPQ.First);
                if (backwardCost == float.MaxValue)
                {
                    return -1;
                }
                long forwardNode=forwardPQ.Dequeue();
                forwardCosts[forwardNode]=forwardCost;
                long backwardNode=backwardPQ.Dequeue();
                backwardCosts[backwardNode]=backwardCost;
                
                
                
                forwardVisited[forwardNode]=1;
                backwardVisited[backwardNode]=1;
                // long maxItter=Math.Max(forwardAdj[forwardNode].Count,backwardAdj[backwardNode].Count);
                foreach(long[] item in forwardAdj[forwardNode])
                {
                    if(forwardVisited[item[0]]==1)
                    {
                        continue;
                    }
                    if(item[1]+forwardCost<forwardPQ.GetPriority(item[0]))
                    {
                        forwardPQ.UpdatePriority(item[0],item[1]+forwardCost);
                        forwardCosts[item[0]]=item[1]+forwardCost;
                    }
                }
                if(backwardVisited[forwardNode]==1)
                {
                    return final(forwardNode,forwardCosts,backwardCosts,forwardAdj);
                }
                foreach(long[] item in backwardAdj[backwardNode])
                {
                    // if(backwardVisited[item[0]]==1)
                    // {
                    //     return final();
                    // }
                    if(backwardVisited[item[0]]==1)
                    {
                        continue;
                    }
                    if(item[1]+backwardCost<backwardPQ.GetPriority(item[0]))
                    {
                        backwardPQ.UpdatePriority(item[0],item[1]+backwardCost);
                        backwardCosts[item[0]]=item[1]+backwardCost;
                    }
                }
                if(forwardVisited[backwardNode]==1)
                {
                    return final(backwardNode,forwardCosts,backwardCosts,forwardAdj);
                }
            }
        }
        public long final(long sharedNode,float[] forwardCosts,float[] backwardCosts,List<long[]>[] forwardAdj)
        {
            float minDistance=float.MaxValue;
            for (int i=0;i<forwardCosts.Length;i++)
            {
                if ((float)forwardCosts[i]+(float)backwardCosts[i]<minDistance)
                    minDistance=forwardCosts[i]+backwardCosts[i];
            }
            return (long)minDistance;
        }
    }
}
