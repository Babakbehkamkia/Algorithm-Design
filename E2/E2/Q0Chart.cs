using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q0Chart : Processor
    {
        public Q0Chart(string testDataName) : base(testDataName)
        {
            // ExcludeTestCaseRangeInclusive(2,2);
            // ExcludeTestCaseRangeInclusive(6,6);
            // ExcludeTestCaseRangeInclusive(14,15);
            // ExcludeTestCaseRangeInclusive(17,17);
            // ExcludeTestCaseRangeInclusive(21,21);
            // ExcludeTestCaseRangeInclusive(28,28);
            // ExcludeTestCaseRangeInclusive(31,31);
            // ExcludeTestCaseRangeInclusive(36,36);
        }

        public override string Process(string inStr) =>
            E2Processors.Q0Processor(inStr, Solve);

        public string[] Solve(int professorsCount,
                              int classCount,
                              int timeCount,
                              long[,,] professorsCanTeach,
                              long[,,] classCanBeOccupied)
        {
            // throw new NotImplementedException();
            List<string[]> onlyOne=new List<string[]>();
            teachesOnlyOneClassInOnTime(professorsCount,classCount,timeCount,onlyOne);
            classOnlyOneTeacher(professorsCount,classCount,timeCount,onlyOne);
            TeachesAtleastOne(professorsCount,classCount,timeCount,onlyOne);
            ClassAtleastOne(professorsCount,classCount,timeCount,onlyOne);
            for(int k=0;k<timeCount;k++)
            {
                for(int i=0;i<professorsCount;i++)
                {
                    for(int j=0;j<classCount;j++)
                    {
                        if(professorsCanTeach[i,j,k]==-1 || classCanBeOccupied[i,j,k]==-1)
                        {
                            onlyOne.Add(new string[1]{$"{-(k*classCount*professorsCount+j*professorsCount+i+1)}"});
                        }
                    }
                }
            }
            // for(int k=1;k<timeCount+1;k++)
            // {
            //     string[] onlyOneOfOR=new string[professorsCount*classCount];
                
            //     for(int j=1;j<classCount+1;j++)
            //     {
            //         string[] onlyOneTeacherPerClass=new string[professorsCount];
            //         for(int i=1;i<professorsCount+1;i++)
            //         {
            //             // if(professorsCanTeach[k-1,j-1,i-1]==1 && classCanBeOccupied[])
            //             onlyOneOfOR[(j-1)*classCount+i-1]=$"{k*timeCount+j*classCount+i}";
            //             onlyOneTeacherPerClass[i-1]=$"{k*timeCount+j*classCount+i}";
            //             //first
            //             for(int itter1=j;itter1<classCount+1;itter1++)
            //             {
            //                 for(int itter2=1;itter2<professorsCount+1;itter2++)
            //                 {
            //                     if(!(itter1==j && itter2<=i))
            //                     {
            //                         onlyOne.Add(new string[2]{$"{-(k*timeCount+j*classCount+i)}",$"{-(k*timeCount+itter1*classCount+itter2)}"});
            //                     }
            //                 }
            //             }
            //             //second
            //             for(int itter3=i+1;itter3<professorsCount+1;itter3++)
            //             {
            //                 onlyOne.Add(new string[2]{$"{-(k*timeCount+j*classCount+i)}",$"{-(k*timeCount+j*classCount+itter3)}"});
            //             }
            //         }
            //         onlyOne.Add(onlyOneTeacherPerClass);
            //     }
            //     onlyOne.Add(onlyOneOfOR);
                
            // }
            string[] ans=new string[onlyOne.Count+1];
            ans[0]=$"{onlyOne.Count} {professorsCount*classCount*timeCount*2}";
            for(int i=0;i<onlyOne.Count;i++)
            {
                List<string> newstr=new List<string>();
                for(int j=0;j<onlyOne[i].Length;j++)
                {
                    newstr.Add(onlyOne[i][j]);
                }
                newstr.Add("0");
                string result=string.Join(" ",newstr);
                ans[i+1]=result;
            }
            return ans;
            // throw new NotImplementedException();
        }
        public void teachesOnlyOneClassInOnTime(int professorsCount,
                                                int classCount,
                                                int timeCount,
                                                List<string[]> onlyOne)
        {
            for(int k=0;k<timeCount;k++)
            {
                for(int i=0;i<professorsCount;i++)
                {
                    for(int j=0;j<classCount;j++)
                    {
                        for(int itter=j+1;itter<classCount;itter++)
                        {
                            onlyOne.Add(new string[2]{$"{-(k*classCount*professorsCount+j*professorsCount+i+1)}",$"{-(k*classCount*professorsCount+itter*professorsCount+i+1)}"});
                        }
                    }
                }
            }
        }
        public void classOnlyOneTeacher(int professorsCount,
                                        int classCount,
                                        int timeCount,
                                        List<string[]> onlyOne)
        {
            for(int k=0;k<timeCount;k++)
            {
                for(int j=0;j<classCount;j++)
                {
                    for(int i=0;i<professorsCount;i++)
                    {
                    
                        for(int itter=i+1;itter<professorsCount;itter++)
                        {
                            onlyOne.Add(new string[2]{$"{-(k*classCount*professorsCount+j*professorsCount+i+1)}",$"{-(k*classCount*professorsCount+j*professorsCount+itter+1)}"});
                        }
                    }
                }
            }
        }

        public void TeachesAtleastOne(int professorsCount,
                                        int classCount,
                                        int timeCount,
                                        List<string[]> onlyOne)
        {
            for(int i=0;i<professorsCount;i++)
            {
                string[] AtleastOneTeacherPertime=new string[timeCount*classCount];
                int count=0;
                for(int k=0;k<timeCount;k++)
                {
                    for(int j=0;j<classCount;j++)
                    {
                        AtleastOneTeacherPertime[count]=$"{(k*classCount*professorsCount+j*professorsCount+i+1)}";
                        count+=1;
                    }
                }
                onlyOne.Add(AtleastOneTeacherPertime);
            }
        }

        public void ClassAtleastOne(int professorsCount,
                                        int classCount,
                                        int timeCount,
                                        List<string[]> onlyOne)
        {
            for(int j=0;j<classCount;j++)
            {
                string[] AtleastOneTeacherPertime=new string[timeCount*professorsCount];
                int count=0;
                for(int k=0;k<timeCount;k++)
                {
                    for(int i=0;i<professorsCount;i++)
                    {
                        AtleastOneTeacherPertime[count]=$"{(k*classCount*professorsCount+j*professorsCount+i+1)}";
                        count+=1;
                    }
                }
                onlyOne.Add(AtleastOneTeacherPertime);
            }
        }

        public override Action<string, string> Verifier { get; set; } =
            TestTools.SatVerifier;

    }

}