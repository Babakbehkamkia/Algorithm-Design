using System;
using TestCommon;

namespace A9
{
    public class Q1InferEnergyValues : Processor
    {
        public Q1InferEnergyValues(string testDataName) : base(testDataName)
        {
        }

        public override string Process(string inStr) =>
            TestTools.Process(inStr, (Func<long, double[,], double[]>)Solve);

        public static double[] Solve(long MATRIX_SIZE, double[,] matrix)
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
                        double ansReal=(int) ans;
                        double ansAshar=ans%1;
                        if(ansReal<0)
                        {
                            if(ansAshar<-0.25 && ansAshar>-0.75)
                            {
                                ansReal-=0.5;
                            }
                            else if(ansAshar<-0.75)
                            {
                                ansReal-=1;
                            }
                        }
                        else
                        {
                            if(ansAshar>0.25 && ansAshar<0.75)
                            {
                                ansReal+=0.5;
                            }
                            else if(ansAshar>0.75)
                            {
                                ansReal+=1;
                            }
                        }
                        result[j]=ansReal;
                    }
                    // else
                    // {
                    //     if(matrix[i,MATRIX_SIZE]!=0)
                    //     {
                    //         return new double[MATRIX_SIZE];
                    //     }
                    // }
                }
            }
            return result;
        }
    }
}
