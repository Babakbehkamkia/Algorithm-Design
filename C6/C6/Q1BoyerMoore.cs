using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace C6
{
    public class Q1BoyerMoore : Processor
    {
        public Q1BoyerMoore(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
            // write your code here
            // throw new NotImplementedException();
            List<long> result=boyerMoore(text,pattern);
            long[] resultArr=result.ToArray();
            if(resultArr.Length==0)
            {
                return new long[]{-1};
            }
            return resultArr;
        }
        
        public List<long> boyerMoore(string text,string pattern)
        {
            List<long> result=new List<long>();
            char[] textArray=text.ToCharArray();
            char[] patternArray=pattern.ToCharArray();
            int l=patternArray.Length;
            Dictionary<char,int> dict=new Dictionary<char,int>();
            for (int i=0;i<l;i++)
            {
                dict[patternArray[i]]=i;
            }
            int shiftedIndex=0; 
            int n=textArray.Length;
            while(shiftedIndex<=n-l)
            {
                int count=1;
                try
                {
                    while(patternArray[l-count]==textArray[shiftedIndex+l-count] && l>=count)
                    {
                        count++;
                    }
                }
                catch{}
                
                if (l<count)
                {
                    result.Add(shiftedIndex);
                    if(shiftedIndex+l<n)
                    {
                        if(dict.ContainsKey(textArray[shiftedIndex+l]))
                        {
                            shiftedIndex+=l-dict[textArray[shiftedIndex+l]];
                        }
                        else
                        {
                            shiftedIndex+=l+1;
                        }
                    }
                    else
                    {
                        shiftedIndex+=1;
                    }
                }
                else
                {
                    if(dict.ContainsKey(textArray[shiftedIndex+l-count]))
                    {
                        shiftedIndex+=Math.Max(1,l-count-dict[textArray[shiftedIndex+l-count]]);
                    }
                    else
                    {
                        shiftedIndex+=Math.Max(1,l-count+1);
                    }
                }
            }
            return result;
        }
    }
}
