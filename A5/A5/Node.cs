using System.Collections.Generic;

namespace A5
{
    public class Node
    {
        public long key;
        public Node parent;
        public List<Edge> edges;
        public bool is_ending;

        public Node(long key, Node parent)
        {
            this.key = key;
            this.parent = parent;
            this.edges=new List<Edge>();
            this.is_ending=false;
        }
    }
}