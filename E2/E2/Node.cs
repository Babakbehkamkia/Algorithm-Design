using System;
using System.Collections.Generic;

namespace E2
{
    public class Node
    {
        public Tuple<int,int> key;
        public int count;
        public Node parent;
        // public char? value;
        public List<Edge> edges;
        public bool is_ending;

        public Node(Tuple<int,int> key,int count, Node parent)
        {
            this.key = key;
            this.count=count;
            this.parent = parent;
            // this.value=value;
            this.edges=new List<Edge>();
            this.is_ending=false;
        }
    }
}