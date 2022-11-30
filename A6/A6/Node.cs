using System;

namespace A6
{
    public class Node : IComparable<Node>
    {
        public int suffix;
        public string text;

        public Node(int suffix, string text)
        {
            this.suffix = suffix;
            this.text = text;
        }

        public int CompareTo(Node other)
        {
            return text.CompareTo(other.text);
        }
        // public static bool operator > (Node n1,Node n2)
        // {
        //     return n1.CompareTo(n2)>0;
        // }
        // public static bool operator < (Node n1,Node n2)
        // {
        //     return n1.CompareTo(n2)<0;
        // }
        // public static bool operator >= (Node n1,Node n2)
        // {
        //     return n1.CompareTo(n2)>=0;
        // }
        // public static bool operator <= (Node n1,Node n2)
        // {
        //     return n1.CompareTo(n2)<=0;
        // }
    }
}