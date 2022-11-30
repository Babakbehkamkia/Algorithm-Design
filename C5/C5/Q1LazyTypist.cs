using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace C5
{
    public class Q1LazyTypist : Processor
    {
        public Q1LazyTypist(string testDataName) : base(testDataName) { }

        public override string Process(string inStr)
        {
            var lines = inStr.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            return Solve(long.Parse(lines[0]), lines.Skip(1).ToArray()).ToString();
        }

        public long Solve(long n, string[] words)
        {
            // long m=0;
            (List<Node> nodes,long m)=makeTrie(n,words);
            Queue<Node> q=new Queue<Node>();
            q.Enqueue(nodes[0]);
            long count=-1;
            while(q.Count!=0)
            {
                
                Node currentNode=q.Dequeue();
                foreach (var edge in currentNode.edges)
                {
                    count+=1;
                    q.Enqueue(edge.rightNode);
                }
            }
            return count*2-m+n+2;
        }
        public (List<Node>,long) makeTrie(long n, string[] patterns)
        {
            // Dictionary<long,Dictionary<char,long>> tree = new Dictionary<long,Dictionary<char,long>>();
            // nodes=[]
            List<Node> nodes=new List<Node>();
            long m=0;
            // root=Node(0,-1,[])
            Node root =new Node(0,null);
            // nodes.append(root)
            nodes.Add(root);
            // index=1
            
            long index=1;
            for (int i=0;i<n;i++)
            {
                long count=0;
                Node current_parent=root;
                for(int j=0;j<patterns[i].Length;j++)
                {
                    count+=1;
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
                if(m<count)
                {
                    m=count;
                }

            }

            // foreach (Node node in nodes)
            // {
            //     tree[node.key]=new Dictionary<char,long>();
            //     foreach (Edge edge in node.edges)
            //     {
            //         tree[node.key][edge.value]=edge.rightNode.key;
            //     }

            // }
            // List<string> result= new List<string>();
            // // tree=null;
            // foreach (var node in nodes)
            // {
            //     Dictionary<char,long> dict=tree[node.key];
            //     foreach (var c in dict)
            //     {
            //         // print("{}->{}:{}".format(node, tree[node][c], c))
            //         result.Add($"{node.key}->{c.Value}:{c.Key}");
            //     }
            // }
            return (nodes,m);
        }
    }
}
