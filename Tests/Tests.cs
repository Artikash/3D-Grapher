using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _3D_Graphing;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMathParser()
        {

            Assert.AreEqual(MathParser.Evaluate(0, 0, "pow(3,4)"), 81f, 0.01, "Math Parser Test 1 Failed");
            Assert.AreEqual(MathParser.Evaluate(0, 0, "pow(pow(2,2),pow(2,2))"), 256f, 0.01, "Math Parser Test 2 Failed");
        }
    }
}
