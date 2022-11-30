using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace C2
{
    public class Q1RoadsInHackerLand : Processor
    {
        public Q1RoadsInHackerLand(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            string[] first = lines[0].TrimEnd().Split(' ');

            long n = long.Parse(first[0]);
            long m = long.Parse(first[1]);

            long[][] roads = lines.Skip(1).Select(line => line.Split(' ').Select(num => long.Parse(num)).ToArray()).ToArray();
            return Solve(n, roads);
        }

        public string Solve(long n, long[][] roads)
        {
            // return Solve2((int) n, roads.Select(x => x.Select(num => (int)num).ToArray()).ToArray());
            return findDistances(n,roads);
            // return computeBinary(result);

        }

        class Dsu
        {
            private readonly int[] parent;

            public Dsu(int n)
            {
                parent = Enumerable.Range(0, n).ToArray();
            }

            public int FindSet(int v)
            {
                if (v == parent[v])
                    return v;
                return parent[v] = FindSet(parent[v]);
            }

            public void UnionSets(int a, int b)
            {
                a = FindSet(a);
                b = FindSet(b);
                if (a != b)
                    parent[b] = a;
            }
        }
        // private static T[] Init<T>(int size) where T : new() { var ret = new T[size]; for (int i = 0; i < size; i++) ret[i] = new T(); return ret; }

        // public string Solve2(int n, int[][] roads)
        // {

        //     var ed = roads;
        //     int m = roads.Length;
        //     Array.Sort(ed, (e1, e2) => e1[2].CompareTo(e2[2]));
        //     var dsu = new Dsu(n);
        //     var g = Init<List<Tuple<int, int>>>(n);
        //     for (int i = 0; i < m; i++)
        //     {
        //         ed[i][0]--;
        //         ed[i][1]--;
        //         if (dsu.FindSet(ed[i][0]) != dsu.FindSet(ed[i][1]))
        //         {
        //             g[ed[i][0]].Add(Tuple.Create(ed[i][1], ed[i][2]));
        //             g[ed[i][1]].Add(Tuple.Create(ed[i][0], ed[i][2]));
        //             dsu.UnionSets(ed[i][0], ed[i][1]);
        //         }
        //     }

        //     var stack = new Stack<Tuple<int, int>>();
        //     var stack2 = new Stack<Tuple<int, int>>();
        //     stack.Push(Tuple.Create(0, -1));
        //     while (stack.Count > 0)
        //     {
        //         var t = stack.Pop();
        //         foreach (var e in g[t.Item1])
        //             if (e.Item1 != t.Item2)
        //                 stack.Push(Tuple.Create(e.Item1, t.Item1));
        //         stack2.Push(t);
        //     }

        //     var cnt = new int[n];
        //     while (stack2.Count > 0)
        //     {
        //         var t = stack2.Pop();
        //         cnt[t.Item1] = 1;
        //         foreach (var e in g[t.Item1])
        //             if (e.Item1 != t.Item2)
        //                 cnt[t.Item1] += cnt[e.Item1];
        //     }

        //     var stack3 = new Stack<Tuple<int, int, int>>();
        //     var ans = Enumerable.Repeat(0L, m).ToList();
        //     stack3.Push(Tuple.Create(0, -1, 0));
        //     while (stack3.Count > 0)
        //     {
        //         var t = stack3.Pop();
        //         foreach (var e in g[t.Item1])
        //             if (e.Item1 != t.Item2)
        //             {
        //                 ans[e.Item2] = 1L * cnt[e.Item1] * (cnt[t.Item1] - cnt[e.Item1] + t.Item3);
        //                 stack3.Push(Tuple.Create(e.Item1, t.Item1, t.Item3 + cnt[t.Item1] - cnt[e.Item1]));
        //             }
        //     }

         // for (int i = 0; i < ans.Count; i++)
        // {
        //     long ex = ans[i] / 2;
        //     ans[i] %= 2;
        //     if (ex > 0)
        //     {
        //         if (i == ans.Count - 1)
        //             ans.Add(0);
        //         ans[i + 1] += ex;
        //     }
        // }
        // while (ans[ans.Count - 1] == 0)
        //     ans.RemoveAt(ans.Count - 1);
        // ans.Reverse();

        // return string.Concat(ans);
    // }
        // my code starts from here

        public string findDistances(long n,long[][] roads)
        {
            string str="";
            // List<long[]> allCosts=new List<long[]>();
            // long result=0;

            // for(int i=0;i<n;i++)
            // {
            //     allCosts.Add(new long[n]);
            // }

            List<long> costs=new List<long>();
            List<long> nodes=new List<long>();
            List<long[]>[] msTree=MST(n,roads);
            // nodes.Add(0);
            // costs.Add(0);
            
            // long currentNode=0;
            Dictionary<string,long> visited=new Dictionary<string,long>();
            // for(int i=0;i<n;i++)
            // {
            //     visited[i]=new long[n];
            // }
            // long result=DFS(n,costs,nodes,msTree,currentNode,visited);
            // return result;
            // long result=0;
            for(int i=0;i<n;i++)
            {
                foreach(long[] item in msTree[i])
                {
                    long small=Math.Min(i,item[0]);
                    long big=Math.Max(i,item[0]);
                    if(!visited.ContainsKey( $"{small}{big}" ))
                    {
                        visited[$"{small}{big}"]=1;
                        // visited[$"{small}{big}"]=1;
                        long count=BFS(n,msTree,i,item[0]);
                        long[] tajzie=Tajzie(n,count);
                        string binaryReverse =computeBinary(tajzie[1]);
                        string binary="";
                        for(int j=0;j<tajzie[0]+item[1]+binaryReverse.Length;j++)
                        {
                            if(j<tajzie[0]+item[1])
                            {
                                binary+="0";
                            }
                            else
                            {
                                binary+=binaryReverse[j-(int)(tajzie[0]+item[1])];
                            }
                        }
                        // binary.ToCharArray().Reverse().ToString();
                        str=rebuildStr(str,binary);
                    }
                }
            }
            // string result="";
            // for(int i=1;i<=str.Length;i++)
            // {
            //     result+=str[str.Length-i];
            // }
            char[] result = str.ToCharArray();
            Array.Reverse( result );
            return new string(result);
        }
        public string rebuildStr(string str,string binary)
        {
            string result="";
            // string binary="";
            // for(int i=1;i<=oldBinary.Length;i++)
            // {
            //     binary+=oldBinary[oldBinary.Length-i];
            // }
            if (str.Length>binary.Length)
            {
                long size=str.Length-binary.Length;
                for(int i=0;i<size;i++)
                {
                    binary+="0";
                }
            }
            else
            {
                long size=binary.Length-str.Length;
                for(int i=0;i<size;i++)
                {
                    str+="0";
                }
            }
            long carry=0;
            for(int i=0;i<str.Length;i++)
            {
                if(i==str.Length-1)
                {
                    if(str[i]=='1' && binary[i]=='1')
                    {
                        if(carry==0)
                        {
                            result+="01";
                        }
                        else
                        {
                            result+="11";
                        }
                    }
                    else
                    {
                        if(str[i]=='0' && binary[i]=='0')
                        {
                            if(carry==0)
                            {
                                result+="0";
                            }
                            else
                            {
                                carry-=1;
                                result+="1";
                            }
                        }
                        else
                        {
                            if(carry==0)
                            {
                                result+="1";
                            }
                            else
                            {
                                result+="01";
                            }
                        }
                    }
                }
                else
                {
                    if(str[i]=='1' && binary[i]=='1')
                    {
                        if(carry==0)
                        {
                            carry+=1;
                            result+="0";
                        }
                        else
                        {
                            result+="1";
                        }
                    }
                    else
                    {
                        if(str[i]=='0' && binary[i]=='0')
                        {
                            if(carry==0)
                            {
                                result+="0";
                            }
                            else
                            {
                                carry-=1;
                                result+="1";
                            }
                        }
                        else
                        {
                            if(carry==0)
                            {
                                result+="1";
                            }
                            else
                            {
                                result+="0";
                            }
                        }
                    }
                }
            }


            return result;
        }
        public long[] Tajzie(long n,long count)
        {
            long prod=count*(n-count);
            long c=0;
            while(prod%2==0)
            {   
                c+=1;
                prod=prod/2;
            }
            
            return new long[2]{c,prod};
        }
        public long BFS(long n,List<long[]>[] msTree,long currentNode,long visitedNode)
        {
            long count=1;
            Queue<long> q=new Queue<long>();
            long[] visited=new long[n];
            q.Enqueue(currentNode);
            visited[currentNode]=1;
            visited[visitedNode]=1;
            while(q.Count!=0)
            {
                long node=q.Dequeue();
                foreach(long[] item in msTree[node])
                {
                    if(visited[item[0]]==0)
                    {
                        visited[item[0]]=1;
                        q.Enqueue(item[0]);
                        count+=1;
                    }
                }
            }
            return count;
        }
        public long DFS(long n,List<long> costs,List<long> nodes,List<long[]>[] msTree,long currentNode,long[] visited)
        {
            long result=0;
            long currentNodeCopy=currentNode;
            foreach(long[] item in msTree[currentNode])
            {
                if(visited[item[0]]==0)
                {
                    nodes.Add(item[0]);
                    costs.Add(item[1]);
                    currentNode=item[0];
                    visited[item[0]]=1;
                    result+=fillingAllCosts(costs,nodes,currentNode,visited);
                    result+=DFS(n,costs,nodes,msTree,currentNode,visited);
                }
                
                // allCosts[(int)currentNode][item[0]]=item[2];
                currentNode=currentNodeCopy;
            }
            nodes.RemoveAt(nodes.Count - 1);
            costs.RemoveAt(costs.Count - 1);
            return result;
        }
        public long fillingAllCosts(List<long> costs,List<long> nodes,long currentNode,long[] visited)
        {
            long lastSum=0;
            int count=costs.Count;
            for(int i=1;i<count;i++)
            {
                lastSum+=costs[count-i];
                // allCosts[(int)currentNode][nodes[count-i-1]]=lastSum;
                // allCosts[(int)nodes[count-i-1]][currentNode]=lastSum;

            }
            return lastSum;
        }
        public List<long[]>[] MST(long n,long[][] roads)
        {
            var ed = roads;
            int m = roads.Length;
            Array.Sort(ed, (e1, e2) => e1[2].CompareTo(e2[2]));
            var dsu = new Dsu((int)n);
            var g = new List<long[]>[n];
            for(int i=0;i<n;i++)
            {
                g[i]=new List<long[]>();
            }
            for (int i = 0; i < m; i++)
            {
                ed[i][0]--;
                ed[i][1]--;
                if (dsu.FindSet((int)ed[i][0]) != dsu.FindSet((int)ed[i][1]))
                {
                    g[ed[i][0]].Add(new long[2]{ed[i][1], ed[i][2]});
                    g[ed[i][1]].Add(new long[2]{ed[i][0], ed[i][2]});
                    dsu.UnionSets((int)ed[i][0],(int)ed[i][1]);
                }
            }
            return g;
        }

        public string computeBinary(long result)
        {
            // long i=0;
            // while(result >Math.Pow(2,i))
            // {
            //     i+=1;
            // }
            // long[] lstr=new long[i];
            string str="";
            while (result>1)
            {
                str+=$"{result%2}";
                // lstr[i-1]=result%2;
                // i-=1;
                result=result/2;

            }
            // lstr[0]=result;
            str+=$"{result}";
            // str.ToArray();
            
            // str.Reverse();
            return str;
        }
    }
}
