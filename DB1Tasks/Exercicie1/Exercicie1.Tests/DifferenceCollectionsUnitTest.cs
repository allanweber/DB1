using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercicie1.Tests
{
    [TestClass]
    public class DifferenceCollectionsUnitTest
    {
        [TestMethod]
        public void TestDifferenceCollections()
        {
            int[] firstArray = { 1, 3, 7, 29, 42, 98, 234, 93 };
            int[] secondArray = { 4, 6, 93, 7, 55, 32, 3 };
            DifferenceCollections difference = new DifferenceCollections(firstArray, secondArray);
            List<int> result = difference.ExtractDifference().ToList();
            Assert.AreEqual(5, result.Count, $"O resultado tinha ${result.Count} itens mas deveria ter 5");
            Assert.IsTrue(result.Contains(1) && result.Contains(29) && result.Contains(42) && result.Contains(98)
                 && result.Contains(234) && !result.Contains(6) && !result.Contains(3),
                 "Os itens do array não estão certos");

            firstArray = new int[] { 1, 2, 3, 4, 5 };
            secondArray = new int[] { 1, 2, 3, 4, 5 };
            difference = new DifferenceCollections(firstArray, secondArray);
            result = difference.ExtractDifference().ToList();
            Assert.AreEqual(0, result.Count, $"O resultado tinha ${result.Count} itens mas deveria ter 0");


            firstArray = new int[] { 1, 2, 3, 4, 5 };
            secondArray = new int[] { 6, 7, 8, 9, 10 };
            difference = new DifferenceCollections(firstArray, secondArray);
            result = difference.ExtractDifference().ToList();
            Assert.AreEqual(5, result.Count, $"O resultado tinha ${result.Count} itens mas deveria ter 5");
            Assert.IsTrue(firstArray.SequenceEqual(result.ToArray()),
                $"O resultado deveria ser igual ao primeiro array { String.Join(",", firstArray.ToList().ConvertAll(i => i.ToString()).ToArray()) } " +
                $"mas estava: { String.Join(",", result.ConvertAll(i => i.ToString()).ToArray()) }");
        }
    }
}
