using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicie1.Tests
{
    [TestClass]
    public class OddNumberUnitTest
    {
        [TestMethod]
        public void TestOddNumber()
        {
            int[] numbers = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            OddNumber odd = new OddNumber(numbers);
            bool result = odd.HasOnlyOdd();
            Assert.IsFalse(result, $"Os números 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 não são somente ímpares");

            numbers = new int[] { 1, 3, 5, 13, 21, 55, 89 };
            odd = new OddNumber(numbers);
            result = odd.HasOnlyOdd();
            Assert.IsTrue(result, $"Os números 1, 3, 5, 13, 21, 55, 89 são somente ímpares");

            numbers = new int[] { 2, 4, 8, 10, 20 };
            odd = new OddNumber(numbers);
            result = odd.HasOnlyOdd();
            Assert.IsFalse(result, $"Os números 2, 4, 8, 10, 20 não são somente ímpares");
        }
    }
}
