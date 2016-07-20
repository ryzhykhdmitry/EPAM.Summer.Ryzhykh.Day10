using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FibonacciSequence;

namespace FibonacciSequence.Tests
{
    [TestFixture]
    public class Tests
    {
        #region TestData
        public class DataClass
        {
            public static IEnumerable<TestCaseData> GetFibonacciSecuenceData
            {
                get
                {
                    yield return new TestCaseData(5).Returns(new long[] { 0, 1, 1, 2, 3 });
                    yield return new TestCaseData(15).Returns(new long[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 });
                }
            }

            public static IEnumerable<TestCaseData> GetFibonacciByIndexData
            {
                get
                {
                    yield return new TestCaseData(5).Returns(3);
                    yield return new TestCaseData(15).Returns(377);
                }
            }
        }
        #endregion

        #region FibonacciTests
        [Test, TestCaseSource(typeof(DataClass), "GetFibonacciSecuenceData")]
        public IEnumerable<long> GetFibonacciSequence_number_fibonacciSequence(int size)
        {
            return Fibonacci.GetFibonacciSequence(size);
        }

        [Test, TestCaseSource(typeof(DataClass), "GetFibonacciByIndexData")]
        public long GetFibonacciByIndex_index_fibonacciNumber(int index)
        {
            return Fibonacci.GetFibonacciByIndex(index);
        } 
        #endregion
    }
}
