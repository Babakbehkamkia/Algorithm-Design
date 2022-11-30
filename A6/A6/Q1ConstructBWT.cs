using System;
using System.Collections.Generic;
using TestCommon;

namespace A6
{
    public class Q1ConstructBWT : Processor
    {
        public Q1ConstructBWT(string testDataName) 
        : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, String>)Solve);

        /// <summary>
        /// Construct the Burrows–Wheeler transform of a string
        /// </summary>
        /// <param name="text"> A string Text ending with a “$” symbol </param>
        /// <returns> BWT(Text) </returns>
        public string Solve(string text)
        {
            List<string> matrix=new List<string>();
    
            int l=text.Length-1;
            int i=0;
            while (i<l+1)
            {
                
                string new_text=$"{text[l]}";
                text=text.Remove(l);
                new_text+=text;
                text=new_text;
                matrix.Add(text);
                i+=1;
            }
            matrix.Sort();
            string last="";
            for(int index=0;index<matrix.Count;index++)
            {
                int len=matrix[index].Length-1;
                last+=(matrix[index][len]);
            }
            return last;
        }
    }
}
