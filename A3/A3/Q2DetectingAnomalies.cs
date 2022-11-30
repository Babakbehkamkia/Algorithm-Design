using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
namespace A3
{
    public class Q2DetectingAnomalies:Processor
    {
        public Q2DetectingAnomalies(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long>)Solve);

        public long[] dist;

        public long Solve(long nodeCount, long[][] edges)
        {
            //Write Your Code Here
            List<long>[] cost =new List<long>[nodeCount];
            for(long i=0;i<nodeCount;i++)
            {
                cost[i] = new List<long>();
            }
            List<long>[] adj =makeAdj(edges,nodeCount,cost);
            long n=adj.Count();
            long[] dist=new long[n];
            for(int i=0;i<n;i++)
            {
                dist[i]=long.MaxValue;
            }
            dist[0]=0;
            for (int i=0;i<n-1;i++)
            {
                bool isChanged=false;
                for (int j=0;j<n;j++)
                {
                    if (dist[j] != long.MaxValue)
                    {
                        for (int k=0;k<adj[j].Count;k++)
                        {
                            if (dist[adj[j][k]] ==long.MaxValue)
                            {
                                dist[adj[j][k]]=dist[j]+cost[j][k];
                                isChanged=true;
                            }
                            else
                            {
                                dist[adj[j][k]]=Math.Min(dist[j]+cost[j][k],dist[adj[j][k]]);
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
            for (int i=0;i<adj.Length;i++)
            {
                if (dist[i] !=long.MaxValue)
                {
                    for (int k=0;k<adj[i].Count;k++)
                    {
                        if (dist[i]+cost[i][k]<dist[adj[i][k]])
                        {
                            return 1;
                        }
                    }
                }
            }
            return 0;
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
