using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
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
            // long[][] result=new long[n][];
            // for(int i=0;i<n;i++)
            // {
            //     result[i]=new long[n];
            // }
            // List<long[]>[] adj=makeAdj(n,distance);
            
            // for(int i=0;i<n;i++)
            // {
                
            //     foreach(long[] item in adj[i])
            //     {
            //         foreach(long[] item2 in adj[item[0]])
            //         {
            //             if(item[1]+item2[1]>distance[i][item2[0]])
            //             {
            //                 result[i][item2[0]]=distance[i][item2[0]];
            //                 result[item[0]][item2[0]]=distance[item[0]][item[0]];
            //                 result[item[0]][item2[0]]=0;
            //             }
            //             else
            //             {
            //                 result[i][item2[0]]=0;
            //                 result[item[0]][item2[0]]=distance[item[0]][item[0]];
            //                 result[item[0]][item2[0]]=distance[item[0]][item2[0]];
            //             }
            //         }
            //     }
            // }
            // return result;
            List<(long,long,long)> edges=new List<(long,long,long)>();
            long[] parents=new long[n];
            long[] ranks=new long[n];
            for(int i=0;i<n;i++)
            {
                for(int j=i+1;j<n;j++)
                {
                    edges.Add((i,j,distance[i][j]));
                }
                parents[i] =i;
            }
            edges=edges.OrderBy(x =>x.Item3).ToList();
            List<(long,long,long)> remainEdges=new List<(long, long, long)>();
            List<List<(long,long)>> adj=new List<List<(long,long)>>();
            for(int i=0;i<n;i++)
            {
                adj.Add(new List<(long,long)>());
            }

            foreach((long,long,long) edge in edges)
            {
                long parent1=find(edge.Item1,parents);
                long parent2=find(edge.Item2,parents);
                if(parent1!=parent2)
                {
                    union(parent1,parent2,ranks,parents);
                    adj[(int)edge.Item1].Add((edge.Item2,edge.Item3));
                    adj[(int)edge.Item2].Add((edge.Item1,edge.Item3));
                }
                else
                {
                    remainEdges.Add(edge);
                }
            }
            bool is_exist=false;
            foreach((long,long,long) edge in remainEdges)
            {
                if(BFS(n,adj,edge.Item1,edge.Item2)>distance[edge.Item1][edge.Item2])
                {
                    adj[(int)edge.Item1].Add((edge.Item2,distance[edge.Item1][edge.Item2]));
                    adj[(int)edge.Item2].Add((edge.Item1,distance[edge.Item1][edge.Item2]));
                    is_exist = true;
                    break;
                }
            }
            
            if (!is_exist)
            {
                adj[(int)remainEdges[0].Item1].Add(((int)remainEdges[0].Item2,(int)remainEdges[0].Item3));
                adj[(int)remainEdges[0].Item2].Add(((int)remainEdges[0].Item1,(int)remainEdges[0].Item3));
            }
            long[][] result=new long[n][];
            long index=0;
            for(int i=0;i<n;i++)
            {
                foreach((long,long) node in adj[i])
                {
                    if(node.Item1>i)
                    {
                        result[index]=new long[]{node.Item1+1,i+1,node.Item2};
                        index++;
                    }
                }
            }
            return result;
        }
        private long find(long node,long[] parents)
        {
            while(node!=parents[node])
            {
                node=parents[node];
            }
            return parents[node];
        }
        private void union(long rightNode,long leftNode,long[] ranks,long[] parents)
        {
            if(ranks[rightNode]>ranks[leftNode])
            {
                parents[leftNode]=rightNode;
            }
            else 
            {
                parents[rightNode]=leftNode;
                if(ranks[rightNode]==ranks[leftNode])
                    ranks[leftNode]+=1;
            }
        }
        private static long BFS(long nodeCount,List<List<(long, long)>> adj,long startnode,long endnode)
        {
            long[] costs=Enumerable.Repeat(long.MaxValue,adj.Count).ToArray();
            Queue<long> q=new Queue<long>();
            q.Enqueue(startnode);
            costs[startnode]=0 ;

            while (q.Count!=0)
            {
                long currentNode=q.Dequeue();
                foreach((long, long) item in adj[(int)currentNode])
                {
                    if (costs[item.Item1]==long.MaxValue)
                    {
                        costs[item.Item1]=costs[currentNode]+item.Item2;
                        q.Enqueue(item.Item1);
                    }
                    if (item.Item1==endnode)
                    {
                        return costs[item.Item1];
                    }
                }
            }
            return 0 ;
        }
        // private List<long[]>[] makeAdj(long n, long[][] distance)
        // {
        //     List<long[]>[] adj=new List<long[]>[n];
        //     for(int i=0;i<n;i++)
        //     {
        //         adj[i]=new List<long[]>();
        //     }
        //     for(int i=0;i<n;i++)
        //     {
        //         for(int j=i+1;j<n;j++)
        //         {
        //             adj[i].Add(new long[2]{j,distance[i][j]});
        //             adj[j].Add(new long[2]{j,distance[i][j]});
        //         }
        //     }
        //     return adj;
        // }
    }
}

