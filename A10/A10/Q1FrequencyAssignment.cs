using System;
using System.Collections.Generic;
using TestCommon;

namespace A10
{
    public class Q1FrequencyAssignment : Processor
    {
        public Q1FrequencyAssignment(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, long[,], string[]>)Solve);


        public String[] Solve(int V, int E, long[,] matrix)
        {
            List<string[]> onlyOne=new List<string[]>();
            for(long currentNode=1;currentNode<V+1;currentNode++)
            {
                string[] arrOfOr=new string[3]{$"{(currentNode*3)}",$"{(currentNode*3-1)}",$"{(currentNode*3-2)}"};
                string[] arrOfAnds1=new string[2]{$"{-(currentNode*3)}",$"{-(currentNode*3-1)}"};
                string[] arrOfAnds2=new string[2]{$"{-(currentNode*3-2)}",$"{-(currentNode*3-1)}"};
                string[] arrOfAnds3=new string[2]{$"{-(currentNode*3)}",$"{-(currentNode*3-2)}"};
                onlyOne.Add(arrOfOr);
                onlyOne.Add(arrOfAnds1);
                onlyOne.Add(arrOfAnds2);
                onlyOne.Add(arrOfAnds3);
            }
            for(int i=0;i<E;i++)
            {
                string[] arr1=new string[2]{$"{-((matrix[i,0])*3)}",$"{-((matrix[i,1])*3)}"};
                string[] arr2=new string[2]{$"{-((matrix[i,0])*3-1)}",$"{-((matrix[i,1])*3-1)}"};
                string[] arr3=new string[2]{$"{-((matrix[i,0])*3-2)}",$"{-((matrix[i,1])*3-2)}"};
                onlyOne.Add(arr1);
                onlyOne.Add(arr2);
                onlyOne.Add(arr3);
            }


            long num=onlyOne.Count;
            string[] ans=new string[num+1];
            ans[0]=$"{4*V+3*E} {3*V}";
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
            // return new string[num]{"1 1","1 -1 0"};
            return ans;

        }
        public List<long>[] makeAdj(int V,int E,long[,] matrix)
        {
            List<long>[] result =new List<long>[V];
            for(long i=0;i<V;i++)
            {
                result[i] = new List<long>();
            }
            for(int i=0;i<E;i++)
            {
                result[matrix[i,0]-1].Add(matrix[i,1]-1);
                result[matrix[i,1]-1].Add(matrix[i,0]-1);
            }
            return result;
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }
}
