using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q3AdBudgetAllocation : Processor
    {
        public Q3AdBudgetAllocation(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long, long[][], long[], string[]>)Solve);

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

        public string[] Solve(long eqCount, long varCount, long[][] A, long[] b)
        {
            // write your code here
            // throw new NotImplementedException();
            List<string[]> result = new List<string[]>();
            // int count=0;
            for(int i=0;i<eqCount;i++)
            {
                List<long> nonZero=findingNonZero(A, varCount, i);


                if(nonZero.Count==0)
                {
                    if(b[i]<0)
                    {
                        return new string[3]{"2 1","1 0","-1 0"};
                    }
                }


                else if(nonZero.Count==1)
                {
                    if(A[i][nonZero[0]]>b[i])
                    {
                        result.Add(new string[1]{$"{-(nonZero[0]+1)}"});
                    }


                    if(0>b[i])
                    {
                        result.Add(new string[1]{$"{(nonZero[0]+1)}"});
                    }
                }


                else if(nonZero.Count==2)
                {
                    if(0>b[i])
                    {
                        result.Add(new string[2]{$"{(nonZero[0]+1)}",$"{(nonZero[1]+1)}"});
                        // result.Add(new string[1]{$"{(nonZero[1]+1)}"});
                    }


                    if(A[i][nonZero[0]]>b[i])
                    {
                        result.Add(new string[2]{$"{-(nonZero[0]+1)}",$"{(nonZero[1]+1)}"});
                    }
                    if(A[i][nonZero[1]]>b[i])
                    {
                        result.Add(new string[2]{$"{(nonZero[0]+1)}",$"{-(nonZero[1]+1)}"});
                    }


                    if(A[i][nonZero[0]]+A[i][nonZero[1]]>b[i])
                    {
                        result.Add(new string[2]{$"{-(nonZero[0]+1)}",$"{-(nonZero[1]+1)}"});
                    }
                }

                else if(nonZero.Count==3)
                {
                    if(0>b[i])
                    {
                        result.Add(new string[3]{$"{(nonZero[0]+1)}",$"{(nonZero[1]+1)}",$"{(nonZero[2]+1)}"});
                    }


                    if(A[i][nonZero[0]]>b[i])
                    {
                        result.Add(new string[3]{$"{-(nonZero[0]+1)}",$"{(nonZero[1]+1)}",$"{(nonZero[2]+1)}"});
                    }
                    if(A[i][nonZero[1]]>b[i])
                    {
                        result.Add(new string[3]{$"{(nonZero[0]+1)}",$"{-(nonZero[1]+1)}",$"{(nonZero[2]+1)}"});
                    }
                    if(A[i][nonZero[2]]>b[i])
                    {
                        result.Add(new string[3]{$"{(nonZero[0]+1)}",$"{(nonZero[1]+1)}",$"{-(nonZero[2]+1)}"});
                    }


                    if(A[i][nonZero[0]]+A[i][nonZero[1]]>b[i])
                    {
                        result.Add(new string[3]{$"{-(nonZero[0]+1)}",$"{-(nonZero[1]+1)}",$"{(nonZero[2]+1)}"});
                    }
                    if(A[i][nonZero[2]]+A[i][nonZero[1]]>b[i])
                    {
                        result.Add(new string[3]{$"{(nonZero[0]+1)}",$"{-(nonZero[1]+1)}",$"{-(nonZero[2]+1)}"});
                    }
                    if(A[i][nonZero[0]]+A[i][nonZero[2]]>b[i])
                    {
                        result.Add(new string[3]{$"{-(nonZero[0]+1)}",$"{(nonZero[1]+1)}",$"{-(nonZero[2]+1)}"});
                    }

                    if(A[i][nonZero[0]]+A[i][nonZero[1]]+A[i][nonZero[2]]>b[i])
                    {
                        result.Add(new string[3]{$"{-(nonZero[0]+1)}",$"{-(nonZero[1]+1)}",$"{-(nonZero[2]+1)}"});
                    }
                }
            }



            string[] ans=new string[result.Count+1];
            int count=result.Count;
            ans[0]=$"{count} {varCount}";
            for(int i=0;i<result.Count;i++)
            {
                List<string> newstr=new List<string>();
                for(int j=0;j<result[i].Length;j++)
                {
                    newstr.Add(result[i][j]);
                }
                newstr.Add("0");
                string str=string.Join(" ",newstr);
                ans[i+1]=str;
            }
            return ans;
        }
        public List<long> findingNonZero(long[][] A,long m,long row)
        {
            List<long> nonZero=new List<long>();
            for(int i=0;i<m;i++)
            {
                if(A[row][i]!=0)
                {
                    nonZero.Add(i);
                }
            }
            return nonZero;
        }
    }
}
