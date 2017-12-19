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
            Assert.AreEqual(MathParser.Evaluate(0,0,MathParser.ConvertToRPN("3^4")), 81f, 0.01, "Math Parser Test 1 Failed");
            Assert.AreEqual(MathParser.Evaluate(0, 0, MathParser.ConvertToRPN("(2^2)^(2^2)")), 256f, 0.01, "Math Parser Test 2 Failed");
            Assert.AreEqual(MathParser.Evaluate(0, 0, MathParser.ConvertToRPN("0.0001+(-0.4)")), -0.4f, 0.01, "Math Parser Test 3 Failed");
            Assert.AreEqual(MathParser.Evaluate(2, 2, MathParser.ConvertToRPN("0.0001+(x * y)")), 4f, 0.01, "Math Parser Test 4 Failed");
        }

        [TestMethod]
        public void TestAllCalculations()
        {
            Assert.IsNotNull(FunctionManager.KeyPoints("y+(x)",-2,2,-2,2,0.1f), "Final Test Failed");
        }
    }
}
