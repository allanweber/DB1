using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicie1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Exercício 1 - Tarefa 1
            CollatzProblem();

            // Exercício 1 - Tarefa 2
            JustOddNmbers();

            // Exercício 1 - Tarefa 3
            DifferenceBetweenCollections();

            Console.ReadKey();
        }

        private static void CollatzProblem()
        {
            Console.WriteLine("Exercício 1, tarefa 1");

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int oneMillion = 1000000;
            CollatzService service = new CollatzService(oneMillion);
            service.Calc();

            Console.WriteLine($"O número {service.StartingNumber} produz a maior sequencia de {service.SequenceLength} iterações.");

            watch.Stop();
            Console.WriteLine($"Essa solução levou {watch.Elapsed.ToString()} para executar.");
            Console.WriteLine();
        }

        private static void JustOddNmbers()
        {
            Console.WriteLine("Exercício 1, tarefa 2");

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int[] numbers = { 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };

            Func<int, bool> oddNumberWhere = (int number) => { return number % 2 != 0; };
            var result = numbers.All(oddNumberWhere);

            Console.WriteLine($"A lista{(!result ? " não" : string.Empty)} contém somente números ímpares");

            watch.Stop();
            Console.WriteLine($"Essa solução levou {watch.Elapsed.ToString()} para executar.");
            Console.WriteLine();
        }

        private static void DifferenceBetweenCollections()
        {
            Console.WriteLine("Exercício 1, tarefa 3");

            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            int[] firstArray = { 1, 3, 7, 29, 42, 98, 234, 93 };

            int[] secondArray = { 4, 6, 93, 7, 55, 32, 3 };

            List<int> result = firstArray.Where(num => !secondArray.Contains(num)).ToList();

            Console.WriteLine($"Lista de diferença entre arrays: { String.Join(",", result.ConvertAll(i => i.ToString()).ToArray()) }");

            watch.Stop();
            Console.WriteLine($"Essa solução levou {watch.Elapsed.ToString()} para executar.");
            Console.WriteLine();
        }
    }
}
