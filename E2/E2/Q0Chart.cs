using System;
using System.Collections.Generic;
using TestCommon;

namespace E2
{
    public class Q0Chart : Processor
    {
        public Q0Chart(string testDataName) : base(testDataName)
        {}

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