using System;
using System.Collections.Generic;
using System.Text;

namespace Exercicie1
{
    public class CollatzService
    {
        private int sequenceLength = 0;
        public int SequenceLength { get { return sequenceLength; } }

        private int startingNumber;
        public int StartingNumber { get { return startingNumber; } }

        private int maxNumber;

        public CollatzService(int maxNumber)
        {
            this.maxNumber = maxNumber;
        }

        public void Calc()
        {
            long sequence;

            int[] cache = new int[this.maxNumber + 1];

            for (int i = 0; i < cache.Length; i++)
            {
                cache[i] = -1;
            }
            cache[1] = 1;

            for (int index = 2; index <= this.maxNumber; index++)
            {
                sequence = index;
                int k = 0;
                while (sequence != 1 && sequence >= index)
                {
                    k++;
                    if (this.NumberIsEven(sequence))
                        sequence = this.CalcEven(sequence);
                    else
                        sequence = this.CalcOdd(sequence);

                }
                cache[index] = k + cache[sequence];

                if (cache[index] > sequenceLength)
                {
                    this.sequenceLength = cache[index];
                    this.startingNumber = index;
                }
            }
        }

        private bool NumberIsEven(long number)
        {
            return number % 2 == 0;
        }

        private long CalcEven(long number)
        {
            return number / 2;
        }

        private long CalcOdd(long number)
        {
            return (3 * number) + 1;
        }
    }
}
