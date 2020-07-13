using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M07
{
    internal static class MethodsOfSort
    {
        /// <summary>
        /// Realize sort the rows in ascending order of maximum element in a matrix row
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortAscMax(int[,] array)
        {
            BubbleSort(array);
            int[] maxOfEachRow = FindMaxElementOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (maxOfEachRow[j] > maxOfEachRow[j + 1])
                    {
                        SwapElements(maxOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }
        /// <summary>
        /// Realize sort the rows in descending order of maximum element in a matrix row
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortDescMax(int[,] array)
        {
            BubbleSort(array);
            int[] maxOfEachRow = FindMaxElementOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (maxOfEachRow[j] < maxOfEachRow[j + 1])
                    {
                        SwapElements(maxOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }
        /// <summary>
        /// Realize sort the rows in ascending order of minimum element in a matrix row
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortAscMin(int[,] array)
        {
            BubbleSort(array);
            int[] minOfEachRow = FindMinElementOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (minOfEachRow[j] > minOfEachRow[j + 1])
                    {
                        SwapElements(minOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }
        /// <summary>
        /// Realize sort the rows in descending order of minumum element in a matrix row
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortDescMin(int[,] array)
        {
            BubbleSort(array);
            int[] minOfEachRow = FindMinElementOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (minOfEachRow[j] < minOfEachRow[j + 1])
                    {
                        SwapElements(minOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }
        /// <summary>
        /// Realize sort the rows in ascending order of sums of matrix row elements
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortAscSum(int[,] array)
        {
            BubbleSort(array);
            int[] sumOfEachRow = CalculateSumOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (sumOfEachRow[j] > sumOfEachRow[j + 1])
                    {
                        SwapElements(sumOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }
        /// <summary>
        /// Realize sort the rows in descending order of sums of matrix row elements
        /// </summary>
        /// <param name="array">Array to sort</param>
        public static void SortDescSum(int[,] array)
        {
            BubbleSort(array);
            int[] sumOfEachRow = CalculateSumOfEachRow(array);
            for (int i = 0; i < array.Rank; i++)
                for (int j = 0; j < array.Rank - i; j++)
                {
                    if (sumOfEachRow[j] < sumOfEachRow[j + 1])
                    {
                        SwapElements(sumOfEachRow, j, j + 1);
                        SwapRows(array, j, j + 1);
                    }
                }
        }

        private static void BubbleSort(int[,] array)
        {
            int rowLength = array.GetLength(0) - 1;
            for (int row = 0; row < array.Rank + 1; row++)
            {
                for (int i = 0; i < rowLength; i++)
                    for (int j = 0; j < rowLength - i; j++)
                    {
                        if (array[row, j] > array[row, j + 1])
                        {
                            var temp = array[row, j];
                            array[row, j] = array[row, j + 1];
                            array[row, j + 1] = temp;
                        }
                    }
            }
        }

        private static int[] CalculateSumOfEachRow(int[,] array)
        {
            int[] sum = new int[array.Rank + 1];
            int rowLength = array.GetLength(0);
            for (int row = 0; row < rowLength; row++)
            {
                sum[row] = 0;
                for (int column = 0; column < rowLength; column++)
                {
                    sum[row] += array[row, column];
                }
            }
            return sum;
        }

        private static int[] FindMaxElementOfEachRow(int[,] array)
        {
            int[] max = new int[array.Rank + 1];

            int rowLength = array.GetLength(0);
            for (int row = 0; row < rowLength; row++)
            {
                max[row] = Int32.MinValue;
                for (int column = 0; column < rowLength; column++)
                {
                    if (max[row] < array[row, column])
                        max[row] = array[row, column];
                }
            }

            return max;
        }

        private static int[] FindMinElementOfEachRow(int[,] array)
        {
            int[] min = new int[array.Rank + 1];
            int rowLength = array.GetLength(0);
            for (int row = 0; row < rowLength; row++)
            {
                min[row] = Int32.MaxValue;
                for (int column = 0; column < rowLength; column++)
                {
                    if (min[row] > array[row, column])
                        min[row] = array[row, column];
                }
            }

            return min;
        }

        private static void SwapElements(int[] array, int firstElementNumber, int secondElementNumber)
        {
            int temp = array[firstElementNumber];
            array[firstElementNumber] = array[secondElementNumber];
            array[secondElementNumber] = temp;
        }

        private static void SwapRows(int[,] array, int firstRowNumber, int secondRowNumber)
        {
            int rowLength = array.GetLength(0);
            for (int i = 0; i < rowLength; i++)
            {
                int temp = array[firstRowNumber, i];
                array[firstRowNumber, i] = array[secondRowNumber, i];
                array[secondRowNumber, i] = temp;
            }
        }
    }
}
