using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q3PatternMatchingSuffixArray : Processor
    {
        public Q3PatternMatchingSuffixArray(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, string[], long[]>)Solve, "\n");

        protected virtual long[] Solve(string text, long n, string[] patterns)
        {
            // write your code here
            // throw new NotImplementedException();
            string text2=text+"$";
            long[] order=initial_order(text2);
            long[] kelas=initial_class(text2,order);
            long L=1;
            while (L<text2.Length)
            {
                order=update_order(text2,L,order,kelas);
                kelas=update_class(text2,L,order,kelas);
                L*=2;
            }
            // ---------------------------------------
            // Dictionary<long,bool> dict=new Dictionary<long, bool>();
            List<long> result=new List<long>();
            foreach(string pattern in patterns)
            {
                // int start=0;
                // int end=order.Length;
                // for(int i=0;i<pattern.Length;i++)
                // {
                //     if(end-start<2)
                //     {
                //         break;
                //     }
                //     start=binaraySearchMin(start,end,order,pattern,text,i);
                //     end=binaraySearchMax(start,end,order,pattern,text,i);
                // }
                // ---------------------------
                patternMaching(text,pattern,order,result);
                // int start=p.Item1;
                // int end=p.Item2;
                // if (end==-1 && start==-1)
                // {
                //     return new long[1]{-1};
                // }
                // if(start!=end)
                // {
                //     for(int i=start;i<end+1;i++)
                //     {
                //         if(!result.Contains(order[i+1]))
                //         {
                //             result.Add(order[i+1]);
                //         }
                //     }
                // }
            }
            if (result.Count==0)
            {
                return new long[1]{-1};
            }
            result.Reverse();
            return result.ToArray();
        }
        // public int binaraySearchMin(int start,int end,long[] order,string pattern,string text,int index)
        // {
        //     if(end-start<1)
        //     {
        //         return end;
        //     }
        //     int avg=(int)((start+end)/2);
        //     if(text[(int)order[avg]+index]<pattern[index])
        //     {
        //         return binaraySearchMin(avg+1,end,order,pattern,text,index);
        //     }
        //     else
        //     {
        //         return binaraySearchMin(start,avg,order,pattern,text,index);
        //     }
        // }
        // public int binaraySearchMax(int start,int end,long[] order,string pattern,string text,int index)
        // {
        //     if(end-start<2)
        //     {
        //         return start;
        //     }
        //     int avg=(int)((start+end)/2);
        //     if(text[(int)order[avg]+index]>pattern[index])
        //     {
        //         return binaraySearchMax(start,avg,order,pattern,text,index);
        //     }
        //     else
        //     {
                
        //         return binaraySearchMax(avg,end,order,pattern,text,index);
        //     }
        // }
        public void patternMaching(string text,string pattern,long[] order,List<long> result)
        {
            int min=0;
            int max=text.Length;
            while(min<max)
            {
                int mid=(min+max)/2;
                // string title = str.Substring(startIndex, endIndex);
                // if((int)order[mid]+pattern.Length>=text.Length)
                // {
                //     break;
                // }
                // string str=text.Substring((int)order[mid],pattern.Length);
                // // 
                int i;
                for(i=0;i<pattern.Length;i++)
                {
                    if(order[mid+1]+i>text.Length)
                    {
                        break;
                    }
                    if(order[mid+1]+i==text.Length)
                    {
                        min=mid+1;
                        break;
                    }
                    if (pattern[i]>text[(int)order[mid+1]+i])
                    {
                        min=mid+1;
                        break;
                    }
                    else if(pattern[i]<text[(int)order[mid+1]+i])
                    {
                        max=mid;
                        break;
                    }
                }
                if (i==pattern.Length) 
                {
                    max=mid;
                }
            }
            var start=min;
            max=text.Length;
            while(min<max)
            {
                int mid=(min+max)/2;
                // if((int)order[mid]+pattern.Length>=text.Length)
                // {
                //     break;
                // }
                // string str=text.Substring((int)order[mid],pattern.Length);
                // if (string.Compare(pattern,str)<0)
                // {
                //     max=mid;
                // }
                // else
                // {
                //     min=mid+1;
                // }
                int i;
                for(i=0;i<pattern.Length;i++)
                {
                    if(order[mid+1]+i>text.Length)
                    {
                        break;
                    }
                    if(order[mid+1]+i==text.Length)
                    {
                        min=mid+1;
                        break;
                    }
                    else if(pattern[i]<text[(int)order[mid+1]+i])
                    {
                        max=mid;
                        break;
                    }
                }
                if (i==pattern.Length && order[mid+1]+i<=text.Length) 
                {
                    min=mid+1;
                }
            }
            int end=max;
            // if (start>end)
            // {
            //     return new Tuple<int, int>(-1,-1);
            // }
            // return new Tuple<int, int>(start,end);
            if (start<=end)
            {
                for(int i=start+1;i<end+1;i++)
                {
                    if (!result.Contains(order[i]))
                    {
                        result.Add(order[i]);
                    }
                }
                
            }
        }
        public long[] initial_order(string text)
        {
            // count=[0 for i in range(5)]
            long[] count=new long[5];
            // order=[0 for i in range(len(text))]
            long[] order=new long[text.Length];
            for (int i=0;i<text.Length;i++)
            {
                if (text[i]=='$')
                {
                    count[0]+=1;
                }
                else if (text[i]=='A')
                {
                    count[1]+=1;
                }
                else if (text[i]=='C')
                {
                    count[2]+=1;
                }
                else if (text[i]=='G')
                {
                    count[3]+=1;
                }
                else if (text[i]=='T')
                {
                    count[4]+=1;
                }
            }
            for (int i=1;i<count.Length;i++)
            {
                count[i]+=count[i-1];
            }
            for (int i=text.Length-1;i>-1;i--)
            {
                int c=0;
                if (text[i]=='A')
                {
                    c=1;
                }
                else if (text[i]=='C')
                {
                    c=2;
                }
                else if (text[i]=='G')
                {
                    c=3;
                }
                else if (text[i]=='T')
                {
                    c=4;
                }
                count[c]-=1;
                order[count[c]]=i;
            }
            return order;
        }

        public long[] initial_class(string text,long[] order)
        {
            // kelas=[0 for i in range(len(order))]
            long[] kelas=new long[order.Length];
            kelas[order[0]]=0;
            for (int i=1;i<order.Length;i++)
            {
                if (text[(int)order[i]]==text[(int)order[i-1]])
                {
                    kelas[order[i]]=kelas[order[i-1]];
                }
                else
                {
                    kelas[order[i]]=kelas[order[i-1]]+1;
                }
            }
            return kelas;
        }


        public long[] update_order(string text,long L,long[] order,long[] kelas)
        {
            // count=[0 for i in range(len(text))]
            long[] count=new long[text.Length];
            // new_order=[0 for i in range(len(text))]
            long[] new_order=new long[text.Length];
            for (int i=0;i<text.Length;i++)
            {
                count[kelas[i]]+=1;
            }
            for (int i=1;i<text.Length;i++)
            {
                count[i]+=count[i-1];
            }
            for (int i=text.Length-1;i>-1;i--)
            {
                long start=(order[i]-L+text.Length)%text.Length;
                long cl=kelas[start];
                count[cl]-=1;
                new_order[count[cl]]=start;
            }
            return new_order;
        }


        public long[] update_class(string text,long L,long[] order,long[] kelas)
        {
            // new_kelas=[0 for i in range(len(kelas))]
            long[] new_kelas=new long[kelas.Length];
            new_kelas[order[0]]=0;
            for (int i=1;i<order.Length;i++)
            {
                long cur=order[i];
                long prev=order[i-1];
                long mid=(order[i]+L)%text.Length;
                long mid_prev=(order[i-1]+L)%text.Length;
                if (kelas[cur]!=kelas[prev] || kelas[mid]!=kelas[mid_prev])
                {
                    new_kelas[cur]=new_kelas[prev]+1;
                }
                else
                {
                    new_kelas[cur]=new_kelas[prev];
                }
            }
            return new_kelas;
        }
    }
}
