namespace A5
{
    public class Edge
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
    }
}