using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q2Manchester : Processor
    {
        public Q2Manchester(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr)
            => E2Processors.Q2Processor(inStr, Solve);

        /**
         * W[i] = Number of wins i-th team currently has.
         * R[i] = Total number of remaining games for i-th team.
         * G[i][j] = Number of upcoming games between teams i and j.
         * 
         * Should return whether the team can get first place.
         */
        public bool Solve(long[] W, long[] R, long[][] G)
        {
            throw new NotImplementedException();
            Dictionary<long,long>[] adj=makeAdj(W,R,G);
            
        }
        
        public Dictionary<long,long>[] makeAdj(long[] W, long[] R, long[][] G)
        {
            // long nodeCount=flightCount+crewCount;
            List<Tuple<int,int>> l=new List<Tuple<int, int>>();
            for(int i=0;i<W.Length-1;i++)
            {
                for(int j=i+1;j<W.Length-1;j++)
                {
                    l.Add(new Tuple<int, int>(i,j));
                }
                
            }
            Dictionary<long,long>[] adj=new Dictionary<long, long>[l.Count+W.Length+1];
            for(int i=0;i<l.Count+W.Length+1;i++)
            {
                adj[i]=new Dictionary<long, long>();
            }
            for(int i=1;i<l.Count+1;i++)
            {
                adj[0][i]=G[l[i-1].Item1][l[i-1].Item2];
            }
            for(int i=1;i<l.Count;i++)
            {
                adj[i][l[i].Item1]=long.MaxValue;
                adj[i][l[i].Item2]=long.MaxValue;
            }
            for(int i=(int)l.Count+1;i<l.Count+W.Length;i++)
            {
                long r=W[i-(int)l.Count-1]-W[W.Length-1];
                long s=R[R.Length-1]-r;
                adj[i][l.Count+W.Length]=s;
            }
            
            return adj;
        }
        public List<long> BFS(long flightCount,long nodeCount,Dictionary<long,long>[] adj)
        {
            long[] visited=new long[nodeCount];
            bool isExist=false;
            long[] parents=new long[nodeCount];
            Queue<long> q=new Queue<long>();
            q.Enqueue(0);
            visited[0]=1;
            while(q.Count!=0 && !isExist)
            {
                long currentNode=q.Dequeue();
                foreach(var item in adj[currentNode].Keys)
                {
                    if(item==nodeCount-1)
                    {
                        isExist=true;
                        parents[item]=currentNode;
                        break;
                    }
                    if(visited[item]==0)
                    {
                        visited[item]=1;
                        q.Enqueue(item);
                        parents[item]=currentNode;
                    }
                }
            }
            
            List<long> path=new List<long>();
            if(isExist==false)
            {
                return null;
            }
            long parent=nodeCount-1;
            while(parent!=0)
            {
                path.Add(parent);
                parent=parents[parent];
            }
            path.Reverse();
            
            return path;
        }
    }
}
