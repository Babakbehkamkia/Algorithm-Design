using System;
using System.Collections.Generic;
using System.Linq;
using TestCommon;

namespace E2
{
    public class Q1Times : Processor
    {
        public Q1Times(string testDataName) : base(testDataName)
        {
            ExcludeTestCaseRangeInclusive(41,41);
        }

        public override string Process(string inStr)
            => E2Processors.Q1Processor(inStr, Solve);

        public string[] Solve(char[][] board, string[] words)
        {
            // List<Node> nodes=makeTree(board);
            // List<string> result=new List<string>();
            // foreach(string word in words)
            // {
            //     Node currentNode=nodes[0];
            //     bool isFound=true;
            //     foreach(char letter in word)
            //     {
            //         // Edge edge=new Edge(letter,null,null);
            //         bool isExist=false;
            //         for(int i=0;i<currentNode.edges.Count;i++)
            //         {
            //             if(currentNode.edges[i].value==letter)
            //             {
            //                 currentNode=nodes[currentNode.edges[i].rightNode.count];
            //                 isExist=true;
            //                 break;
            //             }
            //         }
            //         if(!isExist)
            //         {
            //             isFound=false;
            //             break;
            //         }
            //     }
            //     if(isFound)
            //     {
            //         result.Add(word);
            //     }
            // }
            // return result.ToArray();

            List<string> result = new List<string>();
            foreach (string word in words)
            {
                bool isFound = false;
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board[i].Length; j++)
                    {
                        if (DFS(i,j,board, word))
                        {
                            result.Add(word);
                            isFound=true;
                            break;
                        }
                    }
                    if (isFound)
                    {
                        break;
                    }
                }
            }
            return result.OrderBy(x => x).ToArray();

            
            // throw new NotImplementedException();
        }



        public bool DFS(int i,int j,char[][] board,string pattern)
        {
            int count=0;
            List<Tuple<int,int,int[]>> search=new List<Tuple<int,int,int[]>>();
            search.Add(new Tuple<int, int,int[]>(i,j,new int[4]));

            while(search.Count>0)
            {
                bool isChosen =false;
                Tuple<int,int,int[]> item = search[search.Count-1];
                int row=item.Item1;
                int col=item.Item2;
                int[] visited=item.Item3;
                if (board[row][col] != pattern[count])
                {
                    search.RemoveAt(search.Count-1);
                    count-=1;
                    continue;
                }
                if (count == pattern.Length - 1)
                {
                    return true;
                }

                bool isExist=false;
                if (row - 1 >= 0 && visited[0]==0 && !isChosen)
                {
                    isChosen=true;
                    visited[0]=1;
                    Tuple<int,int,int[]> itemToAdd=new Tuple<int, int,int[]>(row - 1, col,new int[4]);
                    foreach(var t in search)
                    {
                        if(t.Item1==itemToAdd.Item1 && t.Item2==itemToAdd.Item2)
                        {
                            isExist=true;
                            break;
                        }
                    }
                    if(isExist)
                    {
                        continue;
                    }
                    search.Add(itemToAdd);
                    count+=1;
                }

                if (col - 1 >= 0 && visited[1]==0 && !isChosen)
                {
                    isChosen=true;
                    visited[1]=1;
                    Tuple<int,int,int[]> itemToAdd=new Tuple<int, int,int[]>(row , col-1,new int[4]);
                    foreach(var t in search)
                    {
                        if(t.Item1==itemToAdd.Item1 && t.Item2==itemToAdd.Item2)
                        {
                            isExist=true;
                            break;
                        }
                    }
                    if(isExist)
                    {
                        continue;
                    }
                    search.Add(itemToAdd);
                    count+=1;
                }

                if (row + 1 < board.Length && visited[2]==0 && !isChosen)
                {
                    isChosen=true;
                    visited[2]=1;
                    Tuple<int,int,int[]> itemToAdd=new Tuple<int, int,int[]>(row + 1, col,new int[4]);
                    foreach(var t in search)
                    {
                        if(t.Item1==itemToAdd.Item1 && t.Item2==itemToAdd.Item2)
                        {
                            isExist=true;
                            break;
                        }
                    }
                    if(isExist)
                    {
                        continue;
                    }
                    search.Add(itemToAdd);
                    count+=1;
                }

                if (col + 1 < board[0].Length && visited[3]==0 && !isChosen)
                {
                    isChosen=true;
                    visited[3]=1;
                    Tuple<int,int,int[]> itemToAdd=new Tuple<int, int,int[]>(row , col+1,new int[4]);
                    foreach(var t in search)
                    {
                        if(t.Item1==itemToAdd.Item1 && t.Item2==itemToAdd.Item2)
                        {
                            isExist=true;
                            break;
                        }
                    }
                    if(isExist)
                    {
                        continue;
                    }
                    search.Add(itemToAdd);
                    count+=1;
                }
                if(!isChosen)
                {
                    search.RemoveAt(search.Count-1);
                    count-=1;
                }
            }
            return false;


        }
        public List<Node> makeTree(char[][] board)
        {
            List<Node> nodes=new List<Node>();
            int count=0;
            Node root =new Node(new Tuple<int, int>(-1,-1),count,null);
            count+=1;
            nodes.Add(root);
            for (int i=0;i<board.Length;i++)
            {
                for(int j=0;j<board[i].Length;j++)
                {
                    // Node current_parent=root;
                    
                    int[][] visited=new int[board.Length][];
                    for(int k=0;k<board.Length;k++)
                    {
                        visited[k]=new int[board.Length];
                    }
                    Queue<Node> q=new Queue<Node>();
                    Node first=new Node(new Tuple<int, int>(i,j),count,null);
                    // count+=1;
                    Edge firstEdge=new Edge(board[i][j],root,first);
                    if(findPlace(firstEdge,root,nodes))
                    {
                        count++;
                    }
                    // nodes.Add(first);
                    q.Enqueue(first);
                    visited[i][j]=1;
                    while(q.Count!=0)
                    {
                        
                        Node n=q.Dequeue();
                        if(n.key.Item1+1<board.Length && visited[n.key.Item1+1][n.key.Item2]==0)
                        {
                            visited[n.key.Item1+1][n.key.Item2]=1;
                            Node item=new Node(new Tuple<int, int>(n.key.Item1+1,n.key.Item2),count,n);
                            // count+=1;
                            Edge edge=new Edge(board[n.key.Item1+1][n.key.Item2],n,item);
                            q.Enqueue(item);
                            if(findPlace(edge,n,nodes))
                            {
                                count++;
                            }
                            // Node node=new Node(index,n);
                        }
                        if(n.key.Item1-1>0 && visited[n.key.Item1-1][n.key.Item2]==0 )
                        {
                            // visited[n.Item1-1,n.Item2]=1;
                            visited[n.key.Item1-1][n.key.Item2]=1;
                            Node item=new Node(new Tuple<int, int>(n.key.Item1-1,n.key.Item2),count,n);
                            // count+=1;
                            Edge edge=new Edge(board[n.key.Item1-1][n.key.Item2],n,item);
                            q.Enqueue(item);
                            if(findPlace(edge,n,nodes))
                            {
                                count++;
                            }
                        }
                        if(n.key.Item2+1<board.Length && visited[n.key.Item1][n.key.Item2+1]==0 )
                        {
                            visited[n.key.Item1][n.key.Item2+1]=1;
                            Node item=new Node(new Tuple<int, int>(n.key.Item1,n.key.Item2+1),count,n);
                            // count+=1;
                            Edge edge=new Edge(board[n.key.Item1][n.key.Item2+1],n,item);
                            q.Enqueue(item);
                            if(findPlace(edge,n,nodes))
                            {
                                count++;
                            }
                        }
                        if(n.key.Item2-1>0 && visited[n.key.Item1][n.key.Item2-1]==0)
                        {
                            // visited[n.Item1,n.Item2-1]=1;
                            visited[n.key.Item1][n.key.Item2-1]=1;
                            Node item=new Node(new Tuple<int, int>(n.key.Item1,n.key.Item2-1),count,n);
                            // count+=1;
                            Edge edge=new Edge(board[n.key.Item1][n.key.Item2-1],n,item);
                            q.Enqueue(item);
                            if(findPlace(edge,n,nodes))
                            {
                                count++;
                            }
                        }
                    }
                    // for(int j=0;j<patterns[i].Length;j++)
                    // {
                        // char letter=patterns[i][j];
                        // Node node=new Node(index,current_parent);
                        
                    //     Edge edge=new Edge(value:letter,leftNode:current_parent,rightNode:node);
                    //     bool is_exist=false;
                    //     foreach (Edge item in current_parent.edges)
                    //     {
                    //         if (edge.value==item.value)
                    //         {
                    //             node=item.rightNode;
                    //             is_exist=true;
                    //         }
                    //     }
                    //     if (!is_exist)
                    //     {
                    //         current_parent.edges.Add(edge);
                    //         nodes.Add(node);
                    //         index+=1;
                    //     }
                    //     current_parent=node;
                    // }
                }
            }
            return nodes;
        }
        public bool findPlace(Edge edge,Node parent,List<Node> nodes)
        {
            bool isFound=false;
            foreach(var e in nodes[parent.count].edges)
            {
                if(e.CompareTo(edge)==0)
                {
                    isFound=true;
                    edge.rightNode.count=edge.leftNode.count;
                }
            }
            if(!isFound)
            {
                nodes[parent.count].edges.Add(edge);
                nodes.Add(edge.rightNode);
                return true;
            }
            return false;
        }
    }
}