using Microsoft.VisualStudio.TestTools.UnitTesting;
using E2;
using TestCommon;

namespace E2.Tests
{
    [DeploymentItem("TestData", "E2_TestData")]
    [TestClass()]
    public class GradedTests
    {

        [TestMethod(), Timeout(15000)]
        public void Q0ChartTest()
        {
            RunTest(new Q0Chart("TD0"));
        }

        [TestMethod(), Timeout(5000)]
        public void Q1TestEasy()
        {
        //     Assert.Inconclusive();
            RunTest(new Q1Times("TD1.I"));
        }

        [TestMethod(), Timeout(5000)]
        public void Q1TestHard()
        {
            // Assert.Inconclusive();
            RunTest(new Q1Times("TD1.II"));
        }

        [TestMethod(), Timeout(5000)]
        public void Q2Test()
        {
            Assert.Inconclusive();
            RunTest(new Q2Manchester("TD2"));
        }

        public void RunTest(Processor p)
        {
            TestTools.RunLocalTest("E2", p.Process, p.TestDataName, p.Verifier,
                    VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                    excludedTestCases: p.ExcludedTestCases);
        }
    }
}
