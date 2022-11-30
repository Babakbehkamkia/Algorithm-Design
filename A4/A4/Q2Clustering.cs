using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon;
using static A4.Q1BuildingRoads;

namespace A4
{
    public class Q2Clustering : Processor
    {
        public Q2Clustering(string testDataName) : base(testDataName) { }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, long[][], long, double>)Solve);

        public double Solve(long pointCount, long[][] points, long clusterCount)
        {
            double result=0;
            // Node rootNode=new Node(x:0,y:0,parent:-1);
            // Edge edge=new Edge(value:0,leftNode:0,rightNode:0);
            List<Node> nodes=new List<Node>();
            List<Edge> edges=new List<Edge>();
            for(int i=0;i<pointCount;i++)
            {
                Node node =new Node(points[i][0],points[i][1],null);
                nodes.Add(node);
            }
            for(int i=0;i<pointCount;i++)
            {
                for(int j=i+1;j<pointCount;j++)
                {
                    Edge edge =new Edge(distance(points[i][0],points[i][1],points[j][0],points[j][1]),nodes[i],nodes[j]);
                    edges.Add(edge);
                }
            }
            edges.Sort();
            long k=0;
            for(int i=0;i<edges.Count;i++)
            {
                Edge edge=edges[i];
                Node a=find(edge.leftNode);
                Node b=find(edge.rightNode);
                if (a!=b)
                {
                    k+=1;
                    union(a,b);
                    result+=edge.value;
                }
                if(k > nodes.Count - clusterCount)
                {
                    return Math.Round(edge.value,6);
                }
            }
            return -1;
        }
        public double distance(long x1,long y1,long x2,long y2)
        {
            return Math.Pow(Math.Pow((x1-x2),2)+Math.Pow((y1-y2),2),0.5);
        }
        public void union(Node leftNode,Node rightNode)
        {
            rightNode.parent=leftNode;
            rightNode.rank+=1;
        }
        public Node find(Node node)
        {
            Node old_parent=node;
            if (old_parent.parent==null)
            {
                node.rank=0;
                return node;
            }
            while (old_parent.parent!=null)
            {
                old_parent=old_parent.parent;
            }
            
            node.rank=1;
            node.parent=old_parent;
            return old_parent;
        }
    }
}
