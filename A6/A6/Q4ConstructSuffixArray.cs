using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q4ConstructSuffixArray : Processor
    {
        public Q4ConstructSuffixArray(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        /// <summary>
        /// Construct the suffix array of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> SuffixArray(Text), that is, the list of starting positions
        /// (0-based) of sorted suffixes separated by spaces </returns>
        public long[] Solve(string text)
        {
            List<Node> matrix=new List<Node>();
    
            int l=text.Length-1;
            int i=0;
            while (i<l+1)
            {
                
                string new_text=$"{text[l]}";
                text=text.Remove(l);
                // for(int index=0;index<l;index++)
                // {
                //     new_text+=text[index];
                // }
                new_text+=text;
                text=new_text;
                Node n=new Node(text.Length-i-1,text);
                matrix.Add(n);
                i+=1;
            }
            matrix.Sort();
            // string last="";
            // List<long> result=new List<long>();
            
            // foreach(var item in matrix)
            // {
            //     result.Add(item.suffix);
            // }
            long[] result=new long[matrix.Count];
            for(int index=0;index<matrix.Count;index++)
            {
                result[index]=matrix[index].suffix;
            }

            return result;
        }
    }
}
