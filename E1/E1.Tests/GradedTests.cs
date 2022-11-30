using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon;
using E1;

namespace E1.Tests
{
    [DeploymentItem("TestData", "E1_TestData")]    
    [TestClass()]
    public class GradedTests
    {
        [TestMethod(), Timeout(70000)]
        public void SolveTest_Q1BetweennessTest()
        {
            RunTest(new Q1Betweenness("TD1"));

        }

        [TestMethod(), Timeout(5000)]
        [DeploymentItem("TestData", "E1_TestData")]
        public void SolveTest_Q2RoadReconstruction()
        {
            Assert.Inconclusive();
            RunTest(new Q2RoadReconstruction("TD2"));
        }


        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("E1", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}
