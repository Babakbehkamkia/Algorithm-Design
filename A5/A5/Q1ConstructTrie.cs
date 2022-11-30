using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;

namespace A5
{
    public class Q1ConstructTrie : Processor
    {
        public Q1ConstructTrie(string testDataName) : base(testDataName)
        {
            this.VerifyResultWithoutOrder = true;
        }

        public override string Process(string inStr) =>
        TestTools.Process(inStr, (Func<long, String[], String[]>) Solve);

        public string[] Solve(long n, string[] patterns)
        {
            Dictionary<long,Dictionary<char,long>> tree = new Dictionary<long,Dictionary<char,long>>();
            // nodes=[]
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
            }
            foreach (Node node in nodes)
            {
                tree[node.key]=new Dictionary<char,long>();
                foreach (Edge edge in node.edges)
                {
                    tree[node.key][edge.value]=edge.rightNode.key;
                }

            }
            List<string> result= new List<string>();
            // tree=null;
            foreach (var node in nodes)
            {
                Dictionary<char,long> dict=tree[node.key];
                foreach (var c in dict)
                {
                    result.Add($"{node.key}->{c.Value}:{c.Key}");
                }
            }
            return result.ToArray();
        }
    }
}
