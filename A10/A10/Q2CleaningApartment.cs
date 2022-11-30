using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q2CleaningApartment : Processor
    {
        public Q2CleaningApartment(string testDataName) : base(testDataName)
        {
            // ExcludeTestCaseRangeInclusive(18,20);
            // ExcludeTestCaseRangeInclusive(1,1);
            // ExcludeTestCaseRangeInclusive(6,6);
            // ExcludeTestCaseRangeInclusive(10,10);
            // ExcludeTestCaseRangeInclusive(28,28);
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public String[] Solve(int V, int E, long[,] matrix)
        {
            // write your code here
            // throw new NotImplementedException();
            // -------------------my idea--------------
            // ------------------7 tests fail-----------
            // List<string[]> onlyOne=new List<string[]>();
            // List<long[]>[] Adj=makeAdj(V,E,matrix);
            // int count=0;
            // for(int i=0;i<V;i++)
            // {
            //     string[] arrOfOr=new string[Adj[i].Count];
            //     for(int j=0;j<Adj[i].Count;j++)
            //     {
            //         for(int k=j+1;k<Adj[i].Count;k++)
            //         {
            //             onlyOne.Add(new string[2]{$"{-(Adj[i][j][1])}",$"{-(Adj[i][k][1])}"});
            //             count+=1;
            //         }
            //         arrOfOr[j]=$"{Adj[i][j][1]}";
            //     }
            //     onlyOne.Add(arrOfOr);
            //     count+=1;
            // }
            // // for(int i=0;i<E;i++)
            // // {
            // //     onlyOne.Add(new string[2]{$"{-((i+1)*2)}",$"{-((i+1)*2-1)}"});
            // //     onlyOne.Add(new string[2]{$"{-((i+1)*2)}",$"{-((i+1)*2-1)}"});
            // // }
            // string[] ans=new string[onlyOne.Count+1];
            // ans[0]=$"{count} {2*E}";
            // for(int i=0;i<onlyOne.Count;i++)
            // {
            //     List<string> newstr=new List<string>();
            //     for(int j=0;j<onlyOne[i].Length;j++)
            //     {
            //         newstr.Add(onlyOne[i][j]);
            //     }
            //     newstr.Add("0");
            //     string result=string.Join(" ",newstr);
            //     ans[i+1]=result;
            // }
            // return ans;
            //------------------end of my idea-------------------------

            // ---------------what to do from pdf---------------------
            List<string[]> onlyOne=new List<string[]>();
            Dictionary<long,bool>[] Adj=makeAdj(V,E,matrix);
            for (int i=1;i<V+1;i++)
            {
                string[] onlyoneOR=new string[V];
                string[] onlyonePathOR=new string[V];
                for (int j=1;j<V+1;j++)
                {
                    onlyoneOR[j-1]=$"{i*V+j}";
                    onlyonePathOR[j-1]=$"{j*V+i}";
                    for (int k=j+1;k<V+1;k++)
                    {
                        onlyOne.Add(new string[2]{$"{-(i*V+j)}",$"{-(i*V+k)}"});
                        onlyOne.Add(new string[2]{$"{-(j*V+i)}",$"{-(k*V+i)}"});
                    }
                    if(i!=j && !Adj[i].ContainsKey(j))
                    {
                        for(int k=1;k<V;k++)
                        {
                            onlyOne.Add(new string[2]{$"{-(i*V+k)}",$"{-(j*V+(k+1))}"});
                        }
                    }
                }
                onlyOne.Add(onlyoneOR);
            }
            string[] ans=new string[onlyOne.Count+1];
            ans[0]=$"{onlyOne.Count} {2*V*V}";
            for(int i=0;i<onlyOne.Count;i++)
            {
                List<string> newstr=new List<string>();
                for(int j=0;j<onlyOne[i].Length;j++)
                {
                    newstr.Add(onlyOne[i][j]);
                }
                newstr.Add("0");
                string result=string.Join(" ",newstr);
                ans[i+1]=result;
            }
            return ans;

        }
        public Dictionary<long,bool>[] makeAdj(int V,int E,long[,] matrix)
        {
            Dictionary<long,bool>[] result =new Dictionary<long,bool>[V+1];
            for(long i=0;i<V+1;i++)
            {
                result[i] = new Dictionary<long,bool>();
            }
            for(int i=0;i<E;i++)
            {
                result[matrix[i,0]][matrix[i,1]]=true;
                result[matrix[i,1]][matrix[i,0]]=true;
            }
            return result;
        }
        // public List<long[]>[] makeAdj(int V,int E,long[,] matrix)
        // {
        //     List<long[]>[] result =new List<long[]>[V];
        //     for(long i=0;i<V;i++)
        //     {
        //         result[i] = new List<long[]>();
        //     }
        //     for(int i=0;i<E;i++)
        //     {
        //         result[matrix[i,0]-1].Add(new long[2]{matrix[i,1]-1,(i+1)*2});
        //         result[matrix[i,1]-1].Add(new long[2]{matrix[i,0]-1,(i+1)*2-1});
        //     }
        //     return result;
        // }

    }
}
