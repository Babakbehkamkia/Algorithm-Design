using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A7
{
    public class Q2CunstructSuffixArray : Processor
    {
        public Q2CunstructSuffixArray(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<String, long[]>)Solve);

        protected virtual long[] Solve(string text)
        {
            // write your code here        
            // throw new NotImplementedException();
            long[] order=initial_order(text);
            long[] kelas=initial_class(text,order);
            long L=1;
            while (L<text.Length)
            {
                order=update_order(text,L,order,kelas);
                kelas=update_class(text,L,order,kelas);
                L*=2;
            }
            return order;
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
