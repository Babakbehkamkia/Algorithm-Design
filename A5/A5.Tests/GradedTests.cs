﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestCommon;

namespace A5.Tests
{
    [DeploymentItem("TestData")]
    [TestClass()]
    public class GradedTests
    {
        // [TestMethod(), Timeout(300)]
        [TestMethod(), Timeout(800)]
        public void SolveTest_Q1ConstructTrie()
        {
            RunTest(new Q1ConstructTrie("TD1"));
        }

        // [TestMethod(), Timeout(500)]
        [TestMethod(), Timeout(2000)]
        public void SolveTest_Q2MultiplePatternMatching()
        {
            RunTest(new Q2MultiplePatternMatching("TD2"));
        }


        // [TestMethod(), Timeout(750)]
        [TestMethod(), Timeout(2200)]
        public void SolveTest_Q3GeneralizedMPM()
        {
            RunTest(new Q3GeneralizedMPM("TD3"));
        }


        public static void RunTest(Processor p)
        {
            TestTools.RunLocalTest("A5", p.Process, p.TestDataName, p.Verifier,
                VerifyResultWithoutOrder: p.VerifyResultWithoutOrder,
                excludedTestCases: p.ExcludedTestCases);
        }
    }
}