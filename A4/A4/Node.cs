namespace A4
{
    public class Node
    {
        public long x;
        public long y;
        public Node parent;
        public long rank;

        public Node(long x, long y, Node parent)
        {
            this.x = x;
            this.y = y;
            this.parent = parent;
            this.rank=0;
        }
    }
}