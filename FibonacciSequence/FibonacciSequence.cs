using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciSequence
{
    public class Fibonacci
    {
        
        /// <summary>
        /// Return fibonacci sequence.
        /// </summary>
        /// <param name="size">Number of elements</param>
        /// <returns>Sequence of fibonachi elements</returns>
        public static IEnumerable<long> GetFibonacciSequence(int size)
        {
            if (size <= 0)
                throw new ArgumentException();

            long previous = 0, current = 1;
            for (int i = 0; i < size; i++)
            {
                yield return previous;
                long next = previous + current;
                previous = current;
                current = next;
            }
        }

        /// <summary>
        /// Return value of fibonachi series on defenet place
        /// </summary>
        /// <param name="index">Index in fibonachi series</param>
        /// <returns></returns>
        public static long GetFibonacciByIndex(int index)
        {
            if (index <= 0)
                throw new ArgumentException();

            IEnumerable<long> series = GetFibonacciSequence(index);
            return series.Last();
        }
        
    }
}
