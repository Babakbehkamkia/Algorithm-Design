using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using GeoCoordinatePortable;
using Priority_Queue;

namespace A4
{
    public class Q3ComputeDistance : Processor
    {
        public Q3ComputeDistance(string testDataName) : base(testDataName) { }

        public static readonly char[] IgnoreChars = new char[] { '\n', '\r', ' ' };
        public static readonly char[] NewLineChars = new char[] { '\n', '\r' };
        private static double[][] ReadTree(IEnumerable<string> lines)
        {
            return lines.Select(line => 
                line.Split(IgnoreChars, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(n => double.Parse(n)).ToArray()
                            ).ToArray();
        }
        public override string Process(string inStr)
        {
            return Process(inStr, (Func<long, long, double[][], double[][], long,
                                    long[][], double[]>)Solve);
        }
        public static string Process(string inStr, Func<long, long, double[][]
                                  ,double[][], long, long[][], double[]> processor)
        {
           var lines = inStr.Split(NewLineChars, StringSplitOptions.RemoveEmptyEntries);
           long[] count = lines.First().Split(IgnoreChars,
                                              StringSplitOptions.RemoveEmptyEntries)
                                        .Select(n => long.Parse(n))
                                        .ToArray();
            double[][] points = ReadTree(lines.Skip(1).Take((int)count[0]));
            double[][] edges = ReadTree(lines.Skip(1 + (int)count[0]).Take((int)count[1]));
            long queryCount = long.Parse(lines.Skip(1 + (int)count[0] + (int)count[1]) 
                                         .Take(1).FirstOrDefault());
            long[][] queries = ReadTree(lines.Skip(2 + (int)count[0] + (int)count[1]))
                                        .Select(x => x.Select(z => (long)z).ToArray())
                                        .ToArray();

            return string.Join("\n", processor(count[0], count[1], points, edges,
                                queryCount, queries));
        }
        public double[] Solve(long nodeCount,
                            long edgeCount,
                            double[][] points,
                            double[][] edges,
                            long queriesCount,
                            long[][] queries)
        {
            List<double[]>[] adj=makeAdj(nodeCount,edges);
            Node[] nodes=new Node[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                nodes[i]=new Node((long)points[i][0],(long)points[i][1],null);
            }
            double[] result = new double[queries.Length];
            for(int i=0;i<queries.Length;i++)
            {
                result[i]= AStar(nodeCount,adj,nodes,queries[i][0]-1,queries[i][1]-1);
            }
            return result;
        }
        public double distance(long x1,long y1,long x2,long y2)
        {
            return Math.Pow(Math.Pow((x1-x2),2)+Math.Pow((y1-y2),2),0.5);
        }

        private double AStar(long nodeCount,List<double[]>[] adj, Node[] nodes,long startNode,long endNode)
        {
            if(startNode==endNode)
            {
                return 0;
            }
            bool reached=false;
            SimplePriorityQueue<long> pq = new SimplePriorityQueue<long>();
            long[] visited=new long[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                pq.Enqueue(i,long.MaxValue);
            }
            pq.UpdatePriority(startNode,(float)distance(nodes[startNode].x,nodes[startNode].y,
                                                        nodes[endNode].x,nodes[endNode].y));
            visited[startNode]=1;
            
            while(pq.Count!=0)
            {
                double currentCost = pq.GetPriority(pq.First)-distance(nodes[pq.First].x,nodes[pq.First].y,
                                                                                nodes[endNode].x,nodes[endNode].y);
                if(currentCost==long.MaxValue)
                {
                    return -1;
                }
                long currentNode=pq.Dequeue();
                visited[currentNode]=1;
                if (currentNode==endNode && reached)
                {
                    return Math.Round(currentCost);
                }
                foreach(double[] item in adj[currentNode])
                {
                    if (item[0]==endNode)
                    {
                        reached=true;
                    }
                    if(visited[(int)item[0]]==0)
                    {
                        double newPriority=item[1]+currentCost+distance(nodes[(int)item[0]].x,
                                                                                nodes[(int)item[0]].y,
                                                                                nodes[endNode].x,
                                                                                nodes[endNode].y);
                        if(pq.GetPriority((long)item[0])>newPriority)
                        {
                            pq.UpdatePriority((long)item[0],(float)newPriority);
                        }
                    }
                }
            }
            return -1;
        }

        public List<double[]>[] makeAdj(long nodeCount,double[][] edges)
        {
            List<double[]>[] adj=new List<double[]>[nodeCount];
            // double[] costs=new double[nodeCount];
            for(int i=0;i<nodeCount;i++)
            {
                adj[i]=new List<double[]>();
            }
            foreach(double[] item in edges)
            {
                adj[(int)item[0]-1].Add(new double[2]{item[1]-1,item[2]});
            }
            return adj;
        }
    }
}