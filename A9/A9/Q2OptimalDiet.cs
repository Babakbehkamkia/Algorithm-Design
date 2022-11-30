using System;
using System.Collections.Generic;
using TestCommon;

namespace A9
{
    public class Q2OptimalDiet : Processor
    {
        public Q2OptimalDiet(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<int, int, double[,], String>)Solve);

        public string Solve(int N, int M, double[,] matrix1)
        {
            // Comment the line below and write your code here
            
            long[] allEquetions=new long[N+M];
            
            for(int i=0;i<N+M;i++)
            {
                allEquetions[i]=i;
            }
            List<long[]> subsets=new List<long[]>();
            for(int i=0;i<N+1;i++)
            {
                findingSubsets(allEquetions,N,M,new List<long>(),subsets,i);
            }
            List<double[]> matrix=new List<double[]>();
            double[] optimum=makeList(N,M,matrix1,matrix);
            
            List<double[]> solutions=new List<double[]>();
            for(int i=0;i<subsets.Count;i++)
            {
                double[,] newMatrix=new double[M,M+1];
                for(int count=0;count<M;count++)
                {
                    for(int j=0;j<M+1;j++)
                    {
                        newMatrix[count,j]=matrix[(int)subsets[i][count]][j];
                    }
                }
                solutions.Add(findingSolution(M,newMatrix));
            }
            double max=double.MinValue;
            int maxIndex=-1;
            for(int i=0;i<solutions.Count;i++)
            {
                if(checking(N,M,solutions[i],matrix))
                {
                    double sum=0;
                    for(int j=0;j<M;j++)
                    {
                        sum+=solutions[i][j]*optimum[j];
                    }
                    if(sum>=max)
                    {
                        max=sum;
                        maxIndex=i;
                    }
                }
            }
            // Q1InferEnergyValues.Solve()
            // throw new NotImplementedException();
            if(maxIndex==-1)
            {
                return "No Solution";
            }
            
            for(int i=0;i<solutions[maxIndex].Length;i++)
            {
                double ans=solutions[maxIndex][i];
                double ansReal=(int) ans;
                double ansAshar=ans%1;
                if(ansAshar>0.25 && ansAshar<0.75)
                {
                    ansReal+=0.5;
                }
                else if(ansAshar>0.75)
                {
                    ansReal+=1;
                }
                solutions[maxIndex][i]=ansReal;
            }
            string str="Bounded Solution\n";
            str+=string.Join(" ",solutions[maxIndex]);
            return str;
        }

        public bool checking(int N,int M,double[] solution,List<double[]> matrix)
        {
            // throw new NotImplementedException();
            for(int i=0;i<M;i++)
            {
                if(solution[i]<0)
                {
                    return false;
                }
            }
            for(int i=0;i<N;i++)
            {
                double sum=0;
                for(int j=0;j<M;j++)
                {
                    sum+=solution[j]*matrix[i][j];
                }
                if(sum-matrix[i][M]>0.000000001)
                {
                    return false;
                }
            }
            return true;
        }

        public void findingSubsets(long[] allEquetions,int N,int M,List<long> pivot,List<long[]> subsets,long pivotToAdd)
        {
            pivot.Add(pivotToAdd);
            if(pivot.Count!=M)
            {
                for(int i=(int)pivotToAdd+1;i<N+M;i++) //?
                {
                    if(!pivot.Contains(i))
                    {
                        List<long> copy= new List<long>(pivot);
                        findingSubsets(allEquetions,N,M,pivot,subsets,i);
                        pivot=copy;
                    }
                }
            }
            else
            {
                long[] copy2= pivot.ToArray();
                subsets.Add(copy2);
            }
        }
        public double[] makeList(int N,int M,double[,] matrix1,List<double[]> matrix)
        {
            for(int i=0;i<N;i++)
            {
                double[] row =new double[M+1];
                for(int j=0;j<M;j++)
                {
                    row[j]=matrix1[i,j];
                }
                row[M]=matrix1[i,M];
                matrix.Add(row);
            }
            for(int i=0;i<M;i++)
            {
                double[] row =new double[M+1];
                for(int j=0;j<M;j++)
                {
                    if(i==j)
                    {
                        row[j]=1;
                    }
                    else
                    {
                        row[j]=0;
                    }
                }
                row[M]=0;
                matrix.Add(row);
            }
            double[] optimum=new double[M];
            for(int i=0;i<M;i++)
            {
                optimum[i]=matrix1[N,i];
            }
            return optimum;
        }



        //q1
        public static double[] findingSolution(long MATRIX_SIZE, double[,] matrix)
        {
            // Comment the line below and write your code here
            
            int row=0; 
            int col=0;
            while (row<MATRIX_SIZE)
            {
                double value_to_divide=matrix[row,col];
                if (value_to_divide!=0 )
                {
                    if (value_to_divide!=1)
                    {
                        for (int i=col;i<MATRIX_SIZE+1;i++)
                        {
                            matrix[row,i]/=value_to_divide;
                        }
                    }
                    for (int i=0;i<MATRIX_SIZE;i++)
                    {
                        if (i==row)
                        {
                            continue;
                        }
                        double k=matrix[i,col]/matrix[row,col];
                        for (int j=col;j<MATRIX_SIZE+1;j++)
                        {
                            matrix[i,j]-=k*matrix[row,j];
                        }
                    }
                    row+=1;
                    col+=1;
                }
                else
                {
                    int row_to_change=row+1;
                    while (row_to_change<MATRIX_SIZE)
                    {
                        if (matrix[row_to_change,col]!=0)
                        {
                            // (matrix[row],matrix[row_to_change])=(matrix[row_to_change],matrix[row])
                            for (int i = 0; i <= MATRIX_SIZE; i++) 
                            {
                                var t = matrix[row, i];
                                matrix[row, i] = matrix[row_to_change, i];
                                matrix[row_to_change, i] = t;
                            }
                            break;
                        }
                        row_to_change+=1;
                    }
                    if (row_to_change==MATRIX_SIZE)
                    {
                        // not_valid=true;
                        return new double[MATRIX_SIZE];

                    }
                }
            }
            double[] result =new double[MATRIX_SIZE];
            for(int j=0;j<MATRIX_SIZE;j++)
            {
                for(int i=0;i<MATRIX_SIZE;i++)
                {
                    if(matrix[i,j]==1)
                    {
                        double ans=matrix[i,MATRIX_SIZE];
                        result[j]=ans;
                    }
                }
            }
            return result;
        }
    }
}
