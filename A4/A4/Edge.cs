using System;
using System.Diagnostics.CodeAnalysis;

namespace A4
{
    public class Edge : IComparable<Edge>
    {
        public double value;
        public Node leftNode;
        public Node rightNode;

        public Edge(double value, Node leftNode, Node rightNode)
        {
            this.value = value;
            this.leftNode = leftNode;
            this.rightNode = rightNode;
        }

        public int CompareTo(Edge other)
        {
            return value.CompareTo(other.value);
        }
        // Define the is greater than operator.
        public static bool operator >  (Edge e1, Edge e2)
        {
        return e1.CompareTo(e2) > 0;
        }

        // Define the is less than operator.
        public static bool operator <  (Edge e1, Edge e2)
        {
        return e1.CompareTo(e2) < 0;
        }

        // Define the is greater than or equal to operator.
        public static bool operator >=  (Edge e1, Edge e2)
        {
        return e1.CompareTo(e2) >= 0;
        }

        // Define the is less than or equal to operator.
        public static bool operator <=  (Edge e1, Edge e2)
        {
        return e1.CompareTo(e2) <= 0;
        }
    }
}