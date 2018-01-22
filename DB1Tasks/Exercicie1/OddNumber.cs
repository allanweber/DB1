using System;
using System.Linq;

namespace Exercicie1
{
    public class OddNumber
    {
        private readonly int[] numbers;

        public OddNumber(int[] numbers)
        {
            this.numbers = numbers;
        }

        public bool HasOnlyOdd()
        {
            Func<int, bool> oddNumberWhere = (int number) => { return number % 2 != 0; };
            bool result = this.numbers.All(oddNumberWhere);
            return result;
        }
    }
}
