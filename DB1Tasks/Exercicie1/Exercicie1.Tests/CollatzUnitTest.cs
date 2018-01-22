using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Exercicie1.Tests
{
    [TestClass]
    public class CollatzUnitTest
    {
        [TestMethod]
        public void TestCollatz()
        {
            Collatz coll = new Collatz(100);
            coll.Calc();

            Assert.AreEqual(119, coll.SequenceLength, $"Sequência era {coll.SequenceLength} mas deveria ser 119");
            Assert.AreEqual(97, coll.StartingNumber, $"Primeiro número era {coll.StartingNumber} mas deveria ser 97");

            coll = new Collatz(1000);
            coll.Calc();

            Assert.AreEqual(179, coll.SequenceLength, $"Sequência era {coll.SequenceLength} mas deveria ser 179");
            Assert.AreEqual(871, coll.StartingNumber, $"Primeiro número era {coll.StartingNumber} mas deveria ser 871");

            coll = new Collatz(10);
            coll.Calc();

            Assert.AreEqual(20, coll.SequenceLength, $"Sequência era {coll.SequenceLength} mas deveria ser 20");
            Assert.AreEqual(9, coll.StartingNumber, $"Primeiro número era {coll.StartingNumber} mas deveria ser 9");
        }
    }
}
