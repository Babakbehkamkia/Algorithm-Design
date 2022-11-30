using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q3ExchangingMoney : Processor
    {
        public Q3ExchangingMoney(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, string[]>)Solve);

        public string[] Solve(long nodeCount, long[][] edges, long startNode)
        {
            //Write Your Code Here
            long[] distance =new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                distance[i]=long.MaxValue;
            }
            long[] reachable =new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                reachable[i]=0;
            }
            List<long>[] cost =new List<long>[nodeCount];
            for(long i=0;i<nodeCount;i++)
            {
                cost[i] = new List<long>();
            }
            List<long>[] adj=makeAdj(edges,nodeCount,cost);
            long n=adj.Count();

            distance[startNode-1]=0;
            reachable[startNode-1]=1;
            for (int i=0;i<n-1;i++)
            {
                bool isChanged=false;
                for (int j=0;j<n;j++)
                {
                    if (reachable[j] == 1)
                    {
                        for (int k=0;k<adj[j].Count;k++)
                        {
                            if (reachable[adj[j][k]]==0)
                            {
                                distance[adj[j][k]]=distance[j]+cost[j][k];
                                reachable[adj[j][k]]=1;
                                isChanged=true;
                            }
                            else
                            {
                                distance[adj[j][k]]=Math.Min(distance[j]+cost[j][k],distance[adj[j][k]]);
                                isChanged=true;
                            }
                        }
                    }
                }
                if (!isChanged)
                {
                    break;
                }
            }
            long[] shortest=new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                shortest[i]=1;
            }
            for (int i=0;i<nodeCount;i++)
            {
                if (reachable[i] == 1)
                {
                    for (int k=0;k<adj[i].Count;k++)
                    {
                        if (distance[i] + cost[i][k] < distance[adj[i][k]])
                        {
                            distance[adj[i][k]] = distance[i] + cost[i][k];
                            shortest[adj[i][k]] = 0;
                        }
                    }
                }
            }
            for (int i=0;i<n;i++)
            {
                if (shortest[i] == 0)
                {
                    negetive(i, shortest, adj);
                }
            }
            List<string> result=new List<string>();
            for (int x=0;x<n;x++)
            {
                if (reachable[x] == 0)
                {
                    result.Add("*");
                }
                else if (shortest[x] == 0)
                {
                    result.Add("-");
                }
                else
                {
                    result.Add($"{distance[x]}");
                }
            
            }


            return result.ToArray();
        }

        private void negetive(long item, long[] shortest, List<long>[] adj)
        {
            shortest[item] = 0;
            foreach (long i in adj[item])
            {
                if (shortest[i] == 1)
                {
                    negetive(i, shortest, adj);
                }
            }
        }

        public List<long>[] makeAdj(long[][] edges,long nodeCount,List<long>[] costs )
        {
            List<long>[] result =new List<long>[nodeCount];
            
            for(long i=0;i<nodeCount;i++)
            {
                result[i] = new List<long>();
            }
            foreach(long[] x in edges)
            {
                    result[x[0]-1].Add(x[1]-1);
                    costs[x[0]-1].Add(x[2]);

            }
            return result;
        }
    }
}
