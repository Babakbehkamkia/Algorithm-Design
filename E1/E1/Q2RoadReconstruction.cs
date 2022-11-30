using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace E1
{
    public class Q2RoadReconstruction : Processor
    {
        public Q2RoadReconstruction(string testDataName) : base(testDataName)
        {
        }

        public override Action<string, string> Verifier => RoadReconstructionVerifier.Verify;

        public override string Process(string inStr) {
            long count;
            long[][] data;
            TestTools.ParseGraph(inStr, out count, out data);
            return string.Join("\n", Solve(count, data).Select(edge => string.Join(" ", edge)));
        }

        // returns n different edges in the form of {u, v, weight}
        public long[][] Solve(long n, long[][] distance)
        {
            long[][] result=new long[n][];
            for(int i=0;i<n;i++)
            {
                result[i]=new long[n];
            }
            List<long[]>[] adj=makeAdj(n,distance);
            
            for(int i=0;i<n;i++)
            {
                
                foreach(long[] item in adj[i])
                {
                    foreach(long[] item2 in adj[item[0]])
                    {
                        if(item[1]+item2[1]>distance[i][item2[0]])
                        {
                            result[i][item2[0]]=distance[i][item2[0]];
                            result[item[0]][item2[0]]=distance[item[0]][item[0]];
                            result[item[0]][item2[0]]=0;
                        }
                        else
                        {
                            result[i][item2[0]]=0;
                            result[item[0]][item2[0]]=distance[item[0]][item[0]];
                            result[item[0]][item2[0]]=distance[item[0]][item2[0]];
                        }
                    }
                }
            }
            return result;
        }

        private List<long[]>[] makeAdj(long n, long[][] distance)
        {
            List<long[]>[] adj=new List<long[]>[n];
            for(int i=0;i<n;i++)
            {
                adj[i]=new List<long[]>();
            }
            for(int i=0;i<n;i++)
            {
                for(int j=i+1;j<n;j++)
                {
                    adj[i].Add(new long[2]{j,distance[i][j]});
                    adj[j].Add(new long[2]{j,distance[i][j]});
                }
            }
            return adj;
        }
    }
}
