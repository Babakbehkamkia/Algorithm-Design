using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q1FindAllOccur : Processor
    {
        public Q1FindAllOccur(string testDataName) : base(testDataName)
        {
			this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String, long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, string pattern)
        {
            // write your code here
            // throw new NotImplementedException();
            List<long> result=new List<long>();
            string new_str=pattern+"$"+text;
            long[] s=compute_suffix(new_str);
            for(int i=pattern.Length;i<s.Length;i++)
            {
                if (s[i]>=pattern.Length)
                {
                    result.Add(i-2*pattern.Length);
                }
            }
            if(result.Count==0)
            {
                return new long[1]{-1};
            }
            return result.ToArray();
        }
        public long[] compute_suffix(string str)
        {
            // s=[0 for i in range(len(str))]
            long[] s=new long[str.Length];
            int counter=0;
            for(int i=1;i<str.Length;i++)
            {
                while (counter>0 && str[i]!=str[counter])
                {
                    counter=(int)s[counter-1];
                }
                if (str[i]==str[counter])
                {
                    counter+=1;
                }
                s[i]=counter;
            }
            return s;
        }
    }
}
