using EVLlib.Mathamatics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Linq;

namespace EVLlib.Mathamatics.Tests
{
    [TestClass]
    public class CalculateTest
    {
        [TestMethod]
        public void PowerTest_ReturnsTrue()
        {
            Calculate calculate = new Calculate();

            int baseNumber = 8;
            int exponent = 2;

            const long expected = 64;

            long actual = calculate.Power(baseNumber, exponent);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow(5, 20, 15, 80, 26, 19)]
        public void AverageTest_ReturnsTrue(params double[] input)
        {
            Calculate calculate = new Calculate();

            List<double> numbers = input.Cast<double>().ToList();

            const double expected = 27.5;

            double actual = calculate.Average(numbers);

            Assert.AreEqual(expected, actual);
        }
    }
}