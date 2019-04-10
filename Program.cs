using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemoryEater
{
    class Program
    {
        private const int kilo = 1024;
        private const int meg = kilo * 1024;
        private const int gig = meg * 1024;

        static void Main(string[] args)
        {

            var listOfMegs = new List<byte[]>();

            int howManygig = 2;
            if (args != null && args.Length > 0 &&  int.TryParse(args[0], out howManygig))
            {
            }

            while (true)
            {
                ConsumeMemory(howManygig, listOfMegs);
                Console.WriteLine($"Hello World  of {howManygig} GB");
                Console.WriteLine($"How much memory want to eat (in GB)?");
                howManygig = int.Parse(Console.ReadLine());
                listOfMegs.Clear();
                GC.Collect();
                listOfMegs = new List<byte[]>();
            }
            
        }

        private static void ConsumeMemory(int howManygig, List<byte[]>  listOfMegs)
        {
            for (int i = 0; i < howManygig * 1024; i++)
            {
                listOfMegs.Add(GetOneMeg());
                if (i % 1000 == 0)
                {
                    Console.WriteLine($"{i / 1024f} Gigabytes consumed");
                }
            }
        }

        private static byte[] GetOneMeg()
        {
            var megbytes = new byte[meg];
            var initialVale = new byte[] { 127 };
            ArrayFill(megbytes, initialVale);
            return megbytes;
        }

        /// <summary>
        ///         /// <typeparam name="T"></typeparam>
        /// <param name="arrayToFill"></param>
        /// <param name="fillValue"></param>
        /// </summary>
        public static void ArrayFill<T>(T[] arrayToFill, T[] fillValue)
        {
            if (fillValue.Length >= arrayToFill.Length)
            {
                throw new ArgumentException("fillValue array length must be smaller than length of arrayToFill");
            }

            // set the initial array value
            Array.Copy(fillValue, arrayToFill, fillValue.Length);

            int arrayToFillHalfLength = arrayToFill.Length / 2;

            for (int i = fillValue.Length; i < arrayToFill.Length; i *= 2)
            {
                int copyLength = i;
                if (i > arrayToFillHalfLength)
                {
                    copyLength = arrayToFill.Length - i;
                }

                Array.Copy(arrayToFill, 0, arrayToFill, i, copyLength);
            }
        }
    }
}
