using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q2MultiplePatternMatching : Processor
    {
        public Q2MultiplePatternMatching(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<String, long, String[], long[]>)Solve);

        public long[] Solve(string text, long n, string[] patterns)
        {
            List<Node> nodes=makeTrie(n,patterns);
            List<long> result=new List<long>();
            for (int i=0;i<text.Length;i++)
            {
                long index=0;
                int j=i;
                bool is_find=true;
                while (nodes[(int)index].edges.Count!=0 && j<text.Length)
                {
                    if (nodes[(int)index].is_ending)
                    {
                        is_find=true;
                        break;
                    }
                    bool is_exist=false;
                    foreach (var item in nodes[(int)index].edges)
                    {
                        if (text[j]==item.value)
                        {
                            index=item.rightNode.key;
                            j+=1;


                            is_exist=true;
                            break;
                        }
                    }
                    if (!is_exist)
                    {
                        is_find=false;
                        break;
                    }
                }
                if (is_find && nodes[(int)index].is_ending)
                {
                    result.Add(i);
                }
            }
            if(result.Count==0)
            {
                return new long[1]{-1};
            }
            return result.ToArray();
        }
        public List<Node> makeTrie(long n,string[] patterns)
        {
            List<Node> nodes=new List<Node>();
            // root=Node(0,-1,[])
            Node root =new Node(0,null);
            // nodes.append(root)
            nodes.Add(root);
            // index=1
            long index=1;
            for (int i=0;i<n;i++)
            {
                Node current_parent=root;
                for(int j=0;j<patterns[i].Length;j++)
                {
                    char letter=patterns[i][j];
                    Node node=new Node(index,current_parent);
                    
                    Edge edge=new Edge(value:letter,leftNode:current_parent,rightNode:node);
                    bool is_exist=false;
                    foreach (Edge item in current_parent.edges)
                    {
                        if (edge.value==item.value)
                        {
                            node=item.rightNode;
                            is_exist=true;
                        }
                    }
                    if (!is_exist)
                    {
                        current_parent.edges.Add(edge);
                        nodes.Add(node);
                        index+=1;
                    }
                    current_parent=node;
                }
                current_parent.is_ending=true;
            }
            return nodes;
        }
    }
}
