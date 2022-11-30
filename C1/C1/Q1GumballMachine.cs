using System;
using System.Collections.Generic;
using TestCommon;

namespace C1
{
    public class Q1GumballMachine : Processor
    {
        public Q1GumballMachine(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long>)Solve);

        public long Solve(long x, long y)
        {
            long maxNum=Math.Max(x,y);
            long[] parent=new long[maxNum+2];
            long[] visited=new long[maxNum+2];
            List<long>[] adj =new List<long>[maxNum+2];
            for (long i=0;i<=maxNum+1;i++)
            {
                adj[i]=new List<long>();
            }
            for(long i=0;i<=maxNum+1;i++)
            {
                if (i>1)
                {
                    adj[i].Add(i-1);
                }
                if (i!=0 && 2*i<=maxNum+1)
                {
                    adj[i].Add(2*i);
                }
            }
            Queue<long> q=new Queue<long>();
            q.Enqueue(x);
            visited[x]=1;
            parent[x]=-1;
            bool found=false;
            while (q.Count!=0)
            {
                long current=q.Dequeue();
                foreach(long item in adj[current])
                {

                    if(visited[item]==0)
                    {
                        parent[item]=current;
                        visited[item]=1;
                        if (item==y)
                        {
                            found=true;
                            break;
                        }
                        q.Enqueue(item);
                    }
                }
                if (found)
                {
                    break;
                }
                
            }
            if (visited[y]==1)
            {
                long count=0;
                long current=y;
                while(parent[current]!=-1)
                {
                    count+=1;
                    current=parent[current];
                }
                return count;
            }
            else
            {
                return -1;
            }
        }
    }
}
