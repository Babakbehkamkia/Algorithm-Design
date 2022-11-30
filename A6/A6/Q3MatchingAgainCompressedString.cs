using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q3MatchingAgainCompressedString : Processor
    {
        public Q3MatchingAgainCompressedString(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        /// <summary>
        /// Implement BetterBWMatching algorithm
        /// </summary>
        /// <param name="text"> A string BWT(Text) </param>
        /// <param name="n"> Number of patterns </param>
        /// <param name="patterns"> Collection of n strings Patterns </param>
        /// <returns> A list of integers, where the i-th integer corresponds
        /// to the number of substring matches of the i-th member of Patterns
        /// in Text. </returns>
        public long[] Solve(string text, long n, String[] patterns)
        {
            List<long> result=new List<long>();
            long[] last_to_first=computing_last_to_first(text);
            foreach(string patt in patterns)
            {
                string p=Reverse(patt);
                long first=0;
                long last=text.Length-1;
                for(int i=0;i<p.Length;i++)
                {
                    long top_index=first;
                    long bottom_index=last;
                    long count=0;
                    for(long j=first;j<last+1;j++)
                    {
                        if (p[i]==text[(int)j])
                            if (count==0)
                            {
                                top_index=j;
                                count+=1;
                                bottom_index=j;
                            }
                            else
                            {
                                bottom_index=j;
                            }
                    }
                    if (count==0)
                    {
                        last=first-1;
                        break;
                    }
                    first=last_to_first[top_index];
                    last=last_to_first[bottom_index];
                }
                result.Add(last-first+1);
            }
            return result.ToArray();
        }
        public static string Reverse( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }
        public long[] computing_last_to_first(string pattern)
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
            return last_to_first.ToArray();
        }
    }
}
