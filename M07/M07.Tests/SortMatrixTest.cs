using System;
using NUnit.Framework;
namespace M07.Tests
{
    class SortMatrixTest
    {
        [Test]
        public void Sort_AscMin_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Asc, ComparisonType.MinElement);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterAscMin);
        }
        
        [Test]
        public void Sort_AscMax_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Asc, ComparisonType.MaxElement);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterAscMax);
        }

        [Test]
        public void Sort_DescMin_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Desc, ComparisonType.MinElement);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterDescMin);
        }

        [Test]
        public void Sort_DescMax_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Desc, ComparisonType.MaxElement);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterDescMax);
        }

        [Test]
        public void Sort_AscSum_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Asc, ComparisonType.SumRows);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterAscSum);
        }

        [Test]
        public void Sort_DescSum_Test()
        {
            SortMatrix testSort = new SortMatrix(OrderType.Desc, ComparisonType.SumRows);
            int[,] testArray = (int[,])_testArray.Clone();
            testSort.Sort(testArray);
            CollectionAssert.AreEqual(testArray, _expectedArrayAfterDescSum);
        }

        [Test]
        [TestCase(10,0)]
        [TestCase(1,5566)]
        public void Sort_ArgumentException_Test(OrderType orderType, ComparisonType comparisonType)
        {
            SortMatrix sort = null;
            Assert.Throws<ArgumentException>(() => sort = new SortMatrix(orderType, comparisonType));
        }

        [Test]
        public void Sort_NullReferenceException_Test()
        {
            SortMatrix sort = new SortMatrix(OrderType.Asc, ComparisonType.MaxElement);
            Assert.Throws<NullReferenceException>(() => sort.Sort(null));
        }

        private int[,] _testArray = new int[,]
        {
            {5, 10, 0 },
            {-25, 56, 12 },
            {54, 612, -5215 }
        };
        private int[,] _expectedArrayAfterAscMin = new int[,]
        {
            {-5215, 54, 612 },
            {-25, 12, 56 },
            {0, 5, 10 }
        };
        private int[,] _expectedArrayAfterAscMax = new int[,]
        {
            {0, 5, 10 },
            {-25, 12, 56 },
            {-5215, 54, 612 }
        };
        private int[,] _expectedArrayAfterDescMin = new int[,]
        {
            {0, 5, 10 },
            {-25, 12, 56 },
            {-5215, 54, 612 }
        };
        private int[,] _expectedArrayAfterDescMax = new int[,]
        {
            {-5215, 54, 612 },
            {-25, 12, 56 },
            {0, 5, 10 }
        };
        private int[,] _expectedArrayAfterAscSum = new int[,]
        {
            {-5215, 54, 612 },
            {0,5,10 },
            {-25,12,56 }
        };
        private int[,] _expectedArrayAfterDescSum = new int[,]
        {
            {-25,12,56 },
            {0,5,10 },
            {-5215, 54, 612}
        };

    }
}
