using System;

namespace E2
{
    public class Edge : IComparable<Edge>
    {
        public char value;
        public Node leftNode;
        public Node rightNode;

        public Edge(char value, Node leftNode, Node rightNode)
        {
            this.value = value;
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }

        public int CompareTo(Edge obj)
        {
            return value.CompareTo(obj.value);
        }
    }
}