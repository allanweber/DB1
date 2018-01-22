using System.Linq;

namespace Exercicie1
{
    public class DifferenceCollections
    {
        public readonly int[] firstArray;
        public readonly int[] secondArray;

        public DifferenceCollections(int[] firstArray, int[] secondArray)
        {
            this.firstArray = firstArray;
            this.secondArray = secondArray;
        }

        public int[] ExtractDifference()
        {
            int[] result = firstArray.Where(num => !secondArray.Contains(num)).ToArray();
            return result;
        }
    }
}
