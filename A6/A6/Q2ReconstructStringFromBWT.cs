using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q2ReconstructStringFromBWT : Processor
    {
        public Q2ReconstructStringFromBWT(string testDataName) 
        : base(testDataName) {
            // this.ExcludeTestCaseRangeInclusive(1,30);
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Reconstruct a string from its Burrows–Wheeler transform
        /// </summary>
        /// <param name="bwt"> A string Transform with a single “$” sign </param>
        /// <returns> The string Text such that BWT(Text) = Transform.
        /// (There exists a unique such string.) </returns>
        public string Solve(string pattern)
        {
            List<string> result=new List<string>();
            Tuple<long[], long> t=computing_last_to_first(pattern);
            long[] last_to_first=t.Item1;
            long index=last_to_first[t.Item2];
            for (int i=0;i<pattern.Length-1;i++)
            {
                char p=pattern[(int)index];
                
                if (p=='A')
                {
                    result.Add("A");
                    index=last_to_first[index];
                }
                else if (p=='C')
                {
                    result.Add("C");
                    index=last_to_first[index];
                }
                else if (p=='G')
                {
                    result.Add("G");
                    index=last_to_first[index];
                }
                else if (p=='T')
                {
                    result.Add("T");
                    index=last_to_first[index];
                }
                else if (p=='$')
                {
                    result.Add("$");
                    index=last_to_first[index];
                }
                
            }
            result.Reverse();
            string ans=string.Join("",result);
            return ans+="$";
        }
        public Tuple<long[],long> computing_last_to_first(string pattern)
        {
            List<long> last_to_first=new List<long>();
            List<long> nums=new List<long>();
            long num=0;
            foreach (char symbol in new char[4]{'A','C','G','T'})
            {
                
                foreach (var item in pattern)
                {
                    if (symbol==item)
                        num+=1;
                }
                nums.Add(num);
            }
            long[] used=new long[nums.Count];
            long index=0;
            for (int i=0;i<pattern.Length;i++)
            {
                char p=pattern[i];
                
                if (p=='A')
                {
                    last_to_first.Add(1+used[0]);
                    used[0]+=1;
                }
                else if (p=='C')
                {
                    last_to_first.Add(1+nums[0]+used[1]);
                    used[1]+=1;
                }
                else if (p=='G')
                {
                    last_to_first.Add(1+nums[1]+used[2]);
                    used[2]+=1;
                }
                else if (p=='T')
                {
                    last_to_first.Add(1+nums[2]+used[3]);
                    used[3]+=1;
                }
                else if (p=='$')
                {
                    index=i;
                    last_to_first.Add(0);
                }
            }
            return new Tuple<long[],long>(last_to_first.ToArray(),index);
        }
    }
}
